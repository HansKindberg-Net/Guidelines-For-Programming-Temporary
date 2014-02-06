using System.Diagnostics.CodeAnalysis;

namespace Company.Data.Entities
{
	public interface IExampleItem
	{
		#region Properties

		int? Id { get; }
		string Key { get; }

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "New")]
		bool New { get; }

		string Value { get; }

		#endregion
	}
}