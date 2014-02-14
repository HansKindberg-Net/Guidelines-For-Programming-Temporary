using Company.Examples.Testability.Dependencies.HardToMock;
using Company.Examples.Testability.Dependencies.Mockable;

namespace Company.Examples.Testability.Dependencies.Wrappers
{
	public class ClassWithStaticMethodWrapper : IClassWithStaticMethod
	{
		#region Methods

		public virtual string Method()
		{
			return ClassWithStaticMethod.Method();
		}

		#endregion
	}
}