using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.EF.SQL;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Service.Abstraction
{
    public interface IBaseService<TKey, TEntity> 
        where TEntity : class
    {
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> InsertAsync(TEntity newEntity, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity newEntity, CancellationToken cancellationToken);
        Task DeleteAsync(TKey id, CancellationToken cancellationToken);
    }

    public abstract class BaseService<TKey, TEntity> : IBaseService<TKey, TEntity> 
        where TEntity : class
    {
        private PsuHistoryDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();

            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        { 
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if(entity is not null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
