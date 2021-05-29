using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PsuHistory.Data.EF.SQL.Context;

namespace PsuHistory.Data.EF.SQL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPsuHistoryDbContext(this IServiceCollection services, string typeDatabase, IConfiguration configuration)
        {
            services.AddPsuHistoryServiceDbContext(typeDatabase, configuration);
        }

        private static void AddPsuHistoryServiceDbContext(this IServiceCollection services, string typeDatabase, IConfiguration configuration)
        {
            switch(typeDatabase)
            {
                case "MsSql": services.AddDbContext<DbContextMsSql>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)).AddScoped<DbContextBase, DbContextMsSql>(); break;
                case "MySql": services.AddDbContext<DbContextMySql>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)).AddScoped<DbContextBase, DbContextMySql>(); break;
                case "Postgres": services.AddDbContext<DbContextPostgres>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)).AddScoped<DbContextBase, DbContextPostgres>(); break;
            }
        }
    }
}
