using Moq;
using Microsoft.AspNetCore.Mvc;
using UserWalletApi.Controllers;
using UserWalletApi.Services;
using UserWalletApi.Models;
using System.Threading.Tasks;
using Xunit;

namespace UserWalletApi.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreatedAtActionResult_WhenValidUser()
        {
            // Arrange
            var user = new User
            {
                Nome = "João Silva",
                Nascimento = new DateTime(1990, 1, 1),
                CPF = "12345678900"
            };
            _userServiceMock.Setup(service => service.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userController.CreateUser(user);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<User>(actionResult.Value);
            Assert.Equal(user.Nome, returnValue.Nome);
        }
    }
}
