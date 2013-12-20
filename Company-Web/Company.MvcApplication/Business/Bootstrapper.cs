using System.Web.Mvc;
using Company.IoC;
using StructureMap;
using StructureMapServiceLocator = Company.IoC.StructureMap.ServiceLocator;

namespace Company.MvcApplication.Business
{
	public class Bootstrapper : IBootstrapper
	{
		#region Fields

		private static bool _hasStarted;

		#endregion

		#region Methods

		public static void Bootstrap()
		{
			new Bootstrapper().BootstrapStructureMap();

			IContainer container = ObjectFactory.Container;

			DependencyResolver.SetResolver(container.GetInstance<IDependencyResolver>());
			ServiceLocator.Instance = new StructureMapServiceLocator(container);
		}

		public void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(initializer => { initializer.PullConfigurationFromAppConfig = true; });
		}

		public static void Restart()
		{
			if(_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap();
				_hasStarted = true;
			}
		}

		#endregion
	}
}