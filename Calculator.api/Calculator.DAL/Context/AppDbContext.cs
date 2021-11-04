using Calculator.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Expression> Expressions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}