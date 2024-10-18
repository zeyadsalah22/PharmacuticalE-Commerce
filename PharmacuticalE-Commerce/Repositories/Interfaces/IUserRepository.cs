using PharmacuticalE_Commerce.Models;
using PharmacuticalE_Commerce.ViewModels;

namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByEmail(string email);
        User GetUserByUsername(string username);
        Task<UserViewModel> GetUserViewModelById(string id);
        Task<List<UserViewModel>> GetAllUserViewModels();
        Task<List<UserViewModel>> GetAllCustomerUserViewModels();
        Task<List<UserViewModel>> GetAllAdminsViewModels();
    }
}
