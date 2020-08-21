using IL.DTO.Core.ItemDTO;
using IL.Service.Core.CommonService;
using IL.Service.Core.ItemService;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class ItemController : AbstractAPIController
    {
        private readonly IItemService _itemService;
        private readonly ICommonService _commonService;
        public ItemController(IItemService itemService, ICommonService commonService)
        {
            this._itemService = itemService;
            this._commonService = commonService;
        }

        [Route("api/getItems")]
        [HttpGet]
        public IHttpActionResult GetItems()
        {
            var items = this._itemService.GetItems();
            if (items.Any()) return Ok(items);
            else return Content(HttpStatusCode.NotFound, CommonMessageHelper.RECORDNOTFOUND_ALERT);
        }

        [Route("api/saveItems")]
        [HttpPost]
        public IHttpActionResult SaveItems([FromBody]ItemDetailDTO obj)
        {
            if (_itemService.IsItemExists(obj.Name)) return BadRequest(CommonMessageHelper.RECORD_ALREADY_EXISTS_ALERT);
            if (this._itemService.SaveItem(obj.Name, obj.CreatedBy, obj.Comment, obj.UnitId))
                return Ok(CommonMessageHelper.SUCCESSFULL_INSERTED_ALERT);
            return InternalServerError();
        }

        [Route("api/GetCurrentStock/{outletId}")]
        [HttpGet]
        public IHttpActionResult GetCurrentOutletStock(int OutletId)
        {
            if (!this._commonService.GetOutlets().Any(p => p.Id == OutletId)) return BadRequest();
            var currentStock = this._itemService.GetStockedItemByOutlet(OutletId);
            if (currentStock.Any())
                return Ok(currentStock);
            else
                return NotFound();

        }

    }
}
