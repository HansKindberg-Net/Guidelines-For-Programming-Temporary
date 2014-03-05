using System;
using System.DirectoryServices;

namespace Company.DirectoryServices.Extensions
{
	public static class PropertyValueCollectionExtension
	{
		#region Fields

		private static volatile IPropertyValueCollectionExtension _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static IPropertyValueCollectionExtension Instance
		{
			get
			{
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
						{
							_instance = new DefaultPropertyValueCollectionExtension();
						}
					}
				}

				return _instance;
			}
			set
			{
				if(Equals(_instance, value))
					return;

				lock(_lockObject)
				{
					_instance = value;
				}
			}
		}

		#endregion

		#region Methods

		public static PropertyValueCollection GetPropertyValueCollection(this object value, IPropertyValueCollection propertyValueCollection)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			return Instance.GetPropertyValueCollection(propertyValueCollection);
		}

		#endregion
	}
}