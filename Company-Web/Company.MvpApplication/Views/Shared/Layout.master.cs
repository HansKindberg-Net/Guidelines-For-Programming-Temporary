using System.Diagnostics.CodeAnalysis;
using Company.MvpApplication.Business.Web.Mvp.UI.Views;
using Company.MvpApplication.Models;
using Company.MvpApplication.Presenters;
using WebFormsMvp;

namespace Company.MvpApplication.Views.Shared
{
	[SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
	[PresenterBinding(typeof(LayoutPresenter))]
	public partial class Layout : MasterPage<LayoutModel>, ILayoutView {}
}