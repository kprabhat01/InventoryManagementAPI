using IL.DTO.Core.ReportDTO;
using IL.Service.Core.ReportService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory.Controllers
{
    public class ReportController : AbstractAPIController
    {
        public IReportService _reportService;
        public ReportController(IReportService report)
        {
            this._reportService = report;
        }

        [Route("api/getSnapshot")]
        [HttpGet]
        public IHttpActionResult GetSnapshot([FromUri]CommonReport obj)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = this._reportService.GetSnapShot(obj);
            if (result.Any())
                return Ok(result);
            else return NotFound();
        }

        [Route("api/getPODetailReport")]
        [HttpGet]
        public IHttpActionResult GetPODetailReport([FromUri]CommonReport obj)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = this._reportService.GetPODetailReport(obj);
            if (result.Any())
                return Ok(result);
            else
                return NotFound();
        }

    }
}
