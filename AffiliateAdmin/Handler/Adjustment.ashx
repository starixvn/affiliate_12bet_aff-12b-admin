<%@ WebHandler Language="C#" Class="Adjustment" %>

using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;

public class Adjustment : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        int custid = CastAide.AsInt32(context.Request["custid"]);
        decimal dcAdjust = CastAide.AsDecimal(context.Request["adj"], 0);
        string description = context.Request["des"];
        if (dcAdjust == 0)
            return;
        try
        {
            Int64 transId = 0;
            string errMsg = string.Empty;
            DateTime winlostDate = new DateTime();
            int errCode = 0;
            errCode = DataHandler.Affiliate.Adjustment(dcAdjust, custid, AppSettings.Session.CustID, description, ref transId, ref errMsg);
            errMsg = (string.IsNullOrEmpty(errMsg) ? Resources.Lang.UpdateSucc : errMsg);
            context.Response.Write(errCode + "@" + errMsg);
        }
        catch (Exception ex)
        {
            context.Response.Write("1@" + ex.Message);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
