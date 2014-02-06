using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using Company.Net.Mail;
using Company.Validation;
using Company.WebApplication.Business.Web.UI;

namespace Company.WebApplication.Pages.HardToTest
{
	public partial class EmailForm : Page
	{
		#region Fields

		private static readonly EmailAddressValidator _emailAddressValidator = new EmailAddressValidator();

		#endregion

		#region Properties

		protected internal virtual EmailAddressValidator EmailAddressValidator
		{
			get { return _emailAddressValidator; }
		}

		#endregion

		#region Methods

		protected internal virtual void AddInvalidEmailException(IValidationResult validationResult, string fieldName, string value)
		{
			this.AddValidationException(validationResult, new FormatException(string.Format(CultureInfo.InvariantCulture, "\"{0}\" has an invalid value. \"{1}\" is not a valid email-address.", fieldName, value)));
		}

		protected internal virtual void AddRequiredValueException(IValidationResult validationResult, string fieldName)
		{
			this.AddValidationException(validationResult, new FormatException(string.Format(CultureInfo.InvariantCulture, "\"{0}\" can not be empty.", fieldName)));
		}

		protected internal virtual IValidationResult ValidateInput()
		{
			IValidationResult validationResult = new ValidationResult();

			if(string.IsNullOrEmpty(this.ToTextBox.Text))
				this.AddRequiredValueException(validationResult, "To");
			else if(!this.EmailAddressValidator.IsValidEmailAddress(this.ToTextBox.Text))
				this.AddInvalidEmailException(validationResult, "To", this.ToTextBox.Text);

			if(!string.IsNullOrEmpty(this.CopyTextBox.Text) && !this.EmailAddressValidator.IsValidEmailAddress(this.CopyTextBox.Text))
				this.AddInvalidEmailException(validationResult, "Copy", this.CopyTextBox.Text);

			if(!this.ValidateDangerousInput(this.SubjectTextBox.Text))
				this.AddDangerousInputException(validationResult, "Subject");

			if(!this.ValidateDangerousInput(this.MessageTextBox.Text))
				this.AddDangerousInputException(validationResult, "Message");

			return validationResult;
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		protected internal virtual void OnSendClick(object sender, EventArgs e)
		{
			this.ConfirmationControl.Visible = false;
			this.ExceptionControl.Visible = false;

			var validationResult = this.ValidateInput();

			if(!validationResult.IsValid)
			{
				this.ExceptionControl.InformationList = validationResult.Exceptions.Select(exception => exception.Message);
				this.ExceptionControl.Visible = true;
				return;
			}

			try
			{
				using(MailMessage mailMessage = new MailMessage("noreply@company.net", this.ToTextBox.Text))
				{
					mailMessage.Body = this.MessageTextBox.Text;

					if(!string.IsNullOrEmpty(this.CopyTextBox.Text))
						mailMessage.CC.Add(this.CopyTextBox.Text);

					mailMessage.Subject = this.SubjectTextBox.Text;

					using(SmtpClient smtpClient = new SmtpClient())
					{
						if(smtpClient.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory && !Directory.Exists(smtpClient.PickupDirectoryLocation))
							smtpClient.PickupDirectoryLocation = Path.Combine(this.MapPath("~"), smtpClient.PickupDirectoryLocation ?? string.Empty);

						smtpClient.Send(mailMessage);
					}
				}

				this.ConfirmationControl.Visible = true;
			}
			catch(Exception exception)
			{
				this.ExceptionControl.Information = exception.Message;
			}
		}

		#endregion
	}
}