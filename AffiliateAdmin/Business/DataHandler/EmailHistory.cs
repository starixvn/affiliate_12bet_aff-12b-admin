using System;
using System.Web;
using System.Collections;
using System.Configuration;
using DNAide.Data;
namespace DataHandler
{
    /// <summary>
    /// class for controling Email History
    /// Author : DungTong on 20090324
    /// </summary>

    public class EmailHistory
    {

        public static DbResult EmailHistorySel(object mid, string senderName, string email, string affiliateName, object fromdate, object todate, int pagesize, int pageindex)
        {
            Hashtable hsParam = new Hashtable();
            if (fromdate != null && todate != null)
            {
                hsParam.Add("fromdate", fromdate);
                hsParam.Add("todate", Convert.ToDateTime(todate).AddDays(1).AddMilliseconds(-3));
            }
            if (mid!=null)
            {
                hsParam.Add("mid", mid);
            }
            if (!string.IsNullOrEmpty(senderName))
            {
                hsParam.Add("sendername", HttpContext.Current.Server.HtmlEncode(senderName));
            }
            if (!string.IsNullOrEmpty(email))
            {
                hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(email));
            }
            if (!string.IsNullOrEmpty(affiliateName))
            {
                hsParam.Add("affiliateusername", HttpContext.Current.Server.HtmlEncode(affiliateName));
            }
            hsParam.Add("pagesize", Convert.ToInt32(pagesize));
            hsParam.Add("pageindex", pageindex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);
            /* hsParam.Add("language", Common.GetLangChar());*/

            return DbHelper.Execute("Aff_EmailHistorySel", hsParam, DBType.AFFILIATE_12BET);
        }
        /// <summary>
        /// Delete list of selected email id by @DungTong
        /// </summary>
        /// <param name="mailIDList">list of id separated by a comma</param>
        /// <returns>Error Message</returns>
        public static string DeleteMailHistory(string mailIDList)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("listmid", mailIDList);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_EmailHistoryDel", hsParam, DBType.AFFILIATE_12BET);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }
    }

}
