using System.Collections;
using System.Data;
using System.Web;
using DNAide.Data;
using DNAide.Core;
using System.Web.SessionState;
using System;

/// <summary>
/// @Khoa.Tran 
/// Summary description for Member
/// </summary>
namespace DataHandler
{
    public class Member : IRequiresSessionState
    {
        public static DataTable SelMemInfoAll(string strAffUsername, string strUsername, string strUsername2, ref int pageIndex, int intPageSize, ref int totalPages, ref int NRows)
        {
            DbResult dbr = null;
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(strAffUsername))
                hsParam.Add("affusername", strAffUsername);
            hsParam.Add("username", strUsername);
            hsParam.Add("ibcusername", strUsername2);
            hsParam.Add("pagesize", intPageSize); // hardcode pagesize for member/player list
            hsParam.Add("nrows", 0);
            dbr = DbHelper.Execute("Aff_MemberListSel", hsParam, DBType.AFFILIATE_12BET);
            NRows = CastAide.AsInt32(dbr.Outputs["nrows"]);
            totalPages = NRows % intPageSize == 0 ? NRows / intPageSize : (NRows / intPageSize) + 1;
            
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }
        public static decimal GetPSPToday()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("psp", 0);
            DbResult dbr = DbHelper.Execute("DEP_AffiliateGetPSP", hsParam, DBType.DEPOSIT);
            return CastAide.AsDecimal(dbr.Outputs["psp"]);
        }
        public static string DeleteMember(int IbcCustId)
        {
            Hashtable param = new Hashtable();
            param.Add("ibccustid", IbcCustId);
            param.Add("userid", AppSettings.Session.CustID);
            param.Add("ip", HttpContext.Current.Request.UserHostAddress);
            DbResult ret = DbHelper.Execute("Aff_MemberDel", param, DBType.AFFILIATE_12BET);
            if (CastAide.AsInt32(ret.Outputs["errcode"]) != 0)
            {
                return ret.Outputs["errmsg"].ToString();
            }
            else
            {
                //delete memeber from affiliate DB sucessfully
                //remove member from deposit DB
                int affId =CastAide.AsInt32(ret.Outputs["affid"]);
                int refNo = CastAide.AsInt32(ret.Outputs["refno"]);
                int depositCustId = CastAide.AsInt32(ret.Outputs["depositcustid"]);
                Hashtable param1 = new Hashtable();
                param1.Add("depositcustid", depositCustId);
                DbResult ret1 = DbHelper.Execute("DEP_AdminUpdAffID", param1, DBType.DEPOSIT);
                if (CastAide.AsInt32(ret1.Outputs["errcode"]) != 0)
                {
                    return CastAide.AsString(ret1.Outputs["errmsg"]);
                }
                else
                {
                    //write log to Affiliate_12Bet
                    Hashtable logParam = new Hashtable();
                    logParam.Add("creator", AppSettings.Session.CustID);
                    logParam.Add("affid", affId);
                    logParam.Add("typename", "Member");
                    logParam.Add("spname", "DEP_AdminUpdAffID");
                    logParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                    logParam.Add("refno", refNo);
                    logParam.Add("details", "Remove member. DepositCustID=" + depositCustId + " (On Deposit_DB)");
                    DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE_12BET);
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// Get member information
        /// </summary>
        /// <param name="IBCCustID"></param>
        /// <returns></returns>
        public static DataTable SelMemInfo(int IBCCustID)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("ibccustid", IBCCustID);
            DbResult dbr = DbHelper.Execute("Aff_MemberSel", hsParam, DBType.AFFILIATE_12BET);
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }

        /// <summary>
        /// Get member user name by id
        /// </summary>
        /// <param name="IBCCustID"></param>
        /// <returns></returns>
        public static string GetMemberUserName(int IBCCustID)
        {
            DataTable memberInfo = SelMemInfo(IBCCustID);
            
            if(memberInfo.Rows.Count>0)
            {
                return Convert.IsDBNull(memberInfo.Rows[0]["UserName"])?"":memberInfo.Rows[0]["UserName"].ToString();
            }
            return "";
        }

        /// <summary>
        /// Get member from deposit DB
        /// </summary>
        /// <param name="IBCCustID"></param>
        /// <returns></returns>
        public static DataTable SelDepositMemInfo(int AffId, DateTime dtmFDate, DateTime dtmTDate, int intMemberType, bool isGetAll, int intPage, int intPageSize, ref int intTotalPages)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affiliateid", AffId);
            if (!isGetAll)
            {
                hsParam.Add("fromdate", dtmFDate);
                hsParam.Add("todate", dtmTDate);
            }
            hsParam.Add("membertype", intMemberType);
            hsParam.Add("pageindex", intPage);
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("totalpages", intTotalPages);
            DbResult dbr = DbHelper.Execute("DEP_AffiliateSelMemberList", hsParam, DBType.DEPOSIT);
            intTotalPages = CastAide.AsInt32(dbr.Outputs["totalpages"]);
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }

        public static DataTable SelActiveMemberInfo(string strUserName)
        {
            DbResult dbr = null;
            Hashtable hsParam = new Hashtable();
            hsParam.Add("usernames", strUserName);
            dbr = DbHelper.Execute("Aff_CustomerCheckList12BetPlayerAccountsActivated", hsParam, DBType.AFFILIATE_12BET);            
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }

        public static DataTable SelNotActiveMemberInfo(string strUserName)
        {
            DbResult dbr = null;
            Hashtable hsParam = new Hashtable();
            hsParam.Add("usernames", strUserName);
            dbr = DbHelper.Execute("Deposit_MembersNotActivatedSel", hsParam, DBType.DEPOSIT);
            return (dbr.ResultSet.Tables.Count > 0 ? dbr.ResultSet.Tables[0] : new DataTable());
        }
    }
}
