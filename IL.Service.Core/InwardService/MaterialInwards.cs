using IL.DTO.Core.ItemDTO;
using IL.Service.Core.LoggerService;
using IM.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IL.Service.Core.InwardService
{
    public class MaterialInwards : IMaterialInwards
    {
        private readonly ILoggerService _loggerService;
        public MaterialInwards(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public bool SaveInwardRecord(List<ItemOutWardStockDTO> obj)
        {
            using (var entity = DbInventoryEntities.Db_Entites)
            {
                using (var trans = entity.Database.BeginTransaction())
                {
                    try
                    {
                        var logsInventoryAlert = entity.logsInventoryAlerts.Where(p => p.isSeen == false && p.isVisible == false).ToList();

                        List<logsOutletStock> lst = new List<logsOutletStock>();
                        var items = entity.outletStocks;
                        foreach (var item in items)
                        {
                            foreach (var outItem in obj)
                            {
                                if (item.itemId == outItem.ItemId && item.outletId == outItem.OutletId)
                                {
                                    item.currentStock = item.currentStock + outItem.Quantity;
                                    item.lastUpdated = DateTime.Now;
                                    item.lastrate = outItem.Rate;
                                    lst.Add(new logsOutletStock
                                    {
                                        outletId = outItem.OutletId,
                                        itemId = outItem.ItemId,
                                        movementId = outItem.MovementId,
                                        quantity = outItem.Quantity,
                                        rate = item.lastrate,
                                        comment = outItem.Comment,
                                        username = outItem.UserName,
                                        createdDate = outItem.CreatedDate
                                    });
                                    foreach (var logItem in logsInventoryAlert.Where(p => p.outletId == outItem.OutletId && p.itemId == outItem.ItemId && p.isVisible == false && p.isSeen == false))
                                    {
                                        logItem.isVisible = true;
                                        logItem.messageComment = $"{item.item.normalizeName} is available now !! ({DateTime.Now.ToString("dd-MMM-yyyy")})";
                                    }
                                }
                            }

                        }
                        // entity.SaveChanges();
                        entity.logsOutletStocks.AddRange(lst);
                        entity.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        this._loggerService.LogException(ex);
                        trans.Rollback();
                    }
                }
            }
            return true;
        }
    }
}
