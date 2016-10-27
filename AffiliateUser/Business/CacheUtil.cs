using System;
using System.Data;
using Affiliate.Infrastructure.Helpers;

/// <summary>
/// Summary description for CacheUtil
/// </summary>
public class CacheUtil
{
    private static string hotNewsKey = "hotNewsKey";
    private static string importantKeys = "importantKey";
    private static string bannerTypesKey = "bannerTypesKey";
    private static string bannerTypesKeyOld = "bannerTypesKeyOld";

    public static string GetLatestHotNews(out bool isImportant)
    {
        var key = hotNewsKey + "_" + AppSettings.Session.Language;
        var hotNews = CacheHelper.Get<string>(key);
        if (hotNews == null)
        {
            LoadHotNews();
            hotNews = CacheHelper.Get<string>(key);
        }

        isImportant = Convert.ToBoolean(CacheHelper.Get<object>(importantKeys));
        return hotNews;
    }

    public static DataTable GetBannerTypes()
    {
        var table = CacheHelper.Get<DataTable>(bannerTypesKey);
        if (table == null)
        {
            table = DataHandler.Banner.GetBannerTypes();
            CacheHelper.Set(bannerTypesKey, table);
        }

        return table;
    }

    #region Private Methods

    private static void LoadHotNews()
    {
        var table = DataHandler.HotNews.GetLatestHotNews();
        if (table != null && table.Rows.Count > 0)
        {
            var row = table.Rows[0];
            CacheHelper.Set(hotNewsKey + "_" + "zh-TW", row["message_tw"]);
            CacheHelper.Set(hotNewsKey + "_" + "zh-CN", row["message_cn"]);
            CacheHelper.Set(hotNewsKey + "_" + "en-US", row["message_en"]);
            CacheHelper.Set(hotNewsKey + "_" + "es-ES", row["message_es"]);
            CacheHelper.Set(hotNewsKey + "_" + "th-TH", row["message_th"]);
            CacheHelper.Set(hotNewsKey + "_" + "vi-VN", row["message_vi"]);
            CacheHelper.Set(hotNewsKey + "_" + "id-ID", row["message_id"]);
            CacheHelper.Set(hotNewsKey + "_" + "ko-KR", row["message_kr"]);
            CacheHelper.Set(hotNewsKey + "_" + "ja-JP", row["message_jp"]);
            CacheHelper.Set(importantKeys, row["important"]);
        }
    }

    #endregion Private Methods
}