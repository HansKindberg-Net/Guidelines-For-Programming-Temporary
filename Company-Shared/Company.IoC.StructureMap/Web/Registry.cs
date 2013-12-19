using System;
using System.Web;
using System.Web.SessionState;
using StructureMap.Configuration.DSL;

namespace Company.IoC.StructureMap.Web
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

			registry.For<HttpApplicationState>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Application);
			registry.For<HttpApplicationStateBase>().HybridHttpOrThreadLocalScoped().Use<HttpApplicationStateWrapper>();
			registry.For<HttpContext>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current);
			registry.For<HttpContextBase>().HybridHttpOrThreadLocalScoped().Use<HttpContextWrapper>();
			registry.For<HttpRequest>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Request);
			registry.For<HttpRequestBase>().HybridHttpOrThreadLocalScoped().Use<HttpRequestWrapper>();
			registry.For<HttpResponse>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Response);
			registry.For<HttpResponseBase>().HybridHttpOrThreadLocalScoped().Use<HttpResponseWrapper>();
			registry.For<HttpServerUtility>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Server);
			registry.For<HttpServerUtilityBase>().HybridHttpOrThreadLocalScoped().Use<HttpServerUtilityWrapper>();
			registry.For<HttpSessionState>().HybridHttpOrThreadLocalScoped().Use(() => HttpContext.Current.Session);
			registry.For<HttpSessionStateBase>().HybridHttpOrThreadLocalScoped().Use<HttpSessionStateWrapper>();
		}

		#endregion
	}
}