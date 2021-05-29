using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PsuHistory.Data.EF.SQL.Context;

namespace PsuHistory.Data.EF.SQL.ContextFactory
{
    public class DbContextFactoryMsSql : DbContextFactoryBase, IDesignTimeDbContextFactory<DbContextMsSql>
    {
        public DbContextMsSql CreateDbContext(string[] args)
        {
            optionsBuilder = new DbContextOptionsBuilder<DbContextMsSql>();

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlServer(GetConnectionString("MsSqlConnection"));

            return new DbContextMsSql(optionsBuilder.Options);
        }
    }
}
