using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace Company.DirectoryServices
{
	public interface ISchemaNameCollection : IList
	{
		#region Properties

		new string this[int index] { get; set; }

		#endregion

		#region Methods

		int Add(string value);
		void AddRange(string[] value);
		void AddRange(SchemaNameCollection value);
		bool Contains(string value);
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string")]
		void CopyTo(string[] stringArray, int index);
		int IndexOf(string value);
		void Insert(int index, string value);
		void Remove(string value);

		#endregion
	}
}