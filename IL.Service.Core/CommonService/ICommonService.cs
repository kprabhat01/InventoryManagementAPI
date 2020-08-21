﻿using IL.DTO.Core.CommonDTO;
using System.Collections.Generic;

namespace IL.Service.Core.CommonService
{
    public interface ICommonService
    {
        List<CommonDTO> GetOutlets();
        List<CommonDTO> GetRoles();
        List<CommonDTO> GetMovementType();
        List<CommonDTO> GetUnits();
        List<CommonDTO> GetStatus();
        List<CommonDTO> UserSpecificOutlet(int userId);
        bool ImportNewItemsFromCreation(int outletId);
    }
}
