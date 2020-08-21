using IL.DTO.Core.PODTO;
using IM.Data.Core;
using System.Collections.Generic;

namespace IL.Service.Core.PRService
{
    public interface IPRService
    {
        public bool SavePR(prRequest prObj, List<prItem> itemObj);
        public bool ProcessPRItems(ProcessPODTO obj);
        public bool UpdatePRItem(int prId, int statusId, List<prItem> itemObj, string username);       
        public List<POReturnDTO> GetPORequestBasedOnFromOutletId(int outletId);
        public bool ImportPOItemToStore(int toOutletId, int fromOutletId, int prId, string username);
        public bool CheckPRStatus(int prId, int statusId);
        public List<POReturnDTO> GetPORequestBasedOnProcessStatus();
    }
}
