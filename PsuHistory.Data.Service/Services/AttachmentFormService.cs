using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface IAttachmentFormService : IBaseService<Guid, AttachmentForm>
    { }

    class AttachmentFormService : BaseService<Guid, AttachmentForm>, IAttachmentFormService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public AttachmentFormService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
