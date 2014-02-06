using System.Data.Common;

namespace Company.Data.Common
{
	public class DbProviderFactoriesWrapper : IDatabaseProviderFactoryRepository
	{
		#region Methods

		public virtual DbProviderFactory GetFactory(string providerInvariantName)
		{
			return DbProviderFactories.GetFactory(providerInvariantName);
		}

		#endregion
	}
}