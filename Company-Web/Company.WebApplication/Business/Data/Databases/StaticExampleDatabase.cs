using System.Collections.Generic;
using Company.Data.Databases;
using Company.Data.Entities;
using Company.IoC;

namespace Company.WebApplication.Business.Data.Databases
{
	public static class StaticExampleDatabase
	{
		#region Fields

		private static readonly ExampleDatabase _exampleDatabaseInstance = ServiceLocator.Instance.GetService<ExampleDatabase>();

		#endregion

		#region Methods

		public static bool Delete(int id)
		{
			return _exampleDatabaseInstance.Delete(id);
		}

		public static IEnumerable<IExampleItem> Find(IExampleItem queryFilter)
		{
			return _exampleDatabaseInstance.Find(queryFilter);
		}

		public static void Save(IExampleItem exampleItem)
		{
			_exampleDatabaseInstance.Save(exampleItem);
		}

		#endregion
	}
}