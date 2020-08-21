using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.App_Code
{
    public static class ModelObjectHelper
    {
        public static object GetInvaildModelObject(this ModelStateDictionary Model)
        {
            return Model; 
        }
    }
}