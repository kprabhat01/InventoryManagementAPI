using IL.DTO.Core.PODTO;
using IL.DTO.Core.ReportDTO;
using IM.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Service.Core.ReportService
{
    public interface IReportService
    {
        public List<GET_ITEMWISE_SNAPSHOT_Result> GetSnapShot(CommonReport obj);
        public List<POReturnDTO> GetPODetailReport(CommonReport obj);
    }
}
