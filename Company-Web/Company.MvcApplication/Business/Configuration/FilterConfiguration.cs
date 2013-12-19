using System;
using System.Web.Mvc;

namespace Company.MvcApplication.Business.Configuration
{
	public static class FilterConfiguration
	{
		#region Methods

		public static void RegisterGlobalFilters(GlobalFilterCollection globalFilters)
		{
			if(globalFilters == null)
				throw new ArgumentNullException("globalFilters");

			globalFilters.Add(new HandleErrorAttribute());
		}

		#endregion
	}
}