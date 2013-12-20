using System;
using System.Web;
using Company.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.UnitTests.Web
{
	[TestClass]
	public class SiteMapNodeWrapperTest
	{
		#region Methods

		private static SiteMapNode CreateSiteMapNode(string key, string url)
		{
			return CreateSiteMapNodeMock(key, url).Object;
		}

		private static SiteMapNode CreateSiteMapNode()
		{
			return CreateSiteMapNodeMock().Object;
		}

		private static Mock<SiteMapNode> CreateSiteMapNodeMock()
		{
			return new Mock<SiteMapNode>(new object[] {Mock.Of<SiteMapProvider>(), "Test"}) {CallBase = true};
		}

		private static Mock<SiteMapNode> CreateSiteMapNodeMock(string key, string url)
		{
			if(key == null)
				throw new ArgumentNullException("key");

			return new Mock<SiteMapNode>(new object[] {Mock.Of<SiteMapProvider>(), key, url}) {CallBase = true};
		}

		private static SiteMapNodeWrapper CreateSiteMapNodeWrapper()
		{
			return new SiteMapNodeWrapper(CreateSiteMapNode());
		}

		[TestMethod]
		public void Equals_WithObjectParameter_IfTheObjectParameterIsASiteMapNodeWrapper_ShouldCallEqualsWithSiteMapNodeWrapperParameter()
		{
			Mock<SiteMapNodeWrapper> siteMapNodeWrapperMock = new Mock<SiteMapNodeWrapper>(new object[] {CreateSiteMapNode()}) {CallBase = true};
			bool equalsIsCalled = false;

			siteMapNodeWrapperMock.Setup(siteMapNodeWrapper => siteMapNodeWrapper.Equals(It.IsAny<SiteMapNodeWrapper>())).Callback(() => { equalsIsCalled = true; });

			Assert.IsFalse(equalsIsCalled);

			siteMapNodeWrapperMock.Object.Equals(CreateSiteMapNodeWrapper());

			Assert.IsTrue(equalsIsCalled);
		}

		[TestMethod]
		public void Equals_WithObjectParameter_IfTheObjectParameterIsNotASiteMapNodeWrapper_ShouldReturnFalse()
		{
			Assert.IsFalse(CreateSiteMapNodeWrapper().Equals(new object()));
		}

		[TestMethod]
		public void Equals_WithObjectParameter_IfTheObjectParameterIsNull_ShouldReturnFalse()
		{
			Assert.IsFalse(CreateSiteMapNodeWrapper().Equals((object) null));
		}

		[TestMethod]
		public void Equals_WithSiteMapNodeWrapperParameter_IfTheSiteMapNodeWrapperParameterIsAReferenceEqual_ShouldReturnTrue()
		{
			SiteMapNodeWrapper siteMapNodeWrapper = CreateSiteMapNodeWrapper();

			Assert.IsTrue(siteMapNodeWrapper.Equals(siteMapNodeWrapper));
		}

		[TestMethod]
		public void Equals_WithSiteMapNodeWrapperParameter_IfTheSiteMapNodeWrapperParameterIsNull_ShouldReturnFalse()
		{
			Assert.IsFalse(CreateSiteMapNodeWrapper().Equals(null));
		}

		[TestMethod]
		public void Equals_WithSiteMapNodeWrapperParameter_IfTheSiteMapNodeWrapperParameterWrapsAnEqualSiteMapNode_ShouldReturnTrue()
		{
			var firstSiteMapNode = CreateSiteMapNode("Test", null);
			var secondSiteMapNode = CreateSiteMapNode("Test", null);

			Assert.IsTrue(firstSiteMapNode.Equals(secondSiteMapNode));
			Assert.IsTrue(new SiteMapNodeWrapper(firstSiteMapNode).Equals(new SiteMapNodeWrapper(secondSiteMapNode)));

			firstSiteMapNode = CreateSiteMapNode("Test", string.Empty);
			secondSiteMapNode = CreateSiteMapNode("Test", string.Empty);

			Assert.IsTrue(firstSiteMapNode.Equals(secondSiteMapNode));
			Assert.IsTrue(new SiteMapNodeWrapper(firstSiteMapNode).Equals(new SiteMapNodeWrapper(secondSiteMapNode)));

			firstSiteMapNode = CreateSiteMapNode("Test", "test");
			secondSiteMapNode = CreateSiteMapNode("Test", "TEST");

			Assert.IsTrue(firstSiteMapNode.Equals(secondSiteMapNode));
			Assert.IsTrue(new SiteMapNodeWrapper(firstSiteMapNode).Equals(new SiteMapNodeWrapper(secondSiteMapNode)));
		}

		[TestMethod]
		public void Equals_WithSiteMapNodeWrapperParameter_IfTheSiteMapNodeWrapperParameterWrapsAnUnequalSiteMapNode_ShouldReturnFalse()
		{
			var firstSiteMapNode = CreateSiteMapNode("Test1", null);
			var secondSiteMapNode = CreateSiteMapNode("Test2", null);

			Assert.IsFalse(firstSiteMapNode.Equals(secondSiteMapNode));
			Assert.IsFalse(new SiteMapNodeWrapper(firstSiteMapNode).Equals(new SiteMapNodeWrapper(secondSiteMapNode)));

			firstSiteMapNode = CreateSiteMapNode("Test1", string.Empty);
			secondSiteMapNode = CreateSiteMapNode("Test2", string.Empty);

			Assert.IsFalse(firstSiteMapNode.Equals(secondSiteMapNode));
			Assert.IsFalse(new SiteMapNodeWrapper(firstSiteMapNode).Equals(new SiteMapNodeWrapper(secondSiteMapNode)));

			firstSiteMapNode = CreateSiteMapNode("Test1", "test");
			secondSiteMapNode = CreateSiteMapNode("Test2", "TEST");

			Assert.IsFalse(firstSiteMapNode.Equals(secondSiteMapNode));
			Assert.IsFalse(new SiteMapNodeWrapper(firstSiteMapNode).Equals(new SiteMapNodeWrapper(secondSiteMapNode)));

			firstSiteMapNode = CreateSiteMapNode("Test", null);
			secondSiteMapNode = CreateSiteMapNode("Test", "Test");

			Assert.IsFalse(firstSiteMapNode.Equals(secondSiteMapNode));
			Assert.IsFalse(new SiteMapNodeWrapper(firstSiteMapNode).Equals(new SiteMapNodeWrapper(secondSiteMapNode)));

			firstSiteMapNode = CreateSiteMapNode("Test", "Test1");
			secondSiteMapNode = CreateSiteMapNode("Test", "Test2");

			Assert.IsFalse(firstSiteMapNode.Equals(secondSiteMapNode));
			Assert.IsFalse(new SiteMapNodeWrapper(firstSiteMapNode).Equals(new SiteMapNodeWrapper(secondSiteMapNode)));
		}

		[TestMethod]
		public void GetHashCode_ShouldReturnTheHashCodeOfTheWrappedSiteMapNode()
		{
			int hashCode = DateTime.Now.Millisecond;

			Mock<SiteMapNode> siteMapNodeMock = CreateSiteMapNodeMock();
			siteMapNodeMock.Setup(siteMapNode => siteMapNode.GetHashCode()).Returns(hashCode);

			SiteMapNodeWrapper siteMapNodeWrapper = new SiteMapNodeWrapper(siteMapNodeMock.Object);

			Assert.AreEqual(hashCode, siteMapNodeWrapper.GetHashCode());
			Assert.AreEqual(hashCode, siteMapNodeWrapper.SiteMapNode.GetHashCode());
		}

		#endregion
	}
}