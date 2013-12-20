using System;
using System.Web.Mvc;
using StructureMap;

namespace Company.MvcApplication.Business.Web.Mvc
{
	[CLSCompliant(false)]
	public class DependencyResolver : Company.IoC.StructureMap.DependencyResolver, IDependencyResolver
	{
		#region Constructors

		public DependencyResolver(IContainer container) : base(container) {}

		#endregion
	}
}