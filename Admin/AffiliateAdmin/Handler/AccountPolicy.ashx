<%@ WebHandler Language="C#" Class="AccountPolicy" %>

using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class AccountPolicy : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";
        int custid = string.IsNullOrEmpty(context.Request.Form["custid"]) ? 0 : CastAide.AsInt32(context.Request.Form["custid"]);
        string deptname = string.IsNullOrEmpty(context.Request.Form["depname"]) ? "" : context.Request.Form["depname"];
        string deptid = string.IsNullOrEmpty(context.Request.Form["depid"]) ? "" : context.Request.Form["depid"];
        bool isListUser = string.IsNullOrEmpty(context.Request.Form["isLstUser"]) ? false : CastAide.AsBoolean(context.Request.Form["isLstUser"]);
        var service = new AccountPolicyService();
        context.Response.Write(service.GetAccountPolicy(custid, deptname, deptid, isListUser));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}