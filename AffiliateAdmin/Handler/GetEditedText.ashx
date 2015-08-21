<%@ WebHandler Language="C#" Class="GetEditedText" %>

using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using System.Data;

public class GetEditedText : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            int textID = Convert.ToInt32(context.Request["textid"].Trim());
            int noRow = Convert.ToInt32(context.Request["noRow"].Trim());
            var deviceType = context.Request["deviceType"];
            context.Response.Write(GenerateHTML.Text.GetEditedText(textID, noRow, deviceType));

        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}