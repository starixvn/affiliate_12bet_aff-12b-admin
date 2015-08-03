<%@ WebHandler Language="C#" Class="CommGroupAssign" %>

using System;
using System.Web;
using DNAide.Data;
using DNAide.Core;
using System.Web.SessionState;

public class CommGroupAssign : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string []arr = context.Request.Params["params"].Split(new char[]{'$'}, StringSplitOptions.RemoveEmptyEntries);
            
            try
            {                
             
                DbResult result = DataHandler.GroupSetting.CommGroupAssign(Convert.ToInt32(arr[0]),arr[1], context.Request.UserHostAddress);
                context.Response.Write(result.Outputs["errmsg"]);
                
            }
            catch (Exception e)
            {
                context.Response.Write(e.ToString());              
#if DEBUG
                throw;

#else
                DNAide.Web.DNAideLog.Write(e.ToString());
#endif

            }
           
            
        }   
      
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}
