using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Company.DirectoryServices;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithDirectoryEntryDependencyMadeTestable
	{
		// Google search: public ldap server
		// gave these hits among others (see the fields _firstLdapHost and _secondLdapHost)

		#region Fields

		private readonly IDirectory _directory;
		private const string _firstLdapHost = "directory.verisign.com";
		private const string _secondLdapHost = "directory.d-trust.de";

		#endregion

		#region Constructors

		public ClassWithDirectoryEntryDependencyMadeTestable(IDirectory directory)
		{
			if(directory == null)
				throw new ArgumentNullException("directory");

			this._directory = directory;
		}

		#endregion

		#region Properties

		public bool Condition { get; set; }

		protected internal virtual IDirectory Directory
		{
			get { return this._directory; }
		}

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
			using(IDirectoryEntry directoryEntry = this.Directory.Get(this.LdapUrl))
			{
				return directoryEntry.Properties.PropertyNames.Cast<string>().ToArray();
			}
		}

		#endregion
	}
}