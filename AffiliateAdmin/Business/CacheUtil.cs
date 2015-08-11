using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using System.Data;
using Affiliate.Infrastructure;

public class CacheUtil
{
    private static string hsAffInfo = "hsAffInfo";
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
            CacheHelper.Set(hsAffInfo, hsResult, _TimeCache);
        }
        else // no rows -- means there was system errors.Try to refresh after 2 seconds
        {
            CacheHelper.Remove(hsAffInfo);
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
        return CacheHelper.Get<Hashtable>(hsAffInfo);

    }

    public static void InitCache()
    {
        AddAffInfo();
    }
}
