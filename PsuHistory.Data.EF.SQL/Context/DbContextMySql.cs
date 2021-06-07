using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PsuHistory.Data.EF.SQL.Context
{
    public class DbContextMySql : DbContextBase
    {
        public DbContextMySql(DbContextOptions options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=psuhistorydb;", 
                ServerVersion.AutoDetect("server=localhost;user=root;password=root;database=psuhistorydb;"));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
