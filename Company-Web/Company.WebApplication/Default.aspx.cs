using System;
using System.Web;

namespace Company.WebApplication
{
	public partial class Default : System.Web.UI.Page
	{
		#region Methods

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			bool siteMapIsEnabled = SiteMap.Enabled;

			siteMapIsEnabled = siteMapIsEnabled;
		}

		#endregion
	}
}