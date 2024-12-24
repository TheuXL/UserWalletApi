using System.Collections.Generic;
using System.Threading.Tasks;
using UserWalletApi.Models;

namespace UserWalletApi.Services
{
    public interface IWalletService
    {
        Task<Wallet> AddWalletAsync(Wallet wallet);
        Task<List<Wallet>> GetWalletsByUserIdAsync(int userId);
    }
}
