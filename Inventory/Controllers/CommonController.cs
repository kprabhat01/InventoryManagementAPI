using IL.Service.Core.CommonService;
using System.Linq;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class CommonController : AbstractAPIController
    {
        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            this._commonService = commonService;
        }

        [Route("api/getOutlet")]
        [HttpGet]
        public IHttpActionResult GetOutlet()
        {
            var outlet = this._commonService.GetOutlets();
            if (outlet.Any())
                return Ok(outlet);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("api/getUserRoles")]
        public IHttpActionResult GetUserRoles()
        {
            var roles = this._commonService.GetRoles();
            if (roles.Any())
                return Ok(roles);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("api/getUnits")]
        public IHttpActionResult GetUnits()
        {
            var units = this._commonService.GetUnits();
            if (units.Any())
                return Ok(units);
            else return NotFound();
        }

        [HttpGet]
        [Route("api/getStatus")]
        public IHttpActionResult GetStatus()
        {
            var status = this._commonService.GetStatus();
            if (status.Any())
                return Ok(status);
            else return NotFound();
        }

        [HttpGet]
        [Route("api/getMovementType")]
        public IHttpActionResult GetMovementType()
        {
            var movementType = this._commonService.GetMovementType();
            if (movementType.Any())
                return Ok(movementType);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("api/getUserSpecificOutlet/{userId}")]
        public IHttpActionResult GetUserOutlet(int userId)
        {
            var outlets = this._commonService.UserSpecificOutlet(userId);
            if (outlets.Any()) return Ok(outlets);
            else return NotFound();
        }

        [HttpGet]
        [Route("api/getNotificationBasedOnUserId/{userId}")]
        public IHttpActionResult GetNotification(int userId)
        {
            var notification = this._commonService.GetNotifications(userId);
            if (notification.Any())
                return Ok(notification);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("api/updateNotification/{userId}")]
        public IHttpActionResult UpdateNotification([FromUri]int userId)
        {
            if (this._commonService.MarkViewToNotification(userId))
                return Ok();
            else
                return InternalServerError();
        }



    }
}
