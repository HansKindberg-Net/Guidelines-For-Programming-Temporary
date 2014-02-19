using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Linq;

namespace Company.DirectoryServices
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class DirectoryEntriesWrapper : IDirectoryEntries
	{
		#region Fields

		private readonly DirectoryEntries _directoryEntries;

		#endregion

		#region Constructors

		public DirectoryEntriesWrapper(DirectoryEntries directoryEntries)
		{
			if(directoryEntries == null)
				throw new ArgumentNullException("directoryEntries");

			this._directoryEntries = directoryEntries;
		}

		#endregion

		#region Properties

		protected internal virtual DirectoryEntries DirectoryEntries
		{
			get { return this._directoryEntries; }
		}

		#endregion

		#region Methods

		public virtual IDirectoryEntry Add(string name, string schemaClassName)
		{
			return (DirectoryEntryWrapper) this.DirectoryEntries.Add(name, schemaClassName);
		}

		public virtual IDirectoryEntry Find(string name)
		{
			return (DirectoryEntryWrapper) this.DirectoryEntries.Find(name);
		}

		public virtual IDirectoryEntry Find(string name, string schemaClassName)
		{
			return (DirectoryEntryWrapper) this.DirectoryEntries.Find(name, schemaClassName);
		}

		public static DirectoryEntriesWrapper FromDirectoryEntries(DirectoryEntries directoryEntries)
		{
			return directoryEntries;
		}

		public virtual IEnumerator<IDirectoryEntry> GetEnumerator()
		{
			return this.ToList().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public virtual void Remove(IDirectoryEntry entry)
		{
			DirectoryEntryWrapper directoryEntryWrapper = entry as DirectoryEntryWrapper;

			if(directoryEntryWrapper == null)
				throw new NotImplementedException("Don't know how to implement this yet.");

			DirectoryEntry directoryEntry = directoryEntryWrapper.DirectoryEntry;

			directoryEntry.Parent.Children.Remove(directoryEntry);
		}

		protected internal virtual IList<IDirectoryEntry> ToList()
		{
			return (from DirectoryEntry directoryEntry in this.DirectoryEntries select (DirectoryEntryWrapper) directoryEntry).Cast<IDirectoryEntry>().ToList();
		}

		#endregion

		#region Implicit operator

		public static implicit operator DirectoryEntriesWrapper(DirectoryEntries directoryEntries)
		{
			return directoryEntries == null ? null : new DirectoryEntriesWrapper(directoryEntries);
		}

		#endregion
	}
}