<%@ WebHandler Language="C#" Class="BetListParlay" %>

using System.Web;
using AffBetlistService;

public class BetListParlay : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        var affBetlistService = new AffiliateAPI();
        affBetlistService.Timeout = AppSettings.BetListServiceTimeout;
        var parlayHtmlString = affBetlistService.GetBetlistParlay(
            "12bet",
            context.Request["transId"],
            "1",
            "en-US");
        
        context.Response.Write(parlayHtmlString);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}