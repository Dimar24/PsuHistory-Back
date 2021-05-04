using Microsoft.Extensions.DependencyInjection;

namespace PsuHistory.Data.EF.SQL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPsuHistoryServiceDataAccess(this IServiceCollection services)
        {
            services.AddPSUHistoryServiceDbContext();
        }

        private static void AddPSUHistoryServiceDbContext(this IServiceCollection services)
        {
            services.AddScoped<PsuHistoryDbContext>();
        }
    }
}
