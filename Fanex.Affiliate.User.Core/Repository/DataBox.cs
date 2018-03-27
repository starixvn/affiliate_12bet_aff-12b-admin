using System;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class DataBox : IDataBox, IDisposable
    {
        public IObjectDb DB { get; }

        public DataBox(string spKey, string dbMode = DBMode.Main)
        {
            DB = new ObjectDb(spKey + dbMode);
        }

        public void Dispose()
        {
            if (DB != null)
            {
                DB.Dispose();
            }
        }
    }
}