using AutoMapper;
using dotNet_GMZ_backend.Models.DTOModels;
using dotNet_GMZ_backend.Models.IdentityModels;
using dotNet_GMZ_backend.Models.Models;
using dotNet_GMZ_backend.Models.ModelsDTO;
using System.Collections.Generic;

namespace dotNet_GMZ_backend.Services.MapperService
{
    public class AutoMapperApp : Profile
    {
        public AutoMapperApp()
        {
            CreateMap<CreateNewsRecordDTO, NewsRecord>();
            CreateMap<NewsRecord, CreateNewsRecordDTO>();
            CreateMap<NewsRecord, FoundNewsRecordDTO>();
            CreateMap<IEnumerable<NewsRecord>, IEnumerable<FindNewsRecordDTO>>();
            CreateMap<UserRegisterDto, UserApp>();
        }
    }
}