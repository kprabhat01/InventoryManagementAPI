using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IL.DTO.Core.PODTO;
using IL.DTO.Core.ReportDTO;
using IL.Service.Core.PRService;
using IM.Data.Core;

namespace IL.Service.Core.ReportService
{
    public class ReportService : IReportService
    {
        private IPRService _prService;
        public ReportService(IPRService prService)
        {
            this._prService = prService;
        }
        private DateTime sanitizeDate(string date)
        {
            return DateTime.Parse(date.Split('T')[0]).Date;
        }
        public List<GET_ITEMWISE_SNAPSHOT_Result> GetSnapShot(CommonReport obj)
        {
            using (var entity = DbInventoryEntities.Db_Entites)
            {
                var data = entity.GET_ITEMWISE_SNAPSHOT(sanitizeDate(obj.FromDate), sanitizeDate(obj.ToDate), obj.OutletId).ToList();
                return data;
            }
        }
        public List<POReturnDTO> GetPODetailReport(CommonReport obj)
        {
            return this._prService.GetPODetailReport(sanitizeDate(obj.FromDate), sanitizeDate(obj.ToDate), obj.OutletId);
        }
    }
}
