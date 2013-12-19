using System.Web.Mvc;

namespace Company.MvcApplication.Controllers
{
	public class HomeController : Controller
	{
		#region Methods

		public ActionResult About()
		{
			this.ViewBag.Message = "Your application description page.";

			return this.View();
		}

		public ActionResult Contact()
		{
			this.ViewBag.Message = "Your contact page.";

			return this.View();
		}

		public ActionResult Index()
		{
			return this.View();
		}

		#endregion
	}
}