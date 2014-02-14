using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Company.Examples.Testability.Dependencies.HardToMock
{
	// This class do not have to be static. Code-analysis suggests to make it static when it contains only static members.
	public static class ClassWithStaticMethod
	{
		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public static string Method()
		{
			return string.Format(CultureInfo.InvariantCulture, "This value is returned from the method \"{0}.GetStringValue()\".", typeof(ClassWithStaticMethod));
		}

		#endregion
	}
}