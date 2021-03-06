﻿using IL.DTO.Core.PODTO;
using IL.DTO.Core.ReportDTO;
using IM.Data.Core;
using System;
using System.Collections.Generic;

namespace IL.Service.Core.PRService
{
    public interface IPRService
    {
        public bool SavePR(prRequest prObj, List<prItem> itemObj);
        public bool ProcessPRItems(ProcessPODTO obj);
        public bool UpdatePRItem(int prId, int statusId, List<prItem> itemObj, string username);       
        public List<POReturnDTO> GetPORequestBasedOnFromOutletId(int outletId);
        public bool ImportPOItemToStore(int toOutletId, int fromOutletId, int prId, string username, List<int> poItem);
        public bool CheckPRStatus(int prId, int statusId);
        public List<POReturnDTO> GetPORequestBasedOnProcessStatus();
        public bool SaveNotificationForItemAvilability(List<PONotification> obj);
        public List<POReturnDTO> GetPODetailReport(DateTime fromDate, DateTime toDate, int outletId);
    }
}
