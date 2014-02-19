using System;
using System.Diagnostics.CodeAnalysis;
using Company.Net.Mail;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithSmtpClientDependencyMadeTestableSecondAlternative
	{
		#region Fields

		private readonly ISmtpClientFactory _smtpClientFactory;

		#endregion

		#region Constructors

		public ClassWithSmtpClientDependencyMadeTestableSecondAlternative(ISmtpClientFactory smtpClientFactory)
		{
			if(smtpClientFactory == null)
				throw new ArgumentNullException("smtpClientFactory");

			this._smtpClientFactory = smtpClientFactory;
		}

		#endregion

		#region Properties

		protected internal virtual ISmtpClientFactory SmtpClientFactory
		{
			get { return this._smtpClientFactory; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "To")]
		public virtual void Send(string to, string subject, string message)
		{
			using(ISmtpClient smtpClient = this.SmtpClientFactory.Create())
			{
				smtpClient.Send("noreply@company.net", to, subject, message);
			}
		}

		#endregion
	}
}