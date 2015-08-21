using System.Collections;
using System.Text;
using System.Web;
using DNAide.Core;
using Fanex.Data;

namespace AffiliateAdmin.Business.DataAccess
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
            WriteLogCallingSp(pSPName, pHsParam);
            if (HttpContext.Current.Session["lastCalledSP"] == null ||
                    string.Compare(HttpContext.Current.Session["lastCalledSP"].ToString(), pSPName, true) != 0)
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

                HttpContext.Current.Session["lastCalledSP"] = null;
                return result;
            }
            else
            {
                HttpContext.Current.Response.End();
                return null;
            }
        }

        private static void WriteLogCallingSp(string pSPName, Hashtable pHsParam)
        {
            StringBuilder strDoing = new StringBuilder();
            strDoing.Append(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath + " ");
            if (pHsParam != null)
            {
                foreach (DictionaryEntry Item in pHsParam)
                {
                    strDoing.Append(Item.Key.ToString() + " = " + CastAide.AsString(Item.Value) + ", ");
                }
            }

            Hashtable hsParam = new Hashtable();
            hsParam.Add("creator", AppSettings.Session.CustID);
            hsParam.Add("spname", pSPName);
            hsParam.Add("details", strDoing.ToString());
            hsParam.Add("affid", AppSettings.Session.CustID);
            hsParam.Add("isadmin", 0);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            var setting = new DbSetting()
            {
                ConnectionString = ConnectionStringGetter.GetConnectionString(DBType.AFFILIATE_12BET),
                SpName = "aff_writeLog_userlog_runsp"
            };
            using (var objectDB = new ObjectDb(setting))
            {
                objectDB.ExecuteNonQuery(hsParam);
            }
        }
    }
}