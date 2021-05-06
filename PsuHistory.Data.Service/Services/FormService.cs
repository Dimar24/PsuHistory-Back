using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Service.Services
{
    public interface IFormService : IBaseService<Guid, Form>
    { }

    public class FormService : IFormService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public FormService(PsuHistoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Form> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Forms.AsNoTracking().FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Form>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Forms.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Form> InsertAsync(Form entity, CancellationToken cancellationToken)
        {
            await _dbContext.Forms.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Form> UpdateAsync(Form entity, CancellationToken cancellationToken)
        {
            _dbContext.Forms.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbContext.Forms.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
