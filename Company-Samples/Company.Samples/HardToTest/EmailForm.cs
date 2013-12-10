using System;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using Company.Net.Mail;
using Company.Validation;

namespace Company.Samples.HardToTest
{
	public class EmailForm
	{
		#region Properties

		public string Message { get; set; }
		public string Subject { get; set; }
		public string To { get; set; }

		#endregion

		#region Methods

		public void Send()
		{
			IValidationResult validationResult = this.ValidateInput();

			if(!validationResult.IsValid)
				throw validationResult.Exceptions.First();

			using(MailMessage mailMessage = new MailMessage("noreply@company.net", this.To))
			{
				mailMessage.Body = this.Message;
				mailMessage.Subject = this.Subject;

				using(SmtpClient smtpClient = new SmtpClient())
				{
					smtpClient.Send(mailMessage);
				}
			}
		}

		private IValidationResult ValidateInput()
		{
			IValidationResult validationResult = new ValidationResult();

			if(string.IsNullOrEmpty(this.To))
				validationResult.Exceptions.Add(new InvalidOperationException("\"To\" can not be null or empty."));
			else if(!new EmailAddressValidator().IsValidEmailAddress(this.To))
				validationResult.Exceptions.Add(new FormatException(string.Format(CultureInfo.InvariantCulture, "\"To\" has an invalid value. The value \"{0}\" is not a valid email-address.", this.To)));

			if(string.IsNullOrEmpty(this.Subject))
				validationResult.Exceptions.Add(new InvalidOperationException("\"Subject\" can not be null or empty."));

			if(string.IsNullOrEmpty(this.Message))
				validationResult.Exceptions.Add(new InvalidOperationException("\"Message\" can not be null or empty."));

			return new ValidationResult();
		}

		#endregion
	}
}