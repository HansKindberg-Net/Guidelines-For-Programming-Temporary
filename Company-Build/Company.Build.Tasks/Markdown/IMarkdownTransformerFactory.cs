using System;

namespace Company.Build.Tasks.Markdown
{
	[CLSCompliant(false)]
	public interface IMarkdownTransformerFactory
	{
		#region Methods

		IMarkdownTransformer Create();
		IMarkdownTransformer Create(IMarkdownTransformerOptions markdownTransformerOptions);

		#endregion
	}
}