using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Histories;

namespace PsuHistory.Business.DTO.Mappers
{
    class FormProfile : Profile
    {
        public FormProfile()
        {
            CreateMap<CreateForm, Form>()
                .ForMember(f => f.LastName, m => m.MapFrom(cf => cf.LastName))
                .ForMember(f => f.FirstName, m => m.MapFrom(cf => cf.FirstName))
                .ForMember(f => f.MiddleName, m => m.MapFrom(cf => cf.MiddleName))
                .ForMember(f => f.Files, m => m.MapFrom(cf => cf.Files));
            CreateMap<UpdateForm, Form>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.LastName, m => m.MapFrom(cf => cf.LastName))
                .ForMember(f => f.FirstName, m => m.MapFrom(cf => cf.FirstName))
                .ForMember(f => f.MiddleName, m => m.MapFrom(cf => cf.MiddleName))
                .ForMember(f => f.Files, m => m.MapFrom(cf => cf.Files));
        }
    }
}
