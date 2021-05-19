using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Business.DTO.Mappers
{
    class VictimProfile : Profile
    {
        public VictimProfile()
        {
            CreateMap<CreateVictim, Victim>()
                .ForMember(f => f.LastName, m => m.MapFrom(cf => cf.LastName))
                .ForMember(f => f.FirstName, m => m.MapFrom(cf => cf.FirstName))
                .ForMember(f => f.MiddleName, m => m.MapFrom(cf => cf.MiddleName))
                .ForMember(f => f.IsHeroSoviet, m => m.MapFrom(cf => cf.IsHeroSoviet))
                .ForMember(f => f.IsPartisan, m => m.MapFrom(cf => cf.IsPartisan))
                .ForMember(f => f.DateOfBirth, m => m.MapFrom(cf => cf.DateOfBirth))
                .ForMember(f => f.DateOfDeath, m => m.MapFrom(cf => cf.DateOfDeath))
                .ForMember(f => f.TypeVictimId, m => m.MapFrom(cf => cf.TypeVictimId))
                .ForMember(f => f.DutyStationId, m => m.MapFrom(cf => cf.DutyStationId))
                .ForMember(f => f.BirthPlaceId, m => m.MapFrom(cf => cf.BirthPlaceId))
                .ForMember(f => f.ConscriptionPlaceId, m => m.MapFrom(cf => cf.ConscriptionPlaceId))
                .ForMember(f => f.BurialId, m => m.MapFrom(cf => cf.BurialId));
            CreateMap<UpdateVictim, Victim>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.LastName, m => m.MapFrom(cf => cf.LastName))
                .ForMember(f => f.FirstName, m => m.MapFrom(cf => cf.FirstName))
                .ForMember(f => f.MiddleName, m => m.MapFrom(cf => cf.MiddleName))
                .ForMember(f => f.IsHeroSoviet, m => m.MapFrom(cf => cf.IsHeroSoviet))
                .ForMember(f => f.IsPartisan, m => m.MapFrom(cf => cf.IsPartisan))
                .ForMember(f => f.DateOfBirth, m => m.MapFrom(cf => cf.DateOfBirth))
                .ForMember(f => f.DateOfDeath, m => m.MapFrom(cf => cf.DateOfDeath))
                .ForMember(f => f.TypeVictimId, m => m.MapFrom(cf => cf.TypeVictimId))
                .ForMember(f => f.DutyStationId, m => m.MapFrom(cf => cf.DutyStationId))
                .ForMember(f => f.BirthPlaceId, m => m.MapFrom(cf => cf.BirthPlaceId))
                .ForMember(f => f.ConscriptionPlaceId, m => m.MapFrom(cf => cf.ConscriptionPlaceId))
                .ForMember(f => f.BurialId, m => m.MapFrom(cf => cf.BurialId));
        }
    }
}
