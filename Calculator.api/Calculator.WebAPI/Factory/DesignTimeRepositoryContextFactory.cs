using System.IO;
using Calculator.DAL.Context;
using Calculator.DAL.Factory;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Calculator.WebAPI.Factory
{
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var repositoryFactory = new AppDbContextFactory();

            return repositoryFactory.CreateDbContext(connectionString);
        }
    }
}