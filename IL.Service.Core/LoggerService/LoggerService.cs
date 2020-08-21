using System;
using System.IO;
using System.Web;

namespace IL.Service.Core.LoggerService
{
    public class LoggerService : ILoggerService
    {
        private static readonly string _serverFolderApp;

        static LoggerService()
        {
            _serverFolderApp = HttpContext.Current.Server.MapPath("~/logs");
            CreateIfNotLogFolder();

        }
        private static bool CreateIfNotLogFolder()
        {
            if (!Directory.Exists(_serverFolderApp))
                Directory.CreateDirectory(_serverFolderApp);
            return true;
        }

        public void LogException(Exception ex)
        {
            using (StreamWriter writer = new StreamWriter($"{_serverFolderApp}/logs_{DateTime.Now.ToString("ddMMyyyy")}.log"))
            {
                writer.WriteLine($"Exception {DateTime.Now}");
                writer.WriteLine("--------------------------");
                writer.WriteLine(ex.Message);
                writer.WriteLine(ex.StackTrace);
                writer.Close();
            }

        }

        public void Log(string data)
        {
            using (StreamWriter writer = new StreamWriter($"{_serverFolderApp}/logs_{DateTime.Now.ToString("ddMMyyyy")}.log"))
            {
                writer.WriteLine($"System Log {DateTime.Now}");
                writer.WriteLine("--------------------------");
                writer.WriteLine(data);
                writer.Close();
            }
        }
    }
}
