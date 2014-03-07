using Company.Examples.Testability.Dependencies.Mockable;

namespace Company.Examples.UnitTests.Testability.Testable.Mocks
{
	public class ClassWithStaticMethodMock : IClassWithStaticMethod
	{
		#region Fields

		private bool _methodIsCalled;

		#endregion

		#region Properties

		public virtual bool MethodIsCalled
		{
			get { return this._methodIsCalled; }
		}

		#endregion

		#region Methods

		public virtual string Method()
		{
			this._methodIsCalled = true;

			return string.Empty;
		}

		#endregion
	}
}