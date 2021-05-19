using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class BurialProfile : Profile
    {
        public BurialProfile()
        {
            CreateMap<CreateBurial, Burial>()
                .ForMember(f => f.NumberBurial, m => m.MapFrom(cf => cf.NumberBurial))
                .ForMember(f => f.Location, m => m.MapFrom(cf => cf.Location))
                .ForMember(f => f.NumberPeople, m => m.MapFrom(cf => cf.NumberPeople))
                .ForMember(f => f.UnknownNumber, m => m.MapFrom(cf => cf.UnknownNumber))
                .ForMember(f => f.Year, m => m.MapFrom(cf => cf.Year))
                .ForMember(f => f.Latitude, m => m.MapFrom(cf => cf.Latitude))
                .ForMember(f => f.Longitude, m => m.MapFrom(cf => cf.Longitude))
                .ForMember(f => f.Description, m => m.MapFrom(cf => cf.Description))
                .ForMember(f => f.TypeBurialId, m => m.MapFrom(cf => cf.TypeBurialId))
                .ForMember(f => f.Files, m => m.MapFrom(cf => cf.Files));
            CreateMap<UpdateBurial, Burial>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.NumberBurial, m => m.MapFrom(cf => cf.NumberBurial))
                .ForMember(f => f.Location, m => m.MapFrom(cf => cf.Location))
                .ForMember(f => f.NumberPeople, m => m.MapFrom(cf => cf.NumberPeople))
                .ForMember(f => f.UnknownNumber, m => m.MapFrom(cf => cf.UnknownNumber))
                .ForMember(f => f.Year, m => m.MapFrom(cf => cf.Year))
                .ForMember(f => f.Latitude, m => m.MapFrom(cf => cf.Latitude))
                .ForMember(f => f.Longitude, m => m.MapFrom(cf => cf.Longitude))
                .ForMember(f => f.Description, m => m.MapFrom(cf => cf.Description))
                .ForMember(f => f.TypeBurialId, m => m.MapFrom(cf => cf.TypeBurialId))
                .ForMember(f => f.Files, m => m.MapFrom(cf => cf.Files));
        }
    }
}
