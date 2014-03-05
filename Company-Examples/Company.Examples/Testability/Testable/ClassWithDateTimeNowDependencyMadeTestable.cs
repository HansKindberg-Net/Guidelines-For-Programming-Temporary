using System;
using System.Diagnostics.CodeAnalysis;

namespace Company.Examples.Testability.Testable
{
	public class ClassWithDateTimeNowDependencyMadeTestable
	{
		#region Fields

		private readonly IDateTimeContext _dateTimeContext;

		#endregion

		#region Constructors

		public ClassWithDateTimeNowDependencyMadeTestable(IDateTimeContext dateTimeContext)
		{
			if(dateTimeContext == null)
				throw new ArgumentNullException("dateTimeContext");

			this._dateTimeContext = dateTimeContext;
		}

		#endregion

		#region Properties

		protected internal virtual IDateTimeContext DateTimeContext
		{
			get { return this._dateTimeContext; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public virtual DateTime GetCurrentDateTime()
		{
			return this.DateTimeContext.Now;
		}

		#endregion
	}
}