using System;
using System.Globalization;
using Company.Examples.Testability.Dependencies.Mockable;
using Company.Examples.Testability.Testable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Examples.UnitTests.Testability.Testable
{
	[TestClass]
	public class ClassWithSetterInjectableInterfaceDependencyTest
	{
		#region Fields

		private const string _callToTheDependencyMessageFormat = "The dependency method returned \"{0}\" and the dependency property returned \"{1}\".";

		#endregion

		#region Methods

		[TestMethod]
		public void CallToTheDependency_ShouldReturnTheCorrectMessage()
		{
			bool randomBoolean = DateTime.Now.Second%2 == 0;
			string currentDay = DateTime.Now.ToString("dddd", CultureInfo.GetCultureInfo("en"));

			Mock<IDependency> dependencyMock = new Mock<IDependency>();
			dependencyMock.Setup(dependency => dependency.Method()).Returns(randomBoolean);
			dependencyMock.Setup(dependency => dependency.Property).Returns(currentDay);

			string expected = string.Format(CultureInfo.InvariantCulture, _callToTheDependencyMessageFormat, randomBoolean, currentDay);

			Assert.AreEqual(expected, new ClassWithSetterInjectableInterfaceDependency {Dependency = dependencyMock.Object}.CallToTheDependency());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Dependency_Get_IfNotSet_ShouldThrowAnInvalidOperationException()
		{
			Assert.IsNull(new ClassWithSetterInjectableInterfaceDependency().Dependency);
		}

		[TestMethod]
		public void Dependency_Get_ShouldReturnTheSetValue()
		{
			var dependency = Mock.Of<IDependency>();

			Assert.AreEqual(dependency, new ClassWithSetterInjectableInterfaceDependency {Dependency = dependency}.Dependency);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Dependency_Set_IfTheValueParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new ClassWithSetterInjectableInterfaceDependency().Dependency = null;
		}

		#endregion
	}
}