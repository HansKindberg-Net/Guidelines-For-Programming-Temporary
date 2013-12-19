using System;
using Company.MvpApplication.Business;

namespace Company.MvpApplication
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