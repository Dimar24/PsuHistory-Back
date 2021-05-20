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
    //public interface ITypeBurialService : IBaseService<Guid, TypeBurial>
    //{
    //    Task<> FindAsync();
    //    Task<long> CountAsync();
    //    Task<bool> ExistsAsync(TypeBurial entity);
    //}
    //
    //class TypeBurialService : BaseService<Guid, TypeBurial>, ITypeBurialService
    //{
    //    private readonly PsuHistoryDbContext _dbContext;
    //
    //    public TypeBurialService(PsuHistoryDbContext dbContext) : base(dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }
    //
    //    public async Task<bool> ExistsAsync(TypeBurial entity, CancellationToken cancellationToken)
    //    {
    //        return await _dbContext.TypeBurials.AnyAsync(en => en.Name == entity.Name, cancellationToken);
    //    }
    //    
    //    public async Task<IReadOnlyCollection<TypeBurial>> FindAsync(TypeBurialSearchCondition searchCondition, string sortProperty)
    //    {
    //        IQueryable<TypeBurial> query = BuildFindQuery(searchCondition);
    //    
    //        query = searchCondition.ListSortDirection == ListSortDirection.Ascending
    //            ? query.OrderBy(sortProperty)
    //            : query.OrderByDescending(sortProperty);
    //    
    //        return await query.Page(searchCondition.Page, searchCondition.PageSize).ToListAsync();
    //    }
    //    
    //    public async Task<long> CountAsync(TypeBurialSearchCondition searchCondition)
    //    {
    //        IQueryable<TypeBurial> query = BuildFindQuery(searchCondition);
    //    
    //        return await query.LongCountAsync();
    //    }
    //
    //    private IQueryable<TypeBurial> BuildFindQuery()
    //    {
    //        IQueryable<TypeBurial> query = _dbContext.TypeBurials;
    //    
    //        if (searchCondition.Name.Any())
    //        {
    //            foreach (var name in searchCondition.Name)
    //            {
    //                var upperName = name.ToUpper().Trim();
    //                query = query.Where(x =>
    //                    x.Name != null && x.Name.ToUpper().Contains(upperName));
    //            }
    //        }
    //    
    //        return query;
    //    }
    //}

    public interface ITypeBurialService : IBaseService<Guid, TypeBurial>
    { }

    public class TypeBurialService : ITypeBurialService
    {
        private readonly PsuHistoryDbContext db;

        public TypeBurialService(PsuHistoryDbContext db)
        {
            this.db = db;
        }

        public async Task<TypeBurial> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.TypeBurials.FirstOrDefaultAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TypeBurial>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await db.TypeBurials.ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistAsync(TypeBurial entity, CancellationToken cancellationToken)
        {
            return await db.TypeBurials.AnyAsync(db =>
                    db.Name == entity.Name,
                    cancellationToken);
        }

        public async Task<bool> ExistByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await db.TypeBurials.AnyAsync(db => db.Id == id, cancellationToken);
        }

        public async Task<TypeBurial> InsertAsync(TypeBurial entity, CancellationToken cancellationToken)
        {
            await db.TypeBurials.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TypeBurial> UpdateAsync(TypeBurial entity, CancellationToken cancellationToken)
        {
            db.TypeBurials.Update(entity);
            await db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity is not null)
            {
                db.TypeBurials.Remove(entity);
                await db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
