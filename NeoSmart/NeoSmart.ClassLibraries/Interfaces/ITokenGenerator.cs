using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;

namespace NeoSmart.ClassLibraries.Interfaces
{
    public interface ITokenGenerator
    {
        TokenDTO GenerateTokenJwt(User user);
    }
}
