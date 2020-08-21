using AutoMapper;
using IL.DTO.Core.UserDTO;
using IL.Service.Core.UserManagerService;
using IL.Util.Core;
using IM.Data.Core;
using System.Linq;
using System.Web.Http;
using System.Net;
using static Inventory.CommonMessageHelper;

namespace Inventory.Controllers
{

    public class UserMangerController : AbstractAPIController
    {
        private readonly IUserManagerService _userManagerService;
        private readonly IMapper _mapper;
        public UserMangerController(IUserManagerService userManagerService, IMapper mapper)
        {
            this._userManagerService = userManagerService;
            this._mapper = mapper;
        }
        [Route("api/createUser")]
        [HttpPost]
        [Authorize(Roles = "Super-Admin")]
        public IHttpActionResult Post([FromBody]UserDTO obj)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (this._userManagerService.CheckUniqueUserName(obj.UserId)) return BadRequest(USER_ALREADY_EXISTS_ALERT);
            obj.Passcode = obj.Passcode.Encrypt();
            var result = this._userManagerService.CreateUser(this._mapper.Map<User>(obj));
            if (result > 0 && this._userManagerService.SaveUserStoreMapping(obj.Outlet, result))
                return Ok(SUCCESSFULL_INSERTED_ALERT);
            else
                return InternalServerError();
        }
        [Route("api/getUsers")]
        [HttpGet]
        [Authorize(Roles = "Super-Admin")]
        public IHttpActionResult Get()
        {
            var users = this._userManagerService.GetUsers();
            if (users.Any())
                return Ok(users);
            else
                return Content(HttpStatusCode.NotFound, RECORDNOTFOUND_ALERT);
        }

        [Route("api/updatePassword")]
        [HttpPost]
        public IHttpActionResult UpdatePassword([FromBody]ChangeCredential obj)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = this._userManagerService.ChangePassowrd(obj.UserId, obj.oldPassword.Encrypt(), obj.NewPassword.Encrypt());
            if (result) return Ok(SUCCESSFULL_UPDATED_ALERT);
            else if (!result) return BadRequest("old Passowrd doesn't matches");
            return InternalServerError();

        }

        [Route("api/ChangeCredential")]
        [HttpPost]
        [Authorize(Roles = "Super-Admin")]
        public IHttpActionResult ChangeCredential([FromBody]AdminCrendital obj)
        {
            var result = this._userManagerService.ResetPassword(obj.UserId, obj.Password.Encrypt());
            if (result)
                return Ok(SUCCESSFULL_UPDATED_ALERT);
            else
                return BadRequest(RECORDNOTFOUND_ALERT);
        }
        [Route("api/deleteUser/{id}")]
        [HttpPost]
        [Authorize(Roles = "Super-Admin")]
        public IHttpActionResult DeleteUser(int id)
        {
            var result = this._userManagerService.DeleteUser(id);
            if (result)
                return Ok(SUCCESSFULL_UPDATED_ALERT);
            else
                return BadRequest(RECORDNOTFOUND_ALERT);
        }

    }
}
