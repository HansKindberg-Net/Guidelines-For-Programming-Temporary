using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace Company.IoC.StructureMap
{
	[CLSCompliant(false)]
	public class DependencyResolver
	{
		#region Fields

		private readonly IContainer _container;

		#endregion

		#region Constructors

		public DependencyResolver(IContainer container)
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

		protected internal virtual object GetAbstractService(Type abstractServiceType)
		{
			if(abstractServiceType == null)
				throw new ArgumentNullException("abstractServiceType");

			if(!abstractServiceType.IsAbstract && !abstractServiceType.IsInterface)
				throw new ArgumentException("The service-type is not abstract.", "abstractServiceType");

			return this.Container.TryGetInstance(abstractServiceType);
		}

		protected internal virtual object GetConcreteService(Type concreteServiceType)
		{
			if(concreteServiceType == null)
				throw new ArgumentNullException("concreteServiceType");

			if(concreteServiceType.IsAbstract || concreteServiceType.IsInterface)
				throw new ArgumentException("The service-type is not concrete.", "concreteServiceType");

			try
			{
				return this.Container.GetInstance(concreteServiceType);
			}
			catch(StructureMapException)
			{
				return null;
			}
		}

		public virtual object GetService(Type serviceType)
		{
			if(serviceType == null)
				throw new ArgumentNullException("serviceType");

			if(serviceType.IsAbstract || serviceType.IsInterface)
				return this.GetAbstractService(serviceType);

			return this.GetConcreteService(serviceType);
		}

		public virtual IEnumerable<object> GetServices(Type serviceType)
		{
			return this.Container.GetAllInstances(serviceType).Cast<object>();
		}

		#endregion
	}
}