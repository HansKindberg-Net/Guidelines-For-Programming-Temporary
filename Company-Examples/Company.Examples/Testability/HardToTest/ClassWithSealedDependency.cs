using Company.Examples.Testability.Dependencies.HardToMock;

namespace Company.Examples.Testability.HardToTest
{
	public class ClassWithSealedDependency
	{
		#region Methods

		public virtual void Method()
		{
			new ClassWithSealedMethod().Method();
		}

		#endregion
	}
}