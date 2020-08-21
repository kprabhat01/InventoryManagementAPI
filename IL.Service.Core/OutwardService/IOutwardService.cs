using IL.DTO.Core.ItemDTO;
using System.Collections.Generic;

namespace IL.Service.Core.OutwardService
{
    public interface IOutwardService
    {
        public bool SaveOutwardRecord(List<ItemOutWardStockDTO> obj);
    }
}
