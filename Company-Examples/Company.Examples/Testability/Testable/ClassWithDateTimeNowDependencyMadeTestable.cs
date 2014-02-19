using System;
using System.Diagnostics.CodeAnalysis;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithDateTimeNowDependencyMadeTestable
	{
		private readonly IDateTimeContext _dateTimeContext;

		public ClassWithDateTimeNowDependencyMadeTestable(IDateTimeContext dateTimeContext)
		{
			if(dateTimeContext == null)
				throw new ArgumentNullException("dateTimeContext");

			this._dateTimeContext = dateTimeContext;
		}

		protected internal virtual IDateTimeContext DateTimeContext
		{
			get { return this._dateTimeContext; }
		}

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual DateTime GetCurrentDateTime()
		{
			return this.DateTimeContext.Now;
		}

		#endregion
	}
}