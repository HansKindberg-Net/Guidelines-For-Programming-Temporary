using System;
using System.Diagnostics.CodeAnalysis;

namespace Company.Examples.Testability.HardToTest
{
	public class ClassWithDateTimeNowDependency
	{
		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}

		#endregion
	}
}