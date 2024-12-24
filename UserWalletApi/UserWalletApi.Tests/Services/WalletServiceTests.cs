using Moq;
using UserWalletApi.Models;
using UserWalletApi.Repositories;
using UserWalletApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UserWalletApi.Tests.Services
{
    public class WalletServiceTests
    {
        private readonly Mock<IWalletRepository> _walletRepositoryMock;
        private readonly WalletService _walletService;

        public WalletServiceTests()
        {
            _walletRepositoryMock = new Mock<IWalletRepository>();
            _walletService = new WalletService(_walletRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateWallet_ReturnsWallet_WhenValid()
        {
            // Arrange
            var wallet = new Wallet
            {
                Id = 1,
                UserID = 1,
                ValorAtual = 1000.00M,
                Banco = "Banco do Brasil",
                UltimaAtualizacao = DateTime.Now
            };
            _walletRepositoryMock
                .Setup(repo => repo.AddWalletAsync(It.IsAny<Wallet>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _walletService.CreateWalletAsync(wallet);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(wallet.Banco, result.Banco);
        }

        [Fact]
        public async Task GetWalletsByUserId_ReturnsWallets_WhenExist()
        {
            // Arrange
            var wallets = new List<Wallet>
            {
                new Wallet { UserID = 1, ValorAtual = 1000.00M, Banco = "Banco do Brasil", UltimaAtualizacao = DateTime.Now },
                new Wallet { UserID = 1, ValorAtual = 1500.00M, Banco = "Caixa Econômica", UltimaAtualizacao = DateTime.Now }
            };
            _walletRepositoryMock
                .Setup(repo => repo.GetWalletsByUserIdAsync(1))
                .ReturnsAsync(wallets);

            // Act
            var result = await _walletService.GetWalletsByUserIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
