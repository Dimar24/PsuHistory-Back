using Microsoft.Extensions.DependencyInjection;
using PsuHistory.Business.Service.BusinessServices;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Monuments;
using System;

namespace PsuHistory.Business.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPsuHistoryBusinessService(this IServiceCollection services)
        {
            services.AddValidationServices();
            services.AddServices();
        }

        private static void AddValidationServices(this IServiceCollection services)
        {
            //services.AddScoped<IBaseService<Guid, AttachmentBurial>, AttachmentBurialService>();
            //services.AddScoped<IBaseService<Guid, AttachmentForm>, AttachmentFormService>();
            services.AddTransient<IBaseValidation<Guid, BirthPlace>, BirthPlaceValidation>();
            //services.AddScoped<IBaseService<Guid, Burial>, BurialService>();
            services.AddScoped<IBaseValidation<Guid, ConscriptionPlace>, ConscriptionPlaceValidation>();
            services.AddScoped<IBaseValidation<Guid, DutyStation>, DutyStationValidation>();
            //services.AddScoped<IBaseService<Guid, Form>, FormService>();
            //services.AddScoped<IBaseService<Guid, Role>, RoleService>();
            services.AddScoped<IBaseValidation<Guid, TypeBurial>, TypeBurialValidation>();
            services.AddScoped<IBaseValidation<Guid, TypeVictim>, TypeVictimValidation>();
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

        private static void AddServices(this IServiceCollection services)
        {
            //services.AddScoped<IBaseService<Guid, AttachmentBurial>, AttachmentBurialService>();
            //services.AddScoped<IBaseService<Guid, AttachmentForm>, AttachmentFormService>();
            services.AddScoped<IBaseBusinessService<Guid, BirthPlace>, BirthPlaceBusinessService>();
            //services.AddScoped<IBaseService<Guid, Burial>, BurialService>();
            services.AddScoped<IBaseBusinessService<Guid, ConscriptionPlace>, ConscriptionPlaceBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, DutyStation>, DutyStationBusinessService>();
            //services.AddScoped<IBaseService<Guid, Form>, FormService>();
            //services.AddScoped<IBaseService<Guid, Role>, RoleService>();
            services.AddScoped<IBaseBusinessService<Guid, TypeBurial>, TypeBurialBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, TypeVictim>, TypeVictimBusinessService>();
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
