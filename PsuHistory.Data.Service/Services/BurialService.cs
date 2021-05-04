using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Data.Service.Services
{
    public interface IBurialService : IBaseService<Guid, Burial>
    { }

    class BurialService : BaseService<Guid, Burial>, IBurialService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public BurialService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
