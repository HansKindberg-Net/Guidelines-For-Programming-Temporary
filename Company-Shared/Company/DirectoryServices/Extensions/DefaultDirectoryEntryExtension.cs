using System;
using System.DirectoryServices;
using System.Globalization;

namespace Company.DirectoryServices.Extensions
{
	public class DefaultDirectoryEntryExtension : IDirectoryEntryExtension
	{
		#region Methods

		public virtual DirectoryEntry GetDirectoryEntry(IDirectoryEntry directoryEntry)
		{
			if(directoryEntry == null)
				return null;

			IDirectoryEntryInternal directoryEntryInternal = directoryEntry as IDirectoryEntryInternal;

			if(directoryEntryInternal != null)
				return directoryEntryInternal.DirectoryEntry;

			throw new NotImplementedException(string.Format(CultureInfo.InvariantCulture, "The object of type \"{0}\" does not implement \"{1}\".", directoryEntry.GetType(), typeof(IDirectoryEntryInternal)));
		}

		#endregion
	}
}