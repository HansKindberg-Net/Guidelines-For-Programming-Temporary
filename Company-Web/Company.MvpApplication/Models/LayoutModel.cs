using System;
using Company.Collections.Generic;
using Company.MvpApplication.Business.Web;

namespace Company.MvpApplication.Models
{
	public class LayoutModel
	{
		#region Fields

		private readonly ITreeNode<ISiteMapNode> _currentPageTreeNode;
		private readonly ITreeNode<ISiteMapNode> _pageTreeRoot;

		#endregion

		#region Constructors

		public LayoutModel(ISiteMap siteMap)
		{
			if(siteMap == null)
				throw new ArgumentNullException("siteMap");

			if(!siteMap.Enabled)
				return;

			this._currentPageTreeNode = siteMap.CurrentNode;
			this._pageTreeRoot = siteMap.RootNode;
		}

		#endregion

		#region Properties

		public virtual ITreeNode<ISiteMapNode> CurrentPageTreeNode
		{
			get { return this._currentPageTreeNode; }
		}

		public virtual ITreeNode<ISiteMapNode> PageTreeRoot
		{
			get { return this._pageTreeRoot; }
		}

		#endregion
	}
}