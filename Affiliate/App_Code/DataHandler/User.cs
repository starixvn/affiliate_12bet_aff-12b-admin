using System;
using System.Web;
using System.Collections;
using System.Data;
using Affiliate;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;

/// <summary>
/// Creater: Vinh
/// </summary>
namespace DataHandler
{
    public static class UserAff
    {
        public static void CreateUser(string strFName, string strLName, bool Gender, string strEmail, string strPhone, string strMobile, string strAddress, string strCity, string strState, string strPostal, string strCountry, string strLang, ref int intCustId, string strPayment, string strPlayer, string strPreferLang, string strUrl, string strMoreUrl, string strNoSiteDesc, string strQuestion, string strAnswer, ref string strUsername, ref int intErrCode, ref string strErrMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", "0");
            hsParam.Add("firstname", formatInput(strFName));
            hsParam.Add("lastname", formatInput(strLName));
            hsParam.Add("gender", Gender);
            hsParam.Add("email", formatInput(strEmail));
            hsParam.Add("phone", formatInput(strPhone));
            hsParam.Add("mobilephone", formatInput(strMobile));
            hsParam.Add("masterusername", AppSettings.AffMaster);
            hsParam.Add("address", formatInput(strAddress));
            hsParam.Add("city", formatInput(strCity));
            hsParam.Add("state", formatInput(strState));
            hsParam.Add("postal", formatInput(strPostal));
            hsParam.Add("country", formatInput(strCountry));
            hsParam.Add("affmaster", AppSettings.AffMaster);
            hsParam.Add("afftype", (int)AppSettings.AffType);
            hsParam.Add("siteurl", formatInput(strUrl));
            hsParam.Add("moresiteurls", formatInput(strMoreUrl));
            hsParam.Add("nositedesc", formatInput(strNoSiteDesc));            
            hsParam.Add("preferredlanguage", formatInput(strPreferLang));
            hsParam.Add("emailformat", false);//use html template
            hsParam.Add("preferredpayment", formatInput(strPayment));
            hsParam.Add("player_username", formatInput(strPlayer));
            hsParam.Add("question", formatInput(strQuestion));
            hsParam.Add("answer", formatInput(strAnswer));
            hsParam.Add("username", formatInput(strUsername));
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("browser", HttpContext.Current.Request.Browser.ToString());
            hsParam.Add("language", formatInput(strLang));           
            hsParam.Add("errcode", intErrCode);
            hsParam.Add("errmsg", strErrMsg);
            DbResult dbr = DbHelper.Execute("Aff_AffiliateIns", hsParam, DBType.AFFILIATE_12BET, 600);
            intCustId = CastAide.AsInt32(dbr.Outputs["custid"]);
            intErrCode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            strErrMsg = dbr.Outputs["errmsg"].ToString();
            strUsername = dbr.Outputs["username"].ToString();
        }

        public static void UpdateUser(int intAffId, string strNumber, string strMobile, string strURL, string strMoreUrl, string strNoSiteDesc, string strLang, string strPlayerUserName, ref int intErrCode, ref string strErrMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("userid", intAffId);
            //hsParam.Add("gender", Gender);
            //hsParam.Add("address", formatInput(strAddress));

            hsParam.Add("phone", formatInput(strNumber));
            hsParam.Add("mobilephone", formatInput(strMobile));
            hsParam.Add("siteurl", formatInput(strURL));
            hsParam.Add("moresiteurls", formatInput(strMoreUrl));
            hsParam.Add("nositedesc", formatInput(strNoSiteDesc));  

            //hsParam.Add("preferredlanguage", formatInput(strPreferLang));
            hsParam.Add("player_username", formatInput(strPlayerUserName));

            //endcombine
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("language", formatInput(strLang));
            hsParam.Add("errcode", intErrCode);
            hsParam.Add("errmsg", strErrMsg);
            DbResult dbr = DbHelper.Execute("Aff_AffiliateUpdInfo", hsParam, DBType.AFFILIATE_12BET, 600);
            intAffId = CastAide.AsInt32(dbr.Outputs["affid"]);
            intErrCode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            strErrMsg = dbr.Outputs["errmsg"].ToString();
        }
       /* public static DataTable SelAffInfo(int intCustId, ref string strMinCheque, ref string strMinMoneyBooker, ref string strMinNetteller, ref string strMinWire)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("custid", intCustId);
            hsParam.Add("aff_minwire", "0");
            hsParam.Add("aff_minneteller", "0");
            hsParam.Add("aff_mincheque", "0");
            hsParam.Add("aff_minmoneybookers", "0");            

            DbResult result = DbHelper.Execute("Aff_CustomerSelEditAffiliate", hsParam, DBType.AFFILIATE_12BET, 600);
            strMinCheque = NumberFormat.DecFormat(result.Outputs["aff_mincheque"], 0);
            strMinMoneyBooker = NumberFormat.DecFormat(result.Outputs["aff_minmoneybookers"], 0);
            strMinNetteller = NumberFormat.DecFormat(result.Outputs["aff_minneteller"], 0);
            strMinWire = NumberFormat.DecFormat(result.Outputs["aff_minwire"], 0);
            return result.ResultSet.Tables[0];
        }*/
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

