using Company.Web;

namespace Company.WebApplication.MasterPages
{
	public partial class Default : System.Web.UI.MasterPage
	{
		#region Properties

		public virtual ISystemInformation SystemInformation
		{
			get { return this.SystemInformationControl; }
		}

		#endregion
	}
}