using System;
using System.Collections.Generic;

namespace Company.Validation
{
	public interface IValidationResult
	{
		#region Properties

		IList<Exception> Exceptions { get; }
		bool IsValid { get; }

		#endregion

		#region Methods

		void AddExceptions(IEnumerable<Exception> exceptions);

		#endregion
	}
}