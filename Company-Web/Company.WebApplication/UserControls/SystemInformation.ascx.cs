using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Company.WebApplication.UserControls
{
	public partial class SystemInformation : System.Web.UI.UserControl
	{
		#region Fields

		private static readonly IDictionary<SystemInformationType, string> _alertCssClassDictionary = new Dictionary<SystemInformationType, string>
			{
				{SystemInformationType.Confirmation, "alert-success"},
				{SystemInformationType.Exception, "alert-danger"},
				{SystemInformationType.Information, "alert-info"},
				{SystemInformationType.Warning, "alert-warning"}
			};

		#endregion

		#region Properties

		public virtual string AlertCssClass
		{
			get { return _alertCssClassDictionary[this.Type]; }
		}

		public virtual string Heading { get; set; }
		public virtual string Information { get; set; }
		public virtual IEnumerable<string> InformationList { get; set; }
		protected internal virtual bool IsDataBound { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		public virtual SystemInformationType Type { get; set; }

		#endregion

		#region Eventhandlers

		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);

			this.IsDataBound = true;
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(this.Visible && !this.IsDataBound)
				this.DataBind();

			base.OnPreRender(e);
		}

		#endregion
	}
}