using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class BirthPlaceProfile : Profile
    {
        public BirthPlaceProfile()
        {
            CreateMap<CreateBirthPlace, BirthPlace>();
            CreateMap<UpdateBirthPlace, BirthPlace>();
        }
    }
}
