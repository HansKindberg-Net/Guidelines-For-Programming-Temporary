using StructureMap.Pipeline;

namespace Company.MvcApplication.Business.Web.Mvc.Models
{
	public interface IModelFactory
	{
		#region Methods

		TModel Create<TModel>();
		TModel Create<TModel>(ExplicitArguments explicitArguments);

		#endregion
	}
}