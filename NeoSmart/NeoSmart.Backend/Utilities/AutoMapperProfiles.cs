using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;

namespace NeoSmart.BackEnd.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}
