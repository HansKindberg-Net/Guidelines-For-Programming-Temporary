using System.Collections;

namespace Company.DirectoryServices
{
	public interface IPropertyValueCollection : IList
	{
		#region Properties

		string PropertyName { get; }
		object Value { get; set; }

		#endregion

		#region Methods

		void AddRange(object[] value);
		void AddRange(IPropertyValueCollection value);
		void CopyTo(object[] array, int index);

		#endregion
	}
}