using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface IBirthPlaceService : IBaseService<Guid, BirthPlace>
    { }

    class BirthPlaceService : BaseService<Guid, BirthPlace>, IBirthPlaceService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public BirthPlaceService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
