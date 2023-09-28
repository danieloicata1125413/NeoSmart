using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace NeoSmart.Frontend.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(500);

            //var anonimous = new ClaimsIdentity();
            
            var adminUser = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Daniel"),
                new Claim("LastName", "Oicata Hernandez"),
                new Claim(ClaimTypes.Name, "danieloicata1125413@correo.itm.edu.co"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");

            //var otherUser = new ClaimsIdentity(new List<Claim>
            //{
            //    new Claim("FirstName", "Ledys"),
            //    new Claim("LastName", "Bedoya"),
            //    new Claim(ClaimTypes.Name, "ledys@yopmail.com"),
            //    new Claim(ClaimTypes.Role, "User")
            //},
            //authenticationType: "test");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(adminUser)));
        }
    }
}
