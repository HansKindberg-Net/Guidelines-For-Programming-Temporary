using System.Diagnostics.CodeAnalysis;

namespace Company.DirectoryServices
{
	public interface IDirectory
	{
		#region Properties

		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
		string HostUrl { get; }

		#endregion

		#region Methods

		IDirectoryEntry GetDirectoryEntry(string path);
		string GetPath(string distinguishedName);

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		IDirectoryEntry GetRoot();

		#endregion
	}
}