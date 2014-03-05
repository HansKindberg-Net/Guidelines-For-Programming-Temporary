using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using System.Linq;
using Company.DirectoryServices.Extensions;

namespace Company.DirectoryServices
{
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a wrapper.")]
	public class PropertyCollectionWrapper : IPropertyDictionary
	{
		#region Fields

		private readonly PropertyCollection _propertyCollection;

		#endregion

		#region Constructors

		public PropertyCollectionWrapper(PropertyCollection propertyCollection)
		{
			if(propertyCollection == null)
				throw new ArgumentNullException("propertyCollection");

			this._propertyCollection = propertyCollection;
		}

		#endregion

		#region Properties

		public virtual int Count
		{
			get { return this.PropertyCollection.Count; }
		}

		public virtual bool IsFixedSize
		{
			get { return ((IDictionary) this.PropertyCollection).IsFixedSize; }
		}

		public virtual bool IsReadOnly
		{
			get { return ((IDictionary) this.PropertyCollection).IsReadOnly; }
		}

		public virtual bool IsSynchronized
		{
			get { return ((ICollection) this.PropertyCollection).IsSynchronized; }
		}

		public virtual IPropertyValueCollection this[string propertyName]
		{
			get { return (PropertyValueCollectionWrapper) this.PropertyCollection[propertyName]; }
		}

		public virtual object this[object key]
		{
			get { return ((IDictionary) this.PropertyCollection)[key]; }
			set { ((IDictionary) this.PropertyCollection)[key] = value; }
		}

		public virtual ICollection Keys
		{
			get { return ((IDictionary) this.PropertyCollection).Keys; }
		}

		public virtual PropertyCollection PropertyCollection
		{
			get { return this._propertyCollection; }
		}

		public virtual ICollection PropertyNames
		{
			get { return this.PropertyCollection.PropertyNames; }
		}

		public virtual object SyncRoot
		{
			get { return ((ICollection) this.PropertyCollection).SyncRoot; }
		}

		public virtual ICollection Values
		{
			get { return this.PropertyCollection.Values.Cast<PropertyValueCollection>().Select(propertyValueCollection => (PropertyValueCollectionWrapper) propertyValueCollection).ToList(); }
		}

		#endregion

		#region Methods

		public virtual void Add(object key, object value)
		{
			((IDictionary) this.PropertyCollection).Add(key, value);
		}

		public virtual void Clear()
		{
			((IDictionary) this.PropertyCollection).Clear();
		}

		bool IDictionary.Contains(object key)
		{
			return ((IDictionary) this.PropertyCollection).Contains(key);
		}

		public virtual bool Contains(string propertyName)
		{
			return this.PropertyCollection.Contains(propertyName);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection) this.PropertyCollection).CopyTo(array, index);
		}

		public virtual void CopyTo(IPropertyValueCollection[] array, int index)
		{
			this.PropertyCollection.CopyTo(array.Select(this.GetPropertyValueCollection).ToArray(), index);
		}

		public static PropertyCollectionWrapper FromPropertyCollection(PropertyCollection propertyCollection)
		{
			return propertyCollection;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This is a wrapper.")]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new PropertyEnumeratorWrapper(this.PropertyCollection.GetEnumerator());
		}

		public virtual void Remove(object key)
		{
			((IDictionary) this.PropertyCollection).Remove(key);
		}

		#endregion

		#region Implicit operator

		public static implicit operator PropertyCollectionWrapper(PropertyCollection propertyCollection)
		{
			return propertyCollection == null ? null : new PropertyCollectionWrapper(propertyCollection);
		}

		#endregion
	}
}