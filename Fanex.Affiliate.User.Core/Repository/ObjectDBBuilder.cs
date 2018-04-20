using System;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class ObjectDBBuilder : IObjectDBBuilder, IDisposable
    {
        public IObjectDb DB { get; private set; }

        public ObjectDBBuilder(string spKey, string dbMode = DBMode.Main)
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