using System;
using AffiliateUser.AffBetlistService;
using DNAide.Web;

public partial class BetList_BetListCashOutDetail : RootPageLogIn
{
    protected object betlistCashOutDetailHtmlString;

    protected void Page_Load(object sender, EventArgs e)
    {
        var criteria = new BetListDetailCriteria
        {
            TransId = long.Parse(Request["transId"]),
            RefNo = long.Parse(Request["refno"]),
            BetType = int.Parse(Request["betType"]),
            BetdaqId = long.Parse(Request["betdaqId"]),
            ExchangeRate = 1
        };

        var isCashOut = bool.Parse(Request["isCashOut"]);

        using (var affBetlistService = new AffiliateAPI())
        {
            affBetlistService.Timeout = AppSettings.BetListServiceTimeout;
            betlistCashOutDetailHtmlString = affBetlistService.GetBetListCashOutDetail(
                criteria, isCashOut, "en-US");

            var strTemplate = TemplateHandler.LoadTemplate("UserInfo/BetListCashOutDetail.htm");
            TemplateHandler.Assign(ref strTemplate, @"betlistCashOutDetailHtmlString", betlistCashOutDetailHtmlString);

            this.HtmlContent = strTemplate;
        }
    }
}
