using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface ICategoryRepository : IRepository<Category>
	{
		public Task<Category> GetByIdWithParent(int? id);
		public Task<IEnumerable<Category>> GetChilds();
		public Task<IEnumerable<Category>> GetParents();
		public Task<IEnumerable<Category>> GetChildsByparent(int? parentId);
		Task<Category> GetCategoryByName(string name);

	}
}
