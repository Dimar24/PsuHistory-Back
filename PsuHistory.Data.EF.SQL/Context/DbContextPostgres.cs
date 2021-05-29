using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PsuHistory.Data.EF.SQL.Context
{
    public class DbContextPostgres : DbContextBase
    {
        public DbContextPostgres(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=psuhistorydb;Username=admin;Password=admin1234");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
