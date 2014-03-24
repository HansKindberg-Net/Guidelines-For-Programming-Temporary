using System;
using MarkdownSharp;

namespace Company.Build.Tasks.Markdown
{
	[CLSCompliant(false)]
	public class MarkdownTransformerOptions : IMarkdownTransformerOptions
	{
		#region Fields

		private readonly MarkdownOptions _markdownOptions = new MarkdownOptions();

		#endregion

		#region Properties

		public virtual bool AutoHyperlink
		{
			get { return this.MarkdownOptions.AutoHyperlink; }
			set { this.MarkdownOptions.AutoHyperlink = value; }
		}

		public virtual bool AutoNewLines
		{
			get { return this.MarkdownOptions.AutoNewLines; }
			set { this.MarkdownOptions.AutoNewLines = value; }
		}

		public virtual string EmptyElementSuffix
		{
			get { return this.MarkdownOptions.EmptyElementSuffix; }
			set { this.MarkdownOptions.EmptyElementSuffix = value; }
		}

		public virtual bool EncodeProblemUrlCharacters
		{
			get { return this.MarkdownOptions.EncodeProblemUrlCharacters; }
			set { this.MarkdownOptions.EncodeProblemUrlCharacters = value; }
		}

		public virtual bool GenerateHeadingIdentifiers { get; set; }

		public virtual bool LinkEmails
		{
			get { return this.MarkdownOptions.LinkEmails; }
			set { this.MarkdownOptions.LinkEmails = value; }
		}

		protected internal virtual MarkdownOptions MarkdownOptions
		{
			get { return this._markdownOptions; }
		}

		public virtual bool StrictBoldItalic
		{
			get { return this.MarkdownOptions.StrictBoldItalic; }
			set { this.MarkdownOptions.StrictBoldItalic = value; }
		}

		#endregion
	}
}