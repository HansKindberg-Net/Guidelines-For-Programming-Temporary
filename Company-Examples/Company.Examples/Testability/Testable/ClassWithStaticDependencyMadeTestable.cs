using System;
using Company.Examples.Testability.Dependencies.Mockable;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithStaticDependencyMadeTestable
	{
		#region Fields

		private readonly IClassWithStaticMethod _classWithStaticMethod;

		#endregion

		#region Constructors

		public ClassWithStaticDependencyMadeTestable(IClassWithStaticMethod classWithStaticMethod)
		{
			if(classWithStaticMethod == null)
				throw new ArgumentNullException("classWithStaticMethod");

			this._classWithStaticMethod = classWithStaticMethod;
		}

		#endregion

		#region Properties

		protected internal virtual IClassWithStaticMethod ClassWithStaticMethod
		{
			get { return this._classWithStaticMethod; }
		}

		#endregion

		#region Methods

		public virtual void Method()
		{
			this.ClassWithStaticMethod.Method();
		}

		#endregion
	}
}