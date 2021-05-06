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
    public interface ITypeVictimService : IBaseService<Guid, TypeVictim>
    { }

    public class TypeVictimService : ITypeVictimService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public TypeVictimService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TypeVictim> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.TypeVictims.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TypeVictim>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.TypeVictims.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TypeVictim> InsertAsync(TypeVictim entity, CancellationToken cancellationToken)
        {
            await _dbContext.TypeVictims.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TypeVictim> UpdateAsync(TypeVictim entity, CancellationToken cancellationToken)
        {
            _dbContext.TypeVictims.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.TypeVictims.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
