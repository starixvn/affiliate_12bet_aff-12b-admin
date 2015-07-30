using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using Criterias;
using DNAide.Data;
using DNAide.Core;
using System.Globalization;
using System.Threading;
using System.Data;
namespace DataHandler
{
    /// <summary>
    /// Summary description for MemberInfo
    /// </summary>
    public class MemberInfo
    {        
        public static DbResult SelByAffiliate(int affID, object fromdate, object todate, int deposit, int pageIndex, int pageSize, ref int totalPages)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affID);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("fromdate", fromdate);
            hsParam.Add("todate", todate);
            hsParam.Add("isdeposit", deposit);
            hsParam.Add("totalpages", 0);
            DbResult rs = DbHelper.Execute("Aff_MemberInfoSel", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
            totalPages = CastAide.AsInt32(rs.Outputs["totalpages"], 1);            
            return rs;
        }
        public static DataTable SelCreatedDates(string usernames)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("usernamelist", usernames);
            DbResult rs = DbHelper.Execute("Aff_MemberInfoGetCreatedDate", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
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
            DbResult dbr = DbHelper.Execute("Aff_DEP_MemberListSel", hsParam, DBType.AFFILIATE_12BET);
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

            var rs = DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET);
            return CastAide.AsBoolean(rs.Outputs["belonglevel2"]);
        }
    }
}