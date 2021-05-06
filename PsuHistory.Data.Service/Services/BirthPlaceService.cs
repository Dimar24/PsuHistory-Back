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
        private readonly PsuHistoryDbContext _dbContext;

        public BirthPlaceService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BirthPlace> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.BirthPlaces.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<BirthPlace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.BirthPlaces.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<BirthPlace> InsertAsync(BirthPlace entity, CancellationToken cancellationToken)
        {
            await _dbContext.BirthPlaces.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<BirthPlace> UpdateAsync(BirthPlace entity, CancellationToken cancellationToken)
        {
            _dbContext.BirthPlaces.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.BirthPlaces.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
