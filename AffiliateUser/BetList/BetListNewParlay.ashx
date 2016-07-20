<%@ WebHandler Language="C#" Class="BetListNewParlay" %>

using System;
using System.Web;
using System.Web.SessionState;
using AffiliateUser.AffBetlistService;

public class BetListNewParlay : IHttpHandler, IRequiresSessionState {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        var transId = context.Request.Params["transId"].ToString();
        var betTeam = context.Request.Params["betTeam"].ToString();
        var betCheck = context.Request.Params["betCheck"].ToString();
        var handicap1 = Convert.ToDecimal(context.Request.Params["handicap1"].ToString());
        var handicap2 = Convert.ToDecimal(context.Request.Params["handicap2"].ToString());

        AffiliateAPI affBetlistService = new AffiliateAPI();

        var tickets = affBetlistService.GetNewBetlistParlay(
            "12bet",
            transId.ToString(),
            "1", //Exchange Rate
            betTeam,
            betCheck,
            handicap1,
            handicap2,
            AppSettings.Session.Language);

        context.Response.Write(tickets);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}