using System;
using Criterias;
using DNAide.Web;
using AffiliateAdmin.AffBetlistService;

public partial class BetList_BetListResults : IBCPage
{
    protected object betlistResultsHtmlString;

    protected void Page_Load(object sender, EventArgs e)
    {
        var affId = int.Parse(Request["affid"]);
        var validationCriteria = new ValidationCriteria
        {
            AffId = affId,
            MemberName = "12bet$" + Request["UserName"]
        };

        var result = DataHandler.Affiliate.ValidatePermission(validationCriteria);

        if (!result)
        {
            betlistResultsHtmlString =
                "<p style=\"font-size: 11px; font-weight: bold; text-align: center; margin: 0;\">No information is available.</p>";
        }
        else
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
            affBetlistService.Timeout = AppSettings.BetListServiceTimeout;
            betlistResultsHtmlString = affBetlistService.GetBetlistResults(
                "12bet",
                "en-US",
                criteria,
                "en-US");
        }

        var strTemplate = TemplateHandler.LoadTemplate("UserInfo/BetListResults.htm");
        TemplateHandler.Assign(ref strTemplate, "betlistResultsHtmlString", betlistResultsHtmlString);

        this.HtmlContent = strTemplate;
    }
}