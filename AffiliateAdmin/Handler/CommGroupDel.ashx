<%@ WebHandler Language="C#" Class="CommGroupDel" %>

using System;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using System.Web.SessionState;

public class CommGroupDel : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string groupID = context.Request.Params["params"];
            
            if (string.IsNullOrEmpty(groupID))
            {
                context.Response.Write("Invalid group ID");
                return;
            }
            
            try
            {
                DbResult result = DataHandler.GroupSetting.CommGroupDel(Convert.ToInt32(groupID), context.Request.UserHostAddress);
                context.Response.Write(result.Outputs["errmsg"]);
                
            }
            catch (Exception e)
            {
                context.Response.Write(e.ToString());              
#if DEBUG
                throw;

#else
                Fanex.Logging.LogService.Error(e.ToString());
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
