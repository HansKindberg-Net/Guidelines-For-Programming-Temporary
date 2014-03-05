using System;
using System.DirectoryServices;

namespace Company.DirectoryServices.Extensions
{
	public static class DirectoryEntryExtension
	{
		#region Fields

		private static volatile IDirectoryEntryExtension _instance;
		private static readonly object _lockObject = new object();

		#endregion

		#region Properties

		public static IDirectoryEntryExtension Instance
		{
			get
			{
				if(_instance == null)
				{
					lock(_lockObject)
					{
						if(_instance == null)
						{
							_instance = new DefaultDirectoryEntryExtension();
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

		public static DirectoryEntry GetDirectoryEntry(this object value, IDirectoryEntry directoryEntry)
		{
			if(value == null)
				throw new ArgumentNullException("value");

			return Instance.GetDirectoryEntry(directoryEntry);
		}

		#endregion
	}
}