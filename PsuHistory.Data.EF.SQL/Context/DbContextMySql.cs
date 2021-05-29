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
            optionsBuilder.UseMySql("Server=DESKTOP-PHD804P;Database=psuhistorydb;", 
                ServerVersion.AutoDetect("Server=DESKTOP-PHD804P;Database=psuhistorydb;"));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
