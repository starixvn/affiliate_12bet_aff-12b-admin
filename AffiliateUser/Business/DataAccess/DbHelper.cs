using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;
using Fanex.Data;

namespace AffiliateUser.Business.DataAccess
{
    /// <summary>
    /// Summary description for AffSPExecuter
    /// </summary>
    public class DbHelper
    {
        public static int MAX_EXEC_TIME_LENGTH_RPTSP = 30000;//used for timeout of report-oriented SP execution
        public static int MAX_EXEC_TIME_LENGTH_NONRPTSP = 3000;//used for timeout of non report-oriented SP execution


        public static DbResult Execute(string pSPName, Hashtable pHsParam, string pDB, int pTimeOut = 10)
        {
            WriteLogDB(pSPName, pHsParam);
            if (HttpContext.Current.Session["lastCalledSP"] == null ||
                    string.Compare(HttpContext.Current.Session["lastCalledSP"].ToString(), pSPName, true) != 0)
            {
                var setting = new DbSetting()
                {
                    DbName = pDB,
                    SpName = pSPName
                };
                DbResult result;
                using (var objectDB = new ObjectDb(setting))
                {
                    var dataSet = objectDB.ExecuteDataSet(pHsParam);
                    result = new DbResult(dataSet);
                }

                // DbResult ret = DNAide.Data.DbHelper.Execute(pSPName, pHsParam, pDB, pTimeOut);
                HttpContext.Current.Session["lastCalledSP"] = null;
                return result;
            }
            else
            {
                HttpContext.Current.Response.End();
                return null;
            }
        }

        public static DbResult Execute(string pSPName, string pDB)
        {
            WriteLogDB(pSPName, null);
            if (HttpContext.Current.Session["lastCalledSP"] == null ||
                    string.Compare(HttpContext.Current.Session["lastCalledSP"].ToString(), pSPName, true) != 0)
            {
                var setting = new DbSetting()
                {
                    DbName = pDB,
                    SpName = pSPName
                };
                DbResult result;
                using (var objectDB = new ObjectDb(setting))
                {
                    var dataSet = objectDB.ExecuteDataSet();
                    result = new DbResult(dataSet);
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
        /// <summary>
        /// @Johnny: Write log when Store is called
        /// </summary>
        /// <param name="pSPName"></param>
        /// <param name="pHsParam"></param>
        public static void WriteLogDB(string pSPName, Hashtable pHsParam)
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
                DbName = DBType.AFFILIATE_12BET,
                SpName = "aff_writeLog_userlog_runsp"
            };
            using (var objectDB = new ObjectDb(setting))
            {
                objectDB.ExecuteNonQuery(hsParam);
            }
            // DNAide.Data.DbHelper.Execute("aff_writeLog_userlog_runsp", hsParam, DBType.AFFILIATE_12BET);
        }
    }
}