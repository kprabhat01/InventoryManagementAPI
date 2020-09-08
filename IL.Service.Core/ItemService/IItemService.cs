using IL.DTO.Core.ItemDTO;
using System.Collections.Generic;

namespace IL.Service.Core.ItemService
{
    public interface IItemService
    {
        bool SaveItem(string itemName, string username, string comment, int unit, bool hasVarience);
        bool deleteItem(int itemId);
        List<ItemDTO> GetItems();
        bool IsItemExists(string itemName);
        public List<ItemStockDTO> GetStockedItemByOutlet(int outletId);
        public bool CaptureCurrentLogs();
    }
}
