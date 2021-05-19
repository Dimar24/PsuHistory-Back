using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class AttachmentBurialProfile : Profile
    {
        public AttachmentBurialProfile()
        {
            CreateMap<CreateAttachmentBurial, AttachmentBurial>()
                .ForMember(f => f.BurialId, m => m.MapFrom(cf => cf.BurialId))
                .ForMember(f => f.File, m => m.MapFrom(cf => cf.File));
            CreateMap<UpdateAttachmentBurial, AttachmentBurial>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.BurialId, m => m.MapFrom(cf => cf.BurialId))
                .ForMember(f => f.File, m => m.MapFrom(cf => cf.File));
        }
    }
}
