using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PsuHistory.Data.EF.SQL.Context
{
    public class DbContextMsSql : DbContextBase
    {
        public DbContextMsSql(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlServer("Server=database, 1433; Initial Catalog=psuhistorydb; User ID=SA; Password=Pa55w0rd2021;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
