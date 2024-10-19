namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
	public interface IRepository<T>
	{
		Task<T> GetById(int? id);
		Task<IEnumerable<T>> GetAll();
		Task Create(T entity);
		Task Update(T entity);
		Task Delete(int? id);
	}
}
