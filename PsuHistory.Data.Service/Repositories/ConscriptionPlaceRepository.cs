using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Repository.Repositories
{
    public interface IConscriptionPlaceRepository : IBaseRepository<Guid, ConscriptionPlace>
    { }

    public class ConscriptionPlaceRepository : IConscriptionPlaceRepository
    {
        private readonly PsuHistoryDbContext db;

        public ConscriptionPlaceRepository(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<ConscriptionPlace> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.ConscriptionPlaces.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ConscriptionPlace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.ConscriptionPlaces.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(ConscriptionPlace entity, CancellationToken cancellationToken)
        {
            return await db.ConscriptionPlaces.AnyAsync(db =>
                    db.Place == entity.Place,
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.ConscriptionPlaces.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<ConscriptionPlace> InsertAsync(ConscriptionPlace entity, CancellationToken cancellationToken)
        {
            await db.ConscriptionPlaces.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<ConscriptionPlace> UpdateAsync(ConscriptionPlace entity, CancellationToken cancellationToken)
        {
            db.ConscriptionPlaces.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.ConscriptionPlaces.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
