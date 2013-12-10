using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Net.Mail.Fakes;
using System.Text;
using System.Threading.Tasks;
using Company.Samples.HardToTest;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Samples.ShimTests.HardToTest
{
	[TestClass]
	public class EmailFormTest
	{
		private const string _testMessage = "Test message";
		private const string _testSubject = "Test subject";
		private const string _testReceiver = "someone@company.net";

		//[TestMethod]
		//public void Send_IfTheInputIsNotValid_SmtpClientSendShouldNotBeCalledAndShouldThrowAnException()
		//{
		//	EmailForm emailForm = new EmailForm
		//		{
		//			Message = _testMessage,
		//			Subject = _testSubject,
		//			To = _testReceiver
		//		};
		//}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Company.Samples.HardToTest.EmailForm.set_Message(System.String)")]
		public void Send_IfTheInputIsValid_SmtpClientSendShouldBeCalled()
		{
			using (ShimsContext.Create())
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

				EmailForm emailForm = new EmailForm
				{
					Message = _testMessage,
					Subject = _testSubject,
					To = _testReceiver
				};

				emailForm.Send();
				
				Assert.IsTrue(sendIsCalled);
				Assert.IsNotNull(sentMailMessage);
				Assert.AreEqual(_testMessage, sentMailMessage.Body);
				Assert.AreEqual(_testReceiver, sentMailMessage.To.First().Address);
				Assert.AreEqual(_testSubject, sentMailMessage.Subject);
			}
		}
	}
}