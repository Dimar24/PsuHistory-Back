using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PsuHistory.Data.EF.SQL.Context;

namespace PsuHistory.Data.EF.SQL.ContextFactory
{
    public class DbContextFactoryMySql : DbContextFactoryBase, IDesignTimeDbContextFactory<DbContextMySql>
    {
        public DbContextMySql CreateDbContext(string[] args)
        {
            optionsBuilder = new DbContextOptionsBuilder<DbContextMySql>();

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseMySql(GetConnectionString("MySqlConnection"),
                ServerVersion.AutoDetect(GetConnectionString("MySqlConnection")));

            return new DbContextMySql(optionsBuilder.Options);
        }
    }
}
