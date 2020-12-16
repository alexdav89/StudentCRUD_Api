using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Core.Common.Configuration {
    public class ConfigurationManager {

        public static void InitSettings(IConfiguration configuration) {            
            const string CONNECTIONS_SECTION = "ConnectionStrings";
            const string APPSETTINGS_SECTION = "AppSettings";
            //Connections
            if (configuration.GetSection(CONNECTIONS_SECTION).Exists()) {
                foreach (var item in configuration.GetSection(CONNECTIONS_SECTION).AsEnumerable()) {
                    var key = item.Key.Replace(CONNECTIONS_SECTION, "");
                    if (!string.IsNullOrWhiteSpace(key)) {
                        ConfigurationManager.ConnectionStrings.Add(key.TrimStart(':'), new ConfigConnection { ConnectionString = item.Value });
                    }
                }
            }

            //AppSettings
            if (configuration.GetSection(APPSETTINGS_SECTION).Exists()) {
                foreach (var item in configuration.GetSection(APPSETTINGS_SECTION).AsEnumerable()) {
                    var key = item.Key.Replace(APPSETTINGS_SECTION, "");
                    if (!string.IsNullOrWhiteSpace(key)) {
                        ConfigurationManager.AppSettings.Add(key.TrimStart(':'), item.Value);
                    }
                }
            }
        }
        static ConfigurationManager() {
            AppSettings = new NameValueCollection();
            ConnectionStrings = new Dictionary<string, ConfigConnection>();
        }
        public static NameValueCollection AppSettings { get; set; }
        public static Dictionary<string,ConfigConnection> ConnectionStrings { get; set; }
    }

    public class ConfigConnection {
        public string ConnectionString { get; set; }
    }
}
