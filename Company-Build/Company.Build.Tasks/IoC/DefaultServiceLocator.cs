using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Company.Build.Tasks.Html;
using Company.Build.Tasks.Markdown;

namespace Company.Build.Tasks.IoC
{
	public class DefaultServiceLocator : IServiceLocator
	{
		#region Fields

		private readonly IEnvironment _environment = new EnvironmentWrapper();
		private readonly IFileSystem _fileSystem = new FileSystem();
		private readonly IHtmlFormatter _htmlFormatter = new DefaultHtmlFormatter();
		private readonly IMarkdownTransformerFactory _markdownTransformerFactory = new DefaultMarkdownTransformerFactory();

		#endregion

		#region Properties

		protected internal virtual IEnvironment Environment
		{
			get { return this._environment; }
		}

		protected internal virtual IFileSystem FileSystem
		{
			get { return this._fileSystem; }
		}

		protected internal virtual IHtmlFormatter HtmlFormatter
		{
			get { return this._htmlFormatter; }
		}

		[CLSCompliant(false)]
		protected internal virtual IMarkdownTransformerFactory MarkdownTransformerFactory
		{
			get { return this._markdownTransformerFactory; }
		}

		#endregion

		#region Methods

		public virtual object GetService(Type serviceType)
		{
			if(serviceType == null)
				throw new ArgumentNullException("serviceType");

			if(serviceType == typeof(IEnvironment))
				return this.Environment;

			if(serviceType == typeof(IFileSystem))
				return this.FileSystem;

			if(serviceType == typeof(IHtmlFormatter))
				return this.HtmlFormatter;

			if(serviceType == typeof(IMarkdownTransformer))
				return this.MarkdownTransformerFactory.Create();

			if(serviceType == typeof(IMarkdownTransformerFactory))
				return this.MarkdownTransformerFactory;

			return Activator.CreateInstance(serviceType);
		}

		public virtual T GetService<T>()
		{
			return (T) this.GetService(typeof(T));
		}

		public virtual T GetService<T>(string key)
		{
			return (T) this.GetService(typeof(T), key);
		}

		public virtual object GetService(Type serviceType, string key)
		{
			if(key == null)
				return this.GetService(serviceType);

			throw new NotImplementedException();
		}

		public virtual IEnumerable<T> GetServices<T>()
		{
			return this.GetServices(typeof(T)).OfType<T>();
		}

		public virtual IEnumerable<object> GetServices(Type serviceType)
		{
			return new[] {this.GetService(serviceType)};
		}

		#endregion
	}
}