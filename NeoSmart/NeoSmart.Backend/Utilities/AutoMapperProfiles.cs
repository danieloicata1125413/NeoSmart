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

            CreateMap<Request, RequestDTO>();
            CreateMap<RequestDTO, Request>();

            CreateMap<Topic, TopicDTO>();
            CreateMap<TopicDTO, Topic>();

            CreateMap<TrainingExam, TrainingExamDTO>();
            CreateMap<TrainingExamDTO, TrainingExam>();

            CreateMap<TrainingExamQuestion, TrainingExamQuestionDTO>();
            CreateMap<TrainingExamQuestionDTO, TrainingExamQuestion>();

            CreateMap<SessionExam, SessionExamDTO>();
            CreateMap<SessionExamDTO, SessionExam>();

        }
    }
}
