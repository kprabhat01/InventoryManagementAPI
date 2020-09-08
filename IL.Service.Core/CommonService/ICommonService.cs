using IL.DTO.Core.CommonDTO;
using System.Collections.Generic;

namespace IL.Service.Core.CommonService
{
    public interface ICommonService
    {
        List<CommonOutletDTO> GetOutlets();
        List<CommonDTO> GetRoles();
        List<CommonDTO> GetMovementType();
        List<CommonDTO> GetUnits();
        List<CommonDTO> GetStatus();
        List<CommonDTO> UserSpecificOutlet(int userId);
        bool ImportNewItemsFromCreation(int outletId);
        List<Notification> GetNotifications(int userId);
        bool MarkViewToNotification(int userId);
         
    }
}
