using System.ComponentModel;
using System.Web.UI;
using Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Models;
using Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Presenters;
using Company.MvpApplication.Business.Web.UI;
using WebFormsMvp;

namespace Company.MvpApplication.Business.Web.Mvp.UI.WebControls.Views
{
	[PresenterBinding(typeof(PageTreePresenter))]
	public class PageTree : TreeView<PageTreeModel, ISiteMapNode>, IPageTree
	{
		#region Properties

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate FooterTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate HeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate ItemFooterTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate ItemHeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate ItemTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate LevelFooterTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate LevelHeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate SelectedAncestorHeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate SelectedAncestorTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate SelectedItemHeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PageTreeNodeContainer))]
		public override ITemplate SelectedItemTemplate { get; set; }

		#endregion
	}
}