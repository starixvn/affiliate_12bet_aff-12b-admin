<%@ WebHandler Language="C#" Class="UpdateDepartment" %>

using System;
using System.Web;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using System.Web.SessionState;
public class UpdateDepartment : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string action = (string.IsNullOrEmpty(context.Request.Form["sta"]) ? "Add" : context.Request.Form["sta"].ToString());

        int affDomainId = CastAide.AsInt32(context.Request.Form["affDomainId"]);   
        string affDomainName = CastAide.AsString(context.Request.Form["affDomainName"]);                
        DbResult dbr = null;
        
        switch (action)
        {
            case "Edit":
                {                                     
                    dbr = DataHandler.Domain.UpdateAffDomain(affDomainId, affDomainName, AppSettings.Session.CustID);
                    break;   
                }
            case "Add":
                {
                    dbr = DataHandler.Domain.AddAffDomain(affDomainName, AppSettings.Session.CustID);
                    break;
                }
            case "Del":
                {
                    dbr = DataHandler.Domain.DelAffDomain(affDomainId, affDomainName, AppSettings.Session.CustID);
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
