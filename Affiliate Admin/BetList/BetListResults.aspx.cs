using System;
using AffBetlistService;
using DNAide.Web;

public partial class BetList_BetListResults : IBCPage
{
    protected object betlistResultsHtmlString;

    protected void Page_Load(object sender, EventArgs e)
    {
        var bettype = int.Parse(Request["bettype"]);
        var criteria = new BetListResultsCriteria();
        if (bettype == 9 || bettype == 29)
        {
            criteria.MatchId = int.Parse(Request["matchid"]);
            criteria.Bettype = int.Parse(Request["bettype"]);
            criteria.RefNo = long.Parse(Request["refno"]);
            criteria.WinLossDate = DateTime.Parse(Request["winlossdate"]);
            criteria.UserName = Request["username"];
        }
        else
        {
            criteria.MatchId = int.Parse(Request["matchid"]);
            criteria.Bettype = int.Parse(Request["bettype"]);
            criteria.RaceNo = int.Parse(Request["raceno"]);
            criteria.SportType = int.Parse(Request["sporttype"]);
            criteria.IsOutRight = int.Parse(Request["isoutright"]);
            criteria.UserName = Request["username"];
            criteria.BetId = Request["betid"];
        }

        var affBetlistService = new AffiliateAPI();
        betlistResultsHtmlString = affBetlistService.GetBetlistResults(
            "12bet",
            "en-US",
            criteria);
        
        var strTemplate = TemplateHandler.LoadTemplate("UserInfo/BetListResults.htm");
        TemplateHandler.Assign(ref strTemplate, "betlistResultsHtmlString", betlistResultsHtmlString);

        this.HtmlContent = strTemplate;
    }
}