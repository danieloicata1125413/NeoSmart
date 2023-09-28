using NeoSmart.ClassLibraries.Entities;
using Microsoft.AspNetCore.Identity;
using NeoSmart.ClassLibraries.DTOs;

namespace NeoSmart.Backend.Interfaces
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<User> GetUserByUserNameAsync(string username);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(DtoAccount model);

        Task LogoutAsync();
    }
}
