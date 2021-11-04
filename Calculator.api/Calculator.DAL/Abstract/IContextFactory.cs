using Calculator.DAL.Context;

namespace Calculator.DAL.Abstract
{
    public interface IContextFactory
    {
        AppDbContext CreateDbContext(string connectionString);
    }
}