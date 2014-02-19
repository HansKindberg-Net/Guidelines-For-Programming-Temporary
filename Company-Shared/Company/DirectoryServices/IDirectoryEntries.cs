using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Company.DirectoryServices
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public interface IDirectoryEntries : IEnumerable<IDirectoryEntry>
	{
		#region Methods

		IDirectoryEntry Add(string name, string schemaClassName);
		IDirectoryEntry Find(string name);
		IDirectoryEntry Find(string name, string schemaClassName);
		void Remove(IDirectoryEntry entry);

		#endregion
	}
}