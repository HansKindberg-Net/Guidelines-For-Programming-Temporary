using System.Diagnostics.CodeAnalysis;

namespace Company.Web
{
	public interface ISiteMapNode
	{
		#region Properties

		string Description { get; }
		string Title { get; }

		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
		string Url { get; }

		#endregion
	}
}