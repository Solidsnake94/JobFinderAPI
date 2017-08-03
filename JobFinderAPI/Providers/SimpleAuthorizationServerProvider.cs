using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace JobFinderAPI.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var UserId = "";

            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                // set UserId for the claims and later or verifing request with identity.userid()
                UserId = user.Id;
            }

            //// Being to retrieve the userId to authorize only to logged in user resources in controllers
            //var properties = new AuthenticationProperties( new Dictionary<string, string>() {
            //    {   ClaimTypes.NameIdentifier, UserId    },
            //    {   ClaimTypes.Name, context.UserName   }
            //});

            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));

            //var ticket = new AuthenticationTicket(identity, properties);
            //context.Validated(ticket);

            var properties = new Dictionary<string, string>()
            {
                {"sub", context.UserName },
                { "role", "user"},
                { ClaimTypes.NameIdentifier, UserId },
                { ClaimTypes.Name, context.UserName }
            };

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            properties.ToList().ForEach(c => identity.AddClaim(new Claim(c.Key, c.Value)));

            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties(properties));

            context.Validated(ticket);

        }
    }
}