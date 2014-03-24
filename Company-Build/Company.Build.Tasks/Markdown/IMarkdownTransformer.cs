namespace Company.Build.Tasks.Markdown
{
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