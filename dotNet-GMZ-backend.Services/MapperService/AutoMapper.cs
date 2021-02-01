using AutoMapper;
using dotNet_GMZ_backend.Models.DTOModels;
using dotNet_GMZ_backend.Models.Models;
using dotNet_GMZ_backend.Models.ModelsDTO;

namespace dotNet_GMZ_backend.Services.MapperService
{
    public class AutoMapperApp : Profile
    {
        public AutoMapperApp()
        {
            CreateMap<CreateNewsRecordDTO, NewsRecord>();
            CreateMap<NewsRecord, CreateNewsRecordDTO>();
            CreateMap<NewsRecord, FoundNewsRecordDTO>();
        }
    }
}