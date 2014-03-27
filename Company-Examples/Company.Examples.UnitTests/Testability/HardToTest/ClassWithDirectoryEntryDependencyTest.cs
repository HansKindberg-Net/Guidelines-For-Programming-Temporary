using System.Collections.Generic;
using System.Linq;
using Company.Examples.Testability.HardToTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.UnitTests.Testability.HardToTest
{
	[TestClass]
	public class ClassWithDirectoryEntryDependencyTest
	{
		#region Methods

		[TestMethod]
		public void GetLdapRootPropertyNames_IfConditionIsFalse_ShouldReturnThePropertyNamesOfTheRootDirectoryEntryOfVerisign()
		{
			// I dont think this is a good test. Just an example of the difficulty to test a method that depends on System.DirectoryServices and DirectoryEntry.
			// We test something that depends on data that we do not have control over. If more properties are added to the data source (directory) the test will fail.
			// If this test throws an exception a firewall is probably blocking the traffic (port 389).

			IEnumerable<string> propertyNames = new ClassWithDirectoryEntryDependency {Condition = false}.GetLdapRootPropertyNames().ToArray();

			Assert.AreEqual(6, propertyNames.Count());

			Assert.AreEqual("objectclass", propertyNames.ElementAt(0));
			Assert.AreEqual("supportedextension", propertyNames.ElementAt(1));
			Assert.AreEqual("supportedcontrol", propertyNames.ElementAt(2));
			Assert.AreEqual("supportedsaslmechanisms", propertyNames.ElementAt(3));
			Assert.AreEqual("supportedldapversion", propertyNames.ElementAt(4));
			Assert.AreEqual("dataversion", propertyNames.ElementAt(5));
		}

		[TestMethod]
		public void GetLdapRootPropertyNames_IfConditionIsTrue_ShouldReturnThePropertyNamesOfTheRootDirectoryEntryOfDtrust()
		{
			// I dont think this is a good test. Just an example of the difficulty to test a method that depends on System.DirectoryServices and DirectoryEntry.
			// We test something that depends on data that we do not have control over. If more properties are added to the data source (directory) the test will fail.
			// If this test throws an exception a firewall is probably blocking the traffic (port 389).

			IEnumerable<string> propertyNames = new ClassWithDirectoryEntryDependency {Condition = true}.GetLdapRootPropertyNames().ToArray();

			Assert.AreEqual(10, propertyNames.Count());

			Assert.AreEqual("objectClass", propertyNames.ElementAt(0));
			Assert.AreEqual("namingContexts", propertyNames.ElementAt(1));
			Assert.AreEqual("supportedExtension", propertyNames.ElementAt(2));
			Assert.AreEqual("supportedControl", propertyNames.ElementAt(3));
			Assert.AreEqual("supportedSASLMechanisms", propertyNames.ElementAt(4));
			Assert.AreEqual("supportedLDAPVersion", propertyNames.ElementAt(5));
			Assert.AreEqual("vendorName", propertyNames.ElementAt(6));
			Assert.AreEqual("vendorVersion", propertyNames.ElementAt(7));
			Assert.AreEqual("dataversion", propertyNames.ElementAt(8));
			Assert.AreEqual("netscapemdsuffix", propertyNames.ElementAt(9));
		}

		[TestMethod]
		public void LdapUrl_IfConditionIsFalse_ShouldReturnALdapPathToVerisign()
		{
			Assert.AreEqual("LDAP://directory.verisign.com", new ClassWithDirectoryEntryDependency() {Condition = false}.LdapUrl);
		}

		[TestMethod]
		public void LdapUrl_IfConditionIsTrue_ShouldReturnALdapPathToDtrust()
		{
			Assert.AreEqual("LDAP://directory.d-trust.de", new ClassWithDirectoryEntryDependency() {Condition = true}.LdapUrl);
		}

		#endregion
	}
}