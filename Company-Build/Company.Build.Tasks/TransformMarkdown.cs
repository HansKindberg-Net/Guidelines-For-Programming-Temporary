using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using Company.Build.Tasks.Html;
using Company.Build.Tasks.IoC;
using Company.Build.Tasks.Markdown;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Company.Build.Tasks
{
	[CLSCompliant(false)]
	public class TransformMarkdown : Task, ICancelableTask
	{
		#region Fields

		private Encoding _destinationEncoding = Encoding.UTF8;
		private const string _destinationFilesPropertyName = "DestinationFiles";
		private const string _destinationFolderPropertyName = "DestinationFolder";
		private readonly IEnvironment _environment;
		private readonly IFileSystem _fileSystem;
		private bool _generateHeadingIdentifiers;
		private string _htmlFormatTemplate = "<!DOCTYPE html><html><body>{0}</body></html>";
		private readonly IHtmlFormatter _htmlFormatter;
		private Encoding _markdownEncoding = Encoding.Default;
		private IMarkdownTransformer _markdownTransformer;
		private readonly IMarkdownTransformerFactory _markdownTransformerFactory;
		private bool _overwriteReadOnlyFiles;
		private const string _sourceFilesPropertyName = "SourceFiles";

		#endregion

		#region Constructors

		public TransformMarkdown() : this(ServiceLocator.Instance.GetService<IEnvironment>(), ServiceLocator.Instance.GetService<IFileSystem>(), ServiceLocator.Instance.GetService<IHtmlFormatter>(), ServiceLocator.Instance.GetService<IMarkdownTransformerFactory>()) {}

		protected internal TransformMarkdown(IEnvironment environment, IFileSystem fileSystem, IHtmlFormatter htmlFormatter, IMarkdownTransformerFactory markdownTransformerFactory)
		{
			if(environment == null)
				throw new ArgumentNullException("environment");

			if(fileSystem == null)
				throw new ArgumentNullException("fileSystem");

			if(htmlFormatter == null)
				throw new ArgumentNullException("htmlFormatter");

			if(markdownTransformerFactory == null)
				throw new ArgumentNullException("markdownTransformerFactory");

			this._environment = environment;
			this._fileSystem = fileSystem;
			this._htmlFormatter = htmlFormatter;
			this._markdownTransformerFactory = markdownTransformerFactory;
		}

		#endregion

		#region Properties

		protected internal virtual bool Canceling { get; set; }

		public virtual Encoding DestinationEncoding
		{
			get { return this._destinationEncoding; }
			set { this._destinationEncoding = value; }
		}

		[Output]
		public virtual ITaskItem[] DestinationFiles { get; set; }

		public virtual ITaskItem DestinationFolder { get; set; }

		protected internal virtual IEnvironment Environment
		{
			get { return this._environment; }
		}

		protected internal virtual IFileSystem FileSystem
		{
			get { return this._fileSystem; }
		}

		public virtual bool GenerateHeadingIdentifiers
		{
			get { return this._generateHeadingIdentifiers; }
			set
			{
				this._markdownTransformer = null;
				this._generateHeadingIdentifiers = value;
			}
		}

		public virtual string HtmlFormatTemplate
		{
			get { return this._htmlFormatTemplate; }
			set
			{
				if(value == null)
					throw new ArgumentNullException("value");

				if(!value.Contains("{0}"))
					throw new ArgumentException("The value must contain \"{0}\" as format item. For example: \"<!DOCTYPE html><html><body>{0}</body></html>\"", "value");

				this._htmlFormatTemplate = value;
			}
		}

		protected internal virtual IHtmlFormatter HtmlFormatter
		{
			get { return this._htmlFormatter; }
		}

		public virtual Encoding MarkdownEncoding
		{
			get { return this._markdownEncoding; }
			set { this._markdownEncoding = value; }
		}

		protected internal virtual IMarkdownTransformer MarkdownTransformer
		{
			get { return this._markdownTransformer ?? (this._markdownTransformer = this.MarkdownTransformerFactory.Create(new MarkdownTransformerOptions {GenerateHeadingIdentifiers = this.GenerateHeadingIdentifiers})); }
		}

		protected internal virtual IMarkdownTransformerFactory MarkdownTransformerFactory
		{
			get { return this._markdownTransformerFactory; }
		}

		public virtual bool OverwriteReadOnlyFiles
		{
			get { return this.Environment.MSBuildAlwaysOverwriteReadOnlyFiles || this._overwriteReadOnlyFiles; }
			set { this._overwriteReadOnlyFiles = value; }
		}

		public virtual ITaskItem[] ReplacementsAfterTransform { get; set; }
		public virtual ITaskItem[] ReplacementsBeforeTransform { get; set; }

		[Required]
		public virtual ITaskItem[] SourceFiles { get; set; }

		[Output]
		public virtual ITaskItem[] TransformedFiles { get; protected internal set; }

		#endregion

		#region Methods

		public virtual void Cancel()
		{
			this.Canceling = true;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.Build.Utilities.TaskLoggingHelper.LogError(System.String,System.Object[])")]
		public override bool Execute()
		{
			if(this.SourceFiles == null || !this.SourceFiles.Any())
				return true;

			if(!this.ValidateInput() || !this.InitializeDestinationFiles())
				return false;

			bool successful = true;
			var transformedFiles = new List<ITaskItem>();

			for(int i = 0; i < this.SourceFiles.Count() && !this.Canceling; i++)
			{
				try
				{
					this.SetDestinationContent(this.DestinationFiles.ElementAt(i).ItemSpec, this.GetTransformedMarkdownContent(this.SourceFiles.ElementAt(i).ItemSpec));

					transformedFiles.Add(this.DestinationFiles.ElementAt(i));
				}
				catch(IOException ioException)
				{
					this.Log.LogError("Unable to transform content from \"{0}\" to \"{1}\". {2}", new object[] {this.SourceFiles.ElementAt(i).ItemSpec, this.DestinationFiles.ElementAt(i).ItemSpec, ioException.Message});
					successful = false;
				}
			}

			this.TransformedFiles = transformedFiles.ToArray();

			return successful && !this.Canceling;
		}

		protected internal virtual string GetTransformedMarkdownContent(string sourceFilePath)
		{
			string content = this.FileSystem.File.ReadAllText(sourceFilePath, this.MarkdownEncoding);

			content = this.Replace(content, this.ReplacementsBeforeTransform);

			string transformedContent = this.MarkdownTransformer.TransformToHtml(content);

			// MarkdownSharp wraps a code block in both <pre> and <code> tags. If these elements are not together, "<pre><code>", the resulting html will indent the first line in the code block.
			const string codeBlockStartTagOrigin = "<pre><code>";
			const string codeBlockEndTagOrigin = "</code></pre>";
			const string codeBlockStartTagTemporaryReplacer = "<preAndCode>";
			const string codeBlockEndTagTemporaryReplacer = "</preAndCode>";

			transformedContent = transformedContent.Replace(codeBlockStartTagOrigin, codeBlockStartTagTemporaryReplacer).Replace(codeBlockEndTagOrigin, codeBlockEndTagTemporaryReplacer);

			transformedContent = this.HtmlFormatter.Format(string.Format(CultureInfo.InvariantCulture, this.HtmlFormatTemplate, transformedContent), this.DestinationEncoding);

			transformedContent = transformedContent.Replace(codeBlockStartTagTemporaryReplacer, codeBlockStartTagOrigin).Replace(codeBlockEndTagTemporaryReplacer, codeBlockEndTagOrigin);

			transformedContent = this.Replace(transformedContent, this.ReplacementsAfterTransform);

			return transformedContent;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.Build.Utilities.TaskLoggingHelper.LogError(System.String,System.Object[])")]
		protected internal virtual bool InitializeDestinationFiles()
		{
			if(this.DestinationFiles == null)
			{
				var destinationFiles = new List<ITaskItem>();

				for(int i = 0; i < this.SourceFiles.Count(); i++)
				{
					string path;

					try
					{
						// ReSharper disable AssignNullToNotNullAttribute
						path = Path.Combine(this.DestinationFolder.ItemSpec, Path.GetFileNameWithoutExtension(this.SourceFiles.ElementAt(i).ItemSpec));
						path += ".html";
						// ReSharper restore AssignNullToNotNullAttribute
					}
					catch(ArgumentException argumentException)
					{
						this.Log.LogError("Unable to prepare transformation from \"{0}\" to \"{1}\". {2}", new object[] {this.SourceFiles.ElementAt(i).ItemSpec, this.DestinationFolder.ItemSpec, argumentException.Message});
						this.DestinationFiles = new ITaskItem[0];
						return false;
					}

					destinationFiles.Add(new TaskItem(path)); // In .NET 4.0 and up they escape the path value here (see Reflector), maybe it should be done here to.
				}

				this.DestinationFiles = destinationFiles.ToArray();
			}

			return true;
		}

		protected internal virtual string Replace(string value, IEnumerable<ITaskItem> replacements)
		{
			if(value == null || replacements == null)
				return value;

			const string replacementValueMetadataName = "ReplacementValue";

			foreach(var replacement in replacements)
			{
				if(replacement == null || string.IsNullOrEmpty(replacement.ItemSpec))
					throw new ArgumentException("The replacement can not be null or empty.");

				if(!replacement.MetadataNames.Cast<string>().Contains(replacementValueMetadataName))
					throw new ArgumentException("The replacement must have a replacement-value.");

				value = value.Replace(replacement.ItemSpec, replacement.GetMetadata(replacementValueMetadataName));
			}

			return value;
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.Build.Utilities.TaskLoggingHelper.LogMessage(Microsoft.Build.Framework.MessageImportance,System.String,System.Object[])")]
		protected internal virtual void SetDestinationContent(string destinationFilePath, string transformedMarkdownContent)
		{
			var resetToReadOnly = false;
			var destinationFile = this.FileSystem.FileInfo.FromFileName(destinationFilePath);

			if(destinationFile.Exists && destinationFile.IsReadOnly && this.OverwriteReadOnlyFiles)
			{
				this.Log.LogMessage(MessageImportance.High, "The file \"{0}\" is read-only.", new object[] {destinationFilePath});

				resetToReadOnly = true;
				destinationFile.IsReadOnly = false;
			}

			if(!destinationFile.Directory.Exists)
				this.FileSystem.Directory.CreateDirectory(destinationFile.DirectoryName);

			this.FileSystem.File.WriteAllText(destinationFilePath, transformedMarkdownContent, this.DestinationEncoding);

			if(resetToReadOnly)
			{
				this.Log.LogMessage(MessageImportance.High, "The file \"{0}\" is SET to read-only.", new object[] {destinationFilePath});

				this.FileSystem.FileInfo.FromFileName(destinationFilePath).IsReadOnly = true;
			}
		}

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.Build.Utilities.TaskLoggingHelper.LogError(System.String,System.Object[])")]
		protected internal virtual bool ValidateInput()
		{
			if(this.SourceFiles == null || !this.SourceFiles.Any())
				return true;

			if(this.DestinationFiles == null && this.DestinationFolder == null)
			{
				this.Log.LogError("No destination specified for {0}. Please supply either \"{1}\" or \"{2}\".", new object[] {this.GetType().Name, _destinationFilesPropertyName, _destinationFolderPropertyName});
				return false;
			}

			if(this.DestinationFiles != null && this.DestinationFolder != null)
			{
				this.Log.LogError("Both \"{0}\" and \"{1}\" were specified as input parameters for {2} in the project file. Please choose one or the other.", new object[] {_destinationFilesPropertyName, _destinationFolderPropertyName, this.GetType().Name});
				return false;
			}

			if(this.DestinationFiles != null && this.DestinationFiles.Count() != this.SourceFiles.Count())
			{
				this.Log.LogError("\"{0}\" refers to {1} item(s), and \"{2}\" refers to {3} item(s) in {4}. They must have the same number of items.", new object[] {_destinationFilesPropertyName, this.DestinationFiles.Count(), _sourceFilesPropertyName, this.SourceFiles.Count(), this.GetType().Name});
				return false;
			}

			foreach(var sourceFile in this.SourceFiles.Where(sourceFile => !this.FileSystem.File.Exists(sourceFile.ItemSpec)))
			{
				this.Log.LogError("The file \"{0}\" does not exist.", sourceFile.ItemSpec);
				return false;
			}

			return true;
		}

		#endregion
	}
}