using Company.IoC;
using StructureMap;
using WebFormsMvp.Binder;
using StructureMapPresenterFactory = Company.IoC.StructureMap.Web.Mvp.Binder.PresenterFactory;
using StructureMapServiceLocator = Company.IoC.StructureMap.ServiceLocator;

namespace Company.MvpApplication.Business
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

			PresenterBinder.Factory = new StructureMapPresenterFactory(container);
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