using System.Collections.Generic;
using Company.Data.Entities;

namespace Company.Data.Repositories
{
	public interface IExampleRepository
	{
		#region Methods

		bool Delete(int id);
		IEnumerable<IExampleItem> Find(IExampleItem queryFilter);
		void Save(IExampleItem exampleItem);

		#endregion
	}
}