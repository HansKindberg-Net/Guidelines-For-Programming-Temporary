using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace Company.IoC.StructureMap
{
	[CLSCompliant(false)]
	public class ServiceLocator : IServiceLocator
	{
		#region Fields

		private readonly IContainer _container;

		#endregion

		#region Constructors

		public ServiceLocator(IContainer container)
		{
			if(container == null)
				throw new ArgumentNullException("container");

			this._container = container;
		}

		#endregion

		#region Properties

		protected internal virtual IContainer Container
		{
			get { return this._container; }
		}

		#endregion

		#region Methods

		public virtual T GetService<T>()
		{
			return this.Container.GetInstance<T>();
		}

		public virtual T GetService<T>(string key)
		{
			return this.Container.GetInstance<T>(key);
		}

		public virtual object GetService(Type serviceType)
		{
			return this.Container.GetInstance(serviceType);
		}

		public virtual object GetService(Type serviceType, string key)
		{
			return this.Container.GetInstance(serviceType, key);
		}

		public virtual IEnumerable<T> GetServices<T>()
		{
			return this.Container.GetAllInstances<T>();
		}

		public virtual IEnumerable<object> GetServices(Type serviceType)
		{
			return this.Container.GetAllInstances(serviceType).Cast<object>();
		}

		#endregion
	}
}