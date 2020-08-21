using IL.DTO.Core.ItemDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Service.Core.InwardService
{
    public interface IMaterialInwards
    {
        public bool SaveInwardRecord(List<ItemOutWardStockDTO> obj);
    }
}
