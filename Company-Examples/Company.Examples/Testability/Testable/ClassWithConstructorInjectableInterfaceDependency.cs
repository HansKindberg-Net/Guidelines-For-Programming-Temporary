using System;
using System.Globalization;
using Company.Examples.Testability.Dependencies.Mockable;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithConstructorInjectableInterfaceDependency
	{
		#region Fields

		private readonly IDependency _dependency;

		#endregion

		#region Constructors

		public ClassWithConstructorInjectableInterfaceDependency(IDependency dependency)
		{
			if(dependency == null)
				throw new ArgumentNullException("dependency");

			this._dependency = dependency;
		}

		#endregion

		#region Properties

		protected internal virtual IDependency Dependency
		{
			get { return this._dependency; }
		}

		#endregion

		#region Methods

		public virtual string CallToTheDependency()
		{
			bool method = this.Dependency.Method();

			string property = this.Dependency.Property;

			return string.Format(CultureInfo.InvariantCulture, "The dependency method returned \"{0}\" and the dependency property returned \"{1}\".", method, property);
		}

		#endregion
	}
}