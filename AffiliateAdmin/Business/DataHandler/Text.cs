using System;
using System.Web;
using DNAide.Data;
using DNAide.Core;
using System.Collections;
using System.Configuration;

/// <summary>
/// Summary description for Text
/// </summary>
namespace DataHandler
{
    public class Text
    {
        public Text()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static int InsText(string textvalue, string category, string language, string currency, ref int errcode, ref string errmsg, string deviceType)
        {
            DbResult dbr = null;
            int result = 0;
            var device = string.IsNullOrEmpty(deviceType) ? "0" : deviceType;
            Hashtable htb = new Hashtable();
            htb.Add("bannerid", 0);
            htb.Add("message", HttpContext.Current.Server.HtmlEncode(textvalue));
            htb.Add("category", HttpContext.Current.Server.HtmlEncode(category));
            htb.Add("language", HttpContext.Current.Server.HtmlEncode(language));
            htb.Add("currency", HttpContext.Current.Server.HtmlEncode(currency));
            htb.Add("devicetype", byte.Parse(device));
            htb.Add("userid", AppSettings.Session.CustID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");

            dbr = DbHelper.Execute("Aff_TextIns_1", htb, DBType.AFFILIATE_12BET);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            result = CastAide.AsInt32(dbr.Outputs["bannerid"]);
            errmsg = dbr.Outputs["errmsg"].ToString();
            return result;
        }
        public static string UpdText(int id, string textvalue, string category, string language, string currency, string deviceType)
        {
            Hashtable htb = new Hashtable();
            var device = string.IsNullOrEmpty(deviceType) ? "0" : deviceType;
            htb.Add("bannerid", id);
            htb.Add("message", HttpContext.Current.Server.HtmlEncode(textvalue));
            htb.Add("category", HttpContext.Current.Server.HtmlEncode(category));
            htb.Add("language", HttpContext.Current.Server.HtmlEncode(language));
            htb.Add("currency", HttpContext.Current.Server.HtmlEncode(currency));
            htb.Add("devicetype", byte.Parse(device));
            htb.Add("userid", AppSettings.Session.CustID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            DbResult dbr = DbHelper.Execute("Aff_TextUpd_1", htb, DBType.AFFILIATE_12BET);
            return dbr.Outputs["errmsg"].ToString();
        }
        public static string DelDataText(string listBannerID)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("listbannerid", listBannerID);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_TextDel_1", hsParam, DBType.AFFILIATE_12BET);
            return CastAide.AsString(result.Outputs["errmsg"]);
        }
        public static DbResult GetListText(string category, string language, string currency, int pageIndex, int pageSize, string deviceType)
        {
            var hsParam = new Hashtable();
            var device = string.IsNullOrEmpty(deviceType) ? "0" : deviceType;

            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }

            hsParam.Add("devicetype", byte.Parse(device));
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_TextSel_1", hsParam, DBType.AFFILIATE_12BET);
        }
        public static DbResult GetTextDetail(int textId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("textid", textId);
            return DbHelper.Execute("Aff_TextSelDetail_1", hsParam, DBType.AFFILIATE_12BET);
        }
    }
}
