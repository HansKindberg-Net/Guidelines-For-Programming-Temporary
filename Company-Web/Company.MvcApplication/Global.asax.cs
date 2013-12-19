using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Company.MvcApplication.Business.Configuration;

namespace Company.MvcApplication
{
	public class Global : System.Web.HttpApplication
	{
		#region Methods

		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfiguration.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfiguration.RegisterRoutes(RouteTable.Routes);
			BundleConfiguration.RegisterBundles(BundleTable.Bundles);
		}

		#endregion
	}
}