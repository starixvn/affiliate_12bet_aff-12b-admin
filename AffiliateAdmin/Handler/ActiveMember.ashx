<%@ WebHandler Language="C#" Class="ActiveMember" %>
using System.Web.SessionState;
using System;
using System.Web;

public class ActiveMember : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";

        string errUserNames = string.Empty;
        string errMsg = string.Empty;
        
        string usernames = context.Request.Form["usernames"].ToString();

        if (!string.IsNullOrEmpty(usernames))
        {
            Active12BetAccount.Active12BetUsernames(usernames, ref errUserNames, ref errMsg);

            if (string.IsNullOrEmpty(errUserNames) && string.IsNullOrEmpty(errMsg))
            {
                context.Response.Write("Ok");
            }
            else
            {
                if (!string.IsNullOrEmpty(errMsg))
                {
                    context.Response.Write("errMsg," + errMsg);
                }
                else
                {
                    context.Response.Write("errUserNames," + errUserNames);
                }
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}