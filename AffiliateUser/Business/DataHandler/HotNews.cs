using System;
using System.Globalization;
using System.Collections;
using System.Configuration;
using DNAide.Data;
using DNAide.Core;

namespace DataHandler
{
    /// <summary>
    /// class for getting hot news information
    /// Author : DungTong on 20090317
    /// </summary>

    public class HotNews
    {
        /// <summary>
        /// Get the latest hot news
        /// </summary>
        /// <returns>string for the news</returns>
        public static DbResult GetLatestHotNews()
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("msgid", -1);
            return DNAide.Data.DbHelper.Execute("Aff_HotNewsDetail", hsParam, DBType.AFFILIATE_12BET);            
        }
        /// <summary>
        ///  Search Hot News
        /// </summary>
        /// <param name="important">important = -1 : don't search on this column, = 0: search unimportant message, -1: search important message</param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="pageindex"></param>
        /// <param name="totalpages"></param>
        /// <param name="nrows"></param>
        /// <returns>list of selected hot news</returns>
        public static DbResult HotNewsSel(int important, object fromdate, object todate, int pageindex, int intPageSize)
        {            
            Hashtable hsParam = new Hashtable();
            if (fromdate != null && todate != null)
            {
                CultureInfo info = new CultureInfo("en-US");
                hsParam.Add("fromdate", CastAide.AsDateTime(fromdate, info));
                hsParam.Add("todate", CastAide.AsDateTime(todate, info).AddDays(1).AddMilliseconds(-3));
            }
            if (important >= 0)
            {
                hsParam.Add("important", important);
            }
            hsParam.Add("pagesize", intPageSize);
            hsParam.Add("pageindex", pageindex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            hsParam.Add("language", AppSettings.Session.Language);

            return DbHelper.Execute("Aff_HotNewsSel", hsParam, DBType.AFFILIATE_12BET);
        }

    }
}
