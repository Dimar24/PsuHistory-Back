using Microsoft.Extensions.DependencyInjection;
using PsuHistory.Business.DTO.Models.AccountDataModels;
using PsuHistory.Business.Service.Helpers;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Services;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Domain.Models.Users;
using System;

namespace PsuHistory.Business.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPsuHistoryBusinessLogic(this IServiceCollection services)
        {
            services.AddValidationServices();
            services.AddServices();
            services.AddHelpers();
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
            services.AddScoped<IBaseValidation<Guid, User>, UserValidation>();
            services.AddScoped<IBaseAccoutValidation<Login>, AccountValidation>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseService<Guid, Form>, FormService>();
            services.AddScoped<IBaseService<Guid, AttachmentForm>, AttachmentFormService>();
            services.AddScoped<IBaseService<Guid, Burial>, BurialService>();
            services.AddScoped<IBaseService<Guid, TypeBurial>, TypeBurialService>();
            services.AddScoped<IBaseService<Guid, AttachmentBurial>, AttachmentBurialService>();
            services.AddScoped<IBaseService<Guid, Victim>, VictimService>();
            services.AddScoped<IBaseService<Guid, TypeVictim>, TypeVictimService>();
            services.AddScoped<IBaseService<Guid, BirthPlace>, BirthPlaceService>();
            services.AddScoped<IBaseService<Guid, DutyStation>, DutyStationService>();
            services.AddScoped<IBaseService<Guid, ConscriptionPlace>, ConscriptionPlaceService>();
            services.AddScoped<IBaseService<Guid, User>, UserService>();
            services.AddScoped<IBaseAccoutService<Login>, AccountService>();
        }

        private static void AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<FileHelper>();
        }
    }
}
