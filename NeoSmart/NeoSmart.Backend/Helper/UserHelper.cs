using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeoSmart.ClassLibraries.DTOs;
using Microsoft.Build.Framework;
using NeoSmart.BackEnd.Helpers;

namespace NeoSmart.BackEnd.Helper
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DataContext context, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }


        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> RemoveUserAsync(User user)
        {
            var currentUser = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.DeleteAsync(currentUser!);
        }

        public async Task<List<User>> GetUserByRoleAsync(string roleName)
        {
            return (List<User>)await _userManager.GetUsersInRoleAsync(roleName);
        }
        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(User user, List<string> rolesName)
        {
            return await _userManager.AddToRolesAsync(user, rolesName);
        }
        public async Task RemoveUserToRoleAsync(User user, string roleName)
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RemoveUserToRoleAsync(User user, List<string> rolesName)
        {
            return await _userManager.RemoveFromRolesAsync(user, rolesName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await _context.Users
                .Include(x => x.Company!)
                .Include(x => x.Occupation!)
                .ThenInclude(x => x.Process!)
                .ThenInclude(x => x.Company!)
                .Include(x => x.DocumentType)
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .FirstOrDefaultAsync(x => x.Email == email);
            return user!;
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(x => x.Company!)
                .Include(x => x.Occupation!)
                .ThenInclude(x => x.Process!)
                .ThenInclude(x => x.Company!)
                .Include(x => x.DocumentType)
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .FirstOrDefaultAsync(x => x.Id == userId.ToString());
            return user!;
        }

        public async Task<List<string>> GetUserRolesAsync(User user)
        {
            try{
               
                    return new List<string>(await _userManager.GetRolesAsync(user));

            } 
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
