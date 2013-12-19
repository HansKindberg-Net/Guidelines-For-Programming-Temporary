using Company.Collections.Generic;

namespace Company.MvpApplication.Business.Web
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