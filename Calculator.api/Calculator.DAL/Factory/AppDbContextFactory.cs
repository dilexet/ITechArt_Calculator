using Calculator.DAL.Abstract;
using Calculator.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DAL.Factory
{
    public class AppDbContextFactory : IContextFactory
    {
        public AppDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}