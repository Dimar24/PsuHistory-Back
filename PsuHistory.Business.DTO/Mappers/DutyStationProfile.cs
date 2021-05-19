using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class DutyStationProfile : Profile
    {
        public DutyStationProfile()
        {
            CreateMap<CreateDutyStation, DutyStation>()
                .ForMember(f => f.Place, m => m.MapFrom(cf => cf.Place));
            CreateMap<UpdateDutyStation, DutyStation>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.Place, m => m.MapFrom(cf => cf.Place));
        }
    }
}