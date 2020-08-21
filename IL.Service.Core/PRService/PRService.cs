using IL.DTO.Core.PODTO;
using IL.Service.Core.CommonService;
using IL.Service.Core.LoggerService;
using IM.Data.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IL.Service.Core.PRService
{
    public class PRService : IPRService
    {


        private ICommonService _commonService;
        private ILoggerService _loggerService;


        public PRService(ICommonService commonService, ILoggerService logger)
        {
            this._commonService = commonService;
            this._loggerService = logger;
        }
        public bool SavePR(prRequest prObj, List<prItem> itemObj)
        {
            using (var entites = new db_InventoryEntities())
            {
                using (var transaction = entites.Database.BeginTransaction())
                {
                    try
                    {
                        entites.prRequests.Add(prObj);
                        entites.SaveChanges();
                        foreach (var item in itemObj)
                            item.prId = prObj.id;

                        SavePRItem(itemObj, entites);
                        InsertPrStatus(prObj.id, prObj.statusId, prObj.createdBy, entites);
                        // SavePRItemLogs(prObj.id, null, itemObj, prObj.createdBy);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        this._loggerService.LogException(ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        private bool SavePRItem(List<prItem> itemObj, db_InventoryEntities entites)
        {
            entites.prItems.AddRange(itemObj);
            entites.SaveChanges();
            return true;
        }

        private bool InsertPrStatus(int prId, int statusId, string username, db_InventoryEntities entities)
        {
            entities.logsPRStatus.Add(new logsPRStatu
            {
                prId = prId,
                statusId = statusId,
                changedDate = DateTime.Now,
                changedBy = username
            });
            entities.SaveChanges();
            return true;
        }
        private bool SavePRItemLogs(int prId, List<prItem> oldObj, List<prItem> itemObj, string username, db_InventoryEntities entities)
        {

            entities.prItemLogs.Add(new prItemLog
            {
                prid = prId,
                createdDate = DateTime.Now,
                oldRequest = JsonConvert.SerializeObject(itemObj).ToString() ?? null,
                newRequest = JsonConvert.SerializeObject(itemObj).ToString(),
                username = username
            });
            entities.SaveChanges();
            return true;
        }


        public bool UpdatePRItem(int prId, int statusId, List<prItem> itemObj, string username)
        {
            using (var entites = new db_InventoryEntities())
            {
                using (var transaction = entites.Database.BeginTransaction())
                {
                    try
                    {
                        var obj = entites.prItems.Where(p => p.prId == prId).ToList();
                        SavePRItemLogs(prId, obj, itemObj, username, entites);
                        obj = itemObj;
                        entites.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        this._loggerService.LogException(ex);
                        transaction.Rollback();
                        return false;
                    }

                    return true;
                }
            }

        }

        private bool UpdatePRStatus(int prId, int statusId, string username, db_InventoryEntities entities)
        {


            var prRequest = entities.prRequests.SingleOrDefault(p => p.id == prId);
            prRequest.statusId = statusId;
            entities.SaveChanges();
            InsertPrStatus(prId, statusId, username, entities);
            return true;

        }
        public List<POReturnDTO> GetPORequestBasedOnFromOutletId(int outletId)
        {
            using (var entities = new db_InventoryEntities())
            {
                var poRequest = entities.prRequests.Where(p => p.fromOutletId == outletId && p.statusId != 4).AsEnumerable().OrderByDescending(p => p.id)
                     .Select(opt => new POReturnDTO
                     {
                         Id = opt.id,
                         FromStore = new DTO.Core.CommonDTO.CommonDTO { Id = opt.outlet.id, NormalizeName = opt.outlet.NormalizeName },
                         ToStore = new DTO.Core.CommonDTO.CommonDTO { Id = opt.outlet1.id, NormalizeName = opt.outlet1.NormalizeName },
                         Username = opt.createdBy,
                         CreatedDate = opt.createdDate,
                         Status = new DTO.Core.CommonDTO.CommonDTO { Id = opt.Status.id, NormalizeName = opt.Status.name },
                         ItemDetail = entities.prItems.AsQueryable().Where(p => p.prId == opt.id)
                         .Select(it => new POItemReturnDTO
                         {
                             Id = it.id,
                             Item = new DTO.Core.CommonDTO.CommonDTO { Id = it.item.id, NormalizeName = it.item.normalizeName },
                             Quantity = it.quantity,
                             Rate = it.rate ?? 0,
                             Unit = new DTO.Core.CommonDTO.CommonDTO { Id = it.item.unit.id, NormalizeName = it.item.unit.normalizeName }
                         }).ToList()

                     }).ToList();
                return poRequest;
            }
        }

        public bool ProcessPRItems(ProcessPODTO obj)
        {
            using (var entities = new db_InventoryEntities())
            {
                using (var trans = entities.Database.BeginTransaction())
                {
                    try
                    {
                        UpdatePRStatus(obj.Id, obj.StatusID, obj.Username, entities);
                        var items = entities.prItems.Where(p => p.prId == obj.Id);
                        foreach (var item in items)
                        {
                            foreach (var receivedItem in obj.Items)
                            {
                                if (item.id == receivedItem.ItemId)
                                {
                                    item.quantity = receivedItem.Quantity;
                                    item.rate = receivedItem.Rate;
                                }
                            }
                        }
                        entities.SaveChanges();
                        trans.Commit();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        this._loggerService.LogException(ex);
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }
        public bool ImportPOItemToStore(int toOutletId, int fromOutletId, int prId, string username)
        {

            if (this._commonService.ImportNewItemsFromCreation(toOutletId) && this._commonService.ImportNewItemsFromCreation(fromOutletId))
            {
                using (var entities = new db_InventoryEntities())
                    entities.IMPORT_ITEM_FROM_PR(fromOutletId, toOutletId, prId, username);
                return true;
            }
            return false;
        }
        public bool CheckPRStatus(int prId, int statusId)
        {
            using (var entities = new db_InventoryEntities())
                return entities.prRequests.Any(p => p.id == prId && p.statusId == statusId);
        }

        public List<POReturnDTO> GetPORequestBasedOnProcessStatus()
        {
            using (var entities = new db_InventoryEntities())
            {
                var poRequest = entities.prRequests.Where(p => p.statusId == 2 || p.statusId == 3).AsEnumerable().OrderByDescending(p => p.id)
                     .Select(opt => new POReturnDTO
                     {
                         Id = opt.id,
                         FromStore = new DTO.Core.CommonDTO.CommonDTO { Id = opt.outlet.id, NormalizeName = opt.outlet.NormalizeName },
                         ToStore = new DTO.Core.CommonDTO.CommonDTO { Id = opt.outlet1.id, NormalizeName = opt.outlet1.NormalizeName },
                         Username = opt.createdBy,
                         CreatedDate = opt.createdDate,
                         Status = new DTO.Core.CommonDTO.CommonDTO { Id = opt.Status.id, NormalizeName = opt.Status.name },
                         ItemDetail = entities.prItems.AsQueryable().Where(p => p.prId == opt.id)
                         .Select(it => new POItemReturnDTO
                         {
                             Id = it.id,
                             Item = new DTO.Core.CommonDTO.CommonDTO { Id = it.item.id, NormalizeName = it.item.normalizeName },
                             Quantity = it.quantity,
                             Rate = it.rate ?? 0,
                             Unit = new DTO.Core.CommonDTO.CommonDTO { Id = it.item.unit.id, NormalizeName = it.item.unit.normalizeName }
                         }).ToList()

                     }).ToList();
                return poRequest;
            }
        }
    }
}