        public static DbResult SelAffOverview(int AffID, DateTime dtmMonthDate)
        {
            /*Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AffID);
            hsParam.Add("monthdate", dtmMonthDate);            
            hsParam.Add("totalactiveplayer", 0);
            hsParam.Add("totaldeposit", 0);            
            hsParam.Add("totaldepositup", 0);//vinh add
            hsParam.Add("totalturnover", 0); //vinh add      
            hsParam.Add("totalplayerdepup", 0);//vinh add      
            hsParam.Add("newsignupdep", 0);//vinh add                 
            hsParam.Add("balbroughtforward", 0);            
            hsParam.Add("totalqualifiedmembers", 0); //Dung added
            hsParam.Add("totalturnoverformonth", 0); //Dung added
            return DbHelper.Execute("Aff_CustomerSelSummary", hsParam, DBType.AFFILIATE_12BET);*/
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AffID);
            hsParam.Add("monthdate", dtmMonthDate);
            hsParam.Add("totalactiveplayer", 0);
            hsParam.Add("balbroughtforward", 0);
            hsParam.Add("totalplayerdepup", 0);//vinh add      
            hsParam.Add("newsignupdep", 0);//vinh add                 
            hsParam.Add("totaldeposit", 0);
            hsParam.Add("totaldepositup", 0);
            hsParam.Add("totalturnover", 0);
            hsParam.Add("totalturnoverup", 0);
            hsParam.Add("totalqualifiedmembers", 0);
            hsParam.Add("totalqualifiedmembershr", 0);
            hsParam.Add("totalwinlostup", 0);
            hsParam.Add("totalwinlost", 0);
            hsParam.Add("totalqualifiedcasino", 0);
            return DbHelper.Execute("Aff_StatisticsSel", hsParam, DBType.AFFILIATE_12BET);
        }

        public static DbResult SelAffOverview_DEPOSIT(int intCustId, DateTime dtmMonthDate)
        {
            Hashtable p = new Hashtable();
            p.Add("affiliateid", intCustId);
            p.Add("monthdate", dtmMonthDate);
            p.Add("newsignup", 0);
            p.Add("totalplayer", 0);
            return DbHelper.Execute("DEP_AffiliateSelSummary", p, DBType.DEPOSIT);
        }        

