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
    public interface IDutyStationService : IBaseService<Guid, DutyStation>
    { }

    public class DutyStationService : IDutyStationService
    {
        private readonly PsuHistoryDbContext db;

        public DutyStationService(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<DutyStation> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.DutyStations.FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<DutyStation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.DutyStations.ToListAsync(cancellationToken);
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
