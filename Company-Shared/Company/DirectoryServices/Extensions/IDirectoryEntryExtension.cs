using System.DirectoryServices;

namespace Company.DirectoryServices.Extensions
{
	public interface IDirectoryEntryExtension
	{
		#region Methods

		DirectoryEntry GetDirectoryEntry(IDirectoryEntry directoryEntry);

		#endregion
	}
}