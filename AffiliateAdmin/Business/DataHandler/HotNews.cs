using System;
using System.Collections;
using System.Configuration;
using DNAide.Data;
using System.Web;
namespace DataHandler
{
    /// <summary>
    /// class for getting hot news information
    /// Author : DungTong on 20090317
    /// </summary>

    public class HotNews
    {
        /// <summary>
        /// Get hot news information by msgid
        /// </summary>
        /// <returns>result set for this msgid</returns>
        public static DbResult GetHotNews(int id)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("msgid", id);
            return DbHelper.Execute("Aff_HotNewsDetail", hsParam, DBType.AFFILIATE_12BET);
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
        public static DbResult HotNewsSel(int important, object fromdate, object todate, int pageindex, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            if (fromdate != null && todate != null)
            {
                hsParam.Add("fromdate", fromdate);
                hsParam.Add("todate", Convert.ToDateTime(todate).AddDays(1).AddMilliseconds(-3));
            }
            if (important >= 0)
            {
                hsParam.Add("important", important);
            }
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pageindex", pageindex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
           /* hsParam.Add("language", Common.GetLangChar());*/

            return DbHelper.Execute("Aff_HotNewsSel", hsParam, DBType.AFFILIATE_12BET);
        }
        /// <summary>
        /// Delete list of selected messages by @DungTong
        /// </summary>
        /// <param name="hotNewsIDList">list of id separated by a comma</param>
        /// <returns>Error Message</returns>
        public static string DeleteHotNews(string hotNewsIDList)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("listmsgid", hotNewsIDList);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_HotNewsDel", hsParam, DBType.AFFILIATE_12BET);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }
        /// <summary>
        /// Add a hot news into the system.
        /// </summary>
        /// <param name="msgEN">English version of this hotnews</param>
        /// <param name="msgCN">Chinese version of this hotnews</param>
        /// <param name="msgTW">Taiwanese version of this hotnews</param>
        /// <param name="msgTH">Thai version of this hotnews</param>
        /// <param name="important">The hot news is marked by red color or not</param>
        /// <returns>Error message</returns>
        public static string AddHotNews(string msgEN, string msgCN, string msgTW, string msgTH, string msgES, string msgVI, string msgID, string msgKR, string msgJP, bool important)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("id", 0);
            hsParam.Add("msgen", HttpContext.Current.Server.HtmlEncode(msgEN));
            hsParam.Add("msgcn", HttpContext.Current.Server.HtmlEncode(msgCN));
            hsParam.Add("msgtw", HttpContext.Current.Server.HtmlEncode(msgTW));
            hsParam.Add("msgth", HttpContext.Current.Server.HtmlEncode(msgTH));
            hsParam.Add("msges", HttpContext.Current.Server.HtmlEncode(msgES));
            hsParam.Add("msgvi", HttpContext.Current.Server.HtmlEncode(msgVI));
            hsParam.Add("msgid", HttpContext.Current.Server.HtmlEncode(msgID));
            hsParam.Add("msgkr", HttpContext.Current.Server.HtmlEncode(msgKR));
            hsParam.Add("msgjp", HttpContext.Current.Server.HtmlEncode(msgJP));
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("important", important);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_HotNewsIns", hsParam, DBType.AFFILIATE_12BET);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }
        /// <summary>
        /// Update an existing hot news in the system.
        /// </summary>
        /// <param name="msgid">id of this hot news</param>
        /// <param name="msgEN">English version of this hotnews</param>
        /// <param name="msgCN">Chinese version of this hotnews</param>
        /// <param name="msgTW">Taiwanese version of this hotnews</param>
        /// <param name="msgTH">Thai version of this hotnews</param>
        /// <param name="important">The hot news is marked by red color or not</param>
        /// <returns>Error message</returns>
        public static string UpdateHotNews(int id, string msgEN, string msgCN, string msgTW, string msgTH, string msgES, string msgVI, string msgID, string msgKR, string msgJP, bool important)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("id", id);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            hsParam.Add("msgen", HttpContext.Current.Server.HtmlEncode(msgEN));
            hsParam.Add("msgcn", HttpContext.Current.Server.HtmlEncode(msgCN));
            hsParam.Add("msgtw", HttpContext.Current.Server.HtmlEncode(msgTW));
            hsParam.Add("msgth", HttpContext.Current.Server.HtmlEncode(msgTH));
            hsParam.Add("msges", HttpContext.Current.Server.HtmlEncode(msgES));
            hsParam.Add("msgvi", HttpContext.Current.Server.HtmlEncode(msgVI));
            hsParam.Add("msgid", HttpContext.Current.Server.HtmlEncode(msgID));
            hsParam.Add("msgkr", HttpContext.Current.Server.HtmlEncode(msgKR));
            hsParam.Add("msgjp", HttpContext.Current.Server.HtmlEncode(msgJP));
            hsParam.Add("important", important);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            DbResult result = DbHelper.Execute("Aff_HotNewsUpd", hsParam, DBType.AFFILIATE_12BET);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }

    }
}
