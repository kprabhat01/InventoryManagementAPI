using AutoMapper;
using IL.DTO.Core.PODTO;
using IL.Service.Core.PRService;
using IM.Data.Core;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class PORequestController : AbstractAPIController
    {
        private readonly IMapper _mapper;
        private readonly IPRService _prService;
        public PORequestController(IMapper mapper, IPRService prService)
        {
            this._mapper = mapper;
            this._prService = prService;
        }

        [Route("api/savePORequest")]
        [HttpPost]
        public IHttpActionResult SavePORequest([FromBody]PORequestDTO obj)
        {
            if (!ModelState.IsValid) return BadRequest();
            var request = this._mapper.Map<prRequest>(obj);

            List<prItem> lst = new List<prItem>();
            foreach (var item in obj.POItems)
            {
                lst.Add(new prItem
                {
                    itemid = item.itemid,
                    quantity = item.quantity
                });
            }
            var result = this._prService.SavePR(request, lst);
            if (result) return Ok(CommonMessageHelper.SUCCESSFULL_INSERTED_ALERT);
            else return InternalServerError();

        }
        [Route("api/getPORequestBasedOnFromOutletId/{fromOutletId}")]
        [HttpGet]
        public IHttpActionResult GetPORequestBasedOnFromOutletId(int fromOutletId)
        {

            var poRequest = this._prService.GetPORequestBasedOnFromOutletId(fromOutletId);
            if (poRequest.Any())
                return Ok(poRequest);
            else
                return Content(HttpStatusCode.NotFound, CommonMessageHelper.RECORDNOTFOUND_ALERT);
        }

        [Authorize(Roles = "Inventory-Manager, Super-Admin")]
        [Route("api/processPORequest")]
        [HttpPost]
        public IHttpActionResult ProcessPORequest([FromBody]ProcessPODTO obj)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!this._prService.CheckPRStatus(obj.Id, 2)) return BadRequest(CommonMessageHelper.RECORD_NOTFOUND_WITH_CORRECT_STATUS);
            if (this._prService.ProcessPRItems(obj))
                return Ok(CommonMessageHelper.SUCCESSFULL_UPDATED_ALERT);
            else
                return InternalServerError();
        }


        [Authorize(Roles = "Store-Manager, Super-Admin")]
        [Route("api/completePORequest")]
        [HttpPost]
        public IHttpActionResult CompletePORequest(TransferPORequest obj)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (!this._prService.CheckPRStatus(obj.PrId, 3)) return BadRequest(CommonMessageHelper.RECORD_NOTFOUND_WITH_CORRECT_STATUS);
            var data = this._prService.ImportPOItemToStore(obj.ToOutletId, obj.FromOutletId, obj.PrId, obj.Username);
            if (data)
                return Ok(CommonMessageHelper.SUCCESSFULL_UPDATED_ALERT);
            else
                return InternalServerError();

        }

        [Authorize(Roles = "Inventory-Manager, Super-Admin")]
        [Route("api/getPOResultForProcess")]
        [HttpGet]
        public IHttpActionResult POForProcess()
        {
            var data = this._prService.GetPORequestBasedOnProcessStatus();
            if (data.Any()) return Ok(data);
            else return NotFound(); 
        }

    }
}
