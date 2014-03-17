using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Company.DirectoryServices;
using Company.Examples.Testability.Testable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Examples.UnitTests.Testability.Testable
{
	[TestClass]
	public class ClassWithDirectoryEntryDependencyMadeTestableTest
	{
		#region Fields

		private const string _ldapPathToDtrust = "LDAP://directory.d-trust.de";
		private const string _ldapPathToVerisign = "LDAP://directory.verisign.com";

		#endregion

		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "Company.Examples.Testability.Testable.ClassWithDirectoryEntryDependencyMadeTestable")]
		public void Constructor_IfTheDirectoryParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new ClassWithDirectoryEntryDependencyMadeTestable(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "directory")
					throw;
			}
		}

		private static IDirectoryEntry CreateDirectoryEntry(IEnumerable<string> propertyNames)
		{
			var propertyDictionaryMock = new Mock<IPropertyDictionary>();
			propertyDictionaryMock.Setup(propertyDictionary => propertyDictionary.PropertyNames).Returns(new List<string>(propertyNames));

			var directoryEntryMock = new Mock<IDirectoryEntry>();
			directoryEntryMock.Setup(directoryEntry => directoryEntry.Properties).Returns(propertyDictionaryMock.Object);

			return directoryEntryMock.Object;
		}

		private static IEnumerable<string> CreateRandomPropertyNames()
		{
			var numberOfRandomPropertyNames = DateTime.Now.Second;
			var randomPropertyNames = new List<string>();

			for(int i = 1; i <= numberOfRandomPropertyNames; i++)
			{
				randomPropertyNames.Add(i.ToString(CultureInfo.InvariantCulture));
			}

			return randomPropertyNames.ToArray();
		}

		[TestMethod]
		public void GetLdapRootPropertyNames_IfConditionIsFalse_ShouldReturnThePropertyNamesOfTheRootDirectoryEntryOfVerisign()
		{
			var expectedPropertyNames = CreateRandomPropertyNames().ToArray();

			using(IDirectoryEntry directoryEntry = CreateDirectoryEntry(expectedPropertyNames))
			{
				Mock<IDirectory> directoryMock = new Mock<IDirectory>();
				directoryMock.Setup(directory => directory.Get(_ldapPathToVerisign)).Returns(directoryEntry);

				var classWithDirectoryEntryDependencyMadeTestable = new ClassWithDirectoryEntryDependencyMadeTestable(directoryMock.Object) {Condition = false};

				directoryMock.Verify(directory => directory.Get(_ldapPathToVerisign), Times.Never);

				var actualPropertyNames = classWithDirectoryEntryDependencyMadeTestable.GetLdapRootPropertyNames().ToArray();

				directoryMock.Verify(directory => directory.Get(_ldapPathToVerisign), Times.Once);

				Assert.IsFalse(classWithDirectoryEntryDependencyMadeTestable.Condition);
				Assert.AreEqual(expectedPropertyNames.Count(), actualPropertyNames.Count());

				for(int i = 0; i < expectedPropertyNames.Count(); i++)
				{
					Assert.AreEqual(expectedPropertyNames.ElementAt(i), actualPropertyNames.ElementAt(i));
				}
			}
		}

		[TestMethod]
		public void GetLdapRootPropertyNames_IfConditionIsTrue_ShouldReturnThePropertyNamesOfTheRootDirectoryEntryOfDtrust()
		{
			var expectedPropertyNames = CreateRandomPropertyNames().ToArray();

			using(IDirectoryEntry directoryEntry = CreateDirectoryEntry(expectedPropertyNames))
			{
				Mock<IDirectory> directoryMock = new Mock<IDirectory>();
				directoryMock.Setup(directory => directory.Get(_ldapPathToDtrust)).Returns(directoryEntry);

				var classWithDirectoryEntryDependencyMadeTestable = new ClassWithDirectoryEntryDependencyMadeTestable(directoryMock.Object) {Condition = true};

				directoryMock.Verify(directory => directory.Get(_ldapPathToDtrust), Times.Never);

				var actualPropertyNames = classWithDirectoryEntryDependencyMadeTestable.GetLdapRootPropertyNames().ToArray();

				directoryMock.Verify(directory => directory.Get(_ldapPathToDtrust), Times.Once);

				Assert.IsTrue(classWithDirectoryEntryDependencyMadeTestable.Condition);
				Assert.AreEqual(expectedPropertyNames.Count(), actualPropertyNames.Count());

				for(int i = 0; i < expectedPropertyNames.Count(); i++)
				{
					Assert.AreEqual(expectedPropertyNames.ElementAt(i), actualPropertyNames.ElementAt(i));
				}
			}
		}

		[TestMethod]
		public void LdapUrl_IfConditionIsFalse_ShouldReturnALdapPathToVerisign()
		{
			Assert.AreEqual(_ldapPathToVerisign, new ClassWithDirectoryEntryDependencyMadeTestable(Mock.Of<IDirectory>()) {Condition = false}.LdapUrl);
		}

		[TestMethod]
		public void LdapUrl_IfConditionIsTrue_ShouldReturnALdapPathToDtrust()
		{
			Assert.AreEqual(_ldapPathToDtrust, new ClassWithDirectoryEntryDependencyMadeTestable(Mock.Of<IDirectory>()) {Condition = true}.LdapUrl);
		}

		#endregion
	}
}