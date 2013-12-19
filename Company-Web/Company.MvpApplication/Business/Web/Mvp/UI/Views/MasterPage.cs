using System;
using Company.MvpApplication.Business.Mvp.Views;
using WebFormsMvp.Web;

namespace Company.MvpApplication.Business.Web.Mvp.UI.Views
{
	public abstract class MasterPage : System.Web.UI.MasterPage, IView
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

	public abstract class MasterPage<TModel> : MasterPage, IView<TModel>
	{
		#region Properties

		public virtual TModel Model { get; set; }

		#endregion
	}
}