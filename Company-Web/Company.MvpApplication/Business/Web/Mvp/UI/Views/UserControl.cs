using System;
using Company.MvpApplication.Business.Mvp.Views;
using WebFormsMvp.Web;

namespace Company.MvpApplication.Business.Web.Mvp.UI.Views
{
	public abstract class UserControl : System.Web.UI.UserControl, IView
	{
		#region Properties

		protected internal virtual bool EnableAutomaticDataBinding
		{
			get { return false; }
		}

		public virtual bool ThrowExceptionIfNoPresenterBound
		{
			get { return true; }
		}

		#endregion

		#region Methods

		protected override void OnInit(EventArgs e)
		{
			PageViewHost.Register(this, this.Context, this.EnableAutomaticDataBinding);

			base.OnInit(e);
		}

		#endregion
	}

	public abstract class UserControl<TModel> : UserControl, IView<TModel>
	{
		#region Properties

		public virtual TModel Model { get; set; }

		#endregion
	}
}