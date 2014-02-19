using System;

namespace Company
{
	public interface IDateTimeContext
	{
		#region Properties

		DateTime Now { get; }

		#endregion
	}
}