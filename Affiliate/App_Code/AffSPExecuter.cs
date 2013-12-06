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

/// <summary>
/// Summary description for AffSPExecuter
/// </summary>
public class DbHelper
{
    public static int MAX_EXEC_TIME_LENGTH_RPTSP = 30000;//used for timeout of report-oriented SP execution
    public static int MAX_EXEC_TIME_LENGTH_NONRPTSP = 3000;//used for timeout of non report-oriented SP execution


    public static DbResult Execute(string pSPName, Hashtable pHsParam, string pDB, int pTimeOut)
    {
        WriteLogDB(pSPName, pHsParam);
        if (HttpContext.Current.Session["lastCalledSP"] == null ||
                string.Compare(HttpContext.Current.Session["lastCalledSP"].ToString(), pSPName, true) != 0)
        {
            DbResult ret = DNAide.Data.DbHelper.Execute(pSPName, pHsParam, pDB, pTimeOut);
            HttpContext.Current.Session["lastCalledSP"] = null;
            return ret;
        }
        else
        {
            HttpContext.Current.Response.End();
            return null;
        }
    }

    public static DbResult Execute(string pSPName, Hashtable pHsParam, string pDB)
    {
        WriteLogDB(pSPName, pHsParam);
        if (HttpContext.Current.Session["lastCalledSP"] == null ||
                string.Compare(HttpContext.Current.Session["lastCalledSP"].ToString(), pSPName, true) != 0)
        {
            DbResult ret = DNAide.Data.DbHelper.Execute(pSPName, pHsParam, pDB, DbHelper.MAX_EXEC_TIME_LENGTH_NONRPTSP);
            HttpContext.Current.Session["lastCalledSP"] = null;
            return ret;
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
            DbResult ret = DNAide.Data.DbHelper.Execute(pSPName, pDB);
            HttpContext.Current.Session["lastCalledSP"] = null;
            return ret;
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
        DNAide.Data.DbHelper.Execute("aff_writeLog_userlog_runsp", hsParam, DBType.AFFILIATE_12BET);
    }
}
