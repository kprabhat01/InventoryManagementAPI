using IL.Service.Core.AuthenticationService;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Inventory
{
    public class OWINAuthentication : OAuthAuthorizationServerProvider
    {
        private readonly AuthenticationService _authentication;
        public OWINAuthentication()
        {
            _authentication = new AuthenticationService();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var user = _authentication.AuthenticateUser(new IL.DTO.Core.UserDTO.CredentialDTO { UserName = context.UserName, Passcode = context.Password });
            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            IDictionary<string, string> authProp = new Dictionary<string, string>()
            {
                { "UserName", user.Username},
                { "Role", user.Role},
                { "userid", user.UserId.ToString()},
                {"selectedOutlet", this._authentication.GetUserOutlet(user.UserId)}
            };
            this._authentication.SaveLogs(user.UserId);
            var props = new AuthenticationProperties(authProp);
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            return Task.FromResult<object>(null);
        }
    }

    /* private ClaimsPrincipal SetClaims(string token)
     {
         var key = Encoding.ASCII.GetBytes("TokenKey".GetConfigurationValue());
         var handler = new JwtSecurityTokenHandler();
         var validations = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(key),
             ValidateIssuer = false,
             ValidateAudience = false
         };
         SecurityToken securityToken;
         return handler.ValidateToken(token, validations, out securityToken);
     }*/
}
