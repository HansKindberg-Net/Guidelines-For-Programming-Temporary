using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace Company.DirectoryServices
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class DirectoryEntryWrapper : IDirectoryEntry
	{
		#region Fields

		private readonly DirectoryEntry _directoryEntry;

		#endregion

		#region Constructors

		public DirectoryEntryWrapper(DirectoryEntry directoryEntry)
		{
			if(directoryEntry == null)
				throw new ArgumentNullException("directoryEntry");

			this._directoryEntry = directoryEntry;
		}

		#endregion

		#region Properties

		public virtual IDirectoryEntries Children
		{
			get { return (DirectoryEntriesWrapper) this.DirectoryEntry.Children; }
		}

		protected internal virtual DirectoryEntry DirectoryEntry
		{
			get { return this._directoryEntry; }
		}

		public virtual IDirectoryEntry Parent
		{
			get { return (DirectoryEntryWrapper) this.DirectoryEntry.Parent; }
		}

		public virtual string Path
		{
			get { return this.DirectoryEntry.Path; }
			set { this.DirectoryEntry.Path = value; }
		}

		public virtual IDictionary Properties
		{
			get { return this.DirectoryEntry.Properties; }
		}

		#endregion

		#region Methods

		public virtual void CommitChanges()
		{
			this.DirectoryEntry.CommitChanges();
		}

		public virtual void DeleteTree()
		{
			this.DirectoryEntry.DeleteTree();
		}

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			this.DirectoryEntry.Dispose();
		}

		public static DirectoryEntryWrapper FromDirectoryEntry(DirectoryEntry directoryEntry)
		{
			return directoryEntry;
		}

		public virtual object Invoke(string methodName, params object[] arguments)
		{
			return this.DirectoryEntry.Invoke(methodName, arguments);
		}

		#endregion

		#region Implicit operator

		public static implicit operator DirectoryEntryWrapper(DirectoryEntry directoryEntry)
		{
			return directoryEntry == null ? null : new DirectoryEntryWrapper(directoryEntry);
		}

		#endregion
	}
}