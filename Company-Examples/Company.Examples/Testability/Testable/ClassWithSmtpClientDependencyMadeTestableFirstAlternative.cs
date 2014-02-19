using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using Company.Net.Mail;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithSmtpClientDependencyMadeTestableFirstAlternative
	{
		#region Fields

		private readonly ISmtpClient _smtpClient;

		#endregion

		#region Constructors

		public ClassWithSmtpClientDependencyMadeTestableFirstAlternative(ISmtpClient smtpClient)
		{
			if(smtpClient == null)
				throw new ArgumentNullException("smtpClient");

			this._smtpClient = smtpClient;
		}

		#endregion

		#region Properties

		protected internal virtual ISmtpClient SmtpClient
		{
			get { return this._smtpClient; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "To")]
		public virtual void Send(string to, string subject, string message)
		{
			using(MailMessage mailMessage = new MailMessage("noreply@company.net", to))
			{
				mailMessage.Body = message;
				mailMessage.Subject = subject;

				this.SmtpClient.Send(mailMessage);
			}
		}

		#endregion
	}
}