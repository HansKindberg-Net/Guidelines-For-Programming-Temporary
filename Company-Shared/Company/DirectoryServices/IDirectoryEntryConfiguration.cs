using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace Company.DirectoryServices
{
	public interface IDirectoryEntryConfiguration
	{
		#region Properties

		int PageSize { get; set; }
		PasswordEncodingMethod PasswordEncoding { get; set; }
		int PasswordPort { get; set; }
		ReferralChasingOption Referral { get; set; }
		SecurityMasks SecurityMasks { get; set; }

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		string GetCurrentServerName();
		bool IsMutuallyAuthenticated();
		void SetUserNameQueryQuota(string accountName);

		#endregion
	}
}