using System;

namespace Company.Data.Entities
{
	public class ExampleItem : IExampleItem
	{
		#region Fields

		private readonly int? _id;
		private string _key;
		private string _value;

		#endregion

		#region Constructors

		public ExampleItem() {}

		public ExampleItem(int id, string key, string value)
		{
			if(id < 1)
				throw new ArgumentException("The id can not be less than 1.", "id");

			this._id = id;
			this._key = key;
			this._value = value;
		}

		#endregion

		#region Properties

		public virtual int? Id
		{
			get { return this._id; }
		}

		public virtual string Key
		{
			get { return this._key; }
			set { this._key = value; }
		}

		public virtual bool New
		{
			get { return !this._id.HasValue; }
		}

		public virtual string Value
		{
			get { return this._value; }
			set { this._value = value; }
		}

		#endregion
	}
}