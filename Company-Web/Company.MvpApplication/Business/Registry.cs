using Company.Collections.Generic;
using Company.MvpApplication.Business.Mvp.Models;
using Company.Web;

namespace Company.MvpApplication.Business
{
	public abstract class Registry : StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry()
		{
			Company.IoC.StructureMap.Web.Registry.Register(this);

			this.For<IModelFactory>().Singleton().Use<ModelFactory>();
			this.For<ITreeFactory<ISiteMapNode>>().Singleton().Use<TreeFactory<ISiteMapNode>>();
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