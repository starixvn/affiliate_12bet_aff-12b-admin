using System.Collections;
using System.Text;
using System.Web;
using DNAide.Core;
using Fanex.Data;

namespace AffiliateUser.Business.DataAccess
{
    /// <summary>
    /// Summary description for DbHelper
    /// </summary>
    public class DbHelper
    {
        // used for timeout of report-oriented SP execution.
        public static int MAX_EXEC_TIME_LENGTH_RPTSP = 30000;
        // used for timeout of non report-oriented SP execution.
        public static int MAX_EXEC_TIME_LENGTH_NONRPTSP = 3000;

        public static DbResult Execute(string pSPName, string pDB, Hashtable pHsParam = null, int pTimeOut = 120)
        {
            var setting = new DbSetting()
            {
                ConnectionString = ConnectionStringGetter.GetConnectionString(pDB),
                SpName = pSPName,
                ExecuteTimeOut = pTimeOut
            };
            DbResult result;
            using (var objectDB = new ObjectDb(setting))
            {
                var dataSet = objectDB.ExecuteDataSet(pHsParam);
                result = new DbResult(dataSet, objectDB.GetOuputParams());
            }

            return result;
        }
    }
}