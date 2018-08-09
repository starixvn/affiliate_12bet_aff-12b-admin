using System;
using System.Collections;
using System.Data;
using System.Web;
using DNAide.Core;
using AffiliateAdmin.Business.DataAccess;

/// <summary>
/// Summary description for Home
/// </summary>
namespace DataHandler
{
    public class Home
    {
        public static DbResult GetGenInforFromAff()
        {
            return DbHelper.Execute("Aff_Admin_GeneralInfo_Sel_xtest", DBType.AFFILIATE_12BET);
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
            param.Add("nrows", 0);
            param.Add("totalpages", 0);
            return DbHelper.Execute("Aff_UserOnlineList", DBType.AFFILIATE_12BET, param, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
    }
}
