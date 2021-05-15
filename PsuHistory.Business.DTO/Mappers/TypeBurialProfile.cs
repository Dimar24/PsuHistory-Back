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
            CreateMap<CreateTypeBurial, TypeBurial>();
            CreateMap<UpdateTypeBurial, TypeBurial>();
        }
    }
}