using System.Collections.Generic;
using System.Configuration;
using Company.Data.Common;
using Company.Data.Entities;
using Company.IoC;

namespace Company.WebApplication.Business.Data.Databases
{
	public static class ExampleDatabase
	{
		#region Fields

		private static readonly Company.Data.Databases.ExampleDatabase _exampleDatabaseInstance = new Company.Data.Databases.ExampleDatabase(ServiceLocator.Instance.GetService<IDatabaseProviderFactoryRepository>(), ConfigurationManager.ConnectionStrings["Example"]);

		#endregion

		#region Methods

		public static IEnumerable<IExampleItem> List()
		{
			return _exampleDatabaseInstance.Find(null);
		}

		#endregion
	}
}