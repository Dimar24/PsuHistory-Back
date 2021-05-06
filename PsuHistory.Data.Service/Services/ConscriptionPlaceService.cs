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
    public interface IConscriptionPlaceService : IBaseService<Guid, ConscriptionPlace>
    { }

    public class ConscriptionPlaceService : IConscriptionPlaceService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public ConscriptionPlaceService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ConscriptionPlace> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.ConscriptionPlaces.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ConscriptionPlace>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.ConscriptionPlaces.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<ConscriptionPlace> InsertAsync(ConscriptionPlace entity, CancellationToken cancellationToken)
        {
            await _dbContext.ConscriptionPlaces.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<ConscriptionPlace> UpdateAsync(ConscriptionPlace entity, CancellationToken cancellationToken)
        {
            _dbContext.ConscriptionPlaces.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.ConscriptionPlaces.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
