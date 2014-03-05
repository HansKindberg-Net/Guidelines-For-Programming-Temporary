using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace Company.DirectoryServices
{
	public interface IDirectory
	{
		#region Properties

		AuthenticationTypes AuthenticationTypes { get; set; }
		string Password { get; set; }
		string UserName { get; set; }

		#endregion

		#region Methods

		bool Exists(string path);
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		IDirectoryEntry Get(string path);

		#endregion
	}
}