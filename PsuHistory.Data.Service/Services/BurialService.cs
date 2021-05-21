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
        private readonly PsuHistoryDbContext db;

        public BurialService(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<Burial> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Burials.AsNoTracking().Include(db => db.TypeBurial).FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Burial>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.Burials.AsNoTracking().Include(db => db.TypeBurial).ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(Burial entity, CancellationToken cancellationToken)
        {
            return await db.Burials.Include(d => d.TypeBurial).AnyAsync(db =>
                    db.NumberBurial == entity.NumberBurial &&
                    db.Location == entity.Location &&
                    db.KnownNumber == entity.KnownNumber &&
                    db.UnknownNumber == entity.UnknownNumber &&
                    db.Year == entity.Year &&
                    db.Latitude == entity.Latitude &&
                    db.Longitude == entity.Longitude &&
                    db.Description == entity.Description &&
                    db.TypeBurial.Name == entity.TypeBurial.Name,
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Burials.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<Burial> InsertAsync(Burial entity, CancellationToken cancellationToken)
        {
            await db.Burials.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Burial> UpdateAsync(Burial entity, CancellationToken cancellationToken)
        {
            db.Burials.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.Burials.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
