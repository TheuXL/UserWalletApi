using System.Collections.Generic;
using System.Threading.Tasks;
using UserWalletApi.Models;

namespace UserWalletApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);      // Alterado para GetUserByIdAsync
        Task<List<User>> GetUsersAsync();         // Alterado para GetUsersAsync
        Task AddUserAsync(User user);             // Alterado para AddUserAsync
    }
}
