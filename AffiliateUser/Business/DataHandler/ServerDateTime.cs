using System;
using System.Collections;
using AffiliateUser.Business.DataAccess;

namespace DataHandler
{
    public class ServerDateTime
    {
        public static DateTime GetCurrentDBTime(out string GMT)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("dbtime", DateTime.Now);
            hsParam.Add("gmtoffset", "GMT");

            var result = DbHelper.Execute("Aff_AdminSelDBTime", DBType.AFFILIATE_12BET, hsParam);
            GMT = result.Outputs["gmtoffset"].ToString();
            return Convert.ToDateTime(result.Outputs["dbtime"]);
        }
    }
}

