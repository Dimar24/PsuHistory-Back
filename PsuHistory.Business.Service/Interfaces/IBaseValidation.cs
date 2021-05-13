using PsuHistory.Business.Service.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Interfaces
{
    public interface IBaseValidation<TKey, TEntity>
        where TEntity : class
    {
        Task<ValidationModel> GetValidationAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel> InsertValidationAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel> UpdateValidationAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel> DeleteValidationAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
