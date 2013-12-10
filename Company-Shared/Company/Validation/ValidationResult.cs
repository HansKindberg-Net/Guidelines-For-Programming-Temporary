using System;
using System.Collections.Generic;
using System.Linq;

namespace Company.Validation
{
	public class ValidationResult : IValidationResult
	{
		#region Fields

		private readonly List<Exception> _exceptions;

		#endregion

		#region Constructors

		public ValidationResult()
		{
			this._exceptions = new List<Exception>();
		}

		#endregion

		#region Properties

		public virtual IList<Exception> Exceptions
		{
			get { return this._exceptions; }
		}

		public virtual bool IsValid
		{
			get { return !this.Exceptions.Any(); }
		}

		#endregion

		#region Methods

		public virtual void AddExceptions(IEnumerable<Exception> exceptions)
		{
			if(exceptions == null)
				throw new ArgumentNullException("exceptions");

			List<Exception> exceptionList = exceptions.ToList();

			if(exceptionList.Contains(null))
				throw new ArgumentException("The exception-collection can not contain null values", "exceptions");

			foreach(Exception exception in exceptionList)
			{
				this.Exceptions.Add(exception);
			}
		}

		#endregion
	}
}