using System;
using System.Linq;
using System.Web;
using Company.Collections.Generic;

namespace Company.MvpApplication.Business.Web
{
	public class SiteMapWrapper : ISiteMap
	{
		#region Fields

		private Lazy<ITreeNode<ISiteMapNode>> _rootNode;
		private readonly ITreeFactory<ISiteMapNode> _treeFactory;

		#endregion

		#region Constructors

		public SiteMapWrapper(ITreeFactory<ISiteMapNode> treeFactory)
		{
			if(treeFactory == null)
				throw new ArgumentNullException("treeFactory");

			this._treeFactory = treeFactory;
		}

		#endregion

		#region Properties

		public virtual ITreeNode<ISiteMapNode> CurrentNode
		{
			get
			{
				if(SiteMap.CurrentNode == null)
					return null;

				ITreeNode<ISiteMapNode> rootNode = this.RootNode;

				return Equals(SiteMap.CurrentNode, SiteMap.RootNode) ? rootNode : rootNode.Descendants.FirstOrDefault(treeNode => treeNode.Value.Url.Equals(SiteMap.CurrentNode.Url));
			}
		}

		public virtual bool Enabled
		{
			get { return SiteMap.Enabled; }
		}

		public virtual ITreeNode<ISiteMapNode> RootNode
		{
			get
			{
				if(this._rootNode == null || !SiteMap.RootNode.ChildNodes.IsReadOnly)
					this._rootNode = new Lazy<ITreeNode<ISiteMapNode>>(() =>
					{
						ITreeNode<ISiteMapNode> rootNode = new TreeNode<ISiteMapNode>((SiteMapNodeWrapper) SiteMap.RootNode, this.TreeFactory);
						this.PopulateTreeNode(rootNode, SiteMap.RootNode);
						return rootNode;
					});

				return this._rootNode.Value;
			}
		}

		protected internal virtual ITreeFactory<ISiteMapNode> TreeFactory
		{
			get { return this._treeFactory; }
		}

		#endregion

		#region Methods

		protected internal virtual void PopulateTreeNode(ITreeNode<ISiteMapNode> treeNode, SiteMapNode siteMapNode)
		{
			if(treeNode == null)
				throw new ArgumentNullException("treeNode");

			if(siteMapNode == null)
				throw new ArgumentNullException("siteMapNode");

			foreach(var child in siteMapNode.ChildNodes.OfType<SiteMapNode>())
			{
				ITreeNode<ISiteMapNode> childNode = treeNode.Children.Add((SiteMapNodeWrapper) child);
				this.PopulateTreeNode(childNode, child);
			}
		}

		#endregion
	}
}