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
            CreateMap<CreateAttachmentBurial, AttachmentBurial>();
            CreateMap<UpdateAttachmentBurial, AttachmentBurial>();
        }
    }
}
