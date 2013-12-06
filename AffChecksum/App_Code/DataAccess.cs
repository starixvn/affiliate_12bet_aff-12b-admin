using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using DNAide.Data;
using DNAide.Core;
using System.Collections;

namespace DAL
{
    public class DataAccess
    {
        public static DbResult CheckSumMemberDaily(DateTime date, out bool isMatch)
        {
            try
            {
                Hashtable p = new Hashtable();
                p.Add("ismatch", 0);
                p.Add("date", date.Date);
                DbResult rs = DbHelper.Execute("Aff_CheckSum", p, Common.DBType.Affiliate);
                isMatch = CastAide.AsBoolean(rs.Outputs["ismatch"]);
                return rs;
            }
            catch { throw; }
        }

        public static DbResult IA_CheckSumMemberDaily(DateTime date, out bool isMatch)
        {
            try
            {
                Hashtable p = new Hashtable();
                p.Add("ismatch", 0);
                p.Add("date", date.Date);
                DbResult rs = DbHelper.Execute("IA_CheckSum", p, Common.DBType.IncomeAccess);
                isMatch = CastAide.AsBoolean(rs.Outputs["ismatch"]);
                return rs;
            }
            catch { throw; }
        }
    }
}
