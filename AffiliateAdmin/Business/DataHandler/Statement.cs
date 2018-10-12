using System;
using System.Collections;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;

/// <summary>
/// Summary description for Statement
/// </summary>

namespace DataHandler
{
    public class Statement
    {
        //@Vinh
        //Statement history
        public static DbResult SelStatementHistory(int affID, int transType, object dtmFDate, object dtmTDate, int pageIndex, int pageSize, ref int totalPages)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affID);
            if (transType != -1)
                hsParam.Add("transtype", transType);
            if (dtmFDate != null && dtmTDate != null)
            {
                if (Convert.ToDateTime(dtmFDate).CompareTo(Convert.ToDateTime(dtmTDate)) > 0)
                {
                    totalPages = 0;
                    return null;
                }
                else
                {
                    hsParam.Add("fdate", dtmFDate);
                    hsParam.Add("tdate", dtmTDate);
                }
            }

            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("totalpages", 0);
            DbResult rs = DbHelper.Execute("Aff_Admin_StatementHistory_Sel", DBType.AFFILIATE_12BET, hsParam);
            totalPages = CastAide.AsInt32(rs.Outputs["totalpages"], 1);
            return rs;
        }

    }
}
