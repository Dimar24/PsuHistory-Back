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
    public interface IBurialService : IBaseService<Guid, Burial>
    { }

    public class BurialService : IBurialService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public BurialService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Burial> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Burials.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Burial>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Burials.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Burial> InsertAsync(Burial entity, CancellationToken cancellationToken)
        {
            await _dbContext.Burials.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Burial> UpdateAsync(Burial entity, CancellationToken cancellationToken)
        {
            _dbContext.Burials.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.Burials.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
