using Company.Collections.Generic;

namespace Company.Web
{
	public interface ISiteMap
	{
		#region Properties

		ITreeNode<ISiteMapNode> CurrentNode { get; }
		bool Enabled { get; }
		ITreeNode<ISiteMapNode> RootNode { get; }

		#endregion
	}
}