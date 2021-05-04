using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface IFormService : IBaseService<Guid, Form>
    { }

    class FormService : BaseService<Guid, Form>, IFormService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public FormService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
