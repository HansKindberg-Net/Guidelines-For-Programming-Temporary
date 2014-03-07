using System;
using System.Diagnostics.CodeAnalysis;
using Company.Examples.Testability.Dependencies.Mockable;
using Company.Examples.Testability.Testable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Examples.UnitTests.Testability.Testable
{
	[TestClass]
	public class ClassWithVirtualDependencyMadeTestableTest
	{
		#region Methods

		[TestMethod]
		public void ClassWithVirtualMethod_ShouldReturnTheClassWithVirtualMethodPassedInTheConstructor()
		{
			ClassWithVirtualMethod classWithVirtualMethod = Mock.Of<ClassWithVirtualMethod>();

			Assert.AreEqual(classWithVirtualMethod, new ClassWithVirtualDependencyMadeTestable(classWithVirtualMethod).ClassWithVirtualMethod);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Company.Examples.Testability.Testable.ClassWithVirtualDependencyMadeTestable")]
		public void Constructor_IfTheClassWithVirtualMethodParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new ClassWithVirtualDependencyMadeTestable(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "classWithVirtualMethod")
					throw;
			}
		}

		[TestMethod]
		public void Method_ShouldCallMethodOnTheClassWithVirtualMethod()
		{
			var classWithVirtualMethodMock = new Mock<ClassWithVirtualMethod>();
			classWithVirtualMethodMock.Verify(classWithVirtualMethod => classWithVirtualMethod.Method(), Times.Never);
			new ClassWithVirtualDependencyMadeTestable(classWithVirtualMethodMock.Object).Method();
			classWithVirtualMethodMock.Verify(classWithVirtualMethod => classWithVirtualMethod.Method(), Times.Once);
		}

		#endregion
	}
}