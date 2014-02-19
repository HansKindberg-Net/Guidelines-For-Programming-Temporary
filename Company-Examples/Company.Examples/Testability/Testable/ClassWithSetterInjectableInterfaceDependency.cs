using System;
using System.Globalization;
using Company.Examples.Testability.Dependencies.Mockable;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithSetterInjectableInterfaceDependency
	{
		#region Fields

		private IDependency _dependency;

		#endregion

		#region Properties

		public virtual IDependency Dependency
		{
			get
			{
				if(this._dependency == null)
					throw new InvalidOperationException("The dependency have not been set.");

				return this._dependency;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException("value");

				this._dependency = value;
			}
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