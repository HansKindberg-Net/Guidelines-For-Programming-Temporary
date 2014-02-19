using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace Company.Net.Mail
{
	public class DefaultSmtpClientFactory : ISmtpClientFactory
	{
		#region Methods

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "ISmtpClient should be disposed by the caller and SmtpClient.Dispose will be called through the wrapper.")]
		public virtual ISmtpClient Create()
		{
			return new SmtpClientWrapper(new SmtpClient());
		}

		#endregion
	}
}