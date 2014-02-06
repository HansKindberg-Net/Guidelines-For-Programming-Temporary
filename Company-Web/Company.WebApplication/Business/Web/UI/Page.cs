using System;
using System.Globalization;
using System.Web.Util;
using Company.Validation;

namespace Company.WebApplication.Business.Web.UI
{
	public abstract class Page : System.Web.UI.Page
	{
		#region Properties

		protected internal virtual RequestValidator RequestValidator
		{
			get { return RequestValidator.Current; }
		}

		#endregion

		#region Methods

		protected internal virtual void AddDangerousInputException(IValidationResult validationResult, string fieldName)
		{
			this.AddValidationException(validationResult, new FormatException(string.Format(CultureInfo.InvariantCulture, "\"{0}\" contains dangerous input. The characters \"<\" and \">\" are not allowed.", fieldName)));
		}

		protected internal virtual void AddValidationException(IValidationResult validationResult, Exception exception)
		{
			if(validationResult == null)
				throw new ArgumentNullException("validationResult");

			validationResult.Exceptions.Add(exception);
		}

		protected internal virtual bool ValidateDangerousInput(string value)
		{
			int validationFailureIndex;

			return this.RequestValidator.InvokeIsValidRequestString(null, value, RequestValidationSource.Form, null, out validationFailureIndex);
		}

		#endregion
	}
}