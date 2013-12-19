using System.Diagnostics.CodeAnalysis;

namespace Company.MvpApplication.Business.Mvp.Views
{
	[SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces")]
	public interface IView : WebFormsMvp.IView {}

	public interface IView<TModel> : IView
	{
		#region Properties

		TModel Model { get; set; }

		#endregion
	}
}