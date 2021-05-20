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
        private readonly PsuHistoryDbContext db;

        public AttachmentBurialService(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<AttachmentBurial> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.AttachmentBurials.Include(db => db.Burial).FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<bool> ExistAsync(AttachmentBurial entity, CancellationToken cancellationToken)
        {   
            return await db.AttachmentBurials.AnyAsync(db =>
                    db.FileName == entity.FileName &&
                    db.FileType == entity.FileType &&
                    db.FilePath == entity.FilePath, 
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.AttachmentBurials.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<AttachmentBurial>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.AttachmentBurials.Include(db => db.Burial).ToListAsync(cancellationToken);
        }

        public async Task<AttachmentBurial> InsertAsync(AttachmentBurial entity, CancellationToken cancellationToken)
        {
            await db.AttachmentBurials.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<AttachmentBurial> UpdateAsync(AttachmentBurial entity, CancellationToken cancellationToken)
        {
            db.AttachmentBurials.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.AttachmentBurials.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
