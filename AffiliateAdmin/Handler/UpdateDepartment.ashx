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
        string status = (string.IsNullOrEmpty(context.Request.Form["sta"]) ? "Add" : context.Request.Form["sta"].ToString());

        string groupN = CastAide.AsString(context.Request.Form["depname"]);
        string groupD = CastAide.AsString(context.Request.Form["depdesc"]);
        DbResult dbr = null;
        
        switch (status)
        {
            case "Edit":
                {
                    int groupId = CastAide.AsInt32(context.Request.Form["depid"]);
                    dbr = DataHandler.UserControl.UpdateDepartment(groupId, groupN, groupD, AppSettings.Session.CustID);
                    break;   
                }
            case "Add":
                {
                    dbr = DataHandler.UserControl.AddDepartment(groupN, groupD, AppSettings.Session.CustID);
                    break;
                }
            case "Del":
                {
                    int groupId = CastAide.AsInt32(context.Request.Form["depid"]);
                    dbr = DataHandler.UserControl.DelDepartment(groupId, AppSettings.Session.CustID);
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
