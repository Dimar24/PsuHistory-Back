using PsuHistory.Business.Service.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Interfaces
{
    public interface IBaseBusinessService<TKey, TEntity>
        where TEntity : class
    {
        Task<ValidationModel<TEntity>> GetAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel<TEntity>> InsertAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel<TEntity>> UpdateAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel<TEntity>> DeleteAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
