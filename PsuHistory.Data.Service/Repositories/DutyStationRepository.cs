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
    public interface IDutyStationRepository : IBaseRepository<Guid, DutyStation>
    { }

    public class DutyStationRepository : IDutyStationRepository
    {
        private readonly PsuHistoryDbContext db;

        public DutyStationRepository(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<DutyStation> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.DutyStations.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<DutyStation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.DutyStations.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.DutyStations.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<bool> ExistAsync(DutyStation entity, CancellationToken cancellationToken)
        {
            return await db.DutyStations.AnyAsync(db =>
                    db.Place == entity.Place,
                    cancellationToken);
        }

        public async Task<DutyStation> InsertAsync(DutyStation entity, CancellationToken cancellationToken)
        {
            await db.DutyStations.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<DutyStation> UpdateAsync(DutyStation entity, CancellationToken cancellationToken)
        {
            db.DutyStations.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.DutyStations.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
