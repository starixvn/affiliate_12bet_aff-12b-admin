using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Caching;
using DNAide.Core;

public static class Common
{
    public static void InitAccessPermission()
    {
        Dictionary<string, string> pages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            DataTable dtb = DataHandler.UserControl.GetFunctionInfo().ResultSet.Tables[0];
            foreach (DataRow dr in dtb.Rows)
            {
                if (CastAide.AsInt32(dr["SubFuncCount"]) == 0)
                {
                    string strFilesName = dr["FileNames"].ToString();
                    string strFuncName = dr["FuncName"].ToString();
                    string[] strListFiles = strFilesName.Split(';');
                    for (int i = 0; i < strListFiles.Length; i++)
                    {
                        try
                        {
                            pages.Add(strListFiles[i].Trim(), strFuncName.Trim() + "-1");
                        }
                        catch { }
                    }
                }
            }
        }
        catch
        {
            throw;
        }
        HttpContext.Current.Cache.Remove("AccessPermission");
        HttpContext.Current.Cache.Add("AccessPermission", pages, null, Cache.NoAbsoluteExpiration, TimeSpan.Zero, CacheItemPriority.Normal, null);
    }
}
