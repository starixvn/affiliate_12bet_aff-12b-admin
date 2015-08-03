<%@ WebHandler Language="C#" Class="DeleteMember" %>

using System;
using System.Web;
using System.Web.SessionState;

public class DeleteMember : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        string ibccustid = context.Request.Params["ibccustid"];
        if (string.IsNullOrEmpty(ibccustid))
        {
            context.Response.Write(Resources.Lang.EmptyIBCCustID);
            return;
        }
        int id = Int32.Parse(ibccustid);
        context.Response.Write(DataHandler.Member.DeleteMember(id));    
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
