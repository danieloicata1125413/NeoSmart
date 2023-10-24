using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NeoSmart.ClassLibraries.DTOs;

namespace NeoSmart.Backend.Helper
{
    public class TokenGenerator : ITokenGenerator
    {
        public readonly IConfiguration _configurationManager;
        public TokenGenerator(IConfiguration configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public TokenDTO GenerateTokenJwt(User user)
        {
            // appsetting for Token JWT
            var secretKey = _configurationManager["Jwt:Key"];
            var audienceToken = _configurationManager["Jwt:Audience"];
            var issuerToken = _configurationManager["Jwt:Issuer"];
            var expireTime = _configurationManager["Jwt:Expire"];

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim("Document", user.Document),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Address", user.Address),
                new Claim("Photo", user.Photo ?? string.Empty),
                new Claim("CityId", user.CityId.ToString())
            };

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
