using System;
using Company.Examples.Testability.Dependencies.Mockable;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithVirtualDependencyMadeTestable
	{
		#region Fields

		private readonly ClassWithVirtualMethod _classWithVirtualMethod;

		#endregion

		#region Constructors

		public ClassWithVirtualDependencyMadeTestable(ClassWithVirtualMethod classWithVirtualMethod)
		{
			if(classWithVirtualMethod == null)
				throw new ArgumentNullException("classWithVirtualMethod");

			this._classWithVirtualMethod = classWithVirtualMethod;
		}

		#endregion

		#region Properties

		protected internal virtual ClassWithVirtualMethod ClassWithVirtualMethod
		{
			get { return this._classWithVirtualMethod; }
		}

		#endregion

		#region Methods

		public virtual void Method()
		{
			this.ClassWithVirtualMethod.Method();
		}

		#endregion
	}
}