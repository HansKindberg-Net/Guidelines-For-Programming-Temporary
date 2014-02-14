using System;
using System.Globalization;
using System.Web.Util;
using Company.Validation;
using Company.Web;
using DefaultMaster = Company.WebApplication.MasterPages.Default;

namespace Company.WebApplication.Business.Web.UI
{
	public abstract class Page : System.Web.UI.Page
	{
		#region Fields

		private const string _defaultMasterExceptionMessageTemplate = "The page must use a master-page of type \"{0}\".";

		#endregion

		#region Properties

		protected internal virtual DefaultMaster DefaultMaster
		{
			get
			{
				try
				{
					return (DefaultMaster) this.Master;
				}
				catch(Exception exception)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, _defaultMasterExceptionMessageTemplate, typeof(DefaultMaster)), exception);
				}
			}
		}

		protected internal virtual RequestValidator RequestValidator
		{
			get { return RequestValidator.Current; }
		}

		protected internal virtual ISystemInformation SystemInformation
		{
			get { return this.DefaultMaster.SystemInformation; }
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