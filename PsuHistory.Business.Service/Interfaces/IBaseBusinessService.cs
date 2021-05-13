using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Interfaces
{
    public interface IBaseBusinessService<TKey, TEntity>
        where TEntity : class
    {
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> InsertAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> UpdateAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
