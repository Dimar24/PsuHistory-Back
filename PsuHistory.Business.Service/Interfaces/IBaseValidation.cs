using PsuHistory.Business.Service.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Interfaces
{
    public interface IBaseValidation<TKey, TEntity>
        where TEntity : class
    {
        Task<ValidationModel<TEntity>> GetValidationAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel<TEntity>> InsertValidationAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel<TEntity>> UpdateValidationAsync(TEntity newEntity, CancellationToken cancellationToken = default(CancellationToken));
        Task<ValidationModel<TEntity>> DeleteValidationAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
