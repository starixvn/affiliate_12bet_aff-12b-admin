<%@ WebHandler Language="C#" Class="EditRate" %>

using System;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Web;
using DNAide.Core;
using System.Web.SessionState;

public class EditRate : IHttpHandler, IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) 
    {
        //return;//Disable Edit exchange rate
        context.Response.ContentType = "text/html";
        try
        {
            int exId = CastAide.AsInt32(context.Request["id"]);
            decimal rate = CastAide.AsDecimal(context.Request["rate"]);
            string column = CastAide.AsString(context.Request["column"]);
            
            DbResult rs = DataHandler.ExchangeRate.Exchange_Update(exId, rate, column);
            if (CastAide.AsInt32(rs.Outputs["errcode"]) == 0)
            {
                //This SP is no need to run(because exchange rate on one month can have multiple value)
                //DbResult rs2 = DataHandler.ExchangeRate.Exchange_Update_RptDepWinlost(exId);
                context.Response.Write("1");                
            }
            else
            {
                context.Response.Write(rs.Outputs["errmsg"]);
            }            
        }
        catch (Exception ex)  
        {
#if DEBUG
                throw;
#endif
            Fanex.Logging.LogService.Error(ex.ToString());
            context.Response.Write(ex.Message);
        }  
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
