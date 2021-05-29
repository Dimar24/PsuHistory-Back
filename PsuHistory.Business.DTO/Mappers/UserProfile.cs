using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Users;

namespace PsuHistory.Business.DTO.Mappers
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUser, User>()
                .ForMember(f => f.Mail, m => m.MapFrom(cf => cf.Mail))
                .ForMember(f => f.Password, m => m.MapFrom(cf => cf.Password));
            CreateMap<UpdateUser, User>()
                .ForMember(f => f.Id, m => m.MapFrom(cf => cf.Id))
                .ForMember(f => f.Mail, m => m.MapFrom(cf => cf.Mail))
                .ForMember(f => f.Password, m => m.MapFrom(cf => cf.Password));
        }
    }
}
