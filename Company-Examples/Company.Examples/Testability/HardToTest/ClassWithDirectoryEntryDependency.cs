using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Linq;

namespace Company.Examples.Testability.HardToTest
{
	public class ClassWithDirectoryEntryDependency
	{
		// Google search: public ldap server
		// gave these hits among others (see the fields _firstLdapHost and _secondLdapHost)

		#region Fields

		private const string _firstLdapHost = "directory.verisign.com";
		private const string _secondLdapHost = "directory.d-trust.de";

		#endregion

		#region Properties

		public bool Condition { get; set; }

		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "LDAP must be uppercase and will not be if we use an uri.")]
		protected internal virtual string LdapUrl
		{
			get { return "LDAP://" + (!this.Condition ? _firstLdapHost : _secondLdapHost); }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public IEnumerable<string> GetLdapRootPropertyNames()
		{
			using(DirectoryEntry directoryEntry = new DirectoryEntry(this.LdapUrl))
			{
				directoryEntry.AuthenticationType = AuthenticationTypes.Anonymous;

				return directoryEntry.Properties.PropertyNames.Cast<string>().ToArray();
			}
		}

		#endregion
	}
}