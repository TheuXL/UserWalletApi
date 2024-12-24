using System.Collections.Generic;
using System.Threading.Tasks;
using UserWalletApi.Models;
using UserWalletApi.Repositories;

namespace UserWalletApi.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Wallet> AddWalletAsync(Wallet wallet)
        {
            await _walletRepository.AddWalletAsync(wallet);
            return wallet;
        }

        public async Task<Wallet> CreateWalletAsync(Wallet wallet)
        {
            return await AddWalletAsync(wallet);
        }

        public async Task<List<Wallet>> GetWalletsByUserIdAsync(int userId)
        {
            var wallets = await _walletRepository.GetWalletsByUserIdAsync(userId);
            return wallets ?? new List<Wallet>();
        }
    }
}
