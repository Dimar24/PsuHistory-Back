using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Repository.Repositories
{
    public interface IAttachmentFormRepository : IBaseRepository<Guid, AttachmentForm>
    { }

    public class AttachmentFormRepository : IAttachmentFormRepository
    {
        private readonly PsuHistoryDbContext db;

        public AttachmentFormRepository(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<AttachmentForm> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.AttachmentForms.AsNoTracking().Include(db => db.Form).FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<AttachmentForm>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.AttachmentForms.AsNoTracking().Include(db => db.Form).ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(AttachmentForm entity, CancellationToken cancellationToken)
        {
            return await db.AttachmentForms.AnyAsync(db =>
                    db.FileName == entity.FileName &&
                    db.FileType == entity.FileType &&
                    db.FilePath == entity.FilePath,
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.AttachmentForms.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<AttachmentForm> InsertAsync(AttachmentForm entity, CancellationToken cancellationToken)
        {
            await db.AttachmentForms.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<AttachmentForm> UpdateAsync(AttachmentForm entity, CancellationToken cancellationToken)
        {
            db.AttachmentForms.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.AttachmentForms.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
