using IL.DTO.Core.UserDTO;
using IL.Service.Core.AuthenticationService;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class AuthenticationController : AbstractAPIController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }
        [AllowAnonymous]
        [Route("api/Authenticate")]
        [HttpPost]
        public IHttpActionResult AuthenticateUser(CredentialDTO obj)
        {
            var auth = this._authenticationService.AuthenticateUser(obj);
            if (auth != null) return Ok(auth);
            else return Unauthorized();
        }
    }
}
