using System;
using System.Data;
using System.Web;
using Affiliate.Infrastructure;
using AffiliateUser.Business.DataAccess;

/// <summary>
/// Summary description for CacheUtil
/// </summary>
public class CacheUtil
{
    private static string hotNewsKey = "hotNewsKey";
    private static string importantKeys = "importantKey";
    private static string bannerTypesKey = "bannerTypesKey";
    private static string bannerTypesKeyOld = "bannerTypesKeyOld";

    private static DateTime LastRefreshHotNews;

    private static int _TimeCache = 10;
    private static int _TimeCacheBannerTypes = 120;

    static CacheUtil()
    {
        LastRefreshHotNews = DateTime.MinValue;
        InitCache();
    }

    private static void InitCache()
    {
        AddHotNews();
        LoadBannerTypes();
        AddBannerTypesOld();
    }

    private static void AddHotNews()
    {
        var result = DataHandler.HotNews.GetLatestHotNews();        
        if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
            var row = result.ResultSet.Tables[0].Rows[0];
            CacheHelper.Set(hotNewsKey + "_" + "zh-TW", row["message_tw"], _TimeCache);
            CacheHelper.Set(hotNewsKey + "_" + "zh-CN", row["message_cn"], _TimeCache);
            CacheHelper.Set(hotNewsKey + "_" + "en-US", row["message_en"], _TimeCache);            
            CacheHelper.Set(hotNewsKey + "_" + "es-ES", row["message_es"], _TimeCache); 
            CacheHelper.Set(hotNewsKey + "_" + "th-TH", row["message_th"], _TimeCache); 
            CacheHelper.Set(hotNewsKey + "_" + "vi-VN", row["message_vi"], _TimeCache); 
            CacheHelper.Set(hotNewsKey + "_" + "id-ID", row["message_id"], _TimeCache); 
            CacheHelper.Set(hotNewsKey + "_" + "ko-KR", row["message_kr"], _TimeCache); 
            CacheHelper.Set(hotNewsKey + "_" + "ja-JP", row["message_jp"], _TimeCache); 
            CacheHelper.Set(importantKeys, row["important"], _TimeCache); 
        }
        else // no rows -- means there isn't any hot news
        {
            CacheHelper.Remove(hotNewsKey + "_" + "zh-CN");
            CacheHelper.Remove(hotNewsKey + "_" + "zh-TW");
            CacheHelper.Remove(hotNewsKey + "_" + "zh-CN");
            CacheHelper.Remove(hotNewsKey + "_" + "en-US");
            CacheHelper.Remove(hotNewsKey + "_" + "es-ES");
            CacheHelper.Remove(hotNewsKey + "_" + "th-TH");
            CacheHelper.Remove(hotNewsKey + "_" + "vi-VN");
            CacheHelper.Remove(hotNewsKey + "_" + "id-ID");
            CacheHelper.Remove(hotNewsKey + "_" + "ko-KR");
            CacheHelper.Remove(hotNewsKey + "_" + "ja-JP");
            CacheHelper.Remove(importantKeys);
        }
    }

    private static void AddBannerTypesOld()
    {
        var result = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET);
        if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
            CacheHelper.Set(bannerTypesKeyOld, result.ResultSet.Tables[0], _TimeCacheBannerTypes);
        }
        else // no rows -- means there was system errors.Try to refresh after 2 seconds
        {
            CacheHelper.Remove(bannerTypesKeyOld);
        }
    }

    private static void LoadBannerTypes()
    {
        var result = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET);
        if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
            CacheHelper.Set(bannerTypesKey, result.ResultSet.Tables[0], _TimeCacheBannerTypes);
        }
        else // no rows -- means there was system errors.Try to refresh after 2 seconds
        {
            CacheHelper.Remove(bannerTypesKey);
        }

    }

    public static string GetLatestHotNews(out bool isImportant)
    {
        lock (HttpContext.Current.Application)
        {
            if (LastRefreshHotNews.AddSeconds(_TimeCache).CompareTo(DateTime.Now) <= 0)
            {
                LastRefreshHotNews = DateTime.Now;
                AddHotNews();
            }
        }
        isImportant = Convert.ToBoolean(CacheHelper.Get<object>(importantKeys));
        
        return CacheHelper.Get<string>(hotNewsKey + "_" + AppSettings.Session.Language);
    }

    public static DataTable GetBannerTypes()
    {
        lock (HttpContext.Current.Application)
        {
            var table = CacheHelper.Get<DataTable>(bannerTypesKey);
            if (table == null )
            {
                LoadBannerTypes();
                return CacheHelper.Get<DataTable>(bannerTypesKey);
            }

            return table;
        }
    }

    public static DataTable GetBannerTypesOld()
    {
        lock (HttpContext.Current.Application)
        {
            var table = CacheHelper.Get<DataTable>(bannerTypesKeyOld);
            if (table == null)
            {
                AddBannerTypesOld();
                return CacheHelper.Get<DataTable>(bannerTypesKeyOld);
            }

            return table;
        }
    }
}
