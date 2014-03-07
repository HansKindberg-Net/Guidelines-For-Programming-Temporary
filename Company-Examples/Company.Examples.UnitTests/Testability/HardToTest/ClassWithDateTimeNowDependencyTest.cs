using System;
using Company.Examples.Testability.HardToTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.UnitTests.Testability.HardToTest
{
	[TestClass]
	public class ClassWithDateTimeNowDependencyTest
	{
		#region Methods

		[TestMethod]
		public void GetCurrentDateTime_ShouldReturnDateTimeNow()
		{
			// This test passes some times and some times NOT. I guess it has to do how fast the code runs from time to time.
			Assert.AreEqual(DateTime.Now, new ClassWithDateTimeNowDependency().GetCurrentDateTime(), "We can not test if a method returns DateTime.Now.");
		}

		#endregion
	}
}