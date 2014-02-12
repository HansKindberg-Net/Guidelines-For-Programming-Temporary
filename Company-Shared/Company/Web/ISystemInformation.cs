using System.Collections.Generic;

namespace Company.Web
{
	public interface ISystemInformation
	{
		#region Properties

		string Heading { get; set; }
		string Information { get; set; }
		IEnumerable<string> InformationList { get; set; }
		SystemInformationType Type { get; set; }
		bool Visible { get; set; }

		#endregion
	}
}