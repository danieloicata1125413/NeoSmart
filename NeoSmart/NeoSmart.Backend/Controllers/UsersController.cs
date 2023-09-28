using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeoSmart.Backend.Interfaces;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helper;
using NeoSmart.ClassLibraries.Interfaces;
using System.Security.Principal;

namespace NeoSmart.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserHelper _userHelper { get; }
        public IMailHelper _mailHelper { get; }
        public ITokenGenerator _tokenGenerator { get; }

        public UsersController(IUserHelper userHelper, IMailHelper mailHelper, ITokenGenerator tokenGenerator)
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _tokenGenerator = tokenGenerator;
        }



        // POST: api/Users/GetToken
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public async Task<ActionResult<string>> PostToken(DtoAccount model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);

                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());

                    }
                    return _tokenGenerator.GenerateTokenJwt(model.Username);
                }
            }

            return NoContent();
        }

        // GET: api/Users/GetUser
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var _user = await GetUser(User.Identity);
                if (_user == null)
                {
                    return NotFound();
                }
                return _user;
            }
            return Unauthorized();
        }

        [HttpGet]
        private async Task<User> GetUser(IIdentity user)
        {
            var UserName = User.Identities.FirstOrDefault().Claims.FirstOrDefault();
            var _user = await _userHelper.GetUserByUserNameAsync(UserName.Value);
            if (_user != null)
            {
                return _user;
            }
            return null;
        }
    }
}
