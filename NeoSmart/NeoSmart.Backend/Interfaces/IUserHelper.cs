using NeoSmart.ClassLibraries.Entities;
using Microsoft.AspNetCore.Identity;
using NeoSmart.ClassLibraries.DTOs;

namespace NeoSmart.BackEnd.Interfaces
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<IdentityResult> RemoveUserAsync(User user);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<IdentityResult> AddUserToRoleAsync(User user, List<string> rolesName);

        Task RemoveUserToRoleAsync(User user, string roleName);

        Task<IdentityResult> RemoveUserToRoleAsync(User user, List<string> rolesName);

        Task<List<User>> GetUserByRoleAsync(string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginDTO model);

        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<User> GetUserAsync(Guid userId);

        Task<List<string>> GetUserRolesAsync(User user);

        Task<List<IdentityRole>> GetRolesAsync();

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
    }
}
