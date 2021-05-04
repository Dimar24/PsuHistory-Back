using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface IVictimService : IBaseService<Guid, Victim>
    { }

    class VictimService : BaseService<Guid, Victim>, IVictimService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public VictimService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
