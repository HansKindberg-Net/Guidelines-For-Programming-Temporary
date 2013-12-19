using System;
using System.Web.UI;
using Company.Collections.Generic;

namespace Company.MvpApplication.Business.Web.UI
{
	public class PageTreeNodeContainer : Control, INamingContainer
	{
		#region Fields

		private readonly ITreeNode<ISiteMapNode> _treeNode;

		#endregion

		#region Constructors

		public PageTreeNodeContainer(ITreeNode<ISiteMapNode> treeNode)
		{
			if(treeNode == null)
				throw new ArgumentNullException("treeNode");

			this._treeNode = treeNode;
		}

		#endregion

		#region Properties

		public virtual ITreeNode<ISiteMapNode> TreeNode
		{
			get { return this._treeNode; }
		}

		#endregion
	}
}