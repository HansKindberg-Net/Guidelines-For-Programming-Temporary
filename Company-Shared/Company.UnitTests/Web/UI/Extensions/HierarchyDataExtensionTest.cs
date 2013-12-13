using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Company.Web.UI.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.UnitTests.Web.UI.Extensions
{
	[TestClass]
	public class HierarchyDataExtensionTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetLevel_IfTheHierarchyDataParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				((IHierarchyData) null).GetLevel();
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "hierarchyData")
					throw;
			}
		}

		[TestMethod]
		public void GetLevel_ShouldReturnTheNumberOfAncestors()
		{
			int numberOfLevels = DateTime.Now.Second;
			List<IHierarchyData> descendants = new List<IHierarchyData>();

			for(int i = 0; i <= numberOfLevels; i++)
			{
				Mock<IHierarchyData> hierarchyDataMock = new Mock<IHierarchyData>();
				hierarchyDataMock.Setup(hierarchyData => hierarchyData.GetParent()).Returns(i == 0 ? null : descendants[i - 1]);
				descendants.Add(hierarchyDataMock.Object);
			}

			Assert.AreEqual(numberOfLevels, descendants.Last().GetLevel());
		}

		#endregion
	}
}