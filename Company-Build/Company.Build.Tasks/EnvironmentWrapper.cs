using System;

namespace Company.Build.Tasks
{
	public class EnvironmentWrapper : IEnvironment
	{
		#region Properties

		public virtual bool MSBuildAlwaysOverwriteReadOnlyFiles
		{
			get { return this.GetEnvironmentVariable("MSBUILDALWAYSOVERWRITEREADONLYFILES") != null; }
		}

		#endregion

		#region Methods

		public virtual string GetEnvironmentVariable(string name)
		{
			return Environment.GetEnvironmentVariable(name);
		}

		#endregion
	}
}