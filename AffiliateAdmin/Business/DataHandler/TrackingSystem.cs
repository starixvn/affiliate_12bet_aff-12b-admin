using System;
using System.Collections;
using System.Globalization;
using AffiliateAdmin.Business.DataAccess;

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
            return DbHelper.Execute("Aff_AffiliateSelTrackingCode", DBType.AFFILIATE_12BET, hsParam);
        }

        public static DbResult SelClickStatisticsAllMonths(int affiliateID, string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            hsParam.Add("trackingcode", trackingCode);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsAllMonths", DBType.AFFILIATE_12BET, hsParam);
        }

        public static DbResult SelClickStatisticsDetail(int affiliateID, int month, int year, string trackingCode)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", affiliateID);
            if (!string.IsNullOrEmpty(trackingCode))
                hsParam.Add("trackingcode", trackingCode);
            hsParam.Add("month", month);
            hsParam.Add("year", year);
            return DbHelper.Execute("Aff_AffiliateSelClickStatisticsMonthDetail", DBType.AFFILIATE_12BET, hsParam);
        }

        public static DbResult SelClickStatisticsBySite(object fromDate, object toDate, string deviceType)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.AffId);
            hsParam.Add("fromdate", fromDate);
            hsParam.Add("todate", toDate);
            hsParam.Add("devicetype", byte.Parse(deviceType));
            return DbHelper.Execute("Aff_ClickStatisticsSelBySite_xtest", DBType.AFFILIATE_12BET, hsParam);
        }

        public static DbResult SelStatisticsDetail_New(DateTime fDate, DateTime tDate, string domain, string sportCode, string languageCode, int pageSize, int pageIndex, string deviceType)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.AffId);

            CultureInfo culture = new CultureInfo("en-US");
            hsParam.Add("fromdate", fDate);
            hsParam.Add("todate", tDate);
            if (!string.IsNullOrEmpty(domain))
                hsParam.Add("domain", domain);
            if (!string.IsNullOrEmpty(sportCode))
                hsParam.Add("sportcode", sportCode);
            if (!string.IsNullOrEmpty(languageCode))
                hsParam.Add("languagecode", languageCode);
            hsParam.Add("devicetype", byte.Parse(deviceType));
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            return DbHelper.Execute("Aff_ClickStatisticsDetailBySite", DBType.AFFILIATE_12BET, hsParam, DbHelper.MAX_EXEC_TIME_LENGTH_RPTSP);
        }

        public static DbResult SelClickStatisticsByPage(object fromDate, object toDate, string sportCode, string languageCode, string deviceType)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", AppSettings.Session.AffId);
            hsParam.Add("fromdate", fromDate);
            hsParam.Add("todate", toDate);
            if (!string.IsNullOrEmpty(sportCode))
                hsParam.Add("sportcode", sportCode);
            if (!string.IsNullOrEmpty(languageCode))
                hsParam.Add("languagecode", languageCode);
            hsParam.Add("devicetype", byte.Parse(deviceType));

            return DbHelper.Execute("Aff_ClickStatisticsSelByPage_xtest", DBType.AFFILIATE_12BET, hsParam);
        }
    }
}