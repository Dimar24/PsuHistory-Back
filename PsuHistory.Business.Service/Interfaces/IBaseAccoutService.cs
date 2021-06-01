using PsuHistory.Business.DTO.Models.AccountDataModels;
using PsuHistory.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Business.Service.Interfaces
{
    public interface IBaseAccoutService<TLoginEntity>
        where TLoginEntity : class
    {
        Task<ValidationModel<TLoginEntity>> LoginAsync(Login login, CancellationToken cancellationToken = default(CancellationToken));
    }
}
