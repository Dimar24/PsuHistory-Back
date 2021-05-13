using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PsuHistory.Data.Service.Services
{
    public interface IRoleService : IBaseService<Guid, Role>
    { }

    public class RoleService : IRoleService
    {
        private readonly PsuHistoryDbContext db;

        public RoleService(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<Role> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Roles.FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.Roles.ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(Role entity, CancellationToken cancellationToken)
        {
            return await db.TypeVictims.AnyAsync(db =>
                    db.Name == entity.Name,
                    cancellationToken);
        }

        public async Task<Role> InsertAsync(Role entity, CancellationToken cancellationToken)
        {
            await db.Roles.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Role> UpdateAsync(Role entity, CancellationToken cancellationToken)
        {
            db.Roles.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.Roles.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
