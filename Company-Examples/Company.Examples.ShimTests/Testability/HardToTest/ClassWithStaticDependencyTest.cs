using Company.Examples.Testability.Dependencies.HardToMock.Fakes;
using Company.Examples.Testability.HardToTest;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.ShimTests.Testability.HardToTest
{
	[TestClass]
	public class ClassWithStaticDependencyTest
	{
		#region Methods

		[TestMethod]
		public void Method_ShouldCallMethodOnTheClassWithStaticMethod()
		{
			using(ShimsContext.Create())
			{
				bool methodIsCalled = false;

				ShimClassWithStaticMethod.Method = delegate
				{
					methodIsCalled = true;
					return "Test";
				};

				Assert.IsFalse(methodIsCalled);

				new ClassWithStaticDependency().Method();

				Assert.IsTrue(methodIsCalled);
			}
		}

		#endregion
	}
}