<%@ WebHandler Language="C#" Class="BetListParlay" %>

using System.Web;
using AffBetlistService;

public class BetListParlay : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        var affBetlistService = new AffiliateAPI();
        var parlayHtmlString = affBetlistService.GetBetlistParlay(
            "12bet",
            context.Request.Form["transId"],
            "1");
        
        context.Response.Write(parlayHtmlString);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}