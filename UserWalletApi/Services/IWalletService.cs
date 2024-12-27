 using System.Collections.Generic;
 using System.Threading.Tasks;
 using UserWalletApi.Models;

namespace UserWalletApi.Services
{
    public interface IWalletService
    {
      Task<Wallet> AddWalletAsync(Wallet wallet);
      Task<Wallet> CreateWalletAsync(Wallet wallet); // Adicionando o metodo que estava faltando
      Task<List<Wallet>> GetWalletsByUserIdAsync(int userId);
    }
}