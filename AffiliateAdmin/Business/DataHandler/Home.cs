using System;
using System.Collections;
using System.Data;
using System.Web;
using DNAide.Core;
using DNAide.Data;

/// <summary>
/// Summary description for Home
/// </summary>
namespace DataHandler
{
    public class Home
    {
        public static DbResult GetGenInforFromAff()
        {
            return DbHelper.Execute("Aff_HomeGeneralInfo", DBType.AFFILIATE_12BET);
        }

        public static DbResult GetGenInforFromDeposit()
        {
            return DbHelper.Execute("DEP_AffiliateGeneralInfo", DBType.DEPOSIT);
        }

        public static DbResult GetOnlineList(int pageSize, int pageIndex)
        {
            Hashtable param = new Hashtable();
            param.Add("pagesize", pageSize);
            param.Add("pageindex", pageIndex);
            return DbHelper.Execute("Aff_UserOnlineList",param, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
    }
}
