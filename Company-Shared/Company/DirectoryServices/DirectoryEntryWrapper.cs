using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using Company.DirectoryServices.Extensions;

namespace Company.DirectoryServices
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class DirectoryEntryWrapper : IDirectoryEntry, IDirectoryEntryInternal
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

		#region Events

		public virtual event EventHandler Disposed
		{
			add { this.DirectoryEntry.Disposed += value; }
			remove { this.DirectoryEntry.Disposed -= value; }
		}

		#endregion

		#region Properties

		public virtual IDirectoryEntryCollection Children
		{
			get { return (DirectoryEntriesWrapper) this.DirectoryEntry.Children; }
		}

		public virtual DirectoryEntry DirectoryEntry
		{
			get { return this._directoryEntry; }
		}

		public virtual Guid Guid
		{
			get { return this.DirectoryEntry.Guid; }
		}

		public virtual string Name
		{
			get { return this.DirectoryEntry.Name; }
		}

		public virtual string NativeGuid
		{
			get { return this.DirectoryEntry.NativeGuid; }
		}

		public virtual object NativeObject
		{
			get { return this.DirectoryEntry.NativeObject; }
		}

		public virtual IDirectoryEntryConfiguration Options
		{
			get { return (DirectoryEntryConfigurationWrapper) this.DirectoryEntry.Options; }
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

		public virtual IPropertyDictionary Properties
		{
			get { return (PropertyCollectionWrapper) this.DirectoryEntry.Properties; }
		}

		public virtual string SchemaClassName
		{
			get { return this.DirectoryEntry.SchemaClassName; }
		}

		public virtual IDirectoryEntry SchemaEntry
		{
			get { return (DirectoryEntryWrapper) this.DirectoryEntry.SchemaEntry; }
		}

		public virtual ISite Site
		{
			get { return this.DirectoryEntry.Site; }
			set { this.DirectoryEntry.Site = value; }
		}

		public virtual bool UsePropertyCache
		{
			get { return this.DirectoryEntry.UsePropertyCache; }
			set { this.DirectoryEntry.UsePropertyCache = value; }
		}

		#endregion

		#region Methods

		public virtual void Close()
		{
			this.DirectoryEntry.Close();
		}

		public virtual void CommitChanges()
		{
			this.DirectoryEntry.CommitChanges();
		}

		public virtual IDirectoryEntry CopyTo(IDirectoryEntry newParent)
		{
			return (DirectoryEntryWrapper) this.DirectoryEntry.CopyTo(this.GetDirectoryEntry(newParent));
		}

		public virtual IDirectoryEntry CopyTo(IDirectoryEntry newParent, string newName)
		{
			return (DirectoryEntryWrapper) this.DirectoryEntry.CopyTo(this.GetDirectoryEntry(newParent), newName);
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

		public virtual object Invoke(string methodName, params object[] args)
		{
			return this.DirectoryEntry.Invoke(methodName, args);
		}

		public virtual object InvokeGet(string propertyName)
		{
			return this.DirectoryEntry.InvokeGet(propertyName);
		}

		public virtual void InvokeSet(string propertyName, params object[] args)
		{
			this.DirectoryEntry.InvokeSet(propertyName, args);
		}

		public virtual void MoveTo(IDirectoryEntry newParent)
		{
			this.DirectoryEntry.MoveTo(this.GetDirectoryEntry(newParent));
		}

		public virtual void MoveTo(IDirectoryEntry newParent, string newName)
		{
			this.DirectoryEntry.MoveTo(this.GetDirectoryEntry(newParent), newName);
		}

		public virtual void RefreshCache()
		{
			this.DirectoryEntry.RefreshCache();
		}

		public virtual void RefreshCache(string[] propertyNames)
		{
			this.DirectoryEntry.RefreshCache(propertyNames);
		}

		public virtual void Rename(string newName)
		{
			this.DirectoryEntry.Rename(newName);
		}

		public static DirectoryEntryWrapper FromDirectoryEntry(DirectoryEntry directoryEntry)
		{
			return directoryEntry;
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