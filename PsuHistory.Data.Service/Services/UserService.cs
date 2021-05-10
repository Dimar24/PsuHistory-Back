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
    public interface IUserService : IBaseService<Guid, User>
    { }

    public class UserService : IUserService
    {
        private readonly PsuHistoryDbContext db;

        public UserService(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.Users.Include(db => db.Role).FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.Users.Include(db => db.Role).ToListAsync(cancellationToken);
        }

        public async Task<User> InsertAsync(User entity, CancellationToken cancellationToken)
        {
            await db.Users.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken)
        {
            db.Users.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.Users.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
