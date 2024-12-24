using System.Collections.Generic;
using System.Threading.Tasks;
using UserWalletApi.Models;

namespace UserWalletApi.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet> GetWalletByIdAsync(int id);           // Corrigido para async
        Task<List<Wallet>> GetWalletsByUserIdAsync(int userId); // Corrigido para async
        Task AddWalletAsync(Wallet wallet);                // Corrigido para async
    }
}
