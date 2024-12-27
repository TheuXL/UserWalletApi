using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserWalletApi.Models;
using UserWalletApi.Data;

namespace UserWalletApi.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet> GetWalletByIdAsync(int id)
        {
             return await _context.Wallets.FindAsync(id) ?? new Wallet { Banco = string.Empty};
        }

        public async Task<List<Wallet>> GetWalletsByUserIdAsync(int userId)
        {
             return await _context.Wallets
                       .Where(w => w.UserID == userId)
                       .ToListAsync() ?? new List<Wallet>();
         }

        public async Task AddWalletAsync(Wallet wallet)
        {
           await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }
    }
}