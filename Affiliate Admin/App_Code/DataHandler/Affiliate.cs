using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Collections;
using System.Data;
using Criterias;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;
using Microsoft.Practices.EnterpriseLibrary.Caching;

/// <summary>
/// Createrr: Vinh
/// </summary>
namespace DataHandler
{
    public class Affiliate
    {
        public static DbResult ListAff(AffiliateListCriteria criteria)
        {
            var hsParam = new Hashtable();
            hsParam.Add("userid", criteria.UserId);
            hsParam.Add("status", criteria.Status);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("pagesize", criteria.PageSize);
            if (criteria.GroupId != -1)
            {
                hsParam.Add("groupid", criteria.GroupId);
            }

            hsParam.Add("language", criteria.Language);
            
            if (!string.IsNullOrEmpty(criteria.Username))
            {
                hsParam.Add("username", criteria.Username);
            }

            if (!string.IsNullOrEmpty(criteria.Email))
            {
                hsParam.Add("email", criteria.Email);
            }

            if(criteria.AffId !=-1)
                hsParam.Add("affid", criteria.AffId);

            if (criteria.IsClosed)
                hsParam.Add("isclosed", criteria.IsClosed);

            if(criteria.IsAll)
                hsParam.Add("isall", criteria.IsAll);

            return DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET);
        }

