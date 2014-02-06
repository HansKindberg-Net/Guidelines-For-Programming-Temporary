using Company.IoC;
using StructureMap;

namespace Company.WebApplication.Business
{
	public class Bootstrapper : IoC.StructureMap.Bootstrapper
	{
		#region Fields

		private static bool _hasStarted;

		#endregion

		#region Methods

		public static void Bootstrap()
		{
			new Bootstrapper().BootstrapStructureMap();
		}

		public override void BootstrapStructureMap()
		{
			base.BootstrapStructureMap();

			ServiceLocator.Instance = new IoC.StructureMap.ServiceLocator(ObjectFactory.Container);
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