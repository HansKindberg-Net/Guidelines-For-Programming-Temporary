using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.Abstractions;
using System.Linq;
using Company.Build.Tasks.Html;
using Company.Build.Tasks.Markdown;
using Microsoft.Build.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Build.Tasks.UnitTests
{
	[TestClass]
	public class TransformMarkdownTest
	{
		#region Fields

		private const string _destinationFilesPropertyName = "DestinationFiles";
		private const string _destinationFolderPropertyName = "DestinationFolder";
		private const string _sourceFilesPropertyName = "SourceFiles";
		private static readonly string _transformMarkdownTypeName = typeof(TransformMarkdown).Name;

		#endregion

		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Company.Build.Tasks.TransformMarkdown")]
		public void Constructor_WithArguments_IfTheFileSystemParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new TransformMarkdown(Mock.Of<IEnvironment>(), null, Mock.Of<IHtmlFormatter>(), Mock.Of<IMarkdownTransformerFactory>());
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "fileSystem")
					throw;
			}
		}

		private static TransformMarkdown CreateTransformMarkdown()
		{
			return CreateTransformMarkdown(Mock.Of<IEnvironment>(), Mock.Of<IFileSystem>(), Mock.Of<IHtmlFormatter>(), Mock.Of<IMarkdownTransformerFactory>());
		}

		private static TransformMarkdown CreateTransformMarkdown(IBuildEngine buildEngine)
		{
			return CreateTransformMarkdown(buildEngine, Mock.Of<ITaskHost>());
		}

		private static TransformMarkdown CreateTransformMarkdown(IBuildEngine buildEngine, ITaskHost taskHost)
		{
			return CreateTransformMarkdown(buildEngine, Mock.Of<IEnvironment>(), Mock.Of<IFileSystem>(), Mock.Of<IHtmlFormatter>(), Mock.Of<IMarkdownTransformerFactory>(), taskHost);
		}

		private static TransformMarkdown CreateTransformMarkdown(IEnvironment environment, IFileSystem fileSystem, IHtmlFormatter htmlFormatter, IMarkdownTransformerFactory markdownTransformerFactory)
		{
			return CreateTransformMarkdown(Mock.Of<IBuildEngine>(), environment, fileSystem, htmlFormatter, markdownTransformerFactory, Mock.Of<ITaskHost>());
		}

		private static TransformMarkdown CreateTransformMarkdown(IBuildEngine buildEngine, IEnvironment environment, IFileSystem fileSystem, IHtmlFormatter htmlFormatter, IMarkdownTransformerFactory markdownTransformerFactory, ITaskHost taskHost)
		{
			return new TransformMarkdown(environment, fileSystem, htmlFormatter, markdownTransformerFactory)
			{
				BuildEngine = buildEngine,
				HostObject = taskHost
			};
		}

		[TestMethod]
		public void Execute_IfDestinationFilesIsNotNullAndDestinationFolderIsNotNull_ShouldLogError()
		{
			string actualLoggedErrorMessage = null;
			string expectedLoggedErrorMessage = string.Format(CultureInfo.InvariantCulture, "Both \"{0}\" and \"{1}\" were specified as input parameters for {2} in the project file. Please choose one or the other.", new object[] {_destinationFilesPropertyName, _destinationFolderPropertyName, _transformMarkdownTypeName});

			Mock<IBuildEngine> buildEngineMock = new Mock<IBuildEngine>();
			buildEngineMock.Setup(buildEngine => buildEngine.LogErrorEvent(It.IsAny<BuildErrorEventArgs>())).Callback((BuildErrorEventArgs e) => { actualLoggedErrorMessage = e.Message; });

			var transformMarkdown = CreateTransformMarkdown(buildEngineMock.Object);
			transformMarkdown.DestinationFiles = new ITaskItem[0];
			transformMarkdown.DestinationFolder = Mock.Of<ITaskItem>();
			transformMarkdown.SourceFiles = new ITaskItem[1];

			Assert.IsNotNull(transformMarkdown.DestinationFiles);
			Assert.IsNotNull(transformMarkdown.DestinationFolder);

			transformMarkdown.Execute();

			Assert.AreEqual(expectedLoggedErrorMessage, actualLoggedErrorMessage);
		}

		[TestMethod]
		public void Execute_IfDestinationFilesIsNotNullAndDestinationFolderIsNotNull_ShouldReturnFalse()
		{
			var transformMarkdown = CreateTransformMarkdown();
			transformMarkdown.DestinationFiles = new ITaskItem[0];
			transformMarkdown.DestinationFolder = Mock.Of<ITaskItem>();
			transformMarkdown.SourceFiles = new ITaskItem[1];

			Assert.IsNotNull(transformMarkdown.DestinationFiles);
			Assert.IsNotNull(transformMarkdown.DestinationFolder);
			Assert.IsFalse(transformMarkdown.Execute());
		}

		[TestMethod]
		public void Execute_IfDestinationFilesIsNotNullAndDestinationFolderIsNullAndDestinationFilesLengthAndSourceFilesLengthAreNotEqual_ShouldLogError()
		{
			int destinationFilesLength = DateTime.Now.Second;
			int sourceFilesLength = destinationFilesLength + 1;

			string actualLoggedErrorMessage = null;
			string expectedLoggedErrorMessage = string.Format(CultureInfo.InvariantCulture, "\"{0}\" refers to {1} item(s), and \"{2}\" refers to {3} item(s) in {4}. They must have the same number of items.", new object[] {_destinationFilesPropertyName, destinationFilesLength, _sourceFilesPropertyName, sourceFilesLength, _transformMarkdownTypeName});

			Mock<IBuildEngine> buildEngineMock = new Mock<IBuildEngine>();
			buildEngineMock.Setup(buildEngine => buildEngine.LogErrorEvent(It.IsAny<BuildErrorEventArgs>())).Callback((BuildErrorEventArgs e) => { actualLoggedErrorMessage = e.Message; });

			var transformMarkdown = CreateTransformMarkdown(buildEngineMock.Object);
			transformMarkdown.DestinationFiles = new ITaskItem[destinationFilesLength];
			transformMarkdown.SourceFiles = new ITaskItem[sourceFilesLength];

			Assert.IsNotNull(transformMarkdown.DestinationFiles);
			Assert.IsNull(transformMarkdown.DestinationFolder);
			Assert.AreNotEqual(transformMarkdown.DestinationFiles.Count(), transformMarkdown.SourceFiles.Length);

			transformMarkdown.Execute();

			Assert.AreEqual(expectedLoggedErrorMessage, actualLoggedErrorMessage);
		}

		[TestMethod]
		public void Execute_IfDestinationFilesIsNotNullAndDestinationFolderIsNullAndDestinationFilesLengthAndSourceFilesLengthAreNotEqual_ShouldReturnFalse()
		{
			int destinationFilesLength = DateTime.Now.Second;
			int sourceFilesLength = destinationFilesLength + 1;

			var transformMarkdown = CreateTransformMarkdown();
			transformMarkdown.DestinationFiles = new ITaskItem[destinationFilesLength];
			transformMarkdown.SourceFiles = new ITaskItem[sourceFilesLength];

			Assert.IsNotNull(transformMarkdown.DestinationFiles);
			Assert.IsNull(transformMarkdown.DestinationFolder);
			Assert.AreNotEqual(transformMarkdown.DestinationFiles.Count(), transformMarkdown.SourceFiles.Length);
			Assert.IsFalse(transformMarkdown.Execute());
		}

		[TestMethod]
		public void Execute_IfDestinationFilesIsNullAndDestinationFolderIsNull_ShouldLogError()
		{
			string actualLoggedErrorMessage = null;
			string expectedLoggedErrorMessage = string.Format(CultureInfo.InvariantCulture, "No destination specified for {0}. Please supply either \"{1}\" or \"{2}\".", new object[] {_transformMarkdownTypeName, _destinationFilesPropertyName, _destinationFolderPropertyName});

			Mock<IBuildEngine> buildEngineMock = new Mock<IBuildEngine>();
			buildEngineMock.Setup(buildEngine => buildEngine.LogErrorEvent(It.IsAny<BuildErrorEventArgs>())).Callback((BuildErrorEventArgs e) => { actualLoggedErrorMessage = e.Message; });

			var transformMarkdown = CreateTransformMarkdown(buildEngineMock.Object);
			transformMarkdown.SourceFiles = new ITaskItem[1];

			Assert.IsNull(transformMarkdown.DestinationFiles);
			Assert.IsNull(transformMarkdown.DestinationFolder);

			transformMarkdown.Execute();

			Assert.AreEqual(expectedLoggedErrorMessage, actualLoggedErrorMessage);
		}

		[TestMethod]
		public void Execute_IfDestinationFilesIsNullAndDestinationFolderIsNull_ShouldReturnFalse()
		{
			var transformMarkdown = CreateTransformMarkdown();
			transformMarkdown.SourceFiles = new ITaskItem[1];

			Assert.IsNull(transformMarkdown.DestinationFiles);
			Assert.IsNull(transformMarkdown.DestinationFolder);
			Assert.IsFalse(transformMarkdown.Execute());
		}

		[TestMethod]
		public void Execute_IfSourceFilesIsEmpty_ShouldReturnTrue()
		{
			var transformMarkdown = CreateTransformMarkdown();
			transformMarkdown.SourceFiles = new ITaskItem[0];

			Assert.AreEqual(0, transformMarkdown.SourceFiles.Length);
			Assert.IsTrue(transformMarkdown.Execute());
		}

		[TestMethod]
		public void Execute_IfSourceFilesIsNull_ShouldReturnTrue()
		{
			var transformMarkdown = CreateTransformMarkdown();

			Assert.IsNull(transformMarkdown.SourceFiles);
			Assert.IsTrue(transformMarkdown.Execute());
		}

		[TestMethod]
		public void Execute_TemporaryTest()
		{
			var transformMarkdown = CreateTransformMarkdown();
			transformMarkdown.DestinationFolder = Mock.Of<ITaskItem>();
			transformMarkdown.SourceFiles = new ITaskItem[1];

			Assert.IsFalse(transformMarkdown.Execute());
		}

		#endregion
	}
}