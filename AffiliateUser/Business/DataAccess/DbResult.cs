using System.Collections;
using System.Data;

namespace AffiliateUser.Business.DataAccess
{
    public class DbResult
    {
        public DbResult(DataSet resultSet, Hashtable outputs = null)
        {
            this.Outputs = outputs;
            this.ResultSet = resultSet;
        }

        public Hashtable Outputs { get; private set; }
        public DataSet ResultSet { get; private set; }
    }
}