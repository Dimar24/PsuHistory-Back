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
        private readonly PsuHistoryDbContext _dbContext;

        public DutyStationService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DutyStation> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.DutyStations.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<DutyStation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.DutyStations.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<DutyStation> InsertAsync(DutyStation entity, CancellationToken cancellationToken)
        {
            await _dbContext.DutyStations.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<DutyStation> UpdateAsync(DutyStation entity, CancellationToken cancellationToken)
        {
            _dbContext.DutyStations.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.DutyStations.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
