using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Histories;

namespace PsuHistory.Business.DTO.Mappers
{
    class AttachmentFormProfile : Profile
    {
        public AttachmentFormProfile()
        {
            CreateMap<CreateAttachmentForm, AttachmentForm>()
                .ForMember(af => af.FormId, m => m.MapFrom(caf => caf.FormId))
                .ForMember(af => af.File, m => m.MapFrom(caf => caf.File));
            CreateMap<UpdateAttachmentForm, AttachmentForm>()
                .ForMember(af => af.Id, m => m.MapFrom(caf => caf.Id))
                .ForMember(af => af.FormId, m => m.MapFrom(caf => caf.FormId))
                .ForMember(af => af.File, m => m.MapFrom(caf => caf.File));
        }
    }
}
