using System;
using Company.MvpApplication.Business.Mvp.Models;
using Company.MvpApplication.Business.Mvp.Views;

namespace Company.MvpApplication.Business.Mvp.Presenters
{
	public abstract class Presenter<TView> : WebFormsMvp.Presenter<TView> where TView : class, IView
	{
		#region Fields

		private readonly IModelFactory _modelFactory;

		#endregion

		#region Constructors

		protected Presenter(TView view, IModelFactory modelFactory) : base(view)
		{
			if(modelFactory == null)
				throw new ArgumentNullException("modelFactory");

			this._modelFactory = modelFactory;
		}

		#endregion

		#region Properties

		protected internal virtual IModelFactory ModelFactory
		{
			get { return this._modelFactory; }
		}

		#endregion
	}
}