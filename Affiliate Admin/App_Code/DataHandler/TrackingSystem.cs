using System;
using System.Collections;
using System.Configuration;
using DNAide.Data;
using System.Globalization;
using DNAide.Core;
/*
 * Class to handle Clicks Tracking
 * Author: Peter
 * Date : May 05,2009
 */
namespace DataHandler
{
    public class TrackingSystem
    {
        public static DbResult TrackingCodeSel(int affiliateID)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            return DbHelper.Execute("Aff_AffiliateSelTrackingCode", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DbResult SelClickStatisticsAllMonths(int affiliateID,string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            hsParam.Add("trackingcode", trackingCode);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsAllMonths", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DbResult SelClickStatisticsDetail(int affiliateID,int month, int year, string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            if (!string.IsNullOrEmpty(trackingCode))
                hsParam.Add("trackingcode", trackingCode);
            hsParam.Add("month", month);
            hsParam.Add("year", year);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsMonthDetail", hsParam, DBType.AFFILIATE_12BET);
        }
        //Statistics
        public static DbResult SelStatisticsAllForMonth(int affiliateID,DateTime dtmMonthDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            hsParam.Add("monthdate", dtmMonthDate);            
            hsParam.Add("totaluniqueclicks", 0);
            hsParam.Add("totalclicks", 0);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsMonthDetailAll", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        //@Johnny
        public static DbResult SelClickStatisticsBySite(object fromDate, object toDate)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.AffId);
            hsParam.Add("fromdate", fromDate);
            hsParam.Add("todate", toDate);
            return DbHelper.Execute("Aff_ClickStatisticsSelBySite", hsParam, DBType.AFFILIATE_12BET);
        }


        public static DbResult SelStatisticsDetail_New(DateTime fDate, DateTime tDate, string domain, string sportCode, string languageCode, int pageSize, int pageIndex)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.AffId);

            CultureInfo culture = new CultureInfo("en-US");
            hsParam.Add("fromdate", fDate);
            hsParam.Add("todate", tDate);
            if(!string.IsNullOrEmpty(domain))
                hsParam.Add("domain", domain);
            if (!string.IsNullOrEmpty(sportCode))
                hsParam.Add("sportcode", sportCode);
            if (!string.IsNullOrEmpty(languageCode))
                hsParam.Add("languagecode", languageCode);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            return DbHelper.Execute("Aff_ClickStatisticsDetailBySite", hsParam, DBType.AFFILIATE_12BET, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        public static DbResult SelClickStatisticsByPage(object fromDate, object toDate, string sportCode, string languageCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.AffId);
            hsParam.Add("fromdate", fromDate);
            hsParam.Add("todate", toDate);
            if (!string.IsNullOrEmpty(sportCode))
                hsParam.Add("sportcode", sportCode);
            if (!string.IsNullOrEmpty(languageCode))
                hsParam.Add("languagecode", languageCode);
            return DbHelper.Execute("Aff_ClickStatisticsSelByPage", hsParam, DBType.AFFILIATE_12BET);
        }

        public static void SelClickStatisticsOldURL(object fromDate, object toDate, ref int totalClicks, ref int totalUniqueClicks)
        {
            Hashtable hsParam = new Hashtable();
            CultureInfo culture = new CultureInfo("en-US");

            hsParam.Add("affid", AppSettings.Session.AffId);
            hsParam.Add("fromdate", CastAide.AsDateTime(fromDate, culture));
            hsParam.Add("todate", CastAide.AsDateTime(toDate, culture));
            hsParam.Add("totaluniqueclicks", 0);
            hsParam.Add("totalclicks", 0);
            DbResult rs = DbHelper.Execute("Aff_AffiliateSelClickSummaryByDate", hsParam, DBType.AFFILIATE_12BET);
            totalUniqueClicks = CastAide.AsInt32(rs.Outputs["totaluniqueclicks"]);
            totalClicks = CastAide.AsInt32(rs.Outputs["totalclicks"]);
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

