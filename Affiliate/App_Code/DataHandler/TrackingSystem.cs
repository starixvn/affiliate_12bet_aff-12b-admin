using System;
using System.Web;
using System.Collections;
using System.Configuration;
using DNAide.Data;
using DNAide.Core;
using System.Globalization;

/*
 * Class to handle Clicks Tracking
 * Author: Peter
 * Date : May 05,2009
 */
namespace DataHandler
{
    public class TrackingSystem
    {
        public static DbResult TrackingCodeSel()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.CustID);
            return DbHelper.Execute("Aff_AffiliateSelTrackingCode", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DbResult SelClickStatisticsAllMonths(string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.CustID);
            hsParam.Add("trackingcode", trackingCode);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsAllMonths", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DbResult SelClickStatisticsDetail(int month, int year, string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.CustID);
            if (!string.IsNullOrEmpty(trackingCode))
                hsParam.Add("trackingcode", trackingCode);
            hsParam.Add("month", month);
            hsParam.Add("year", year);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsMonthDetail", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        public static DbResult SelClickStatisticsBySite(object fromDate, object toDate, string deviceType)
        {
            Hashtable hsParam = new Hashtable();
            CultureInfo culture = new CultureInfo("en-US");

            hsParam.Add("affid", AppSettings.Session.CustID);
            hsParam.Add("fromdate", CastAide.AsDateTime(fromDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(toDate, culture));
            hsParam.Add("devicetype", byte.Parse(deviceType));
            return DbHelper.Execute("Aff_ClickStatisticsSelBySite", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DbResult SelStatisticsDetail_New(DateTime fDate, DateTime tDate, string domain, string sportCode, string languageCode, int pageSize, int pageIndex, string deviceType)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.CustID);

            CultureInfo culture = new CultureInfo("en-US");
            hsParam.Add("fromdate", CastAide.AsDateTime(fDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(tDate, culture));
            if(!string.IsNullOrEmpty(domain))
                hsParam.Add("domain", domain);
            if (!string.IsNullOrEmpty(sportCode))
                hsParam.Add("sportcode", sportCode);
            if (!string.IsNullOrEmpty(languageCode))
                hsParam.Add("languagecode", languageCode);
            hsParam.Add("devicetype", byte.Parse(deviceType));
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            return DbHelper.Execute("Aff_ClickStatisticsDetailBySite", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        #region Statistics by Page
        public static DbResult SelClickStatisticsByPage(object fromDate, object toDate, string sportCode, string languageCode)
        {
            Hashtable hsParam = new Hashtable();
            CultureInfo culture = new CultureInfo("en-US");

            hsParam.Add("affid", AppSettings.Session.CustID);
            hsParam.Add("fromdate", CastAide.AsDateTime(fromDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(toDate, culture));
            if(!string.IsNullOrEmpty(sportCode))
                hsParam.Add("sportcode", sportCode);
            if (!string.IsNullOrEmpty(languageCode))
                hsParam.Add("languagecode", languageCode);
            return DbHelper.Execute("Aff_ClickStatisticsSelByPage", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DbResult SelClickStatisticsDetailByPage(DateTime fDate, DateTime tDate, string sportCode, string languageCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.CustID);

            CultureInfo culture = new CultureInfo("en-US");
            hsParam.Add("fromdate", CastAide.AsDateTime(fDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(tDate, culture));
            hsParam.Add("sportcode", sportCode);
            hsParam.Add("languagecode", languageCode);
            return DbHelper.Execute("Aff_ClickStatisticsDetailByPage", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }
        #endregion

        public static void SelClickStatisticsOldURL(object fromDate, object toDate, ref int totalClicks, ref int totalUniqueClicks)
        {
            Hashtable hsParam = new Hashtable();
            CultureInfo culture = new CultureInfo("en-US");

            hsParam.Add("affid", AppSettings.Session.CustID);
            hsParam.Add("fromdate", CastAide.AsDateTime(fromDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(toDate, culture));
            hsParam.Add("totaluniqueclicks", 0);
            hsParam.Add("totalclicks", 0);
            DbResult rs = DbHelper.Execute("Aff_AffiliateSelClickSummaryByDate", hsParam, DBType.AFFILIATE_12BET);
            totalUniqueClicks = CastAide.AsInt32(rs.Outputs["totaluniqueclicks"]);
            totalClicks = CastAide.AsInt32(rs.Outputs["totalclicks"]);
        }

        public static bool InsertStatistic(int affId, string trackingCode, string pageCode, string languageCode, HttpRequest request, string affDomain, out string redirectPage, out Int64 clickID, ref int errCode, ref string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("clickid", 0);
            hsParam.Add("affdomain", affDomain);
            hsParam.Add("affiliateid", affId);
            hsParam.Add("trackingcode", trackingCode);
            hsParam.Add("pagecode", pageCode);
            hsParam.Add("languagecode", languageCode);
            hsParam.Add("ip", GenerateHTML.TrackingSystem.GetIPAddressOfClient());
            hsParam.Add("browser", request.Browser.Browser);
            hsParam.Add("urlreferrer", request.UrlReferrer != null ? request.UrlReferrer.AbsoluteUri : null);
            hsParam.Add("domainreferrer", request.UrlReferrer != null ? request.UrlReferrer.Host : null);
            hsParam.Add("os", GenerateHTML.TrackingSystem.GetOSName(request.UserAgent));
            hsParam.Add("device", GenerateHTML.TrackingSystem.GetDeviceName(request.UserAgent));
            hsParam.Add("browserversion", request.Browser.Version);
            hsParam.Add("browserresolution", request.Browser.ScreenPixelsWidth.ToString() + "x" + request.Browser.ScreenPixelsHeight.ToString());
            hsParam.Add("browserlanguage", string.IsNullOrEmpty(request.ServerVariables["HTTP_ACCEPT_LANGUAGE"]) ? string.Empty : request.ServerVariables["HTTP_ACCEPT_LANGUAGE"]);
            hsParam.Add("useragent", request.UserAgent);
            hsParam.Add("country", GenerateHTML.TrackingSystem.ResolveCountry() != null ? GenerateHTML.TrackingSystem.ResolveCountry().EnglishName : System.Globalization.CultureInfo.CurrentCulture.ToString());
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);

            var data = DbHelper.Execute("Aff_AffiliateInsClickStatistics_New", hsParam, DBType.AFFILIATE_12BET);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            redirectPage = data.Outputs["redirectpage"].ToString();
            clickID = Int64.Parse(data.Outputs["clickid"].ToString());
            errCode = Int32.Parse(data.Outputs["errcode"].ToString());
            errMsg = data.Outputs["errmsg"].ToString();
            return result == 0 ? true : false;
        }

        public static void SelAffSignupsByDate_DEPOSIT(int intCustId, object fromDate, object toDate, ref int newSignups, ref int totalDepositUsers)
        {
            Hashtable p = new Hashtable();
            p.Add("affiliateid", intCustId);
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");

            p.Add("fromdate", CastAide.AsDateTime(fromDate, culture));
            p.Add("todate", CastAide.AsDateTime(toDate, culture));
            p.Add("newsignup", 0);
            p.Add("totaldepositusers", 0);
            DbResult rs = DbHelper.Execute("DEP_AffiliateSelSignupsByDate", p, DBType.DEPOSIT);
            newSignups = CastAide.AsInt32(rs.Outputs["newsignup"], 0);
            totalDepositUsers = CastAide.AsInt32(rs.Outputs["totaldepositusers"], 0);
        }
        
    }
 }

