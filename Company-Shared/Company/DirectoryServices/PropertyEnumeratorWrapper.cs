using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;

namespace Company.DirectoryServices
{
	[SuppressMessage("Microsoft.Design", "CA1038:EnumeratorsShouldBeStronglyTyped", Justification = "This is a wrapper.")]
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class PropertyEnumeratorWrapper : IDictionaryEnumerator, IDisposable
	{
		#region Fields

		private readonly IDictionaryEnumerator _propertyEnumerator;

		#endregion

		#region Constructors

		public PropertyEnumeratorWrapper(IDictionaryEnumerator propertyEnumerator)
		{
			if(propertyEnumerator == null)
				throw new ArgumentNullException("propertyEnumerator");

			this._propertyEnumerator = propertyEnumerator;
		}

		#endregion

		#region Properties

		public virtual object Current
		{
			get { return this.Entry.Value; }
		}

		public virtual DictionaryEntry Entry
		{
			get { return new DictionaryEntry(this.PropertyEnumerator.Entry.Key, (PropertyValueCollectionWrapper) (PropertyValueCollection) this.PropertyEnumerator.Entry.Value); }
		}

		public virtual object Key
		{
			get { return this.Entry.Key; }
		}

		protected internal virtual IDictionaryEnumerator PropertyEnumerator
		{
			get { return this._propertyEnumerator; }
		}

		public virtual object Value
		{
			get { return this.Entry.Value; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			IDisposable disposable = this.PropertyEnumerator as IDisposable;

			if(disposable != null)
				disposable.Dispose();
		}

		public virtual bool MoveNext()
		{
			return this.PropertyEnumerator.MoveNext();
		}

		public virtual void Reset()
		{
			this.PropertyEnumerator.Reset();
		}

		#endregion
	}
}