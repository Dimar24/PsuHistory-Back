using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface IDutyStationService : IBaseService<Guid, DutyStation>
    { }

    class DutyStationService : BaseService<Guid, DutyStation>, IDutyStationService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public DutyStationService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
