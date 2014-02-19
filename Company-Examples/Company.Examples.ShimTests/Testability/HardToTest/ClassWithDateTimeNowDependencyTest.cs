using System;
using System.Fakes;
using Company.Examples.Testability.HardToTest;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.ShimTests.Testability.HardToTest
{
	[TestClass]
	public class ClassWithDateTimeNowDependencyTest
	{
		#region Methods

		[TestMethod]
		public void GetCurrentDateTime_ShouldReturnDateTimeNow()
		{
			using(ShimsContext.Create())
			{
				DateTime now = new DateTime(2020, 1, 1);

				ShimDateTime.NowGet = () => now;

				Assert.AreEqual(now, new ClassWithDateTimeNowDependency().GetCurrentDateTime());
			}
		}

		#endregion
	}
}