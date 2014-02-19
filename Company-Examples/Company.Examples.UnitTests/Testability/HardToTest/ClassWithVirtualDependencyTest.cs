using Company.Examples.Testability.HardToTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.UnitTests.Testability.HardToTest
{
	[TestClass]
	public class ClassWithVirtualDependencyTest
	{
		#region Methods

		[TestMethod]
		public void Method_ShouldCallMethodOnTheClassWithVirtualMethod()
		{
			// The only thing we want to test is that when we call "new ClassWithVirtualDependency().Method()" we want to verify that ClassWithVirtualMethod.Method() have been called.
			// We can not verify that ClassWithVirtualMethod.Method() is called because we do not have control over that class and we can not override the method.
			new ClassWithVirtualDependency().Method();
			Assert.Inconclusive("It is impossible to test this because you can not verify if the virtual method of the dependency have been called.");
		}

		#endregion
	}
}