using System.Diagnostics.CodeAnalysis;
using Company.Examples.Testability.Testable;
using Company.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Company.Examples.UnitTests.Testability.Testable
{
	[TestClass]
	public class ClassWithSmtpClientDependencyMadeTestableSecondAlternativeTest
	{
		#region Methods

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Company.Examples.Testability.Testable.ClassWithSmtpClientDependencyMadeTestableSecondAlternative.Send(System.String,System.String,System.String)")]
		public void Send_ShouldCallSendOnTheSmtpClientCreatedByTheFactory()
		{
			const string from = "noreply@company.net";
			const string message = "Test message";
			const string subject = "Test subject";
			const string to = "nobody@company.net";

			var smtpClientMock = new Mock<ISmtpClient>();
			smtpClientMock.Setup(smtpClient => smtpClient.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback((string fromArgument, string recipientsArgument, string subjectArgument, string bodyArgument) =>
			{
				Assert.AreEqual(from, fromArgument);
				Assert.AreEqual(message, bodyArgument);
				Assert.AreEqual(subject, subjectArgument);
				Assert.AreEqual(to, recipientsArgument);
			});

			var smtpClientFactoryMock = new Mock<ISmtpClientFactory>();
			smtpClientFactoryMock.Setup(smtpClientFactory => smtpClientFactory.Create()).Returns(smtpClientMock.Object);

			new ClassWithSmtpClientDependencyMadeTestableSecondAlternative(smtpClientFactoryMock.Object).Send(to, subject, message);
		}

		#endregion
	}
}