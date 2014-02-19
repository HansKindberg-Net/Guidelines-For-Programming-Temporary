using System.Diagnostics.CodeAnalysis;
using Company.Examples.Testability.HardToTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.Examples.UnitTests.Testability.HardToTest
{
	[TestClass]
	public class ClassWithSmtpClientDependencyTest
	{
		#region Methods

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Company.Examples.Testability.HardToTest.ClassWithSmtpClientDependency.Send(System.String,System.String,System.String)"), TestMethod]
		public void Send_ShouldCallSendOnSmtpClient()
		{
			// We want to test if the method calls SmtpClient.Send(MailMessage mailMessage) and if the parameters are correctly set on the MailMessage.
			new ClassWithSmtpClientDependency().Send("nobody@company.net", "Test subject", "Test message");
			Assert.Inconclusive("It is impossible to test this because you can not verify if the SmtpClient.Send(MailMessage mailMessage) method of the dependency have been called.");
		}

		#endregion
	}
}