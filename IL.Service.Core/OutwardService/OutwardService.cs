﻿using IL.DTO.Core.ItemDTO;
using IL.Service.Core.LoggerService;
using IM.Data.Core;
using System;
using System.Collections.Generic;

namespace IL.Service.Core.OutwardService
{
    public class OutwardService : IOutwardService
    {
        private readonly ILoggerService _loggerService;
        public OutwardService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public bool SaveOutwardRecord(List<ItemOutWardStockDTO> obj)
        {
            using (var entity = DbInventoryEntities.Db_Entites)
            {
                using (var trans = entity.Database.BeginTransaction())
                {
                    try
                    {
                        List<logsOutletStock> lst = new List<logsOutletStock>();
                        var items = entity.outletStocks;
                        foreach (var item in items)
                        {
                            foreach (var outItem in obj)
                            {
                                if (item.itemId == outItem.ItemId && item.outletId == outItem.OutletId)
                                {
                                    item.currentStock = item.currentStock - outItem.Quantity;
                                    item.lastUpdated = DateTime.Now;
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
                                }
                            }

                        }
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
