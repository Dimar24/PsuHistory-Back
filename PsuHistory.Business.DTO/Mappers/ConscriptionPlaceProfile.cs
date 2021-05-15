using AutoMapper;
using PsuHistory.Business.DTO.Models.CreateDataModels;
using PsuHistory.Business.DTO.Models.UpdateDataModels;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Business.DTO.Mappers
{
    class ConscriptionPlaceProfile : Profile
    {
        public ConscriptionPlaceProfile()
        {
            CreateMap<CreateConscriptionPlace, ConscriptionPlace>();
            CreateMap<UpdateConscriptionPlace, ConscriptionPlace>();
        }
    }
}
