<%@ WebHandler Language="C#" Class="GetViewedText" %>

using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using DNAide.Data;
using System.Data;

public class GetViewedText : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            int textID = Convert.ToInt32(context.Request["textid"].Trim());
            int noRow = Convert.ToInt32(context.Request["noRow"].Trim());
            context.Response.Write(GenerateHTML.Text.GetViewedText(textID, noRow));

        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}