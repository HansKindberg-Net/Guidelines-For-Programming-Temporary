using Company.Examples.Testability.HardToTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.UnitTests.Testability.HardToTest
{
	[TestClass]
	public class ClassWithSealedDependencyTest
	{
		#region Methods

		[TestMethod]
		public void Method_ShouldCallMethodOnTheClassWithSealedMethod()
		{
			// The only thing we want to test is that when we call "new ClassWithSealedDependency().Method()" we want to verify that ClassWithSealedMethod.Method() have been called.
			// We can not verify that ClassWithSealedMethod.Method() is called because we do not have control over that class and we can not override the method.
			new ClassWithSealedDependency().Method();
			Assert.Inconclusive("It is impossible to test this because you can not verify if the sealed method of the dependency have been called.");
		}

		#endregion
	}
}