using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Responses;

namespace NeoSmart.BackEnd.Interfaces
{
    public interface ISessionInscriptionExamHelper
    {
        Task<Response<bool>> ProcessSessionInscriptionExamAsync(User user, SessionInscriptionExamDTO sessionInscriptionExamDTO);
    }
}
