using System;
using System.Web.UI;

namespace Company.Web.UI.Extensions
{
	public static class HierarchyDataExtension
	{
		#region Methods

		public static int GetLevel(this IHierarchyData hierarchyData)
		{
			if(hierarchyData == null)
				throw new ArgumentNullException("hierarchyData");

			int level = 0;

			IHierarchyData parent = hierarchyData.GetParent();

			while(parent != null)
			{
				level++;

				parent = parent.GetParent();
			}

			return level;
		}

		#endregion
	}
}