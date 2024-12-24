using Moq;
using Microsoft.AspNetCore.Mvc;
using UserWalletApi.Controllers;
using UserWalletApi.Services;
using UserWalletApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UserWalletApi.Tests.Controllers
{
    public class WalletControllerTests
    {
        private readonly Mock<IWalletService> _walletServiceMock;
        private readonly WalletController _walletController;

        public WalletControllerTests()
        {
            _walletServiceMock = new Mock<IWalletService>();
            _walletController = new WalletController(_walletServiceMock.Object);
        }

        [Fact]
        public async Task CreateWallet_ReturnsCreatedAtActionResult_WhenValidWallet()
        {
            // Arrange
            var wallet = new Wallet
            {
                UserID = 1,
                ValorAtual = 1000.00M,
                Banco = "Banco do Brasil",
                UltimaAtualizacao = DateTime.Now
            };
            _walletServiceMock.Setup(service => service.CreateWalletAsync(It.IsAny<Wallet>())).ReturnsAsync(wallet);

            // Act
            var result = await _walletController.CreateWallet(wallet);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Wallet>(actionResult.Value);
            Assert.Equal(wallet.Banco, returnValue.Banco);
        }

        [Fact]
        public async Task GetWallets_ReturnsOkResult_WhenWalletsExist()
        {
            // Arrange
            var wallets = new List<Wallet>
            {
                new Wallet { UserID = 1, ValorAtual = 1000.00M, Banco = "Banco do Brasil", UltimaAtualizacao = DateTime.Now },
                new Wallet { UserID = 1, ValorAtual = 1500.00M, Banco = "Caixa Econômica", UltimaAtualizacao = DateTime.Now }
            };
            _walletServiceMock.Setup(service => service.GetWalletsByUserIdAsync(1)).ReturnsAsync(wallets);

            // Act
            var result = await _walletController.GetWallets(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Wallet>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetWallets_ReturnsNotFoundResult_WhenNoWallets()
        {
            // Arrange
            _walletServiceMock.Setup(service => service.GetWalletsByUserIdAsync(1)).ReturnsAsync(new List<Wallet>());

            // Act
            var result = await _walletController.GetWallets(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
