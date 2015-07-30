using System.Collections;
using DNAide.Data;
using DNAide.Core;
using System;
/// <summary>
/// Summary description for Statement
/// </summary>
/// 
namespace DataHandler
{
    public class Statement
    {
        public static DbResult SelStatementHistory(int affID, int transType, object dtmFDate, object dtmTDate, int pageIndex, int pageSize, ref int totalPages)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affID);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("totalpages", 0);
            DbResult rs = DbHelper.Execute("Aff_AffiliateStatementHistorySel", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
            totalPages = CastAide.AsInt32(rs.Outputs["totalpages"], 1);
            return rs;
        }      
    }
}
