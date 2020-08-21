using IL.DTO.Core.CommonDTO;
using IL.DTO.Core.UserDTO;
using IL.Util.Core;
using IM.Data.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;


namespace IL.Service.Core.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {


        public AuthenticationService()
        {


        }
        public AuthenticationDTO AuthenticateUser(CredentialDTO obj)
        {
            obj.Passcode = obj.Passcode.Encrypt();
            using (var entities = new db_InventoryEntities())
            {
                var UserIdentity = entities.Users.AsQueryable().SingleOrDefault(p => p.userId == obj.UserName && p.passcode == obj.Passcode && p.deleteflag == false);

                return new AuthenticationDTO
                {
                    UserId = UserIdentity.id,
                    Username = UserIdentity.firstName + " " + UserIdentity.lastName,
                    Role = entities.UserRoles.AsQueryable().SingleOrDefault(s => s.id == UserIdentity.roleId).name
                };
            }
        }
        /* private string GetToken(User obj)
          {
              DateTime issuedAt = DateTime.UtcNow;
              DateTime expires = DateTime.UtcNow.AddDays(7);
              var tokenHandler = new JwtSecurityTokenHandler();
              ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
              {
                  new Claim(ClaimTypes.NameIdentifier, obj.id.ToString()),
                  new Claim(ClaimTypes.Name,$"{obj.firstName},{obj.lastName}"),
                  new Claim(ClaimTypes.Role,this._InventoryEntities.UserRoles.AsQueryable().SingleOrDefault(p=> p.id==obj.roleId).name)
              });
              var now = DateTime.UtcNow;
              var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes("TokenKey".GetConfigurationValue()));
              var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
              var token =
                 (JwtSecurityToken)
                     tokenHandler.CreateJwtSecurityToken(issuer: "TokenIssuer".GetConfigurationValue(), audience: "TokenAudience".GetConfigurationValue(),
                         subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
              var tokenString = tokenHandler.WriteToken(token);

              return tokenString;

          }*/
        public bool SaveLogs(int userid)
        {
            using (var entities = new db_InventoryEntities())
            {
                entities.logs_User.Add(new logs_User
                {
                    userId = userid,
                    loginDate = DateTime.Now
                });
                entities.SaveChanges();
                return true;
            }
        }
        public string GetUserOutlet(int userId)
        {
            using (var entities = new db_InventoryEntities())
            {
                var outlet = entities.OutletMappings.Where(p => p.userId == userId).Select(opt => opt.outlet.id.ToString()).ToArray();
                return string.Join(",", outlet);
            }
        }

    }
}
