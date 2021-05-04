using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface IAttachmentBurialService : IBaseService<Guid, AttachmentBurial>
    { }

    class AttachmentBurialService : BaseService<Guid, AttachmentBurial>, IAttachmentBurialService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public AttachmentBurialService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
