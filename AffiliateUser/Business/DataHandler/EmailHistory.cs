using System.Collections;
using System.Web;
using DNAide.Data;

namespace DataHandler
{
    /// <summary>
    /// class to insert a new email into the system
    /// Author : DungTong on 20090317
    /// </summary>

    public class EmailHistory
    {
        public static string InsertEmail(string content,string sender,string affiliate,string email,string subject)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("mid", 0);
            hsParam.Add("creator", AppSettings.Session.CustID);
            hsParam.Add("content",HttpContext.Current.Server.HtmlEncode(content));
            hsParam.Add("sender", HttpContext.Current.Server.HtmlEncode(sender));
            hsParam.Add("affiliate", HttpContext.Current.Server.HtmlEncode(affiliate));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(email));
            hsParam.Add("subject", HttpContext.Current.Server.HtmlEncode(subject));
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("browser", HttpContext.Current.Request.Browser.Browser);
            hsParam.Add("sessionid", HttpContext.Current.Session.SessionID);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_EmailHistoryIns", hsParam, DBType.AFFILIATE_12BET);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]); 
        }
    }
}
