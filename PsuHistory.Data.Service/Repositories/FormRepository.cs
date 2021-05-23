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
    public interface IFormRepository : IBaseRepository<Guid, Form>
    { }

    public class FormRepository : IFormRepository
    {
        private readonly PsuHistoryDbContext db;

        public FormRepository(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<Form> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Forms.AsNoTracking().Include(db => db.AttachmentForms).FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Form>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.Forms.AsNoTracking().Include(db => db.AttachmentForms).ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(Form entity, CancellationToken cancellationToken)
        {
            return await db.Forms.AnyAsync(db =>
                    db.FirstName == entity.FirstName &&
                    db.LastName == entity.LastName &&
                    db.MiddleName == entity.MiddleName,
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Forms.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<Form> InsertAsync(Form entity, CancellationToken cancellationToken)
        {
            await db.Forms.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Form> UpdateAsync(Form entity, CancellationToken cancellationToken)
        {
            db.Forms.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.Forms.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
