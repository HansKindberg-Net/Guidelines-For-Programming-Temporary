using System;

namespace Company.Build.Tasks.Markdown
{
	[CLSCompliant(false)]
	public class DefaultMarkdownTransformerFactory : IMarkdownTransformerFactory
	{
		#region Methods

		public virtual IMarkdownTransformer Create()
		{
			return new MarkdownTransformer();
		}

		public virtual IMarkdownTransformer Create(IMarkdownTransformerOptions markdownTransformerOptions)
		{
			return new MarkdownTransformer(markdownTransformerOptions);
		}

		#endregion
	}
}