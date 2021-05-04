using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Abstraction;
using System;

namespace PsuHistory.Data.Service.Services
{
    public interface ITypeBurialService : IBaseService<Guid, TypeBurial>
    {
        //Task<> FindAsync();
        //Task<long> CountAsync();
        //Task<bool> ExistsAsync(TypeBurial entity);
    }

    class TypeBurialService : BaseService<Guid, TypeBurial>, ITypeBurialService
    {
        private readonly PsuHistoryDbContext _dbContext;

        public TypeBurialService(PsuHistoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<bool> ExistsAsync(TypeBurial entity, CancellationToken cancellationToken)
        //{
        //    return await _dbContext.TypeBurials.AnyAsync(en => en.Name == entity.Name, cancellationToken);
        //}
        //
        //public async Task<IReadOnlyCollection<TypeBurial>> FindAsync(TypeBurialSearchCondition searchCondition, string sortProperty)
        //{
        //    IQueryable<TypeBurial> query = BuildFindQuery(searchCondition);
        //
        //    query = searchCondition.ListSortDirection == ListSortDirection.Ascending
        //        ? query.OrderBy(sortProperty)
        //        : query.OrderByDescending(sortProperty);
        //
        //    return await query.Page(searchCondition.Page, searchCondition.PageSize).ToListAsync();
        //}
        //
        //public async Task<long> CountAsync(TypeBurialSearchCondition searchCondition)
        //{
        //    IQueryable<TypeBurial> query = BuildFindQuery(searchCondition);
        //
        //    return await query.LongCountAsync();
        //}

        //private IQueryable<TypeBurial> BuildFindQuery()
        //{
        //    IQueryable<TypeBurial> query = _dbContext.TypeBurials;
        //
        //    if (searchCondition.Name.Any())
        //    {
        //        foreach (var name in searchCondition.Name)
        //        {
        //            var upperName = name.ToUpper().Trim();
        //            query = query.Where(x =>
        //                x.Name != null && x.Name.ToUpper().Contains(upperName));
        //        }
        //    }
        //
        //    return query;
        //}
    }
}
