using System.DirectoryServices;

namespace Company.DirectoryServices
{
	public class Directory : IDirectory
	{
		#region Fields

		private AuthenticationTypes _authenticationTypes;

		#endregion

		#region Constructors

		public Directory()
		{
			using(DirectoryEntry directoryEntry = new DirectoryEntry())
			{
				this._authenticationTypes = directoryEntry.AuthenticationType;
			}
		}

		#endregion

		#region Properties

		public virtual AuthenticationTypes AuthenticationTypes
		{
			get { return this._authenticationTypes; }
			set { this._authenticationTypes = value; }
		}

		public virtual string Password { get; set; }
		public virtual string UserName { get; set; }

		#endregion

		#region Methods

		public virtual bool Exists(string path)
		{
			return DirectoryEntry.Exists(path);
		}

		public virtual IDirectoryEntry Get(string path)
		{
			return (DirectoryEntryWrapper) this.GetDirectoryEntry(path);
		}

		protected internal virtual DirectoryEntry GetDirectoryEntry(string path)
		{
			return new DirectoryEntry(path, this.UserName, this.Password, this.AuthenticationTypes);
		}

		#endregion
	}
}