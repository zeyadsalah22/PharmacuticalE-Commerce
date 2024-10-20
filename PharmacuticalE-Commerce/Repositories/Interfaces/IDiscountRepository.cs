using PharmacuticalE_Commerce.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount> GetByIdWithProduct(int? id);
    }
}
