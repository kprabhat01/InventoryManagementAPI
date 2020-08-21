using IL.Service.Core.LoggerService;
using System.Net.Http;
using System.Web.Http.Filters;
 

namespace Inventory.App_Code.Filters
{
    public class ExceptionFilter 
    {
        private readonly LoggerService _logger;
        public ExceptionFilter()
        {
            this._logger = new LoggerService();
        }
        
       
    }
}