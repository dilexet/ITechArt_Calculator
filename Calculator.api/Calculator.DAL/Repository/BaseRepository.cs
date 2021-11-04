using Calculator.DAL.Abstract;

namespace Calculator.DAL.Repository
{
    public class BaseRepository
    {
        protected string ConnectionString { get; }
        protected IContextFactory ContextFactory { get; }

        public BaseRepository(string connectionString, IContextFactory contextFactory)
        {
            ConnectionString = connectionString;
            ContextFactory = contextFactory;
        }
    }
}