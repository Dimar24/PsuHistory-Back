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
        private readonly PsuHistoryDbContext db;

        public VictimService(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<Victim> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Victims
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
            return await db.Victims.ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(Victim entity, CancellationToken cancellationToken)
        {
            return await db.Victims.Include(d => d.TypeVictim).Include(d => d.DutyStation).Include(d => d.BirthPlace)
                .Include(d => d.ConscriptionPlace).Include(d => d.Burial).AnyAsync(db =>
                    db.LastName == entity.LastName &&
                    db.FirstName == entity.FirstName &&
                    db.MiddleName == entity.MiddleName &&
                    db.IsHeroSoviet == entity.IsHeroSoviet &&
                    db.IsPartisan == entity.IsPartisan &&
                    db.DateOfBirth == entity.DateOfBirth &&
                    db.DateOfDeath == entity.DateOfDeath &&
                    db.TypeVictim.Name == entity.TypeVictim.Name &&
                    db.DutyStation.Place == entity.DutyStation.Place &&
                    db.BirthPlace.Place == entity.BirthPlace.Place &&
                    db.ConscriptionPlace.Place == entity.ConscriptionPlace.Place &&
                    db.Burial.Id == entity.Burial.Id,
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
