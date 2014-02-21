<%@ WebHandler Language="C#" Class="UpdAffType" %>

using System;
using System.Web;
using System.Web.SessionState;
using DNAide.Data;
using DNAide.Utils;
using DNAide.Core;
using DNAide.Web;

public class UpdAffType : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context = IBCHandler.InitializeCulture(context);
        try
        {
            int afftype = CastAide.AsInt32(context.Request["afftype"], 2);

            DbResult result = DataHandler.Affiliate.AffUpdateAffType(AppSettings.Session.AffId, afftype);
           
            if (CastAide.AsInt32(result.Outputs["errcode"], 0) == 0)
                context.Response.Write("0@Update Affiliate Type successfully! :)");
            else
                context.Response.Write("1@Error :( " + result.Outputs["errcode"]);
        }
        catch (Exception ex)
        {
            context.Response.Write("1@" + ex.Message);
            DNAideLog.Write(ex.Message);
        }
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
