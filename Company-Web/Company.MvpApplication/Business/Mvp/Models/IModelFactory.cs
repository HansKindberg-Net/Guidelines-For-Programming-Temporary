using StructureMap.Pipeline;

namespace Company.MvpApplication.Business.Mvp.Models
{
	public interface IModelFactory
	{
		#region Methods

		TModel Create<TModel>();
		TModel Create<TModel>(ExplicitArguments explicitArguments);

		#endregion
	}
}