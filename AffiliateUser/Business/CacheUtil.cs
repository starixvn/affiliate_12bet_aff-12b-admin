using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using DNAide.Data;
using System.Data;

/// <summary>
/// Summary description for CacheUtil
/// </summary>
public class CacheUtil
{
    private static string hotNewsKey = "hotNewsKey";
    private static string importantKeys = "importantKey";
    private static string bannerTypesKey = "bannerTypesKey";
    private static string bannerTypesKeyOld = "bannerTypesKeyOld";
    static DateTime LastRefreshHotNews;
    static DateTime LastRefreshBannerTypes;
    static int _TimeCache = 5;
    static int _TimeCacheBannerTypes = 120;
    static CacheUtil()
    {
        LastRefreshHotNews = DateTime.MinValue;
        LastRefreshBannerTypes = DateTime.MinValue;
        InitCache();
    }
    private static void AddHotNews()
    {
        DbResult result = DataHandler.HotNews.GetLatestHotNews();
        if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "zh-TW", result.ResultSet.Tables[0].Rows[0]["message_tw"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "zh-CN", result.ResultSet.Tables[0].Rows[0]["message_cn"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "en-US", result.ResultSet.Tables[0].Rows[0]["message_en"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "es-ES", result.ResultSet.Tables[0].Rows[0]["message_es"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "th-TH", result.ResultSet.Tables[0].Rows[0]["message_th"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "vi-VN", result.ResultSet.Tables[0].Rows[0]["message_vi"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "id-ID", result.ResultSet.Tables[0].Rows[0]["message_id"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "ko-KR", result.ResultSet.Tables[0].Rows[0]["message_kr"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "ja-JP", result.ResultSet.Tables[0].Rows[0]["message_jp"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(importantKeys, result.ResultSet.Tables[0].Rows[0]["important"], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
        }
        else // no rows -- means there isn't any hot news
        {
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "zh-TW", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "zh-CN", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "en-US", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "es-ES", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "th-TH", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "vi-VN", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "id-ID", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "ko-KR", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(hotNewsKey + "_" + "ja-JP", string.Empty, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            CacheFactory.GetCacheManager().Add(importantKeys, false, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
        }


    }
    private static void AddBannerTypesOld()
    {
        DbResult result = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET);
        if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
            CacheFactory.GetCacheManager().Add(bannerTypesKeyOld, result.ResultSet.Tables[0], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCacheBannerTypes)));
        }
        else // no rows -- means there was system errors.Try to refresh after 2 seconds
        {
            CacheFactory.GetCacheManager().Add(bannerTypesKeyOld, null, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCacheBannerTypes)));
        }

    }
    private static void AddBannerTypes()
    {
        DbResult result = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET);
        if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
            CacheFactory.GetCacheManager().Add(bannerTypesKey, result.ResultSet.Tables[0], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCacheBannerTypes)));
        }
        else // no rows -- means there was system errors.Try to refresh after 2 seconds
        {
            CacheFactory.GetCacheManager().Add(bannerTypesKey, null, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCacheBannerTypes)));
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
        isImportant = Convert.ToBoolean(CacheFactory.GetCacheManager().GetData(importantKeys));
        return CacheFactory.GetCacheManager().GetData(hotNewsKey + "_" + AppSettings.Session.Language).ToString();
    }
    public static object GetBannerTypes()
    {
        lock (HttpContext.Current.Application)
        {
            object bt = CacheFactory.GetCacheManager().GetData(bannerTypesKey);
            if ((bt == null && LastRefreshBannerTypes.AddSeconds(_TimeCache).CompareTo(DateTime.Now) <= 0)
                || LastRefreshBannerTypes.AddSeconds(_TimeCacheBannerTypes).CompareTo(DateTime.Now) <= 0)
            {
                LastRefreshBannerTypes = DateTime.Now;
                AddBannerTypes();
            }
        }
        return CacheFactory.GetCacheManager().GetData(bannerTypesKey);

    }

    public static object GetBannerTypesOld()
    {
        lock (HttpContext.Current.Application)
        {
            object bt = CacheFactory.GetCacheManager().GetData(bannerTypesKeyOld);
            if ((bt == null && LastRefreshBannerTypes.AddSeconds(_TimeCache).CompareTo(DateTime.Now) <= 0)
                || LastRefreshBannerTypes.AddSeconds(_TimeCacheBannerTypes).CompareTo(DateTime.Now) <= 0)
            {
                LastRefreshBannerTypes = DateTime.Now;
                AddBannerTypesOld();
            }
        }
        return CacheFactory.GetCacheManager().GetData(bannerTypesKeyOld);

    }
    public static void InitCache()
    {
        AddHotNews();
        AddBannerTypes();
        AddBannerTypesOld();
    }
}
