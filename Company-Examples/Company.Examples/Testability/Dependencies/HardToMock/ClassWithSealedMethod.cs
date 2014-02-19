using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Company.Examples.Testability.Dependencies.HardToMock
{
	public class ClassWithSealedMethod
	{
		#region Methods

		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public string Method()
		{
			return string.Format(CultureInfo.InvariantCulture, "This value is returned from the method \"{0}.Method()\".", typeof(ClassWithSealedMethod));
		}

		#endregion
	}
}