using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using Company.Examples.Testability.Testable;
using Company.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Examples.UnitTests.Testability.Testable
{
	[TestClass]
	public class ClassWithSmtpClientDependencyMadeTestableFirstAlternativeTest
	{
		#region Methods

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Company.Examples.Testability.Testable.ClassWithSmtpClientDependencyMadeTestableFirstAlternative.Send(System.String,System.String,System.String)")]
		public void Send_ShouldCallSendOnTheSmtpClientPassedInTheConstructor()
		{
			const string from = "noreply@company.net";
			const string message = "Test message";
			const string subject = "Test subject";
			const string to = "nobody@company.net";

			var smtpClientMock = new Mock<ISmtpClient>();
			smtpClientMock.Setup(smtpClient => smtpClient.Send(It.IsAny<MailMessage>())).Callback((MailMessage mailMessage) =>
			{
				Assert.AreEqual(from, mailMessage.From.Address);
				Assert.AreEqual(message, mailMessage.Body);
				Assert.AreEqual(subject, mailMessage.Subject);
				Assert.AreEqual(to, mailMessage.To.First().Address);
			});

			new ClassWithSmtpClientDependencyMadeTestableFirstAlternative(smtpClientMock.Object).Send(to, subject, message);
		}

		#endregion
	}
}