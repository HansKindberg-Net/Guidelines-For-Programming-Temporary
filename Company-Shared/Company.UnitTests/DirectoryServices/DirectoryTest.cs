using System;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Globalization;
using Company.DirectoryServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.UnitTests.DirectoryServices
{
	[TestClass]
	public class DirectoryTest
	{
		#region Methods

		[TestMethod]
		public void AuthenticationTypes_ShouldReturnTheDefaultValueOfDirectoryEntryAuthenticationTypeByDefault()
		{
			AuthenticationTypes defaultAuthenticationTypes = new Directory().AuthenticationTypes;

			using (DirectoryEntry directoryEntry = new DirectoryEntry())
			{
				Assert.AreEqual(defaultAuthenticationTypes, directoryEntry.AuthenticationType);
			}

			using (DirectoryEntry directoryEntry = new DirectoryEntry("Test"))
			{
				Assert.AreEqual(defaultAuthenticationTypes, directoryEntry.AuthenticationType);
			}

			using (DirectoryEntry directoryEntry = new DirectoryEntry("Test", "Test", "Test"))
			{
				Assert.AreEqual(defaultAuthenticationTypes, directoryEntry.AuthenticationType);
			}
		}

		#endregion
	}
}