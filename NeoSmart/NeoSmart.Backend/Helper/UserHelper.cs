
using NeoSmart.Backend.Interfaces;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeoSmart.ClassLibraries.DTOs;

namespace NeoSmart.Backend.Helper
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

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
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
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .FirstOrDefaultAsync(x => x.Email == email);
            return user!;
        }
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .Include(x => x.DocumentType)
                .FirstOrDefaultAsync(x => x.UserName.Equals(username));
            return user!;
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(DtoAccount model)
        {
            return await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
