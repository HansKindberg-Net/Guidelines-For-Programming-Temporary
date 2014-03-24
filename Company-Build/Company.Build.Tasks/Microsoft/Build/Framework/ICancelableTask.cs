// ReSharper disable CheckNamespace

namespace Microsoft.Build.Framework // ReSharper restore CheckNamespace
{
	public interface ICancelableTask : ITask
	{
		#region Methods

		void Cancel();

		#endregion
	}
}