using System;
using MarkdownSharp;

namespace Company.Build.Tasks.Markdown
{
	[CLSCompliant(false)]
	public interface IMarkdownTransformerOptions : IMarkdownOptions
	{
		#region Properties

		bool GenerateHeadingIdentifiers { get; }

		#endregion
	}
}