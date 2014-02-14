using Company.Examples.Testability.Dependencies.HardToMock;

namespace Company.Examples.Testability.HardToTest
{
	public class ClassWithStaticDependency
	{
		#region Methods

		public virtual void Method()
		{
			ClassWithStaticMethod.Method();
		}

		#endregion
	}
}