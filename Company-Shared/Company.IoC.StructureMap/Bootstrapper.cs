using StructureMap;

namespace Company.IoC.StructureMap
{
	public class Bootstrapper : IBootstrapper
	{
		#region Methods

		public virtual void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(initializer => { initializer.PullConfigurationFromAppConfig = true; });
		}

		#endregion
	}
}