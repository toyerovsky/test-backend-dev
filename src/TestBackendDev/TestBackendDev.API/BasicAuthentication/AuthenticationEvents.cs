using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace TestBackendDev.API.BasicAuthentication
{
    public class AuthenticationEvents : BasicAuthenticationEvents
    {
        public override Task ValidatePrincipalAsync(ValidatePrincipalContext context)
        {
            if (context.UserName == "userName" && context.Password == "password")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, context.UserName, context.Options.ClaimsIssuer)
                };

                var principal = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, BasicAuthenticationDefaults.AuthenticationScheme));
                context.Principal = principal;
            }

            return Task.CompletedTask;
        }
    }
}
