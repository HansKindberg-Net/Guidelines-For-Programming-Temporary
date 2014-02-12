using System.Configuration;
using Company.Data.Databases;

namespace Company.WebApplication.Business
{
	public abstract class Registry : StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry()
		{
			IoC.StructureMap.Data.Registry.Register(this);
			IoC.StructureMap.Web.Registry.Register(this);

			this.For<ExampleDatabase>().Singleton().Use<ExampleDatabase>().Ctor<ConnectionStringSettings>("connectionStringSettings").Is(ConfigurationManager.ConnectionStrings["Example"]);
		}

		#endregion
	}

	public class DevelopmentRegistry : Registry
	{
		#region Constructors

		public DevelopmentRegistry() {}

		#endregion
	}

	public class ProductionRegistry : Registry
	{
		#region Constructors

		public ProductionRegistry() {}

		#endregion
	}

	public class TestRegistry : Registry
	{
		#region Constructors

		public TestRegistry() {}

		#endregion
	}
}