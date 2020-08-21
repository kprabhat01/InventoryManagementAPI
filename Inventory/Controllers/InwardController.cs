using IL.DTO.Core.ItemDTO;
using IL.Service.Core.CommonService;
using IL.Service.Core.InwardService;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class InwardController : AbstractAPIController
    {
        private readonly IMaterialInwards _materialInwards;
        private readonly ICommonService _commonService;
        public InwardController(IMaterialInwards materialInwards, ICommonService commonService)
        {
            this._materialInwards = materialInwards;
            this._commonService = commonService;
        }

        [Route("api/saveInwardMaterial")]
        [HttpPost]
        public IHttpActionResult SaveInwardMaterial([FromBody]List<ItemOutWardStockDTO> obj)
        {
            if (!obj.Any()) return BadRequest();
            foreach (var outlet in obj.Select(p => p.OutletId).Distinct())
                this._commonService.ImportNewItemsFromCreation(outlet);

            if (this._materialInwards.SaveInwardRecord(obj))
                return Ok(CommonMessageHelper.SUCCESSFULL_INSERTED_ALERT);
            else
                return InternalServerError();
        }
    }
}