        public static DataTable SelAffBannerType()
        {
            return DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET).ResultSet.Tables[0];
        }
        public static DbResult SelAffBanner(string category, string language, string imageType, string size, string currency, int pageIndex, int pageSize, int type, int afftype, string deviceType)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }

            // ++ Added by Frank.Ngo on IRP 671 - Jan 2012
            hsParam.Add("type", type);
            // -- Added by Frank.Ngo on IRP 671 - Jan 2012

            hsParam.Add("afftype", afftype);
            hsParam.Add("devicetype", deviceType);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSel_1", hsParam, DBType.AFFILIATE_12BET);
        }

        public static DbResult SelAffBannerOld(string category, string language, string imageType, string size, string currency, int pageIndex, int pageSize)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSel", hsParam, DBType.AFFILIATE_12BET);
        }
        // Added by Frank.Ngo on IRP 671 - 10 Feb 2012
        // Get banners in parent category
        public static DbResult SelAffBannerByParentCategory(string category, string language, string imageType, string size, string currency, int pageIndex, int pageSize, int type, int afftype, string deviceType)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }

            // ++ Added by Frank.Ngo on IRP 671 - Jan 2012
            hsParam.Add("type", type);
            // -- Added by Frank.Ngo on IRP 671 - Jan 2012

            hsParam.Add("afftype", afftype);
            hsParam.Add("devicetype", byte.Parse(deviceType));

            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSelByParentCate", hsParam, DBType.AFFILIATE_12BET);
        }
        public static void ChangePassword(int intCustId, string strNewPass, string strOldPass, int intUserId, string strLang, ref int intErrCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intCustId);
            hsParam.Add("affpwd", Encryption.CfsEnCode(strNewPass));
            hsParam.Add("oldpwd", Encryption.CfsEnCode(strOldPass));
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("language", strLang);
            hsParam.Add("errcode", "0");
            //DbResult result = DbHelper.ExecuteCmd("Aff_CustomerUpdPwd", hsParam, DBType.MAIN);
            DbResult result = DbHelper.Execute("Aff_AffiliateUpdPwd", hsParam, DBType.AFFILIATE_12BET); 
            intErrCode = CastAide.AsInt32(result.Outputs["errcode"]);
        }
         public static DbResult SelAffEarningDaily(int intCustId, int intSportType, DateTime dtmMonthDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intCustId);
            hsParam.Add("sporttype", intSportType);
            hsParam.Add("monthdate", dtmMonthDate);
            hsParam.Add("oddsfeepercent", 0);
            hsParam.Add("marketingpercent", 0);
            hsParam.Add("pspfeepercent", 0);
            hsParam.Add("playtechfeepercent", 0);
            return DbHelper.Execute("Aff_EarningSelDaily", hsParam, DBType.AFFILIATE_12BET) ;
        }
        
        //Vinh@20090908
        public static DbResult SportTypeSel()
        {
            return DbHelper.Execute("Aff_SportTypeSel", null, DBType.AFFILIATE_12BET);
        }
        
        public static DbResult SelAffText(string category, string language, string currency, int pageIndex, int pageSize, string deviceType)
        {
            var hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }

            hsParam.Add("devicetype", byte.Parse(deviceType));
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_TextSel_1", hsParam, DBType.AFFILIATE_12BET);
        }

        public static DbResult SportTypeSelByAff(int intCustId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intCustId);
            return DbHelper.Execute("Aff_SporttypeSelByAff", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DataTable SelAffEarningDetail(int intAffId, int SportType, DateTime dtmMonthDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("sporttype", SportType);
            hsParam.Add("monthdate", dtmMonthDate);
            return DbHelper.Execute("Aff_EarningSelDetail", hsParam, DBType.AFFILIATE_12BET).ResultSet.Tables[0]; ;
        }

        static string formatInput(string val)
        {
            return HttpContext.Current.Server.HtmlEncode(string.IsNullOrEmpty(val) ? string.Empty : val.Trim());
        }

        public static DbResult ExistedMemberOfAff(int custid, int affid)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("custid", custid);
            hsParam.Add("affid", affid);

            return DbHelper.Execute("Aff_ExistedMemberOfAff", hsParam, DBType.AFFILIATE_12BET);
        }

        public static void ForceChangePass(int intAffId, string strOldPassword, string strNewPassword, string strQuestion, string strAnswer, ref int intErrCode, ref string strErrMsg)
        {
            Hashtable hsParam = new Hashtable();

            hsParam.Add("affid", intAffId);
            hsParam.Add("hashpwd", Encryption.CfsEnCode(strOldPassword));
            hsParam.Add("rehashpwd", Encryption.CfsEnCode(strNewPassword));

            hsParam.Add("question", formatInput(strQuestion));
            hsParam.Add("answer", formatInput(strAnswer));
            
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", intErrCode);
            hsParam.Add("errmsg", strErrMsg);

            DbResult dbr = DbHelper.Execute("Aff_AffiliateForceChangePwd", hsParam, DBType.AFFILIATE_12BET);
            
            intErrCode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            strErrMsg = dbr.Outputs["errmsg"].ToString();
        }
    }
}
