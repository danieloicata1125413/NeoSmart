using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NeoSmart.BackEnd.Helper;
using NeoSmart.BackEnd.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Enum;
using NeoSmart.ClassLibraries.Helpers;
using NeoSmart.ClassLibraries.Interfaces;
using NeoSmart.ClassLibraries.Responses;
using NeoSmart.Data.Entities;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace NeoSmart.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IFileStorage _fileStorage;
        private readonly IMailHelper _mailHelper;
        private readonly DataContext _context;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly string _container;

        public AccountsController(IUserHelper userHelper, IConfiguration configuration, IFileStorage fileStorage, IMailHelper mailHelper, DataContext context, ITokenGenerator tokenGenerator, IMapper mapper)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _fileStorage = fileStorage;
            _mailHelper = mailHelper;
            _context = context;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
            _container = "users";
        }


        [HttpGet("all")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetAll([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Users
                .Include(c => c.Company)
                .Include(c => c.Occupation)
                .Include(u => u.City)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.FirstName.ToLower().Contains(pagination.Filter.ToLower()) ||
                                                 x.LastName.ToLower().Contains(pagination.Filter.ToLower()));
            }
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Company!.Id == user.Company!.Id);
            }
            return Ok(await queryable
                .OrderBy(s => s.Company!.Name)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.FirstName.ToLower().Contains(pagination.Filter.ToLower()) ||
                                                 x.LastName.ToLower().Contains(pagination.Filter.ToLower()));
            }
            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user.Company != null)
            {
                queryable = queryable.Where(c => c.Company!.Id == user.Company!.Id);
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpPost("RecoverPassword")]
        public async Task<ActionResult> RecoverPassword([FromBody] EmailDTO model)
        {
            var user = await _userHelper.GetUserAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
            var tokenLink = Url.Action("ResetPassword", "accounts", new
            {
                userid = user.Id,
                token = myToken
            }, HttpContext.Request.Scheme, _configuration["Url FrontEnd"]);

            var response = _mailHelper.SendMail(user.FullName, user.Email!,
                $"NeoSmart - Recuperación de contraseña",
                $"<h1>NeoSmart - Recuperación de contraseña</h1>" +
                $"<p>Para recuperar su contraseña, por favor hacer clic 'Recuperar Contraseña':</p>" +
                $"<b><a href ={tokenLink}>Recuperar Contraseña</a></b>");

            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var user = await _userHelper.GetUserAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors.FirstOrDefault()!.Description);
        }

        [HttpPost("changePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ChangePasswordAsync(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault()!.Description);
            }

            return NoContent();
        }



        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PutAsync(User user)
        {
            try
            {
                var currentUser = await _userHelper.GetUserAsync(user.UserName!);
                if (currentUser == null)
                {
                    return NotFound();
                }

                if (!string.IsNullOrEmpty(user.Photo))
                {
                    var photoUser = Convert.FromBase64String(user.Photo);
                    user.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
                }
                currentUser.CompanyId = null;
                if (user.CompanyId > 0)
                {
                    currentUser.CompanyId = user.CompanyId;
                }

                currentUser.OccupationId = null;
                if (user.OccupationId > 0)
                {
                    currentUser.OccupationId = user.OccupationId;
                }

                currentUser.Document = user.Document;
                currentUser.DocumentTypeId = user.DocumentTypeId;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Address = user.Address;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Photo = !string.IsNullOrEmpty(user.Photo) && user.Photo != currentUser.Photo ? user.Photo : currentUser.Photo;
                currentUser.CityId = user.CityId;

                var result = await _userHelper.UpdateUserAsync(currentUser);
                if (result.Succeeded)
                {
                    var listRoles = await _userHelper.GetUserRolesAsync(user);
                    return Ok(_tokenGenerator.GenerateTokenJwtAsync(user, listRoles));
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _userHelper.GetUserAsync(User.Identity!.Name!));
        }

        [HttpGet("{userName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> GetAsync(string userName)
        {
            return Ok(await _userHelper.GetUserAsync(userName));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUserAsync([FromBody] UserDTO model)
        {

            User user = _mapper.Map<User>(model);

            if (!string.IsNullOrEmpty(model.Photo))
            {
                var photoUser = Convert.FromBase64String(model.Photo);
                model.Photo = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
            }

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                foreach (var UserType in model.UserTypes!)
                {
                    await _userHelper.AddUserToRoleAsync(user, UserType.ToString());
                }
                var response = await SendConfirmationEmailAsync(user);
                if (response.IsSuccess)
                {
                    return NoContent();
                }

                return BadRequest(response.Message);
            }

            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpDelete("{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> DeleteUserAsync(Guid userId)
        {
            var currentUser = await _userHelper.GetUserAsync(userId);
            if (currentUser != null)
            {
                var listUserRoles = await _userHelper.GetUserRolesAsync(currentUser);
                var result = await _userHelper.RemoveUserToRoleAsync(currentUser, listUserRoles.Select(x => x.ToString()).ToList());
                if (result.Succeeded)
                {
                    result = await _userHelper.RemoveUserAsync(currentUser);
                    if (result.Succeeded)
                    {
                        var response = SendRemoveConfirmationEmailAsync(currentUser);
                        if (response.IsSuccess)
                        {
                            return NoContent();
                        }

                        return BadRequest(response.Message);
                    }
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest("No existe el usuario");
        }

        [HttpGet("Roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetRolesAsync()
        {
            var identityRoles = await _userHelper.GetRolesAsync();
            if (identityRoles == null)
            {
                return NotFound();
            }
            List<RoleDTO> listRolesDTO = new List<RoleDTO>();
            foreach (var identityRole in identityRoles!)
            {
                listRolesDTO.Add( new RoleDTO()
                {
                    Id = identityRole.Id,
                    Name = identityRole.Name,
                });
            }
            
            return Ok(listRolesDTO);
        }

        [HttpGet("UserRoles/{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> GetUserRolesAsync(Guid userId)
        {
            var currentUser = await _userHelper.GetUserAsync(userId);
            if (currentUser == null)
            {
                return NotFound();
            }
            UserDTO model = new UserDTO();
            model.UserName = currentUser.UserName;
            model.UserTypes = await _userHelper.GetUserRolesAsync(currentUser);

            return Ok(model);
        }

        [HttpPost("UserRoles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> PostUserRolesAsync([FromBody] UserRolesDTO model)
        {
            var currentUser = await _userHelper.GetUserAsync(model.UserName!);
            if (currentUser == null)
            {
                return NotFound();
            }
            var listRoles = await _userHelper.GetRolesAsync();
            var listUserRoles = await _userHelper.GetUserRolesAsync(currentUser);
            //var result = await _userHelper.RemoveUserToRoleAsync(currentUser, listRoles.Where(x => x.Name != UserType.SuperAdmin.ToString()).Select(x => x.Name!.ToString()).ToList());
            var result = await _userHelper.RemoveUserToRoleAsync(currentUser, listUserRoles.Select(x => x.ToString()).ToList());
            if (result.Succeeded)
            {
                result = await _userHelper.AddUserToRoleAsync(currentUser, model.UserTypes!.Select(x => x.ToString()).ToList());
                if (result.Succeeded)
                {
                    return Ok(model);
                }
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            var result = await _userHelper.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _userHelper.GetUserAsync(model.Email);
                var listRoles = await _userHelper.GetUserRolesAsync(user);
                return Ok(_tokenGenerator.GenerateTokenJwtAsync(user, listRoles));
            }

            if (result.IsLockedOut)
            {
                return BadRequest("Ha superado el máximo número de intentos, su cuenta está bloqueada, intente de nuevo en 5 minutos.");
            }

            if (result.IsNotAllowed)
            {
                return BadRequest("El usuario no ha sido habilitado, debes de seguir las instrucciones del correo enviado para poder habilitar el usuario.");
            }

            return BadRequest("Email o contraseña incorrectos.");
        }

        [HttpPost("ResedToken")]
        public async Task<ActionResult> ResedToken([FromBody] EmailDTO model)
        {
            User user = await _userHelper.GetUserAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            var response = await SendConfirmationEmailAsync(user);
            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Message);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmailAsync(string userId, string token)
        {
            token = token.Replace(" ", "+");
            var user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault());
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("GetValidate/{userName}")]
        public async Task<ActionResult<bool>> GetValidate(string userName)
        {
            var CMAUser = await _userHelper.GetUserAsync(userName);
            if (CMAUser != null)
            {
                return true;
            }
            return false;
        }

        private async Task<Response<string>> SendConfirmationEmailAsync(User user)
        {
            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            var tokenLink = Url.Action("ConfirmEmail", "accounts", new
            {
                userid = user.Id,
                token = myToken
            }, HttpContext.Request.Scheme, _configuration["Url FrontEnd"]);

            return _mailHelper.SendMail(user.FullName, user.Email!,
                $"NeoSmart - Confirmación de cuenta",
                $"<h1>NeoSmart - Confirmación de cuenta</h1>" +
                $"<p>Para habilitar el usuario, por favor hacer clic 'Confirmar Email':</p>" +
                $"<b><a href ={tokenLink}>Confirmar Email</a></b>");
        }

        private Response<string> SendRemoveConfirmationEmailAsync(User user)
        {
            return _mailHelper.SendMail(user.FullName, user.Email!,
                $"NeoSmart - Se ha eliminado tu cuenta",
                $"<h1>NeoSmart - Confirmación de eliminación la cuenta</h1>" +
                $"<p>{user.FullName}, Sentimos mucho que te tengas que ir, recuerda que puedes crear de nuevo tu cuenta cuando quieras.</p>" +
                $"<p>Atentamente:</p>" +
                $"<p>Equipo de neosmart.</p>");
        }


        //Recordar contraseña
        [AllowAnonymous]
        [HttpGet("GetPasswordResetToken/{userName}")]
        public async Task<ActionResult<bool>> GetPasswordResetToken(string userName)
        {
            var user = await _userHelper.GetUserAsync(userName);
            if (user != null)
            {
                var token = await _userHelper.GeneratePasswordResetTokenAsync(user);
                if (token != null)
                {
                    var result = await DeleteAllUserTokenReset(user);
                    if (result)
                    {
                        var usersTokenReset = await GetUserTokenResetCreate(user, token);
                        if (usersTokenReset != null)
                        {
                            var tokenLink = Url.Action("/api/accounts/ResetPasswordByToken", "accounts", new
                            {
                                UserName = userName,
                                Token = usersTokenReset.Id
                            }, HttpContext.Request.Scheme, _configuration["Url FrontEnd"]);
                            var estado = _mailHelper.SendMail(user.FullName, user.Email!,
                                                            "NeoSmart - Restablecimiento de contraseña",
                                                            $"<h1>NeoSmart - Restablecimiento de contraseña</h1>" +
                                                            $"Para restablecer su contraseña, " +
                                                            $"por favor hacer clic en el siguiente enlace:" +
                                                            $"</br></br><a href ={tokenLink}>Restablecer Contraseña</a>" +
                                                            $"</br>Tiempo máximo para realizar el proceso: " + usersTokenReset.FechaMax +
                                                            $"</br></br>NeoSmart");
                            if (estado.IsSuccess)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        [AllowAnonymous]
        [HttpPost("SetResetPassword")]
        public async Task<ActionResult> GetResetPassword([FromBody] ResetPasswordDTO model)
        {
            var user = await _userHelper.GetUserAsync(model.Email);
            if (user != null)
            {
                var aspNetUsersTokenReset = await GetUserTokenResetById(Guid.Parse(model.Token));
                if (aspNetUsersTokenReset != null)
                {
                    if (aspNetUsersTokenReset.Deleted == null)
                    {
                        if (aspNetUsersTokenReset.FechaMax > DateTime.Now)
                        {
                            var identityResult = await _userHelper.ResetPasswordAsync(user, aspNetUsersTokenReset!.Token!, model.Password);
                            if (identityResult.Succeeded)
                            {
                                aspNetUsersTokenReset.Processed = true;
                                await PutUsersTokenReset(aspNetUsersTokenReset);
                                _mailHelper.SendMail(user.FullName, user.Email!, "NeoSmart - Restablecimiento de contraseña exitoso", $"<h1>NeoSmart - Restablecimiento de contraseña exitoso!</h1>" +
                                  $"Su contraseña ha sido modificada exitosamente." +
                                  $"</br>Proceso realizado: " + DateTime.Now +
                                  $"</br></br>NeoSmart");
                                await DeleteAllUserTokenReset(user);
                                return Ok();
                            }
                        }
                        else
                        {
                            await DeleteUserTokenReset(aspNetUsersTokenReset.Id);
                        }
                    }
                }
            }
            return NotFound();
        }
        private async Task<bool> PutUsersTokenReset(UserTokenReset usersTokenReset)
        {
            _context.Entry(usersTokenReset).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Error:", ex.Message);
            }
            return false;
        }
        private async Task<bool> DeleteAllUserTokenReset(User user)
        {
            var usersTokenResetList = await _context.AspNetUserTokenReset
                .Where(x => x.UserId!.Equals(user.Id))
                .ToListAsync();
            foreach (UserTokenReset item in usersTokenResetList)
            {
                try
                {
                    _context.AspNetUserTokenReset.Remove(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return true;
        }
        private async Task<bool> DeleteUserTokenReset(Guid guid)
        {
            var usersTokenReset = await _context.AspNetUserTokenReset
                .FindAsync(guid);
            if (usersTokenReset != null)
            {
                try
                {
                    _context.AspNetUserTokenReset.Remove(usersTokenReset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return true;
        }
        private async Task<UserTokenReset?> GetUserTokenResetCreate(User user, string token)
        {
            try
            {
                UserTokenReset userTokenReset = new UserTokenReset()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Fecha = DateTime.Now,
                    FechaMax = DateTime.Now.AddHours(1),
                    Token = token,
                    Processed = false
                };
                _context.AspNetUserTokenReset.Add(userTokenReset);
                await _context.SaveChangesAsync();
                return userTokenReset;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
            }
            return null;
        }

        private async Task<UserTokenReset?> GetUserTokenResetById(Guid guid)
        {
            var userTokenReset = await _context.AspNetUserTokenReset
                .FindAsync(guid);
            if (userTokenReset != null)
            {
                return userTokenReset;
            }
            return null;
        }

    }
}
