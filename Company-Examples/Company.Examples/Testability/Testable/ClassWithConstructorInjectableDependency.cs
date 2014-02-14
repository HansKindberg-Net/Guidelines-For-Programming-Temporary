using System;
using Company.Examples.Testability.Dependencies.Mockable;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithConstructorInjectableDependency
	{
		#region Fields

		private readonly IClassWithStaticMethod _classWithStaticMethod;

		#endregion

		#region Constructors

		public ClassWithConstructorInjectableDependency(IClassWithStaticMethod classWithStaticMethod)
		{
			if(classWithStaticMethod == null)
				throw new ArgumentNullException("classWithStaticMethod");

			this._classWithStaticMethod = classWithStaticMethod;
		}

		#endregion

		#region Methods

		public virtual void Method()
		{
			this._classWithStaticMethod.Method();
		}

		#endregion
	}
}