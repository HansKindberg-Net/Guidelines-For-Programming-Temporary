using System.DirectoryServices;

namespace Company.DirectoryServices
{
	public interface IDirectoryEntryInternal
	{
		#region Properties

		DirectoryEntry DirectoryEntry { get; }

		#endregion
	}
}