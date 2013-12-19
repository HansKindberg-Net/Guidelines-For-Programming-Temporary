using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using StructureMap;
using StructureMap.Pipeline;
using WebFormsMvp;
using WebFormsMvp.Binder;

namespace Company.IoC.StructureMap.Web.Mvp.Binder
{
	public class PresenterFactory : IPresenterFactory, IDisposable
	{
		#region Fields

		private readonly IContainer _container;
		private static readonly string _genericIPresenterFriendlyName = typeof(IPresenter).FullName + "<TView>";
		private readonly IDictionary<Type, Type> _presenterTypeToViewTypeMappings = new Dictionary<Type, Type>();
		private readonly object _presenterTypeToViewTypeMappingsLockObject = new object();

		#endregion

		#region Constructors

		public PresenterFactory(IContainer container)
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

		public virtual IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
		{
			Type abstractViewType = this.GetAbstractViewType(presenterType);

			if(viewType == null)
				throw new ArgumentNullException("viewType");

			if(!typeof(IView).IsAssignableFrom(viewType))
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The view-type \"{0}\" must implement \"{1}\".", viewType.FullName, typeof(IView).FullName), "viewType");

			if(viewInstance == null)
				throw new ArgumentNullException("viewInstance");

			ExplicitArguments explicitArguments = new ExplicitArguments();
			explicitArguments.Set(abstractViewType, viewInstance);

			IPresenter presenter = (IPresenter) this.Container.GetInstance(presenterType, explicitArguments);

			if(presenter == null)
				throw new StructureMapException(202, new object[] {presenterType});

			return presenter;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(disposing && this.Container != null)
				this.Container.Dispose();
		}

		protected internal virtual Type GetAbstractViewType(Type presenterType)
		{
			if(presenterType == null)
				throw new ArgumentNullException("presenterType");

			lock(this._presenterTypeToViewTypeMappingsLockObject)
			{
				if(!this._presenterTypeToViewTypeMappings.ContainsKey(presenterType))
				{
					try
					{
						// ReSharper disable PossibleNullReferenceException
						Type viewType = presenterType
							.GetInterfaces()
							.SingleOrDefault(type => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IPresenter<>))
							.GetGenericArguments()[0];
						// ReSharper restore PossibleNullReferenceException

						if(viewType == null)
							throw new ArgumentNullException("presenterType", "The view-type is null.");

						this._presenterTypeToViewTypeMappings.Add(presenterType, viewType);

						return viewType;
					}
					catch(Exception exception)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Could not resolve the view-type for presenter-type \"{0}\". The presenter-type must implement \"{1}\".", presenterType.FullName, _genericIPresenterFriendlyName), "presenterType", exception);
					}
				}

				return this._presenterTypeToViewTypeMappings[presenterType];
			}
		}

		public virtual void Release(IPresenter presenter)
		{
			this.Container.EjectAllInstancesOf<IPresenter>();

			// ReSharper disable SuspiciousTypeConversion.Global
			IDisposable disposablePresenter = presenter as IDisposable;
			// ReSharper restore SuspiciousTypeConversion.Global

			if(disposablePresenter != null)
				disposablePresenter.Dispose();
		}

		#endregion
	}
}