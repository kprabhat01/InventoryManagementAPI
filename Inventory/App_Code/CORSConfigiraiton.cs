using IL.Util.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Inventory.App_Code
{
    public static  class CORSConfigiraiton
    {
        public static void EnableClientCORS(this HttpConfiguration config) {
            EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:8100", "*", "*");
            config.EnableCors(cors);
        }
    }
}