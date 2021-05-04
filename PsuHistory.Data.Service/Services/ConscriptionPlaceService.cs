using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface IConscriptionPlaceService : IBaseService<Guid, ConscriptionPlace>
    { }

    class ConscriptionPlaceService : BaseService<Guid, ConscriptionPlace>, IConscriptionPlaceService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public ConscriptionPlaceService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
