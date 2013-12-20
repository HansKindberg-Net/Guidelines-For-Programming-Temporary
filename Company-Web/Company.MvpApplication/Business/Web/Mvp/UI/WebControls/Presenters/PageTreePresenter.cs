using System.Web.UI;
using Company.Collections.Generic;
using Company.MvpApplication.Business.Mvp.Models;
using Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Models;
using Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Views;
using Company.MvpApplication.Business.Web.UI;
using Company.Web;

namespace Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Presenters
{
	public class PageTreePresenter : TreePresenter<IPageTree, PageTreeModel, ISiteMapNode>
	{
		#region Constructors

		public PageTreePresenter(IPageTree view, IModelFactory modelFactory) : base(view, modelFactory) {}

		#endregion

		#region Methods

		protected internal override Control CreateTreeNodeContainer(ITreeNode<ISiteMapNode> treeNode)
		{
			return new PageTreeNodeContainer(treeNode);
		}

		#endregion
	}
}