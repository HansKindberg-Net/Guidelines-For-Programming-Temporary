namespace Company.Net.Mail
{
	public interface ISmtpClientFactory
	{
		#region Methods

		ISmtpClient Create();

		#endregion
	}
}