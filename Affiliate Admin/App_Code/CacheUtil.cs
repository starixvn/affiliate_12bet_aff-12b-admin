using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using DNAide.Data;
using System.Data;

public class CacheUtil
{
    private static string hsAffInfo = "hsAffInfo";
    //private static string dtbAffInfo = "dtbAffInfo";
    static DateTime LastRefresh;
    static int _TimeCache = 120;
    static CacheUtil()
    {
        LastRefresh = DateTime.MinValue;
        InitCache();
    }
    private static void AddAffInfo()
    {
        DbResult result = DbHelper.Execute("Aff_AffiliateSelAffInfo", DBType.AFFILIATE_12BET);
        Hashtable hsResult = new Hashtable();
        foreach (DataRow dr in result.ResultSet.Tables[0].Rows)
        {
            hsResult[dr["affid"].ToString()] = dr["username"];
        }

        if (result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
            CacheFactory.GetCacheManager().Add(hsAffInfo, hsResult, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            //CacheFactory.GetCacheManager().Add(dtbAffInfo, result.ResultSet.Tables[0], CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
        }
        else // no rows -- means there was system errors.Try to refresh after 2 seconds
        {
            CacheFactory.GetCacheManager().Add(hsAffInfo, null, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
            //CacheFactory.GetCacheManager().Add(dtbAffInfo, null, CacheItemPriority.None, null, new AbsoluteTime(TimeSpan.FromSeconds(_TimeCache)));
        }

    }
    public static Hashtable GetHSAffInfo()
    {
        lock (HttpContext.Current.Application)
        {
            if (LastRefresh.AddSeconds(_TimeCache).CompareTo(DateTime.Now) <= 0)
            {
                LastRefresh = DateTime.Now;
                AddAffInfo();
            }
        }
        return (Hashtable)CacheFactory.GetCacheManager().GetData(hsAffInfo);

    }
    /*public static DataTable GetDTBAffInfo()
    {
        lock (HttpContext.Current.Application)
        {
            if (LastRefresh.AddSeconds(_TimeCache).CompareTo(DateTime.Now) <= 0)
            {
                LastRefresh = DateTime.Now;
                AddAffInfo();
            }
        }
        return (DataTable)CacheFactory.GetCacheManager().GetData(dtbAffInfo);

    }*/

    public static void InitCache()
    {
        AddAffInfo();
    }
}
