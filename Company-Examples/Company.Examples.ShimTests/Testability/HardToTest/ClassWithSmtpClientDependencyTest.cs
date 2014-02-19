using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Net.Mail.Fakes;
using Company.Examples.Testability.HardToTest;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.ShimTests.Testability.HardToTest
{
	[TestClass]
	public class ClassWithSmtpClientDependencyTest
	{
		#region Methods

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Company.Examples.Testability.HardToTest.ClassWithSmtpClientDependency.Send(System.String,System.String,System.String)")]
		public void Send_ShouldCallSmtpClientSend()
		{
			const string testMessage = "Test message";
			const string testReceiver = "someone@company.net";
			const string testSubject = "Test subject";

			using(ShimsContext.Create())
			{
				bool sendIsCalled = false;
				MailMessage sentMailMessage = null;

				ShimSmtpClient.AllInstances.SendMailMessage = delegate(SmtpClient client, MailMessage mailMessage)
				{
					sentMailMessage = mailMessage;
					sendIsCalled = true;
				};

				Assert.IsFalse(sendIsCalled);
				Assert.IsNull(sentMailMessage);

				new ClassWithSmtpClientDependency().Send(testReceiver, testSubject, testMessage);

				Assert.IsTrue(sendIsCalled);
				Assert.IsNotNull(sentMailMessage);
				Assert.AreEqual(testMessage, sentMailMessage.Body);
				Assert.AreEqual(testReceiver, sentMailMessage.To.First().Address);
				Assert.AreEqual(testSubject, sentMailMessage.Subject);
			}
		}

		#endregion
	}
}