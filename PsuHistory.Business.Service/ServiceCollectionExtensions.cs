using Microsoft.Extensions.DependencyInjection;
using System;

namespace PsuHistory.Business.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPsuHistoryBusinessService(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            //services.AddScoped<IBaseService<Guid, AttachmentBurial>, AttachmentBurialService>();
            //services.AddScoped<IBaseService<Guid, AttachmentForm>, AttachmentFormService>();
            //services.AddScoped<IBaseService<Guid, BirthPlace>, BirthPlaceService>();
            //services.AddScoped<IBaseService<Guid, Burial>, BurialService>();
            //services.AddScoped<IBaseService<Guid, ConscriptionPlace>, ConscriptionPlaceService>();
            //services.AddScoped<IBaseService<Guid, DutyStation>, DutyStationService>();
            //services.AddScoped<IBaseService<Guid, Form>, FormService>();
            //services.AddScoped<IBaseService<Guid, Role>, RoleService>();
            //services.AddScoped<IBaseService<Guid, TypeBurial>, TypeBurialService>();
            //services.AddScoped<IBaseService<Guid, TypeVictim>, TypeVictimService>();
            //services.AddScoped<IBaseService<Guid, User>, UserService>();
            //services.AddScoped<IBaseService<Guid, Victim>, VictimService>();

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
