using System;

namespace Company.MvcApplication.Models
{
	public abstract class SiteModel
	{
		#region Fields

		private readonly LayoutModel _layoutModel;

		#endregion

		#region Constructors

		protected SiteModel(LayoutModel layoutModel)
		{
			if(layoutModel == null)
				throw new ArgumentNullException("layoutModel");

			this._layoutModel = layoutModel;
		}

		#endregion

		#region Properties

		public virtual LayoutModel LayoutModel
		{
			get { return this._layoutModel; }
		}

		#endregion
	}
}