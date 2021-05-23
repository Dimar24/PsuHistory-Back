using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Repository.Repositories
{
    public interface ITypeVictimRepository : IBaseRepository<Guid, TypeVictim>
    { }

    public class TypeVictimRepository : ITypeVictimRepository
    {
        private readonly PsuHistoryDbContext db;

        public TypeVictimRepository(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<TypeVictim> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.TypeVictims.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TypeVictim>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.TypeVictims.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(TypeVictim entity, CancellationToken cancellationToken)
        {
            return await db.TypeVictims.AnyAsync(db =>
                    db.Name == entity.Name,
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.TypeVictims.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<TypeVictim> InsertAsync(TypeVictim entity, CancellationToken cancellationToken)
        {
            await db.TypeVictims.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TypeVictim> UpdateAsync(TypeVictim entity, CancellationToken cancellationToken)
        {
            db.TypeVictims.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.TypeVictims.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
