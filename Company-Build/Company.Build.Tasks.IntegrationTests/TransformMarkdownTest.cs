using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Build.Tasks.IntegrationTests
{
	[TestClass]
	public class TransformMarkdownTest
	{
		#region Fields

		private static readonly string _outputDirectory = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Output");
		private static readonly string _solutionDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName;

		#endregion

		#region Methods

		private static TransformMarkdown CreateTransformMarkdown()
		{
			return CreateTransformMarkdown(Mock.Of<IBuildEngine>(), Mock.Of<ITaskHost>());
		}

		private static TransformMarkdown CreateTransformMarkdown(IBuildEngine buildEngine, ITaskHost taskHost)
		{
			return new TransformMarkdown
			{
				BuildEngine = buildEngine,
				HostObject = taskHost
			};
		}

		[TestMethod]
		public void Test()
		{
			var transformMarkdown = CreateTransformMarkdown();

			List<ITaskItem> taskItems = new List<ITaskItem>
			{
				new TaskItem(Path.Combine(_solutionDirectory, "ReadMe.sv.md"))
			};

			transformMarkdown.DestinationFolder = new TaskItem(_outputDirectory);
			transformMarkdown.GenerateHeadingIdentifiers = true;
			transformMarkdown.SourceFiles = taskItems.ToArray();

			//Task myCustomTask = new CustomTask();
			//myCustomTask.BuildEngine = this.BuildEngine;
			//myCustomTask.Execute();

			Assert.IsTrue(transformMarkdown.Execute());
		}

		#endregion
	}
}