using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserWalletApi.Models;
using UserWalletApi.Data;

namespace UserWalletApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id) ?? new User { Nome = string.Empty, CPF = string.Empty };
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync() ?? new List<User>();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}