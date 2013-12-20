using System.Web.Mvc;
using Company.MvcApplication.Business.Web.Mvc.Models;
using DependencyResolver = Company.MvcApplication.Business.Web.Mvc.DependencyResolver;

namespace Company.MvcApplication.Business
{
	public abstract class Registry : StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry()
		{
			Company.IoC.StructureMap.Web.Registry.Register(this);

			this.For<IDependencyResolver>().Use<DependencyResolver>();
			this.For<IModelFactory>().Singleton().Use<ModelFactory>();
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

	public class TestRegistry : ProductionRegistry
	{
		#region Constructors

		public TestRegistry() {}

		#endregion
	}
}