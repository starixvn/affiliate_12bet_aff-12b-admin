<%@ WebHandler Language="C#" Class="DeleteMessageList" %>

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


public class DeleteMessageList : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer != null)
        {

            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";

            string msgidlist = context.Request.Params["params"].Trim();
            if (string.IsNullOrEmpty(msgidlist))
            {
                context.Response.Write("Null data is sent to server");
                return;
            }
            //Drop a comma at the end of message
            msgidlist = msgidlist.Substring(0, msgidlist.Length - 1);

            StringBuilder result = new StringBuilder();
            result.Append(DataHandler.HotNews.DeleteHotNews(msgidlist));           
            context.Response.Write(result.ToString());


        }

    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
