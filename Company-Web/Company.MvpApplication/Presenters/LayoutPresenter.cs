using System;
using Company.MvpApplication.Business.Mvp.Models;
using Company.MvpApplication.Business.Mvp.Presenters;
using Company.MvpApplication.Models;
using Company.MvpApplication.Views.Shared;

namespace Company.MvpApplication.Presenters
{
	public class LayoutPresenter : Presenter<ILayoutView>
	{
		#region Constructors

		public LayoutPresenter(ILayoutView view, IModelFactory modelFactory) : base(view, modelFactory)
		{
			this.View.Load += this.OnViewLoad;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.View.Model = this.ModelFactory.Create<LayoutModel>();
		}

		#endregion
	}
}