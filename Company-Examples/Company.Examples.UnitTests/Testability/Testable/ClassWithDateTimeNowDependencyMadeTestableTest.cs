using System;
using Company.Examples.Testability.Testable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Examples.UnitTests.Testability.Testable
{
	[TestClass]
	public class ClassWithDateTimeNowDependencyMadeTestableTest
	{
		#region Methods

		[TestMethod]
		public void GetCurrentDateTime_ShouldReturnNowFromTheDateTimeContextDependency()
		{
			DateTime now = new DateTime(2020, 1, 1);
			Mock<IDateTimeContext> dateTimeContextMock = new Mock<IDateTimeContext>();
			dateTimeContextMock.Setup(dateTimeContext => dateTimeContext.Now).Returns(now);

			Assert.AreEqual(now, new ClassWithDateTimeNowDependencyMadeTestable(dateTimeContextMock.Object).GetCurrentDateTime());
		}

		#endregion
	}
}