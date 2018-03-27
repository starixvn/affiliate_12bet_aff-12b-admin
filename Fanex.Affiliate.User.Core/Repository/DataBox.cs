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
        private readonly IDbSetting _dbSetting;
        private readonly string _dBMode;
        private readonly string _spKey;
        public DataBox(string spKey, string dbMode = DBMode.Main)
        {

            _dBMode = dbMode;
            _spKey = spKey;
        }

        public IObjectDb ObjectDb => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
