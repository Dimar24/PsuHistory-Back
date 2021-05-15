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
            CreateMap<CreateDutyStation, DutyStation>();
            CreateMap<UpdateDutyStation, DutyStation>();
        }
    }
}