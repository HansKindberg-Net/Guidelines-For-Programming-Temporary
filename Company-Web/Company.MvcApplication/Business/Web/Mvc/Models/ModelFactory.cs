using System;
using StructureMap;
using StructureMap.Pipeline;

namespace Company.MvcApplication.Business.Web.Mvc.Models
{
	public class ModelFactory : IModelFactory
	{
		#region Fields

		private readonly IContainer _container;

		#endregion

		#region Constructors

		public ModelFactory(IContainer container)
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

		public virtual TModel Create<TModel>()
		{
			return this.Container.GetInstance<TModel>();
		}

		public virtual TModel Create<TModel>(ExplicitArguments explicitArguments)
		{
			return this.Container.GetInstance<TModel>(explicitArguments);
		}

		#endregion
	}
}