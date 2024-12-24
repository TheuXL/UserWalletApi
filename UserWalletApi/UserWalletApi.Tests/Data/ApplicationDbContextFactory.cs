using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserWalletApi.Data;

namespace UserWalletApi.Tests.Data
{
    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }
    }
}
