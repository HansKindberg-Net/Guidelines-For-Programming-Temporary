using System.Collections;

namespace Company.DirectoryServices
{
	public interface IDirectoryEntryCollection : IEnumerable
	{
		#region Properties

		ISchemaNameCollection SchemaFilter { get; }

		#endregion

		#region Methods

		IDirectoryEntry Add(string name, string schemaClassName);
		IDirectoryEntry Find(string name);
		IDirectoryEntry Find(string name, string schemaClassName);
		void Remove(IDirectoryEntry entry);

		#endregion
	}
}