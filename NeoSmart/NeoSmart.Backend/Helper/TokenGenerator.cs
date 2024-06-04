using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NeoSmart.ClassLibraries.DTOs;
using NeoSmart.BackEnd.Interfaces;

namespace NeoSmart.BackEnd.Helper
{
    public class TokenGenerator : ITokenGenerator
    {
        public readonly IConfiguration _configurationManager;

        public TokenGenerator(IConfiguration configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public TokenDTO GenerateTokenJwtAsync(User user, List<string> roleList)
        {
            // appsetting for Token JWT
            var secretKey = _configurationManager["Jwt:Key"];
            var audienceToken = _configurationManager["Jwt:Audience"];
            var issuerToken = _configurationManager["Jwt:Issuer"];
            var expireTime = _configurationManager["Jwt:Expire"];
            //var roleList = await _userHelper.GetUserRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim("Document", user.Document),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Address", user.Address),
                new Claim("Photo", user.Photo ?? string.Empty),
                new Claim("CityId", user.CityId.ToString()),
                
            };
            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                claims.Add(new Claim("Phone", user.PhoneNumber));
            }
            if (user.Company != null)
            {
                claims.Add(new Claim("Company", user.Company!.Name.ToString()));
            }

            if (user.Occupation != null)
            {
                claims.Add(new Claim("Occupation", user.Occupation!.Description.ToString()));
                if (user.Occupation!.Process != null)
                {
                    claims.Add(new Claim("Process", user.Occupation!.Process!.Description.ToString()));
                }
            }

            foreach (string role in roleList)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(double.Parse(expireTime));
            var token = new JwtSecurityToken(
                issuer: issuerToken,
                audience: audienceToken,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

        }
    }
}
