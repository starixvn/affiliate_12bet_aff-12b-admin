using System;
using System.Collections;
using DNAide.Data;

namespace DataHandler
{
    public class ServerDateTime
    {
        public static DateTime GetCurrentDBTime(out string GMT)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("dbtime", DateTime.Now);
            hsParam.Add("gmtoffset", "GMT");

            DbResult result = DNAide.Data.DbHelper.Execute("Aff_AdminSelDBTime", hsParam, DBType.AFFILIATE_12BET);
            GMT = result.Outputs["gmtoffset"].ToString();
            return Convert.ToDateTime(result.Outputs["dbtime"]);
        }
    }
}

