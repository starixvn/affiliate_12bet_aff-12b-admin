<%@ WebHandler Language="C#" Class="DeleteMailList" %>

using System;
using System.Web;
using DNAide.Data;
using DNAide.Utils;
using DNAide.Core;
using System.Data;
using System.Web.Security;
using System.Web.SessionState;
using DataHandler;
using System.Text;


public class DeleteMailList : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer != null)
        {

            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";

            string midlist = context.Request.Params["params"].Trim();
            if (string.IsNullOrEmpty(midlist))
            {
                context.Response.Write("Null data is sent to server");
                return;
            }
            bool isHost = Common.isHost(AppSettings.Session.CustID, "[HotNews]");
            if (isHost)
            {
                //Drop the comma at the end of message
                midlist = midlist.Substring(0, midlist.Length - 1);
                StringBuilder result = new StringBuilder();
                result.Append(DataHandler.EmailHistory.DeleteMailHistory(midlist));
                context.Response.Write(result.ToString());
            }
            else
            {
                context.Response.Write(Resources.Lang.UpdateRolePolicyErr);
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
