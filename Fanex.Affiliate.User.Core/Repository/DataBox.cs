using Fanex.Affiliate.User.Core.DTO;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
