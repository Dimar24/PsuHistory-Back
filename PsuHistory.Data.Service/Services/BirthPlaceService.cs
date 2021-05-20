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
    public interface IBirthPlaceService : IBaseService<Guid, BirthPlace>
    { }

    public class BirthPlaceService : IBirthPlaceService
    {
        private readonly PsuHistoryDbContext db;

        public BirthPlaceService(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<BirthPlace> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.BirthPlaces.FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<BirthPlace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.BirthPlaces.ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(BirthPlace entity, CancellationToken cancellationToken)
        {
            return await db.BirthPlaces.AnyAsync(db =>
                    db.Place == entity.Place,
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.BirthPlaces.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<BirthPlace> InsertAsync(BirthPlace entity, CancellationToken cancellationToken)
        {
            await db.BirthPlaces.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<BirthPlace> UpdateAsync(BirthPlace entity, CancellationToken cancellationToken)
        {
            db.BirthPlaces.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.BirthPlaces.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
