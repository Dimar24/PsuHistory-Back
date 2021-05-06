using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Service.Services
{
    public interface IAttachmentFormService : IBaseService<Guid, AttachmentForm>
    { }

    public class AttachmentFormService : IAttachmentFormService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public AttachmentFormService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AttachmentForm> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.AttachmentForms.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<AttachmentForm>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.AttachmentForms.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<AttachmentForm> InsertAsync(AttachmentForm entity, CancellationToken cancellationToken)
        {
            await _dbContext.AttachmentForms.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<AttachmentForm> UpdateAsync(AttachmentForm entity, CancellationToken cancellationToken)
        {
            _dbContext.AttachmentForms.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.AttachmentForms.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
