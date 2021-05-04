using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface ITypeVictimService : IBaseService<Guid, TypeVictim>
    { }

    class TypeVictimService : BaseService<Guid, TypeVictim>, ITypeVictimService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public TypeVictimService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
