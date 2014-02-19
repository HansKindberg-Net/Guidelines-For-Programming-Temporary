using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Company.Examples.Testability.Dependencies.Mockable;
using Company.Examples.Testability.Testable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Examples.UnitTests.Testability.Testable
{
	[TestClass]
	public class ClassWithConstructorInjectableInterfaceDependencyTest
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

			Assert.AreEqual(expected, new ClassWithConstructorInjectableInterfaceDependency(dependencyMock.Object).CallToTheDependency());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Company.Examples.Testability.Testable.ClassWithConstructorInjectableInterfaceDependency")]
		public void Constructor_IfTheDependencyParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new ClassWithConstructorInjectableInterfaceDependency(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "dependency")
					throw;
			}
		}

		[TestMethod]
		public void Dependency_ShouldReturnTheDependencyPassedInTheConstructor()
		{
			IDependency dependency = Mock.Of<IDependency>();

			Assert.AreEqual(dependency, new ClassWithConstructorInjectableInterfaceDependency(dependency).Dependency);
		}

		#endregion
	}
}