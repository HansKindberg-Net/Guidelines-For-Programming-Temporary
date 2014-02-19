using System;
using System.Net.Mail;

namespace Company.Net.Mail
{
	public interface ISmtpClient : IDisposable
	{
		#region Methods

		void Send(MailMessage mailMessage);
		void Send(string from, string recipients, string subject, string body);

		#endregion
	}
}