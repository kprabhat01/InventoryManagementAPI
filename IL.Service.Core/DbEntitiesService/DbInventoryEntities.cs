using IM.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Service.Core
{
    class DbInventoryEntities
    {
        public static db_InventoryEntities Db_Entites
        {
            get { return new db_InventoryEntities(); }
        }
    }
}
