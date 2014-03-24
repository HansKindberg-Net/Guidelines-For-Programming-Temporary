using System;

namespace Company.Build.Tasks.Markdown
{
	[CLSCompliant(false)]
	public interface IMarkdownTransformer
	{
		#region Properties

		IMarkdownTransformerOptions Options { get; }

		#endregion

		#region Methods

		string TransformToHtml(string markdown);

		#endregion
	}
}