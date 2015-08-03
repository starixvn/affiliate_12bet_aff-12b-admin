<%@ WebHandler Language="C#" Class="DomainControl" %>

using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;


public class DomainControl : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        int errCode = 0;
        string errMsg = string.Empty;
        bool status = CastAide.AsBoolean(context.Request["status"]);
        string action = context.Request["action"].ToString();

        bool isSusccess = false;
        switch (action)
        {
            case "ins":
                isSusccess = DataHandler.Domain.DomainControlIns(context.Request["market"], context.Request["domainAff"], context.Request["domain12bet"], status, ref errCode, ref errMsg);
                break;
            case "upd":
                isSusccess = DataHandler.Domain.DomainControlUpd(CastAide.AsInt32(context.Request["id"]), context.Request["market"], context.Request["domainAff"], context.Request["domain12bet"], status, ref errCode, ref errMsg);
                break;
            case "del":
                isSusccess = DataHandler.Domain.DomainControlDel(CastAide.AsInt32(context.Request["id"]), ref errCode, ref errMsg);
                break;                
        }
            

        if (isSusccess)
        {
            context.Response.Write("1");
        }
        else
        {
            context.Response.Write(errMsg);
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}