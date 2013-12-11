using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Net.Mail.Fakes;
using Company.Examples.HardToTest;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.ShimTests.HardToTest
{
	[TestClass]
	public class EmailFormTest
	{
		#region Fields

		private const string _testMessage = "Test message";
		private const string _testReceiver = "someone@company.net";
		private const string _testSubject = "Test subject";

		#endregion

		#region Methods

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Company.Examples.HardToTest.EmailForm.set_Message(System.String)")]
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		public void Send_IfTheInputIsNotValid_ShouldNotCallSmtpClientSendAndShouldThrowAnException()
		{
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

				EmailForm emailForm = new EmailForm
					{
						Message = _testMessage,
						Subject = _testSubject,
						To = null
					};

				Exception expectedException = null;

				try
				{
					emailForm.Send();
				}
				catch(Exception exception)
				{
					expectedException = exception;
				}

				Assert.IsFalse(sendIsCalled);
				Assert.IsNull(sentMailMessage);

				if(expectedException != null)
					throw new Exception();
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Company.Examples.HardToTest.EmailForm.set_Message(System.String)")]
		public void Send_IfTheInputIsValid_ShouldCallSmtpClientSend()
		{
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

		#endregion
	}
}