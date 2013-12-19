using Company.MvpApplication.Business.Web.Mvp.UI.Views;
using Company.MvpApplication.Models;
using Company.MvpApplication.Presenters;
using Company.MvpApplication.Views.Home;
using WebFormsMvp;

namespace Company.MvpApplication
{
	[PresenterBinding(typeof(HomePresenter))]
	public partial class Index : Page<HomeModel>, IHomeView {}
}