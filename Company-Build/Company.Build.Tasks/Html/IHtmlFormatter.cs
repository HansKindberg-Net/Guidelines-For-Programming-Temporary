using System.Text;

namespace Company.Build.Tasks.Html
{
	public interface IHtmlFormatter
	{
		#region Methods

		string Format(string html, Encoding encoding);

		#endregion
	}
}