        public static DbResult ListAffFavorie(AffiliateListCriteria criteria)
        {
            var hsParam = new Hashtable();
            hsParam.Add("userid", criteria.UserId);
            hsParam.Add("status", criteria.Status);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("pagesize", criteria.PageSize);

            if (criteria.GroupId != -1)
            {
                hsParam.Add("groupid", criteria.GroupId);
            }

            if (!string.IsNullOrEmpty(criteria.Username))
            {
                hsParam.Add("username", criteria.Username);
            }

            if (!string.IsNullOrEmpty(criteria.Email))
            {
                hsParam.Add("email", criteria.Email);
            }

            if (criteria.AffId != -1)
                hsParam.Add("affid", criteria.AffId);
            return DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET);
        }
        public static DataTable SelAffInfo(int intCustId, ref string strMinCheque, ref string strMinMoneyBooker, ref string strMinNetteller, ref string strMinWire, ref string strMinPlayer)
        {
            //Hashtable hsParam = new Hashtable();
            //hsParam.Add("custid", intCustId);
            //return DbHelper.Execute("Aff_CustomerSelEditAffiliate", hsParam, DBType.MAIN).ResultSet.Tables[0];
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intCustId);
            hsParam.Add("aff_minwire", "0");
            hsParam.Add("aff_minneteller", "0");
            hsParam.Add("aff_mincheque", "0");
            hsParam.Add("aff_minmoneybookers", "0");
            hsParam.Add("aff_minplayer", "0");
            DbResult result = DbHelper.Execute("Aff_AffiliateSelEditInfo", hsParam, DBType.AFFILIATE_12BET);
            strMinCheque = NumberFormat.DecFormat(result.Outputs["aff_mincheque"], 0);
            strMinMoneyBooker = NumberFormat.DecFormat(result.Outputs["aff_minmoneybookers"], 0);
            strMinNetteller = NumberFormat.DecFormat(result.Outputs["aff_minneteller"], 0);
            strMinWire = NumberFormat.DecFormat(result.Outputs["aff_minwire"], 0);
            strMinPlayer = NumberFormat.DecFormat(result.Outputs["aff_minplayer"], 0);
            return result.ResultSet.Tables[0];
        }
        public static DbResult SelAffInfo_LeftPanel(string strUsername)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("username", strUsername);
            hsParam.Add("iscasino", 0);
            return DbHelper.Execute("Aff_AffiliateSelGeneralInfo", hsParam, DBType.AFFILIATE_12BET);
        }
        public static void UpdateUser(int intAffId, int intUserId, string strFname, string strLname, string strGender, string strCountry, string strEmail, string strPhone, string strMobile, string strAddress, string strCity, string strState, string strPostal, string strLang, string strUrl, string strMoreUrl, string strNoSiteDesc, string strPlayerUserName, string strPayment, string strNetellerId, string strMoneyBookerEmail, string strBankName, string strBankAddress, string strSwiftCode, string strAccName, string strAccNo, string strCheque_FullName, string strCheque_Address, string strCheque_City, string strCheque_State, string strCheque_Postal, string strCheque_Country, string strEmailFormat, ref int intErrCode, ref string strErrMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("userid", intUserId);
            hsParam.Add("firstname", HttpContext.Current.Server.HtmlEncode(strFname));
            hsParam.Add("lastname", HttpContext.Current.Server.HtmlEncode(strLname));
            if(strGender == "0" || strGender == "1")
                hsParam.Add("gender", strGender == "0" ? false : true);
            
            hsParam.Add("country", HttpContext.Current.Server.HtmlEncode(strCountry));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(strEmail));
            hsParam.Add("phone", HttpContext.Current.Server.HtmlEncode(strPhone));
            hsParam.Add("mobilephone", HttpContext.Current.Server.HtmlEncode(strMobile));
            hsParam.Add("address", HttpContext.Current.Server.HtmlEncode(strAddress));
            hsParam.Add("city", HttpContext.Current.Server.HtmlEncode(strCity));
            hsParam.Add("state", HttpContext.Current.Server.HtmlEncode(strState));
            hsParam.Add("postal", HttpContext.Current.Server.HtmlEncode(strPostal));
            hsParam.Add("siteurl", HttpContext.Current.Server.HtmlEncode(strUrl));
            hsParam.Add("moresiteurls", HttpContext.Current.Server.HtmlEncode(strMoreUrl.Trim()));
            hsParam.Add("nositedesc", HttpContext.Current.Server.HtmlEncode(strNoSiteDesc.Trim()));
            
            //combine
            hsParam.Add("preferredpayment", HttpContext.Current.Server.HtmlEncode(strPayment));
            hsParam.Add("player_username", HttpContext.Current.Server.HtmlEncode(strPlayerUserName));
            hsParam.Add("neteller_accountid", HttpContext.Current.Server.HtmlEncode(strNetellerId));
            hsParam.Add("moneybookers_email", HttpContext.Current.Server.HtmlEncode(strMoneyBookerEmail));
            hsParam.Add("wire_bankname", HttpContext.Current.Server.HtmlEncode(strBankName));
            hsParam.Add("wire_bankaddress", HttpContext.Current.Server.HtmlEncode(strBankAddress));
            hsParam.Add("wire_swiftcode", HttpContext.Current.Server.HtmlEncode(strSwiftCode));
            hsParam.Add("wire_accountname", HttpContext.Current.Server.HtmlEncode(strAccName));
            hsParam.Add("wire_accountno", HttpContext.Current.Server.HtmlEncode(strAccNo));
            hsParam.Add("cheque_fullname", HttpContext.Current.Server.HtmlEncode(strCheque_FullName));
            hsParam.Add("cheque_address", HttpContext.Current.Server.HtmlEncode(strCheque_Address));
            hsParam.Add("cheque_city", HttpContext.Current.Server.HtmlEncode(strCheque_City));
            hsParam.Add("cheque_state", HttpContext.Current.Server.HtmlEncode(strCheque_State));
            hsParam.Add("cheque_postal", HttpContext.Current.Server.HtmlEncode(strCheque_Postal));
            hsParam.Add("cheque_country", HttpContext.Current.Server.HtmlEncode(strCheque_Country));
            if (strEmailFormat == "0" || strEmailFormat == "1")
                hsParam.Add("emailformat", strEmailFormat == "0" ? false : true);
            //endcombine
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("language", strLang);
            hsParam.Add("errcode", intErrCode);
            hsParam.Add("errmsg", strErrMsg);
            DbResult dbr = DbHelper.Execute("Aff_AffiliateUpdInfo", hsParam, DBType.AFFILIATE_12BET);
            intErrCode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            strErrMsg = dbr.Outputs["errmsg"].ToString();
        }
        public static void ResetPass(int intCustId, int intUserId, string strNewPass, ref int intErrCode,ref string errmsg, int isAdmin)
        {
            Hashtable hsParam = new Hashtable();
            DbResult result;
            if (isAdmin == 0)
            {
                hsParam.Add("affid", intCustId);
                hsParam.Add("affpwd", Encryption.CfsEnCode(strNewPass));
                hsParam.Add("userid", intUserId);
                hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                hsParam.Add("errcode", "0");
                hsParam.Add("errmsg", "");
                //result = DbHelper.ExecuteCmd("Aff_AffiliateUpdPwd", hsParam, DBType.AFFILIATE_12BET);
                result = DbHelper.Execute("Aff_AffiliateUpdPwd", hsParam, DBType.AFFILIATE_12BET);
            }
            else
            {
                hsParam.Add("userid", intUserId);
                hsParam.Add("custid", intCustId);
                hsParam.Add("userpwd",  Encryption.CfsEnCode(strNewPass));
                hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                hsParam.Add("errcode", "0");
                hsParam.Add("errmsg", "");
                result = DbHelper.Execute("Aff_AdminUpdPwd", hsParam, DBType.AFFILIATE_12BET);
            }
            intErrCode = CastAide.AsInt32(result.Outputs["errcode"]);
            errmsg = result.Outputs["errmsg"].ToString();
        }
        //@Peter
        //Statistics
        public static DbResult SelStatistics(int AffID, DateTime dtmMonthDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AffID);
            hsParam.Add("monthdate", dtmMonthDate);
            hsParam.Add("totalactiveplayer", 0);
            hsParam.Add("totaldeposit", 0);
            hsParam.Add("totaldepositup", 0);
            hsParam.Add("totalturnover", 0);
            hsParam.Add("totalturnoverup", 0);
            hsParam.Add("totalplayerdepup", 0);
            hsParam.Add("newsignupdep", 0);
            hsParam.Add("totalqualifiedmembers", 0);
            hsParam.Add("totalqualifiedmembershr", 0);
            hsParam.Add("totalqualifiedcasino", 0);
            hsParam.Add("totalwinlostup", 0);
            hsParam.Add("totalwinlost", 0);
            hsParam.Add("totaluniqueclicks", 0);
            hsParam.Add("totalclicks", 0);
            hsParam.Add("totaluniqueclicksbysite", 0);
            hsParam.Add("totalclicksbysite", 0);
            hsParam.Add("totaluniqueclicksbymobile", 0);
            hsParam.Add("totalclicksbymobile", 0);
            return DbHelper.Execute("Aff_StatisticsSel", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Vinh
        //Statistics
        public static DbResult SelBalanceBF(int affiliateID)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            hsParam.Add("balbroughtforward", 0);
            return DbHelper.Execute("Aff_CustomerSelBalBF", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DbResult SelAffOverview_DEPOSIT(int intCustId, DateTime dtmMonthDate)
        {
            Hashtable p = new Hashtable();
            p.Add("affiliateid", intCustId);
            p.Add("monthdate", dtmMonthDate);
            p.Add("newsignup", 0);
            //p.Add("newsignupdep", 0);
            p.Add("totalplayer", 0);
            //p.Add("totalplayerdep", 0);
            return DbHelper.Execute("DEP_AffiliateSelSummary", p, DBType.DEPOSIT, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        public static DataTable SelAffEarningDaily(int intAffID, int SportType, DateTime dtmMonthDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffID);
            hsParam.Add("sporttype", SportType);
            hsParam.Add("monthdate", dtmMonthDate);
            return DbHelper.Execute("Aff_EarningSelDaily", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP).ResultSet.Tables[0]; ;
        }
        public static DataTable SelAffEarningDetail(int intAffId, int SportType, DateTime dtmMonthDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("sporttype", SportType);
            hsParam.Add("monthdate", dtmMonthDate);
            return DbHelper.Execute("Aff_EarningSelDetail", hsParam, DBType.AFFILIATE_12BET).ResultSet.Tables[0]; ;
        }
        //@Vinh
        public static DbResult SelMemberReportByAff(int intCustId, DateTime dtmFDate, DateTime dtmTDate,int affType)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intCustId);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            return DbHelper.Execute("Aff_ReportDetailSelByAff", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Vinh
        public static DbResult SelMemberReport(DateTime dtmFDate, DateTime dtmTDate,int affType)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            return DbHelper.Execute("Aff_ReportDetailSelAll", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

         //@Vinh
        public static DbResult SelAffiliateReport(AffiliateReportCriteria criteria)
        {
            Hashtable hsParam = new Hashtable();
            if(!string.IsNullOrEmpty(criteria.PreferredLanguage))
                hsParam.Add("preferredlanguage", criteria.PreferredLanguage);
            if (criteria.SportId > 0)
                hsParam.Add("sportid", criteria.SportId);
            hsParam.Add("fromdate", criteria.FromDate);
            hsParam.Add("todate", criteria.ToDate);
            hsParam.Add("isgetall", criteria.IsGetAll);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("pagesize", criteria.PageSize);
            return DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //@Vinh
        public static DbResult SelAffiliateReportByAff(string strAffUsername, DateTime dtmFDate, DateTime dtmTDate, int intSportId, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            if (intSportId > 0)
                hsParam.Add("sportid", intSportId);
            hsParam.Add("affusername", strAffUsername);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            hsParam.Add("affid", 0);
            return DbHelper.Execute("Aff_AffiliateReportDetail", hsParam, DBType.AFFILIATE_12BET, 180);
        }

        //@Vinh 20091209
        public static DbResult SelAffiliateStatistics(MemberStatisticsReportCriteria criteria)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("username", criteria.Username);
            if (!string.IsNullOrEmpty(criteria.PreferredLanguage))
                hsParam.Add("preferredlanguage", criteria.PreferredLanguage);
            hsParam.Add("fromdate", criteria.DtmFDate);
            hsParam.Add("todate", criteria.DtmTDate);
            hsParam.Add("isgetall", criteria.IsGetAll);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("pagesize", criteria.PageSize);
            hsParam.Add("sortby", criteria.SortBy);
            return DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Vinh 20091209
        public static DbResult SelAffiliateTotalRegister(string strListAffId, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            hsParam.Add("listaffid", strListAffId);
            return DbHelper.Execute("DEP_AffiliateSelTotalRegistered", hsParam, DBType.DEPOSIT, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //@Vinh 20091212
        public static DbResult SelAffiliateStatisticsDetail(int intAffId, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            return DbHelper.Execute("Aff_AffiliateStatisticsDetail", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Vinh 20091212
        public static DbResult SelAffiliateTotalRegisterDetail(int intAffId, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            return DbHelper.Execute("DEP_AffiliateSelTotalRegisteredByDate", hsParam, DBType.DEPOSIT, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }



        //@Vinh
        public static DbResult SelPlayersDetail(int CustId, int sporttype, string strMonth, int pageIndex, int pageSize, ref int totalPages)
        {
            Hashtable p = new Hashtable();
            p.Add("affid", CustId);
            p.Add("sporttype", sporttype);
            if (!string.IsNullOrEmpty(strMonth)) p.Add("monthdate", CastAide.AsDateTime(strMonth));
            p.Add("pageindex", pageIndex);
            p.Add("pagesize", pageSize); // hardcode pagesize for member/player list
            p.Add("totalpages", 0);
            DbResult rs = DbHelper.Execute("Aff_PlayersSelDetail", p, DBType.AFFILIATE_12BET);
            totalPages = CastAide.AsInt32(rs.Outputs["totalpages"], 1);
            return rs;
        }

        //@Vinh
        public static DbResult SelNoteList(int affid, int pageIndex, ref int rows, ref int totalPages, int pageSize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            DbResult rs = DbHelper.Execute("Aff_AdminNoteSel", hsParam, DBType.AFFILIATE_12BET);
            rows = CastAide.AsInt32(rs.Outputs["nrows"]);
            totalPages = CastAide.AsInt32(rs.Outputs["totalpages"]);
            return rs;
        }
        //@Vinh
        public static DbResult SelNoteDetail(int affid, object noteid)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("noteid", noteid);
            DbResult rs = DbHelper.Execute("Aff_AdminNoteSel", hsParam, DBType.AFFILIATE_12BET);
            return rs;
        }
        //@Vinh
        public static DbResult AddNote(int custid, int updateby, string strNote)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("noteid", 0);
            hsParam.Add("note", HttpContext.Current.Server.HtmlEncode(strNote));
            hsParam.Add("custid", custid);
            hsParam.Add("updatedby", updateby);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AdminNoteIns", hsParam, DBType.AFFILIATE_12BET);
        }

        public static DbResult UpdateNote(int CustId, int updateby, int noteid, string strNote)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("noteid", noteid);
            hsParam.Add("note", HttpContext.Current.Server.HtmlEncode(strNote));
            hsParam.Add("userid", CustId);
            hsParam.Add("updatedby", updateby);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AdminNoteUpd", hsParam, DBType.AFFILIATE_12BET);
        }

        //@Vinh
        public static DbResult DeleteNote(int AffId, int UserId, string strListNote)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AffId);
            hsParam.Add("userid", UserId);
            hsParam.Add("listnode", strListNote);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AdminNoteDel", hsParam, DBType.AFFILIATE_12BET);
        }

        //@Vinh
        public static DbResult SelPermission(int CustId, string funcName)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("custid", CustId);
            hsParam.Add("funcname", funcName);
            hsParam.Add("system", Common.System);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
	    return DbHelper.Execute("Aff_Con_CheckPermission", hsParam, DBType.AFFILIATE_12BET);	
        }

        //@Peter 
        public static DbResult UpdateStatus(int affid, int groupID,string strStatus, string strPassword)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("status", HttpContext.Current.Server.HtmlEncode(strStatus));            
            if (!string.IsNullOrEmpty(strPassword.Trim()))
                hsParam.Add("password", DNAide.Utils.Encryption.CfsEnCode(strPassword));
            if (groupID != -1)
            {
                hsParam.Add("groupid", groupID);
            }
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AffiliateUpdStatus", hsParam, DBType.AFFILIATE_12BET);
        }
       
        //@Peter 
        public static DbResult AffUpdateClosedStatus(int affid, bool isClosed)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("closed", isClosed);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_AffiliateUpdClosedStatus", hsParam, DBType.AFFILIATE_12BET);
        }

        //@Peter 
        public static DbResult AffUpdateAffType(int affid, int afftype)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("afftype", afftype);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("language", AppSettings.Session.Language);
            hsParam.Add("errcode", 0);            
            return DbHelper.Execute("Aff_UpdAffType", hsParam, DBType.AFFILIATE_12BET);
        }

        public static DbResult UpdateLanguage(int affID, string strLang)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affID);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("preferredlanguage", HttpContext.Current.Server.HtmlEncode(strLang));
            hsParam.Add("errcode", 0);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_PreferLangUpd", hsParam, DBType.AFFILIATE_12BET);
        }
        //Vinh@20090512
        public static int Adjustment(decimal Amt, int CustId, int UserId, string Desc, ref Int64 intTransId, ref string strErrorMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("amount", Amt);
            hsParam.Add("affid", CustId);
            hsParam.Add("adminid", UserId);
            hsParam.Add("transdesc", HttpContext.Current.Server.HtmlEncode(Desc));
            hsParam.Add("transid", 0);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);                        
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");

            DbResult result = DbHelper.Execute("Aff_AdjustmentIns", hsParam, DBType.AFFILIATE_12BET, 600);
            intTransId = CastAide.AsInt32(result.Outputs["transid"]);
            //dtmWinlostDate = CastAide.AsDateTime(result.Outputs["winlostdate"]);
            strErrorMsg = result.Outputs["errmsg"].ToString();
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }
        //Vinh@20090512
        public static DbResult SelAffUsename()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affmaster", AppSettings.AffMaster);
            return DbHelper.Execute("Aff_CustomerSelAffUsename", hsParam, DBType.AFFILIATE_12BET);
        }
        //Vinh@20090627
        public static DbResult AffFavoriteIns(int intAffId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("adminid", AppSettings.Session.CustID);
            hsParam.Add("affid", intAffId);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            return DbHelper.Execute("Aff_FavoriteIns", hsParam, DBType.AFFILIATE_12BET);
        }
        //Vinh@20090627
        public static DbResult AffFavoriteDel(int intAffId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("adminid", AppSettings.Session.CustID);
            hsParam.Add("affid", intAffId);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            return DbHelper.Execute("Aff_FavoriteDel", hsParam, DBType.AFFILIATE_12BET);
        }
        //Vinh@20090908
        public static DbResult SportTypeSel()
        {
            return DbHelper.Execute("Aff_SportTypeSel", null, DBType.AFFILIATE_12BET);
        }

        //Bryan@20091212
        public static DbResult SelAffiliateQMReport(QMReportCriteria criteria)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(criteria.Username) && String.Compare(criteria.Username, ("[" + Resources.Lang.All + "]"), true) != 0)
                hsParam.Add("affname", criteria.Username);
            if (!string.IsNullOrEmpty(criteria.PreferredLanguage))
                hsParam.Add("preferredlanguage", criteria.PreferredLanguage);
            hsParam.Add("month", criteria.Month);
            hsParam.Add("year", criteria.Year);
            hsParam.Add("pagesize", criteria.PageSize);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("sortby", criteria.SortBy);
            return DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //Bryan20091212
        public static DbResult SelAffiliateQMReportDetail(int affid, int month,int year, int intPage, int intPageSize)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affid);
            hsParam.Add("month", month);
            hsParam.Add("year", year);
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("pageindex", intPage);
            return DbHelper.Execute("Aff_ReportQMDetail", hsParam, DBType.AFFILIATE_12BET);
        }

        //Pete20110110
        public static DbResult SelAffiliateCommissionReport(CommissionReportCriteria criteria)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(criteria.Username) && String.Compare(criteria.Username, ("[" + Resources.Lang.All + "]"), true) != 0)
                hsParam.Add("username", criteria.Username);
            if (!string.IsNullOrEmpty(criteria.PreferredLanguage))
                hsParam.Add("preferredlanguage", criteria.PreferredLanguage);
            DateTime d = new DateTime(criteria.Year, criteria.Month, 1);
            hsParam.Add("monthdate", d);
            if (criteria.Sport > 0)
                hsParam.Add("sportid", criteria.Sport);
            hsParam.Add("pagesize", criteria.PageSize);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("sortby", criteria.SortBy);
            hsParam.Add("isdesc", criteria.IsDesc);
            return DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET);
        }
        //Johnny20100109
        public static void Update12BetPlayerStatusToDeposit(int AffId, string depositUserName, int ibcCustid, string ibcUserName, out int errcode, out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("depusername", HttpContext.Current.Server.HtmlEncode(depositUserName));
            hsParam.Add("ibccustid", ibcCustid);
            hsParam.Add("ibcusername", HttpContext.Current.Server.HtmlEncode(ibcUserName));
            hsParam.Add("errorcode", 0);
            hsParam.Add("errormsg", string.Empty);
            DbResult result = DbHelper.Execute("Deposit_MemberIBCDetail", hsParam, DBType.DEPOSIT);
            errcode = CastAide.AsInt32(result.Outputs["errorcode"]);
            errmsg = result.Outputs["errormsg"].ToString();

            //wirite log if the user updating processed successfully
            if (string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator", AppSettings.Session.CustID);
                logParam.Add("affid", AffId);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Deposit_MemberIBCDetail");
                logParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                logParam.Add("refno", refNo);
                logParam.Add("details", "Fix the problem when updating 12Bet player status to deposit DepositCustID=" + ibcCustid);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE_12BET);
            }
        }

        //@Johnny, 2010 Jan 14: Check 12Bet Exist in Deposit_Customer table or not
        public static DepositCustomerInfo Get12BetPlayerDepositInfo(string deposit_username)
        {
            DepositCustomerInfo customer = new DepositCustomerInfo();
            customer.DepositUserName = deposit_username;
            Hashtable hsParam = new Hashtable();
            hsParam.Add("deposit_username", customer.DepositUserName);
            DbResult result = DbHelper.Execute("Deposit_GetCustDataForCreateIBCACByUserName", hsParam, DBType.DEPOSIT);
            DataTable tbl = result.ResultSet.Tables[0];
            customer.Success = CastAide.AsInt32(tbl.Rows[0]["success"]);
            customer.Address = tbl.Rows[0]["address"].ToString();
            customer.AffiliateId = CastAide.AsInt32(tbl.Rows[0]["affiliateid"]);
            customer.AgentID = CastAide.AsInt32(tbl.Rows[0]["agentid"]);
            // customer.AgentType = CastAide.AsInt32(tbl.Rows[0]["agenttype"]);
            customer.Birthday = CastAide.AsDateTime(tbl.Rows[0]["birthday"]);
            customer.BonusCode = tbl.Rows[0]["bonuscode"].ToString();
            //  customer.CasinoFlag = CastAide.AsInt32(tbl.Rows[0]["casinoflag"]);
            customer.City = tbl.Rows[0]["city"].ToString();
            customer.Country = tbl.Rows[0]["country"].ToString();
            customer.Currency = CastAide.AsInt32(tbl.Rows[0]["currency"]);
            // customer.DepositCustID = CastAide.AsInt32(tbl.Rows[0]["depositcustid"]);
            customer.DepositUserId = CastAide.AsInt32(tbl.Rows[0]["deposituserid"]);
            customer.Email = tbl.Rows[0]["email"].ToString();
            customer.Gender = CastAide.AsInt32(tbl.Rows[0]["gender"]);
            customer.FirstName = tbl.Rows[0]["firstname"].ToString();
            customer.LastName = tbl.Rows[0]["lastname"].ToString();
            customer.OddsType = tbl.Rows[0]["oddstype"].ToString();
            customer.Password = tbl.Rows[0]["password"].ToString();
            customer.Phone = tbl.Rows[0]["phone"].ToString();
            customer.State = tbl.Rows[0]["state"].ToString();
            customer.ZipCode = tbl.Rows[0]["zipcode"].ToString();
            return customer;

        }

        //@Thai, 2012 July 27: Get list of deposit customers
        public static System.Collections.Generic.List<DepositCustomerInfo> Get12BetPlayersDepositInfo(string lstDepositUserNames)
        {
            System.Collections.Generic.List<DepositCustomerInfo> customers = new System.Collections.Generic.List<DepositCustomerInfo>();
            Hashtable hsParam = new Hashtable();

            hsParam.Add("deposit_usernames", lstDepositUserNames);
            DbResult rs = DbHelper.Execute("Deposit_GetCustDataForCreateIBCACByUserNames", hsParam, DBType.DEPOSIT);
            
            DataTable tbl = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                tbl = rs.ResultSet.Tables[0];

            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow dr in tbl.Rows)
                {
                    DepositCustomerInfo customer = new DepositCustomerInfo();
                    customer.DepositUserName = dr["username"].ToString();
                    customer.Address = dr["address"].ToString();
                    customer.AffiliateId = CastAide.AsInt32(dr["affiliateid"]);
                    customer.AgentID = CastAide.AsInt32(dr["agentid"]);
                    // customer.AgentType = CastAide.AsInt32(dr["agenttype"]);
                    customer.Birthday = CastAide.AsDateTime(dr["birthday"]);
                    customer.BonusCode = dr["bonuscode"].ToString();
                    //  customer.CasinoFlag = CastAide.AsInt32(dr["casinoflag"]);
                    customer.City = dr["city"].ToString();
                    customer.Country = dr["country"].ToString();
                    customer.Currency = CastAide.AsInt32(dr["currency"]);
                    // customer.DepositCustID = CastAide.AsInt32(dr["depositcustid"]);
                    customer.DepositUserId = CastAide.AsInt32(dr["deposituserid"]);
                    customer.Email = dr["email"].ToString();
                    customer.Gender = CastAide.AsInt32(dr["gender"]);
                    customer.FirstName = dr["firstname"].ToString();
                    customer.LastName = dr["lastname"].ToString();
                    customer.OddsType = dr["oddstype"].ToString();
                    customer.Password = dr["password"].ToString();
                    customer.Phone = dr["phone"].ToString();
                    customer.State = dr["state"].ToString();
                    customer.ZipCode = dr["zipcode"].ToString();

                    customers.Add(customer);
                }
            }

            return customers;
        }

        //@Johnny, 2010 Jan 14: Add 1 customer to our DB with username2=depositusername
        public static void Add12BetPlayerAccountToIBC(DepositCustomerInfo customer, out int custid, out string username, out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("firstname", HttpContext.Current.Server.HtmlEncode(customer.FirstName));
            hsParam.Add("lastname", HttpContext.Current.Server.HtmlEncode(customer.LastName));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(customer.Email));
            hsParam.Add("depositusername", HttpContext.Current.Server.HtmlEncode(customer.DepositUserName));
            hsParam.Add("deposituserid", customer.DepositUserId);
            hsParam.Add("password", customer.Password);
            hsParam.Add("oddstype", HttpContext.Current.Server.HtmlEncode(customer.OddsType));
            hsParam.Add("agentid", customer.AgentID);
            hsParam.Add("affiliateid", customer.AffiliateId);
            hsParam.Add("bonuscode", HttpContext.Current.Server.HtmlEncode(customer.BonusCode));
            hsParam.Add("custid", 0);
            hsParam.Add("username", string.Empty);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Dot_Dep_CustomerInsMember_Ag_Cust", hsParam, DBType.CustDB);
            username = result.Outputs["username"].ToString();
            custid = CastAide.AsInt32(result.Outputs["custid"]);
            errmsg = result.Outputs["errmsg"].ToString();

            //wirite log if the user creating processed successfully
            if (string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator", AppSettings.Session.CustID);
                logParam.Add("affid", AppSettings.Session.CustID);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Dot_Dep_CustomerInsMember_Ag_Cust");
                logParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                logParam.Add("refno", refNo);
                logParam.Add("details", "Add 12Bet player account to IBC DepositCustID=" + customer.DepositUserId);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE_12BET);
            }

        }

        //@Johnny, 2010 Jan 14: Update AccStatus = 2 in Deposit_Customer table in Deposit_DB. 
        public static void Update12BetPlayerStatusToDeposit(string depositUserName, int ibcCustid, string ibcUserName, out int errcode, out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("depusername", HttpContext.Current.Server.HtmlEncode(depositUserName));
            hsParam.Add("ibccustid", ibcCustid);
            hsParam.Add("ibcusername", HttpContext.Current.Server.HtmlEncode(ibcUserName));
            hsParam.Add("errorcode", 0);
            hsParam.Add("errormsg", string.Empty);
            DbResult result = DbHelper.Execute("Deposit_MemberIBCDetail", hsParam, DBType.DEPOSIT);
            errcode = CastAide.AsInt32(result.Outputs["errorcode"]);
            errmsg = result.Outputs["errormsg"].ToString();

            //wirite log if the user updating processed successfully
            if (string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator", AppSettings.Session.CustID);
                logParam.Add("affid", AppSettings.Session.CustID);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Deposit_MemberIBCDetail");
                logParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                logParam.Add("refno", refNo);
                logParam.Add("details", "Update 12Bet player status to deposit DepositCustID=" + ibcCustid);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE_12BET);
            }
        }
        //@Johnny, 2010 Jan 14: Check 12betUsername of Affiliate valid or not 
        public static bool Check12BetPlayerAccountActivated(int affiliate, out string depositUserName, out string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("language", AppSettings.Session.Language);
            hsParam.Add("affid", affiliate);
            hsParam.Add("12betplayerusername", string.Empty);
            hsParam.Add("result", false);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            DbResult result = DbHelper.Execute("Aff_CustomerCheck12BetPlayerAccountActivated", hsParam, DBType.AFFILIATE_12BET);
            errMsg = result.Outputs["errmsg"].ToString();
            depositUserName = result.Outputs["12betplayerusername"].ToString();
            return CastAide.AsBoolean(result.Outputs["result"]);
        }
        //@Smith 20101223
        public static DbResult SignUpMembersReport(int? affId, DateTime dtmFDate, DateTime dtmTDate, bool isGetAll)
        {
            Hashtable hsParam = new Hashtable();
            if(affId!=-1)
                hsParam.Add("affid", affId);
            hsParam.Add("fromdate", dtmFDate);
            hsParam.Add("todate", dtmTDate);
            hsParam.Add("isgetall", isGetAll);
            return DbHelper.Execute("DEP_SignUpMembersReport", hsParam, DBType.DEPOSIT, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        //@Smith 20101223
        public static DbResult ActiveMemberReport(SignUpActiveCriteria criteria)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("fromdate", criteria.DtmFDate);
            hsParam.Add("todate", criteria.DtmTDate);
            hsParam.Add("isgetall", criteria.IsGetAll);
            hsParam.Add("username", criteria.Username);
            if(!string.IsNullOrEmpty(criteria.PreferredLanguage))
                hsParam.Add("preferredlanguage", criteria.PreferredLanguage);
            hsParam.Add("affyype", criteria.AffType);
            hsParam.Add("pagesize", criteria.PageSize);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            hsParam.Add("sortby", criteria.SortBy);
            DbResult result = DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
            return result;
        }

        //@smith
        public static DbResult PerformanceReport(PerformanceCriteria criteria)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("username", criteria.Username);
            if (criteria.Sport > 0)
                hsParam.Add("sportid", criteria.Sport);
            hsParam.Add("language", criteria.Language);
            hsParam.Add("fromdate", criteria.FromDate);
            hsParam.Add("todate", criteria.ToDate);
            hsParam.Add("sortby", criteria.SortBy);
            hsParam.Add("pagesize", criteria.PageSize);
            hsParam.Add("pageindex", criteria.PageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            hsParam.Add("isdesc", criteria.IsDesc);

            return DbHelper.Execute(criteria.SpName, hsParam, DBType.AFFILIATE_12BET,
                DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //@smith
        public static DbResult PerformanceReportByAffiliate(int sportID, int affId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affId);
            hsParam.Add("sportid", sportID);

            return DbHelper.Execute("Aff_PerformanceReportByAff", hsParam, DBType.AFFILIATE_12BET,
                DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        public static bool ValidatePermission(ValidationCriteria criteria)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("adminname", null);
            parameters.Add("affid", criteria.AffId);
            parameters.Add("membername", criteria.MemberName);

            var rs = DbHelper.Execute(criteria.SpName, parameters, DBType.AFFILIATE_12BET);
            var resutl = CastAide.AsBoolean(rs.Outputs["belonglevel2"]);
            return resutl;
        }
    }
}

