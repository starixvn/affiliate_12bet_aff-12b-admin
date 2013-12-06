<%@ WebHandler Language="C#" Class="UpText" %>

using System;
using System.Web;
using System.Web.SessionState;

public class UpText : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/html";
            string idbanner = context.Request.Params["hdBannerID"];
            string textvalue = context.Request.Params["textvalue"];
            string category = context.Request.Params["slCategory"];
            string language = context.Request.Params["slLanguage"];
            string currency = context.Request.Params["slCurrency"];

            string err = DataHandler.Text.UpdText(Convert.ToInt32(idbanner), textvalue, category, language, currency);
            if (!string.IsNullOrEmpty(err))
            {
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                return;
            }
            //Upload new file
            context.Response.Write("<script type=\"text/javascript\">parent.CancelEdit();</script>");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}