using System.Web.Mvc;
using System.Web.Routing;

namespace Company.MvcApplication.Business.Configuration
{
	public static class RouteConfiguration
	{
		#region Methods

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
				);
		}

		#endregion
	}
}