using System;

namespace Company.MvcApplication.Models
{
	public abstract class SiteModel
	{
		#region Fields

		private readonly NavigationModel _navigationModel;

		#endregion

		#region Constructors

		protected SiteModel(NavigationModel navigationModel)
		{
			if(navigationModel == null)
				throw new ArgumentNullException("navigationModel");

			this._navigationModel = navigationModel;
		}

		#endregion

		#region Properties

		public virtual NavigationModel NavigationModel
		{
			get { return this._navigationModel; }
		}

		#endregion
	}
}