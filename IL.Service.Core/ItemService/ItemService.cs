using IL.DTO.Core.ItemDTO;
using IM.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IL.Service.Core.ItemService
{
    public class ItemService : IItemService
    {

        private db_InventoryEntities _dbEntities
        {
            get { return new db_InventoryEntities(); }
        }
        public ItemService()
        {

        }
        public bool deleteItem(int itemId)
        {
            var item = this._dbEntities.items.SingleOrDefault(p => p.id == itemId);
            item.deleteflag = true;
            this._dbEntities.SaveChanges();
            return true;
        }

        public bool SaveItem(string itemName, string username, string comment, int unit)
        {
            this._dbEntities.items.Add(new item
            {
                normalizeName = itemName,
                createdBy = username,
                comment = comment,
                deleteflag = false,
                unitId = unit,
                createdDate = DateTime.Now
            });
            this._dbEntities.SaveChanges();
            return true;
        }
        public List<ItemDTO> GetItems()
        {
            return this._dbEntities.items.Where(p => p.deleteflag == false).AsEnumerable()
                   .Select(opt => new ItemDTO
                   {
                       ItemId = opt.id,
                       NormalizeName = opt.normalizeName,
                       Units = new DTO.Core.CommonDTO.CommonDTO
                       {
                           Id = opt.unit.id,
                           NormalizeName = opt.unit.normalizeName
                       }
                   }).ToList();

        }
        public bool IsItemExists(string itemName) =>
              this._dbEntities.items.Any(p => p.normalizeName.ToLower() == itemName.ToLower());

        public List<ItemStockDTO> GetStockedItemByOutlet(int outletId)
        {
            var item = this._dbEntities.outletStocks.Where(p => p.outletId == outletId).Select(opt =>
                 new ItemStockDTO
                 {
                     ItemId = opt.item.id,
                     NormalizeName = opt.item.normalizeName,
                     Quantity = opt.currentStock,
                     LastUpdated = opt.lastUpdated,
                     Units = new DTO.Core.CommonDTO.CommonDTO
                     {
                         Id = opt.item.unit.id,
                         NormalizeName = opt.item.unit.normalizeName
                     }
                 }
            ).OrderBy(o => o.NormalizeName).ToList();
            return item;
        }
        public bool CaptureCurrentLogs()
        {
            this._dbEntities.CAPTUREDAILYLOGS();
            return true;
        }

    }
}
