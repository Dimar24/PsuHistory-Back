using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class TypeVictimProfile : Profile
    {
        public TypeVictimProfile()
        {
            CreateMap<CreateTypeVictim, TypeVictim>()
                .ForMember(f => f.Name, m => m.MapFrom(cf => cf.Name));
            CreateMap<UpdateTypeVictim, TypeVictim>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.Name, m => m.MapFrom(cf => cf.Name));
        }
    }
}