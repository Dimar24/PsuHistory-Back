using Microsoft.Extensions.DependencyInjection;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Interfaces;

namespace PsuHistory.Data.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPsuHistoryDataService(this IServiceCollection services)
        {
            services.AddPsuHistoryServiceDataAccess();
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            var currentAssembly = typeof(ServiceCollectionExtensions);

            services.Scan(scan => scan.FromAssembliesOf(currentAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IBaseService<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}
