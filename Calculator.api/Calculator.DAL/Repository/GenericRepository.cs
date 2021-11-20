using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Calculator.DAL.Abstract;
using Calculator.DAL.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// ReSharper disable TemplateIsNotCompileTimeConstantProblem
namespace Calculator.DAL.Repository
{
    public class GenericRepository : BaseRepository, IRepository
    {
        private AppDbContext Context { get; }
        private readonly ILogger<GenericRepository> _log;

        public GenericRepository(string connectionString, IContextFactory contextFactory,
            ILogger<GenericRepository> log)
            : base(connectionString, contextFactory)
        {
            _log = log;
            Context = contextFactory.CreateDbContext(connectionString);
        }

        public async Task<TEntity> FindByIdAsync<TEntity>(Guid? id) where TEntity : class
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            TEntity entity = null;
            try
            {
                entity = await Context.Set<TEntity>().FindAsync(id);
            }
            catch (SqlException e)
            {
                _log.LogError(e.ToString());
            }

            return entity;
        }

        public IQueryable<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class
        {
            IQueryable<TEntity> entities = null;
            try
            {
                entities = predicate != null
                    ? Context.Set<TEntity>().Where(predicate)
                    : Context.Set<TEntity>();
            }
            catch (SqlException e)
            {
                _log.LogError(e.ToString());
            }

            return entities;
        }

        public async Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            TEntity entity = null;
            try
            {
                entity = predicate != null
                    ? await Context.Set<TEntity>().Where(predicate).SingleOrDefaultAsync()
                    : await Context.Set<TEntity>().FirstOrDefaultAsync();
            }
            catch (SqlException e)
            {
                _log.LogError(e.ToString());
            }

            return entity;
        }

        public async Task<bool> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await Context.Set<TEntity>().AddAsync(entity);
                return true;
            }
            catch (SqlException e)
            {
                _log.LogError(e.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                await Task.Factory.StartNew(() => { Context.Entry(entity).State = EntityState.Modified; });
                return true;
            }
            catch (SqlException e)
            {
                _log.LogError(e.ToString());
                return false;
            }
        }

        public bool Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                Context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (SqlException e)
            {
                _log.LogError(e.ToString());
                return false;
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await Context.SaveChangesAsync(); // return int
                return true;
            }
            catch (SqlException e)
            {
                _log.LogError(e.ToString());
                return false;
            }
        }
    }
}