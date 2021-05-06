using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Service.Services
{
    public interface IAttachmentBurialService : IBaseService<Guid, AttachmentBurial>
    { }

    public class AttachmentBurialService : IAttachmentBurialService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public AttachmentBurialService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AttachmentBurial> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.AttachmentBurials.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<AttachmentBurial>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.AttachmentBurials.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<AttachmentBurial> InsertAsync(AttachmentBurial entity, CancellationToken cancellationToken)
        {
            await _dbContext.AttachmentBurials.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<AttachmentBurial> UpdateAsync(AttachmentBurial entity, CancellationToken cancellationToken)
        {
            _dbContext.AttachmentBurials.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.AttachmentBurials.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
