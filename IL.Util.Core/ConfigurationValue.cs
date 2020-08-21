using System;
using System.Configuration;
namespace IL.Util.Core
{
    public static class ConfigurationValue
    {
        public static string GetConfigurationValue(this string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                    return ConfigurationSettings.AppSettings[key];
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }


        }

    }
}
