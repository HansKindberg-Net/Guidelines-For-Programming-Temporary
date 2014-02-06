namespace Company.WebApplication.Business
{
	public abstract class Registry : StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry()
		{
			IoC.StructureMap.Data.Registry.Register(this);
			IoC.StructureMap.Web.Registry.Register(this);
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