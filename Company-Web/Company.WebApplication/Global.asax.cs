using System;
using Company.WebApplication.Business;

namespace Company.WebApplication
{
	public class Global : System.Web.HttpApplication
	{
		#region Methods

		protected void Application_Start(object sender, EventArgs e)
		{
			Bootstrapper.Bootstrap();
		}

		#endregion
	}
}