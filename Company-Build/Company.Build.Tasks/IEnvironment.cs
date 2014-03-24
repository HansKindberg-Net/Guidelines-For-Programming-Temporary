namespace Company.Build.Tasks
{
	public interface IEnvironment
	{
		// ReSharper disable InconsistentNaming

		#region Properties

		bool MSBuildAlwaysOverwriteReadOnlyFiles { get; }

		#endregion

		// ReSharper restore InconsistentNaming

		#region Methods

		string GetEnvironmentVariable(string name);

		#endregion
	}
}