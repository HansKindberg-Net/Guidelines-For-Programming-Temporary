using System;
using System.Web.UI;
using Company.Web.UI.Extensions;

namespace Company.WebApplication.Business.Web.UI.WebControls
{
	public class HierarchyDataContainer : Control, INamingContainer
	{
		#region Fields

		private readonly IHierarchyData _hierarchyData;
		private Lazy<int> _level;

		#endregion

		#region Constructors

		public HierarchyDataContainer(IHierarchyData hierarchyData)
		{
			if(hierarchyData == null)
				throw new ArgumentNullException("hierarchyData");

			this._hierarchyData = hierarchyData;
		}

		#endregion

		#region Properties

		public virtual IHierarchyData HierarchyData
		{
			get { return this._hierarchyData; }
		}

		public virtual int Level
		{
			get
			{
				if(this._level == null)
					this._level = new Lazy<int>(() => this.HierarchyData.GetLevel());

				return this._level.Value;
			}
		}

		#endregion
	}
}