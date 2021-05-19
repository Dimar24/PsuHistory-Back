using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class BirthPlaceProfile : Profile
    {
        public BirthPlaceProfile()
        {
            CreateMap<CreateBirthPlace, BirthPlace>()
                .ForMember(f => f.Place, m => m.MapFrom(cf => cf.Place));
            CreateMap<UpdateBirthPlace, BirthPlace>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.Place, m => m.MapFrom(cf => cf.Place));
        }
    }
}
