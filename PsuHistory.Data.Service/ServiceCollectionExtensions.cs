using Microsoft.Extensions.DependencyInjection;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Data.Repository.Repositories;
using System;

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
            services.AddScoped<IBaseRepository <Guid, AttachmentBurial>, AttachmentBurialRepository>();
            services.AddScoped<IBaseRepository<Guid, AttachmentForm>, AttachmentFormRepository>();
            services.AddScoped<IBaseRepository<Guid, BirthPlace>, BirthPlaceRepository>();
            services.AddScoped<IBaseRepository<Guid, Burial>, BurialRepository>();
            services.AddScoped<IBaseRepository<Guid, ConscriptionPlace>, ConscriptionPlaceRepository>();
            services.AddScoped<IBaseRepository<Guid, DutyStation>, DutyStationRepository>();
            services.AddScoped<IBaseRepository<Guid, Form>, FormRepository>();
            services.AddScoped<IBaseRepository<Guid, Role>, RoleRepository>();
            services.AddScoped<IBaseRepository<Guid, TypeBurial>, TypeBurialRepository>();
            services.AddScoped<IBaseRepository<Guid, TypeVictim>, TypeVictimRepository>();
            services.AddScoped<IBaseRepository<Guid, User>, UserRepository>();
            services.AddScoped<IBaseRepository<Guid, Victim>, VictimRepository>();

            //var currentAssembly = typeof(ServiceCollectionExtensions);
            //
            //services.Scan(scan => scan.FromAssembliesOf(currentAssembly)
            //    .AddClasses(classes => classes.AssignableTo(typeof(IBaseService<,>)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime()
            //);
        }
    }
}
