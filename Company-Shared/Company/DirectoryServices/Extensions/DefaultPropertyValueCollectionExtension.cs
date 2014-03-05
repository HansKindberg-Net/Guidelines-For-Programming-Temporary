using System;
using System.DirectoryServices;
using System.Globalization;

namespace Company.DirectoryServices.Extensions
{
	public class DefaultPropertyValueCollectionExtension : IPropertyValueCollectionExtension
	{
		#region Methods

		public virtual PropertyValueCollection GetPropertyValueCollection(IPropertyValueCollection propertyValueCollection)
		{
			if(propertyValueCollection == null)
				return null;

			IPropertyValueCollectionInternal propertyValueCollectionInternal = propertyValueCollection as IPropertyValueCollectionInternal;

			if(propertyValueCollectionInternal != null)
				return propertyValueCollectionInternal.PropertyValueCollection;

			throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "The object of type \"{0}\" does not implement \"{1}\".", propertyValueCollection.GetType(), typeof(IPropertyValueCollectionInternal)));
		}

		#endregion
	}
}