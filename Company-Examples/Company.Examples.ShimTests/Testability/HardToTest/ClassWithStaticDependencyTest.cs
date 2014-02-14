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
			//// The only thing we want to test is that when we call "new ClassWithStaticDependency().Method()" we want to verify that ClassWithStaticMethod.Method() have been called.
			//// We can not verify that ClassWithStaticMethod.Method() is called because we do not have control over that class and it is a static method.
			//new ClassWithStaticDependency().Method();
			//Assert.Fail("It is impossible to test this because you can not verify if the static method of the dependency have been called.");
		}

		#endregion
	}
}