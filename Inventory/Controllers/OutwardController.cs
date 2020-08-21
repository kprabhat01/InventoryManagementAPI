using IL.DTO.Core.ItemDTO;
using IL.Service.Core.CommonService;
using IL.Service.Core.OutwardService;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class OutwardController : AbstractAPIController
    {
        private readonly IOutwardService _outwardService;
        private readonly ICommonService _commonService;
        public OutwardController(IOutwardService outwardService, ICommonService commonService)
        {
            this._outwardService = outwardService;
            this._commonService = commonService;
        }

        [Route("api/saveOutwardMaterial")]
        [HttpPost]
        public IHttpActionResult SaveOutwardMaterial([FromBody]List<ItemOutWardStockDTO> obj)
        {

            if (!obj.Any()) return BadRequest();
            foreach (var outlet in obj.Select(p => p.OutletId).Distinct())
                this._commonService.ImportNewItemsFromCreation(outlet);
            if (this._outwardService.SaveOutwardRecord(obj)) return Ok(CommonMessageHelper.SUCCESSFULL_INSERTED_ALERT);
            else return InternalServerError();
        }
    }
}
