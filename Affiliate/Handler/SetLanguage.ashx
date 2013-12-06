<%@ WebHandler Language="C#" Class="SetLanguage" %>

using System;
using System.Web;
using System.Web.SessionState;

public class SetLanguage : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context = AffHandler.InitializeCulture(context);        
        try
        {
            string strLang = context.Request["lang"];
            AppSettings.Session.Flag = strLang;
            AppSettings.Session.Language = strLang;            
            context.Response.Write(strLang);
        }
        catch (Exception ex)
        {
            DNAide.Web.DNAideLog.Write(ex.Message);
            context.Response.Write("");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
