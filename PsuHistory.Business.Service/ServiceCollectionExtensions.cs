using Microsoft.Extensions.DependencyInjection;
using PsuHistory.Business.Service.BusinessServices;
using PsuHistory.Business.Service.Helpers;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Histories;
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
            services.AddScoped<IBaseValidation<Guid, Form>, FormValidation>();
            services.AddScoped<IBaseValidation<Guid, AttachmentForm>, AttachmentFormValidation>();
            services.AddScoped<IBaseValidation<Guid, Burial>, BurialValidation>();
            services.AddScoped<IBaseValidation<Guid, TypeBurial>, TypeBurialValidation>();
            services.AddScoped<IBaseValidation<Guid, AttachmentBurial>, AttachmentBurialValidation>();
            services.AddScoped<IBaseValidation<Guid, Victim>, VictimValidation>();
            services.AddScoped<IBaseValidation<Guid, TypeVictim>, TypeVictimValidation>();
            services.AddScoped<IBaseValidation<Guid, BirthPlace>, BirthPlaceValidation>();
            services.AddScoped<IBaseValidation<Guid, DutyStation>, DutyStationValidation>();
            services.AddScoped<IBaseValidation<Guid, ConscriptionPlace>, ConscriptionPlaceValidation>();
            //services.AddScoped<IBaseService<Guid, Role>, RoleService>();
            //services.AddScoped<IBaseService<Guid, User>, UserService>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseBusinessService<Guid, Form>, FormBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, AttachmentForm>, AttachmentFormBusinessService>();
            //services.AddScoped<IBaseBusinessService<Guid, Burial>, BurialBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, TypeBurial>, TypeBurialBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, AttachmentBurial>, AttachmentBurialBusinessService>();
            //services.AddScoped<IBaseBusinessService<Guid, Victim>, VictimBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, TypeVictim>, TypeVictimBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, BirthPlace>, BirthPlaceBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, DutyStation>, DutyStationBusinessService>();
            services.AddScoped<IBaseBusinessService<Guid, ConscriptionPlace>, ConscriptionPlaceBusinessService>();
            //services.AddScoped<IBaseBusinessService<Guid, Role>, RoleBusinessService>();
            //services.AddScoped<IBaseBusinessService<Guid, User>, UserBusinessService>();

            services.AddScoped<FileHelper>();
        }
    }
}
