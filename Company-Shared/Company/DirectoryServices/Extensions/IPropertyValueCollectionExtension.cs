using System.DirectoryServices;

namespace Company.DirectoryServices.Extensions
{
	public interface IPropertyValueCollectionExtension
	{
		#region Methods

		PropertyValueCollection GetPropertyValueCollection(IPropertyValueCollection propertyValueCollection);

		#endregion
	}
}