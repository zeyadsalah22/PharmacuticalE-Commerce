using PharmacuticalE_Commerce.Models;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        CartItem GetById(int? cartId, int? prodId);
        
        IEnumerable<CartItem> GetAll();
        
        void Create(CartItem entity);
        
        void Update(CartItem entity);
    }
}
