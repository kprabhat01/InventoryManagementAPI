using System;

namespace IL.Service.Core.LoggerService
{
    public interface ILoggerService
    {
        public void Log(string data);
        public void LogException(Exception ex);
    }
}
