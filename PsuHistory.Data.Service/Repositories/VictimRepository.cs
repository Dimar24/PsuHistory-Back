using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL.Context;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Repository.Repositories
{
    public interface IVictimRepository : IBaseRepository<Guid, Victim>
    { }

    public class VictimRepository : IVictimRepository
    {
        private readonly DbContextBase db;

        public VictimRepository(DbContextBase db)
        {
            this.db = db;
        }

        public async Task<Victim> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Victims.AsNoTracking()
                .Include(db => db.TypeVictim)
                .Include(db => db.DutyStation)
                .Include(db => db.BirthPlace)
                .Include(db => db.ConscriptionPlace)
                .Include(db => db.Burial)
                .ThenInclude(db => db.TypeBurial)
                .FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Victim>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.Victims.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(Victim entity, CancellationToken cancellationToken)
        {
            return await db.Victims.AnyAsync(db =>
                    db.LastName == entity.LastName &&
                    db.FirstName == entity.FirstName &&
                    db.MiddleName == entity.MiddleName &&
                    db.IsHeroSoviet == entity.IsHeroSoviet &&
                    db.IsPartisan == entity.IsPartisan &&
                    db.DateOfBirth == entity.DateOfBirth &&
                    db.DateOfDeath == entity.DateOfDeath &&
                    db.TypeVictimId == entity.TypeVictimId &&
                    db.DutyStationId == entity.DutyStationId &&
                    db.BirthPlaceId == entity.BirthPlaceId &&
                    db.ConscriptionPlaceId == entity.ConscriptionPlaceId &&
                    db.BurialId == entity.BurialId,
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Victims.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<Victim> InsertAsync(Victim entity, CancellationToken cancellationToken)
        {
            await db.Victims.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Victim> UpdateAsync(Victim entity, CancellationToken cancellationToken)
        {
            db.Victims.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.Victims.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
