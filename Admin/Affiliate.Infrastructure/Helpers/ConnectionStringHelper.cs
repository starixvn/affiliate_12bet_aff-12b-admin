using System;
using System.Collections.Concurrent;
using System.Configuration;

namespace Affiliate.Infrastructure.Helpers
{
    public static class ConnectionStringHelper
    {
        private static ConcurrentDictionary<string, string> connectionStringList = new ConcurrentDictionary<string, string>();

        public static string GetConnectionString(string connectionStringName, bool isEncryptedConnection = true)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentNullException(nameof(connectionStringName));
            }
            var key = string.Format("{0}:ConnectionString", connectionStringName);
            var text = connectionStringList.ContainsKey(key) ? connectionStringList[key] : string.Empty;

            if (string.IsNullOrEmpty(text))
            {
                if (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
                {
                    throw new ArgumentNullException("Can not find connection string :" + connectionStringName);
                }
                text = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new ArgumentNullException(connectionStringName + "is null or empty");
                }
                if (isEncryptedConnection)
                {
                    text = Cryptography.DeCryptConnectionString(text);
                }

                connectionStringList.AddOrUpdate(key, text, (ckey, oldvalue) => text);
            }

            return text;
        }
    }
}