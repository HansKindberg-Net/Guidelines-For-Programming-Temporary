using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace Company.DirectoryServices
{
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Justification = "This is a wrapper.")]
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a wrapper.")]
	public class SchemaNameCollectionWrapper : ISchemaNameCollection
	{
		#region Fields

		private readonly SchemaNameCollection _schemaNameCollection;

		#endregion

		#region Constructors

		public SchemaNameCollectionWrapper(SchemaNameCollection schemaNameCollection)
		{
			if(schemaNameCollection == null)
				throw new ArgumentNullException("schemaNameCollection");

			this._schemaNameCollection = schemaNameCollection;
		}

		#endregion

		#region Properties

		public virtual int Count
		{
			get { return this.SchemaNameCollection.Count; }
		}

		public virtual bool IsFixedSize
		{
			get { return ((IList) this.SchemaNameCollection).IsFixedSize; }
		}

		public virtual bool IsReadOnly
		{
			get { return ((IList) this.SchemaNameCollection).IsReadOnly; }
		}

		public virtual bool IsSynchronized
		{
			get { return ((ICollection) this.SchemaNameCollection).IsSynchronized; }
		}

		public virtual string this[int index]
		{
			get { return this.SchemaNameCollection[index]; }
			set { this.SchemaNameCollection[index] = value; }
		}

		object IList.this[int index]
		{
			get { return ((IList) this.SchemaNameCollection)[index]; }
			set { ((IList) this.SchemaNameCollection)[index] = value; }
		}

		public virtual SchemaNameCollection SchemaNameCollection
		{
			get { return this._schemaNameCollection; }
		}

		public virtual object SyncRoot
		{
			get { return ((ICollection) this.SchemaNameCollection).SyncRoot; }
		}

		#endregion

		#region Methods

		public virtual int Add(string value)
		{
			return this.SchemaNameCollection.Add(value);
		}

		int IList.Add(object value)
		{
			return ((IList) this.SchemaNameCollection).Add(value);
		}

		public virtual void AddRange(string[] value)
		{
			this.SchemaNameCollection.AddRange(value);
		}

		public virtual void AddRange(SchemaNameCollection value)
		{
			this.SchemaNameCollection.AddRange(value);
		}

		public virtual void Clear()
		{
			this.SchemaNameCollection.Clear();
		}

		bool IList.Contains(object value)
		{
			return ((IList) this.SchemaNameCollection).Contains(value);
		}

		public virtual bool Contains(string value)
		{
			return this.SchemaNameCollection.Contains(value);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection) this.SchemaNameCollection).CopyTo(array, index);
		}

		public virtual void CopyTo(string[] stringArray, int index)
		{
			this.SchemaNameCollection.CopyTo(stringArray, index);
		}

		public static SchemaNameCollectionWrapper FromSchemaNameCollection(SchemaNameCollection schemaNameCollection)
		{
			return schemaNameCollection;
		}

		public virtual IEnumerator GetEnumerator()
		{
			return this.SchemaNameCollection.GetEnumerator();
		}

		int IList.IndexOf(object value)
		{
			return ((IList) this.SchemaNameCollection).IndexOf(value);
		}

		public virtual int IndexOf(string value)
		{
			return this.SchemaNameCollection.IndexOf(value);
		}

		void IList.Insert(int index, object value)
		{
			((IList) this.SchemaNameCollection).Insert(index, value);
		}

		public virtual void Insert(int index, string value)
		{
			this.SchemaNameCollection.Insert(index, value);
		}

		void IList.Remove(object value)
		{
			((IList) this.SchemaNameCollection).Remove(value);
		}

		public virtual void Remove(string value)
		{
			this.SchemaNameCollection.Remove(value);
		}

		public virtual void RemoveAt(int index)
		{
			this.SchemaNameCollection.RemoveAt(index);
		}

		#endregion

		#region Implicit operator

		public static implicit operator SchemaNameCollectionWrapper(SchemaNameCollection schemaNameCollection)
		{
			return schemaNameCollection == null ? null : new SchemaNameCollectionWrapper(schemaNameCollection);
		}

		#endregion
	}
}