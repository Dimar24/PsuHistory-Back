using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Service.Services
{
    public interface IVictimService : IBaseService<Guid, Victim>
    { }

    public class VictimService : IVictimService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public VictimService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Victim> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Victims.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Victim>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Victims.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Victim> InsertAsync(Victim entity, CancellationToken cancellationToken)
        {
            await _dbContext.Victims.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Victim> UpdateAsync(Victim entity, CancellationToken cancellationToken)
        {
            _dbContext.Victims.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.Victims.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
