using System.Web.Mvc;
using Company.MvcApplication.Business.Web.Mvc.Models;
using Company.MvcApplication.Models;

namespace Company.MvcApplication.Controllers
{
	public class HomeController : SiteController
	{
		#region Constructors

		public HomeController(IModelFactory modelFactory) : base(modelFactory) {}

		#endregion

		#region Methods

		public virtual ActionResult Index()
		{
			return this.View(this.ModelFactory.Create<HomeModel>());
		}

		#endregion
	}
}