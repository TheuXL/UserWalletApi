using Moq;
using UserWalletApi.Models;
using UserWalletApi.Repositories;
using UserWalletApi.Services;
using System.Threading.Tasks;
using Xunit;

namespace UserWalletApi.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateUser_ReturnsUser_WhenValid()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Nome = "João Silva",
                Nascimento = new DateTime(1990, 1, 1),
                CPF = "12345678900"
            };
            _userRepositoryMock
                .Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.CreateUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Nome, result.Nome);
        }
    }
}
