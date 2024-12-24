using System.Collections.Generic;
using System.Threading.Tasks;
using UserWalletApi.Models;

namespace UserWalletApi.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<List<User>> GetUsersAsync();
    }
}
