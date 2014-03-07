using Company.Examples.Testability.Dependencies.Mockable;
using Company.Examples.Testability.Testable;
using Company.Examples.UnitTests.Testability.Testable.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Examples.UnitTests.Testability.Testable
{
	[TestClass]
	public class ClassWithStaticDependencyMadeTestableTest
	{
		#region Methods

		[TestMethod]
		public void Method_ShouldCallMethodOnTheClassWithStaticMethod()
		{
			// Test using a "home-made" mock.
			var homeMadeClassWithStaticMethodMock = new ClassWithStaticMethodMock();
			Assert.IsFalse(homeMadeClassWithStaticMethodMock.MethodIsCalled);
			new ClassWithStaticDependencyMadeTestable(homeMadeClassWithStaticMethodMock).Method();
			Assert.IsTrue(homeMadeClassWithStaticMethodMock.MethodIsCalled);

			// Test using Moq (a mocking framework for .NET)
			var classWithStaticMethodMock = new Mock<IClassWithStaticMethod>();
			classWithStaticMethodMock.Verify(classWithStaticMethod => classWithStaticMethod.Method(), Times.Never);
			new ClassWithStaticDependencyMadeTestable(classWithStaticMethodMock.Object).Method();
			classWithStaticMethodMock.Verify(classWithStaticMethod => classWithStaticMethod.Method(), Times.Once);
		}

		#endregion
	}
}