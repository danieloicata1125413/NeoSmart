using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NeoSmart.Backend.Helper
{
    public class TokenGenerator : ITokenGenerator
    {
        public readonly IConfiguration _configurationManager;
        public TokenGenerator(IConfiguration configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public string GenerateTokenJwt(string UserName)
        {
            // appsetting for Token JWT
            var secretKey = _configurationManager["Jwt:Key"];
            var audienceToken = _configurationManager["Jwt:Audience"];
            var issuerToken = _configurationManager["Jwt:Issuer"];
            var expireTime = _configurationManager["Jwt:Expire"];

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(secretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserName", UserName),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuerToken,
                Audience = audienceToken,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
