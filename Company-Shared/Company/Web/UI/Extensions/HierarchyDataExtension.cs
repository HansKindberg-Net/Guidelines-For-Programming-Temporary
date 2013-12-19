using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Company.Web.UI.Extensions
{
	public static class HierarchyDataExtension
	{
		#region Methods

		public static IEnumerable<IHierarchyData> GetAncestors(this IHierarchyData hierarchyData)
		{
			if(hierarchyData == null)
				throw new ArgumentNullException("hierarchyData");

			List<IHierarchyData> ancestors = new List<IHierarchyData>();

			IHierarchyData parent = hierarchyData.GetParent();

			while(parent != null)
			{
				ancestors.Add(parent);

				parent = parent.GetParent();
			}

			return ancestors.ToArray();
		}

		public static int GetLevel(this IHierarchyData hierarchyData)
		{
			return hierarchyData.GetAncestors().Count();
		}

		#endregion
	}
}