using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Calculator.DAL.Abstract
{
    public interface IRepository
    {
        Task<bool> CreateAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> FindByIdAsync<TEntity>(Guid? id) where TEntity : class;

        IQueryable<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class;

        Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        bool Remove<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> SaveAsync();
    }
}