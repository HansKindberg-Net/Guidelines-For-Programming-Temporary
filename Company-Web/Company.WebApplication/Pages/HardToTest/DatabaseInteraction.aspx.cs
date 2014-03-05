using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using Company.Data.Entities;
using Company.Validation;
using Company.WebApplication.Business.Data.Databases;
using Company.WebApplication.Business.Web;
using Company.WebApplication.Business.Web.UI;

namespace Company.WebApplication.Pages.HardToTest
{
	public partial class DatabaseInteraction : Page
	{
		#region Fields

		private HttpVerb? _action;
		private const string _actionParameterName = "Action";

		#endregion

		#region Properties

		protected internal virtual HttpVerb Action
		{
			get
			{
				if(!this._action.HasValue)
				{
					this._action = HttpVerb.Get;

					string actionString = this.Request.QueryString[this.ActionParameterName];

					if(!string.IsNullOrEmpty(actionString))
					{
						HttpVerb action;
						if(Enum.TryParse(actionString, true, out action))
							this._action = action;
					}
				}

				return this._action.Value;
			}
		}

		protected internal virtual string ActionParameterName
		{
			get { return _actionParameterName; }
		}

		public virtual bool AddNewItem
		{
			get { return this.Action == HttpVerb.Post; }
		}

		public virtual Uri AddNewItemUrl
		{
			get { return this.CreateRelativeUrl(HttpVerb.Post); }
		}

		public virtual bool DeleteItem
		{
			get { return this.Action == HttpVerb.Delete; }
		}

		public virtual bool EditItem
		{
			get { return this.Action == HttpVerb.Put; }
		}

		public virtual Uri EditItemUrl
		{
			get { return this.CreateRelativeUrl(HttpVerb.Put); }
		}

		protected internal virtual IEnumerable<IExampleItem> ExampleItems { get; set; }

		public virtual bool Search
		{
			get { return this.Action == HttpVerb.Get; }
		}

		#endregion

		#region Methods

		protected internal virtual Uri CreateRelativeUrl(HttpVerb action)
		{
			UriBuilder uriBuilder = new UriBuilder(this.Request.Url);

			NameValueCollection queryString = HttpUtility.ParseQueryString(uriBuilder.Query);

			queryString.Set(this.ActionParameterName, action.ToString());

			uriBuilder.Query = queryString.ToString();

			return new Uri(uriBuilder.Uri.PathAndQuery, UriKind.Relative);
		}

		//#region Fields
		//private static readonly EmailAddressValidator _emailAddressValidator = new EmailAddressValidator();
		//#endregion
		//#region Properties
		//protected internal virtual EmailAddressValidator EmailAddressValidator
		//{
		//	get { return _emailAddressValidator; }
		//}
		//#endregion
		//#region Methods
		//protected internal virtual void AddInvalidEmailException(IValidationResult validationResult, string fieldName, string value)
		//{
		//	this.AddValidationException(validationResult, new FormatException(string.Format(CultureInfo.InvariantCulture, "\"{0}\" has an invalid value. \"{1}\" is not a valid email-address.", fieldName, value)));
		//}
		//protected internal virtual void AddRequiredValueException(IValidationResult validationResult, string fieldName)
		//{
		//	this.AddValidationException(validationResult, new FormatException(string.Format(CultureInfo.InvariantCulture, "\"{0}\" can not be empty.", fieldName)));
		//}
		protected internal virtual IValidationResult ValidateSearchInput()
		{
			IValidationResult validationResult = new ValidationResult();

			if(!this.ValidateDangerousInput(this.KeyCriteriaTextBox.Text))
				this.AddDangerousInputException(validationResult, "Key");

			if(!this.ValidateDangerousInput(this.ValueCriteriaTextBox.Text))
				this.AddDangerousInputException(validationResult, "Value");

			return validationResult;
		}

		#endregion

		#region Eventhandlers

		protected override void OnPreRender(EventArgs e)
		{
			this.ExampleItemRepeater.DataBind();

			base.OnPreRender(e);
		}

		//[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected internal virtual void OnSaveClick(object sender, EventArgs e)
		{
			this.ConfirmationControl.Visible = false;
			this.ExceptionControl.Visible = false;

			//var validationResult = this.ValidateInput();

			//if (!validationResult.IsValid)
			//{
			//	this.ExceptionControl.InformationList = validationResult.Exceptions.Select(exception => exception.Message);
			//	this.ExceptionControl.Visible = true;
			//	return;
			//}

			//try
			//{
			//	using (MailMessage mailMessage = new MailMessage("noreply@company.net", this.ToTextBox.Text))
			//	{
			//		mailMessage.Body = this.MessageTextBox.Text;

			//		if (!string.IsNullOrEmpty(this.CopyTextBox.Text))
			//			mailMessage.CC.Add(this.CopyTextBox.Text);

			//		mailMessage.Subject = this.SubjectTextBox.Text;

			//		using (SmtpClient smtpClient = new SmtpClient())
			//		{
			//			if (smtpClient.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory && !Directory.Exists(smtpClient.PickupDirectoryLocation))
			//				smtpClient.PickupDirectoryLocation = Path.Combine(this.MapPath("~"), smtpClient.PickupDirectoryLocation ?? string.Empty);

			//			smtpClient.Send(mailMessage);
			//		}
			//	}

			//	this.ConfirmationControl.Visible = true;
			//}
			//catch (Exception exception)
			//{
			//	this.ExceptionControl.Information = exception.Message;
			//}
		}

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected internal virtual void OnSearchClick(object sender, EventArgs e)
		{
			this.ConfirmationControl.Visible = false;
			this.ExceptionControl.Visible = false;

			var validationResult = this.ValidateSearchInput();

			if(!validationResult.IsValid)
			{
				this.ExceptionControl.InformationList = validationResult.Exceptions.Select(exception => exception.Message);
				this.ExceptionControl.Visible = true;
				return;
			}

			try
			{
				this.ExampleItems = StaticExampleDatabase.Find(new ExampleItem {Key = this.KeyCriteriaTextBox.Text, Value = this.ValueCriteriaTextBox.Text});
			}
			catch(Exception exception)
			{
				this.ExceptionControl.Information = exception.Message;
				this.ExceptionControl.Visible = true;
			}
		}

		#endregion

		//protected internal virtual IValidationResult ValidateSaveInput()
		//{
		//	IValidationResult validationResult = new ValidationResult();
		//	if (string.IsNullOrEmpty(this.ToTextBox.Text))
		//		this.AddRequiredValueException(validationResult, "To");
		//	else if (!this.EmailAddressValidator.IsValidEmailAddress(this.ToTextBox.Text))
		//		this.AddInvalidEmailException(validationResult, "To", this.ToTextBox.Text);
		//	if (!string.IsNullOrEmpty(this.CopyTextBox.Text) && !this.EmailAddressValidator.IsValidEmailAddress(this.CopyTextBox.Text))
		//		this.AddInvalidEmailException(validationResult, "Copy", this.CopyTextBox.Text);
		//	if (!this.ValidateDangerousInput(this.SubjectTextBox.Text))
		//		this.AddDangerousInputException(validationResult, "Subject");
		//	if (!this.ValidateDangerousInput(this.MessageTextBox.Text))
		//		this.AddDangerousInputException(validationResult, "Message");
		//	return validationResult;
		//}
		//#endregion
	}
}