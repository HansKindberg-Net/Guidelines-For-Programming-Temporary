using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace Company.Examples.Testability.HardToTest
{
	public class ClassWithSmtpClientDependency
	{
		#region Methods

		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public void Send(string to, string subject, string message)
		{
			using(MailMessage mailMessage = new MailMessage("noreply@company.net", to))
			{
				mailMessage.Body = message;
				mailMessage.Subject = subject;

				using(SmtpClient smtpClient = new SmtpClient())
				{
					smtpClient.Send(mailMessage);
				}
			}
		}

		#endregion
	}
}