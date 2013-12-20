using System;
using System.Web.Mvc;
using Company.MvcApplication.Business.Web.Mvc.Models;

namespace Company.MvcApplication.Controllers
{
	public abstract class SiteController : Controller
	{
		#region Fields

		private readonly IModelFactory _modelFactory;

		#endregion

		#region Constructors

		protected SiteController(IModelFactory modelFactory)
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