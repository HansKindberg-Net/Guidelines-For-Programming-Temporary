using Company.Examples.Testability.Dependencies.Mockable;
using Company.Examples.Testability.Testable;
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
			Mock<IClassWithStaticMethod> classWithStaticMethodMock = new Mock<IClassWithStaticMethod>();
			classWithStaticMethodMock.Verify(classWithStaticMethod => classWithStaticMethod.Method(), Times.Never);
			new ClassWithStaticDependencyMadeTestable(classWithStaticMethodMock.Object).Method();
			classWithStaticMethodMock.Verify(classWithStaticMethod => classWithStaticMethod.Method(), Times.Once);
		}

		#endregion
	}
}