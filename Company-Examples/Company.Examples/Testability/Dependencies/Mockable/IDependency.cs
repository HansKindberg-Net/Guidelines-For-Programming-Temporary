using System.Diagnostics.CodeAnalysis;

namespace Company.Examples.Testability.Dependencies.Mockable
{
	public interface IDependency
	{
		#region Properties

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
		string Property { get; set; }

		#endregion

		#region Methods

		bool Method();

		#endregion
	}
}