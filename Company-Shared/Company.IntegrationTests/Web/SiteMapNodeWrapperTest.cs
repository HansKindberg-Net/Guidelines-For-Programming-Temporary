using System;
using System.Web;
using Company.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.IntegrationTests.Web
{
	[TestClass]
	public class SiteMapNodeWrapperTest
	{
		#region Methods

		private static SiteMapNode CreateSiteMapNode()
		{
			return CreateSiteMapNodeMock().Object;
		}

		private static Mock<SiteMapNode> CreateSiteMapNodeMock()
		{
			return new Mock<SiteMapNode>(new object[] {Mock.Of<SiteMapProvider>(), "Test"}) {CallBase = true};
		}

		[TestMethod]
		public void ObjectEquals_Static_IfTheFirstObjectParameterIsASiteMapNodeWrapperAndTheSecondObjectParameterIsNotNull_ShouldCallSiteMapNodeWrapperEqualsOfThatObject()
		{
			Mock<SiteMapNodeWrapper> siteMapNodeWrapperMock = new Mock<SiteMapNodeWrapper>(new object[] {CreateSiteMapNode()}) {CallBase = true};
			bool equalsIsCalled = false;

			siteMapNodeWrapperMock.Setup(siteMapNodeWrapper => siteMapNodeWrapper.Equals(It.IsAny<object>())).Callback(() => { equalsIsCalled = true; }).Returns(false);

			Assert.IsFalse(equalsIsCalled);

			Assert.IsFalse(Equals(siteMapNodeWrapperMock.Object, DateTime.Now.Second%2 == 1 ? new object() : "Test")); // Sometimes the second parameter is of type object and other times of type string.

			Assert.IsTrue(equalsIsCalled);
		}

		#endregion
	}
}