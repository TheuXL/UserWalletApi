using System.Collections.Generic;
using System.Threading.Tasks;
using UserWalletApi.Models;
using UserWalletApi.Repositories;

namespace UserWalletApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await AddUserAsync(user);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }
    }
}
