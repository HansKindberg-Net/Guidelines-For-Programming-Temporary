using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Company.Web;

namespace Company.WebApplication.UserControls
{
	public partial class SystemInformation : System.Web.UI.UserControl, ISystemInformation
	{
		#region Fields

		private static readonly IDictionary<SystemInformationType, string> _alertCssClassDictionary = new Dictionary<SystemInformationType, string>
			{
				{SystemInformationType.Confirmation, "alert-success"},
				{SystemInformationType.Exception, "alert-danger"},
				{SystemInformationType.Information, "alert-info"},
				{SystemInformationType.Warning, "alert-warning"}
			};

		private string _heading;

		#endregion

		#region Properties

		public virtual string AlertCssClass
		{
			get { return _alertCssClassDictionary[this.Type]; }
		}

		public virtual string Heading
		{
			get { return this._heading ?? (this._heading = this.GetHeading(this.Type)); }
			set { this._heading = value; }
		}

		public virtual string Information { get; set; }
		public virtual IEnumerable<string> InformationList { get; set; }
		protected internal virtual bool IsDataBound { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		public virtual SystemInformationType Type { get; set; }

		#endregion

		#region Methods

		protected internal virtual string GetHeading(SystemInformationType systemInformationType)
		{
			switch(systemInformationType)
			{
				case SystemInformationType.Confirmation:
					return "Confirmation";
				case SystemInformationType.Exception:
					return "Error";
				case SystemInformationType.Warning:
					return "Warning";
				default:
					return "Information";
			}
		}

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