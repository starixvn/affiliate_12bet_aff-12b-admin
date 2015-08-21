
using System.Collections;
using System.Data;
using AffiliateUser.Business.DataAccess;
using Criterias;
using DNAide.Core;
namespace DataHandler
{
    /// <summary>
    /// Summary description for MemberInfo
    /// </summary>
    public class MemberInfo
    {               
        public static DataTable SelCreatedDates(string usernames)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("usernamelist", usernames);
            DbResult rs = DbHelper.Execute("Aff_MemberInfoGetCreatedDate", DBType.AFFILIATE_12BET, hsParam, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
            return (rs.ResultSet.Tables.Count==0? null : rs.ResultSet.Tables[0]);
        }
        /// <summary>
        /// Get member from deposit DB
        /// </summary>
        /// <param name="IBCCustID"></param>
        /// <returns></returns>
        public static DbResult SelDepositMemInfo(int AffId, object dtmFDate, object dtmTDate, int intMemberType, int intPage, int intPageSize, ref int intTotalPages)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affiliateid", AffId);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("membertype", intMemberType);
            hsParam.Add("pageindex", intPage);
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("totalpages", intTotalPages);
            DbResult dbr = DbHelper.Execute("Aff_DEP_MemberListSel", DBType.AFFILIATE_12BET, hsParam);
            intTotalPages = CastAide.AsInt32(dbr.Outputs["totalpages"]);
            return dbr;
        }

        public static bool ValidatePermission(ValidationCriteria criteria)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("adminname", "");
            hsParam.Add("affid", criteria.AffId);
            hsParam.Add("membername", criteria.MemberName);
            hsParam.Add("belonglevel1", 0);
            hsParam.Add("belonglevel2", 0);

            var rs = DbHelper.Execute(criteria.SpName, DBType.AFFILIATE_12BET, hsParam);
            return CastAide.AsBoolean(rs.Outputs["belonglevel2"]);
        }
    }
}