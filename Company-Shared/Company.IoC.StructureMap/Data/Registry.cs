using System;
using Company.Data.Common;
using StructureMap.Configuration.DSL;

namespace Company.IoC.StructureMap.Data
{
	[CLSCompliant(false)]
	public abstract class Registry : global::StructureMap.Configuration.DSL.Registry
	{
		#region Constructors

		protected Registry()
		{
			Register(this);
		}

		#endregion

		#region Methods

		public static void Register(IRegistry registry)
		{
			if(registry == null)
				throw new ArgumentNullException("registry");

			registry.For<IDatabaseProviderFactoryRepository>().Singleton().Use<DbProviderFactoriesWrapper>();
		}

		#endregion
	}
}