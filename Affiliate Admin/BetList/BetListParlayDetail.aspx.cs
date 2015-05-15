using System;
using AffBetlistService;
using DNAide.Web;

public partial class BetList_BetListParlayDetail : IBCPage
{
    protected object betlistParlayDetailHtmlString;

    protected void Page_Load(object sender, EventArgs e)
    {
        var criteria = new BetListSystemParlayDetailCriteria
            {
                TransId = long.Parse(Request["transId"]),
                RefNo = long.Parse(Request["refno"]),
                BetId = int.Parse(Request["betId"]),
                CustId = int.Parse(Request["custid"])
            };

        var affBetlistService = new AffiliateAPI();
        affBetlistService.Timeout = AppSettings.BetListServiceTimeout;
        betlistParlayDetailHtmlString = affBetlistService.GetBetlistSystemParlay(
            "12bet",
            "1",
            criteria,
            "en-US");

        var strTemplate = TemplateHandler.LoadTemplate("UserInfo/BetListParlayDetail.htm");
        TemplateHandler.Assign(ref strTemplate, "betlistParlayDetailHtmlString", betlistParlayDetailHtmlString);

        this.HtmlContent = strTemplate;
    }
}