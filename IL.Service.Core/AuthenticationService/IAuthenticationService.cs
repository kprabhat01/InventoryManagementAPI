using IL.DTO.Core.CommonDTO;
using IL.DTO.Core.UserDTO;
using System.Collections.Generic;

namespace IL.Service.Core.AuthenticationService
{
    public interface IAuthenticationService
    {
        public AuthenticationDTO AuthenticateUser(CredentialDTO obj);
        public bool SaveLogs(int userid);
        public string GetUserOutlet(int userId);
    }
}
