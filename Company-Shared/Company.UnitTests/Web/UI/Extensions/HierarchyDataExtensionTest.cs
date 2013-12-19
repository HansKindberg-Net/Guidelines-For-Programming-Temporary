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

		private static IEnumerable<IHierarchyData> CreateRandomDescendants()
		{
			int numberOfLevels = DateTime.Now.Second;
			List<IHierarchyData> descendants = new List<IHierarchyData>();

			for(int i = 0; i <= numberOfLevels; i++)
			{
				Mock<IHierarchyData> hierarchyDataMock = new Mock<IHierarchyData>();
				hierarchyDataMock.Setup(hierarchyData => hierarchyData.GetParent()).Returns(i == 0 ? null : descendants[i - 1]);
				descendants.Add(hierarchyDataMock.Object);
			}

			return descendants.ToArray();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetAncestors_IfTheHierarchyDataParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				((IHierarchyData) null).GetAncestors();
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "hierarchyData")
					throw;
			}
		}

		[TestMethod]
		public void GetAncestors_ShouldReturnTheAncestors()
		{
			var descendants = CreateRandomDescendants().ToList();

			IHierarchyData hierarchyData = descendants.Last();

			var descendantsWithoutSelf = descendants.ToList();
			descendantsWithoutSelf.RemoveAt(descendants.Count() - 1);

			var ancestors = hierarchyData.GetAncestors().ToArray();

			Assert.AreEqual(descendantsWithoutSelf.Count(), ancestors.Count());

			descendantsWithoutSelf.Reverse();

			for(int i = 0; i < descendantsWithoutSelf.Count(); i++)
			{
				Assert.AreEqual(descendantsWithoutSelf.ElementAt(i), ancestors.ElementAt(i));
			}
		}

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
			var descendants = CreateRandomDescendants().ToArray();
			Assert.AreEqual(descendants.Count() - 1, descendants.Last().GetLevel());
		}

		#endregion
	}
}