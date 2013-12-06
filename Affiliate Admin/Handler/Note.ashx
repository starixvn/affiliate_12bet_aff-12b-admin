<%@ WebHandler Language="C#" Class="Note" %>

using System;
using System.Web;
using System.Web.SessionState;
using DNAide.Core;
using DNAide.Data;

public class Note : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        context.Response.Cache.SetCacheability(HttpCacheability.Private);
        int intStatus = CastAide.AsInt32(context.Request["status"]);
        int intCustId = CastAide.AsInt32(context.Request["custid"]);
        int noteid = CastAide.AsInt32(context.Request["id"]);
        string strNoteContent = context.Request["note"];
        //strNoteContent = context.Server.HtmlEncode(strNoteContent);
        try
        {
            DbResult result = null;
            switch (intStatus)
            {
                case 1://Add
                    result = DataHandler.Affiliate.AddNote(intCustId, AppSettings.Session.CustID, strNoteContent);
                    break;
                case 2:
                    result = DataHandler.Affiliate.UpdateNote(intCustId, AppSettings.Session.CustID, noteid, strNoteContent);
                    break;
                case 3://Delete
                    string strListNote = context.Request["id"];
                    if (string.IsNullOrEmpty(strListNote.Trim()))
                        break;
                    result = DataHandler.Affiliate.DeleteNote(AppSettings.Session.CustID, AppSettings.Session.CustID, strListNote);
                    break;
                default:
                    result = DataHandler.Affiliate.AddNote(intCustId, AppSettings.Session.CustID, strNoteContent);
                    break;
            }
            if (CastAide.AsInt32(result.Outputs["errcode"], -1) == 0)
                context.Response.Write("ok");
            else
                context.Response.Write("error@" + result.Outputs["errmsg"].ToString());
        }
        catch(Exception ex)
        {
            context.Response.Write("error@" + ex.Message);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
