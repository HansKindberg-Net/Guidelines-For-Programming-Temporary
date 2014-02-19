using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace Company.Net.Mail
{
	[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
	public class SmtpClientWrapper : ISmtpClient
	{
		#region Fields

		private readonly SmtpClient _smtpClient;

		#endregion

		#region Constructors

		public SmtpClientWrapper(SmtpClient smtpClient)
		{
			if(smtpClient == null)
				throw new ArgumentNullException("smtpClient");

			this._smtpClient = smtpClient;
		}

		#endregion

		#region Properties

		protected internal virtual SmtpClient SmtpClient
		{
			get { return this._smtpClient; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "This is a wrapper.")]
		[SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "This is a wrapper.")]
		public virtual void Dispose()
		{
			this.SmtpClient.Dispose();
		}

		public virtual void Send(MailMessage mailMessage)
		{
			this.SmtpClient.Send(mailMessage);
		}

		public virtual void Send(string from, string recipients, string subject, string body)
		{
			this.SmtpClient.Send(from, recipients, subject, body);
		}

		#endregion
	}
}