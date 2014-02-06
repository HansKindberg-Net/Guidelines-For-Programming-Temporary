using System.Data.Common;

namespace Company.Data.Common
{
	public interface IDatabaseProviderFactoryRepository
	{
		#region Methods

		DbProviderFactory GetFactory(string providerInvariantName);

		#endregion
	}
}