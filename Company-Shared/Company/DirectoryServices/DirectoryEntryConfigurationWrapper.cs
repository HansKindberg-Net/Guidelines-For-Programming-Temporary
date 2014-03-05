using System;
using System.DirectoryServices;

namespace Company.DirectoryServices
{
	public class DirectoryEntryConfigurationWrapper : IDirectoryEntryConfiguration
	{
		#region Fields

		private readonly DirectoryEntryConfiguration _directoryEntryConfiguration;

		#endregion

		#region Constructors

		public DirectoryEntryConfigurationWrapper(DirectoryEntryConfiguration directoryEntryConfiguration)
		{
			if(directoryEntryConfiguration == null)
				throw new ArgumentNullException("directoryEntryConfiguration");

			this._directoryEntryConfiguration = directoryEntryConfiguration;
		}

		#endregion

		#region Properties

		protected internal virtual DirectoryEntryConfiguration DirectoryEntryConfiguration
		{
			get { return this._directoryEntryConfiguration; }
		}

		public virtual int PageSize
		{
			get { return this.DirectoryEntryConfiguration.PageSize; }
			set { this.DirectoryEntryConfiguration.PageSize = value; }
		}

		public virtual PasswordEncodingMethod PasswordEncoding
		{
			get { return this.DirectoryEntryConfiguration.PasswordEncoding; }
			set { this.DirectoryEntryConfiguration.PasswordEncoding = value; }
		}

		public virtual int PasswordPort
		{
			get { return this.DirectoryEntryConfiguration.PasswordPort; }
			set { this.DirectoryEntryConfiguration.PasswordPort = value; }
		}

		public virtual ReferralChasingOption Referral
		{
			get { return this.DirectoryEntryConfiguration.Referral; }
			set { this.DirectoryEntryConfiguration.Referral = value; }
		}

		public virtual SecurityMasks SecurityMasks
		{
			get { return this.DirectoryEntryConfiguration.SecurityMasks; }
			set { this.DirectoryEntryConfiguration.SecurityMasks = value; }
		}

		#endregion

		#region Methods

		public virtual string GetCurrentServerName()
		{
			return this.DirectoryEntryConfiguration.GetCurrentServerName();
		}

		public virtual bool IsMutuallyAuthenticated()
		{
			return this.DirectoryEntryConfiguration.IsMutuallyAuthenticated();
		}

		public virtual void SetUserNameQueryQuota(string accountName)
		{
			this.DirectoryEntryConfiguration.SetUserNameQueryQuota(accountName);
		}

		public static DirectoryEntryConfigurationWrapper FromDirectoryEntryConfiguration(DirectoryEntryConfiguration directoryEntryConfiguration)
		{
			return directoryEntryConfiguration;
		}

		#endregion

		#region Implicit operator

		public static implicit operator DirectoryEntryConfigurationWrapper(DirectoryEntryConfiguration directoryEntryConfiguration)
		{
			return directoryEntryConfiguration == null ? null : new DirectoryEntryConfigurationWrapper(directoryEntryConfiguration);
		}

		#endregion
	}
}