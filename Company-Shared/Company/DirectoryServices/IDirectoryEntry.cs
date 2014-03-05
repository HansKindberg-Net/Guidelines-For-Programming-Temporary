using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Company.DirectoryServices
{
	public interface IDirectoryEntry : IComponent
	{
		#region Properties

		IDirectoryEntryCollection Children { get; }
		Guid Guid { get; }
		string Name { get; }
		string NativeGuid { get; }
		object NativeObject { get; }
		//ActiveDirectorySecurity ObjectSecurity { get; set; }
		IDirectoryEntryConfiguration Options { get; }
		IDirectoryEntry Parent { get; }
		string Path { get; set; }
		IPropertyDictionary Properties { get; }
		string SchemaClassName { get; }
		IDirectoryEntry SchemaEntry { get; }
		bool UsePropertyCache { get; set; }

		#endregion

		#region Methods

		void Close();
		void CommitChanges();
		IDirectoryEntry CopyTo(IDirectoryEntry newParent);
		IDirectoryEntry CopyTo(IDirectoryEntry newParent, string newName);
		void DeleteTree();
		object Invoke(string methodName, params object[] args);
		object InvokeGet(string propertyName);
		void InvokeSet(string propertyName, params object[] args);
		void MoveTo(IDirectoryEntry newParent);
		void MoveTo(IDirectoryEntry newParent, string newName);
		void RefreshCache();
		void RefreshCache(string[] propertyNames);
		void Rename(string newName);

		#endregion
	}
}