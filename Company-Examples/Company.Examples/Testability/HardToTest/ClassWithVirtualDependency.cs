using Company.Examples.Testability.Dependencies.Mockable;

namespace Company.Examples.Testability.HardToTest
{
	public class ClassWithVirtualDependency
	{
		#region Methods

		public virtual void Method()
		{
			new ClassWithVirtualMethod().Method();
		}

		#endregion
	}
}