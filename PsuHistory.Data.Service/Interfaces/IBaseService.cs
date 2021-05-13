using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.EF.SQL;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Service.Interfaces
{
    public interface IBaseService<TKey, TEntity> 
        where TEntity : class
    {
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> ExistAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> InsertAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> UpdateAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
