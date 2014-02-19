using System.Globalization;

namespace Company.Examples.Testability.Dependencies.Mockable
{
	public class ClassWithVirtualMethod
	{
		#region Methods

		public virtual string Method()
		{
			return string.Format(CultureInfo.InvariantCulture, "This value is returned from the method \"{0}.Method()\".", typeof(ClassWithVirtualMethod));
		}

		#endregion
	}
}