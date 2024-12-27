using System.Collections.Generic;
using System.Threading.Tasks;
using UserWalletApi.Models;

namespace UserWalletApi.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<User> CreateUserAsync(User user); // Adicionando o metodo que estava faltando
        Task<List<User>> GetUsersAsync();
    }
}