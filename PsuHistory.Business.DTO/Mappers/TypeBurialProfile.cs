using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class TypeBurialProfile : Profile
    {
        public TypeBurialProfile()
        {
            CreateMap<CreateTypeBurial, TypeBurial>()
                .ForMember(f => f.Name, m => m.MapFrom(cf => cf.Name));
            CreateMap<UpdateTypeBurial, TypeBurial>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.Name, m => m.MapFrom(cf => cf.Name));
        }
    }
}