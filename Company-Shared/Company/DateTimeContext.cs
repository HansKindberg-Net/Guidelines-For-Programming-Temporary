using System;

namespace Company
{
	public class DateTimeContext : IDateTimeContext
	{
		#region Properties

		public virtual DateTime Now
		{
			get { return DateTime.Now; }
		}

		#endregion
	}
}