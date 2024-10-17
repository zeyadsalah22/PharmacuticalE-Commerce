using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface ICartRepository: IRepository<Cart>
    {
        public IEnumerable<Cart> GetAllWithShopping();

        public IEnumerable<Cart> GetAllWithPresc();

        public IEnumerable<Cart> GetAllByUserIdWithShopping(int? userId);

        public IEnumerable<Cart> GetAllByUserIdWithPresc(int? userId);

        public Cart GetByIdWithShopping(int? id);

        public Cart GetByIdWithPresc(int? id);
    }
}
