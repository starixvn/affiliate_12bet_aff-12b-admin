<%@ WebHandler Language="C#" Class="GetOnlineList" %>

using System;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide;
using DNAide.Core;
using System.Web.SessionState;

public class GetOnlineList : IHttpHandler, IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string param = context.Request["params"];
        int page = CastAide.AsInt32(param.Split(',')[0]);
        int pagesize = CastAide.AsInt32(param.Split(',')[1]);
        string ret = OnlineList.GenerateOnlineListTable(page, pagesize);
        context.Response.Write(ret);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
