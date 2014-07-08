<%@ WebHandler Language="C#" Class="UpdateDepartment" %>

using System;
using System.Web;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using System.Web.SessionState;
public class UpdateDepartment : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string action = (string.IsNullOrEmpty(context.Request.Form["sta"]) ? "Add" : context.Request.Form["sta"].ToString());

        int _12BetDomainId = CastAide.AsInt32(context.Request.Form["_12BetDomainId"]);   
        string _12BetDomainName = CastAide.AsString(context.Request.Form["_12BetDomainName"]);
        string deviceType = CastAide.AsString(context.Request.Form["deviceType"]); 
        DbResult dbr = null;
        
        switch (action)
        {
            case "Edit":
                {
                    dbr = DataHandler.Domain.Update12BetDomain(_12BetDomainId, _12BetDomainName, deviceType, AppSettings.Session.CustID);
                    break;   
                }
            case "Add":
                {
                    dbr = DataHandler.Domain.Add12BetDomain(_12BetDomainName, deviceType, AppSettings.Session.CustID);
                    break;
                }
            case "Del":
                {
                    dbr = DataHandler.Domain.Del12BetDomain(_12BetDomainId, _12BetDomainName, AppSettings.Session.CustID);
                    break;
                }
        }       
        
        if (CastAide.AsInt32(dbr.Outputs["errcode"]) == 0)
        {
            context.Response.Write("Ok");
        }
        else
        {
            context.Response.Write(dbr.Outputs["errmsg"].ToString());
        }  
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
