using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class ConscriptionPlaceProfile : Profile
    {
        public ConscriptionPlaceProfile()
        {
            CreateMap<CreateConscriptionPlace, ConscriptionPlace>()
                .ForMember(f => f.Place, m => m.MapFrom(cf => cf.Place));
            CreateMap<UpdateConscriptionPlace, ConscriptionPlace>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.Place, m => m.MapFrom(cf => cf.Place));
        }
    }
}
