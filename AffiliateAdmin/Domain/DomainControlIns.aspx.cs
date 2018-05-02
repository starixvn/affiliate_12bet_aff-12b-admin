using System;
using DNAide.Web;

public partial class Domain_DomainControlIns : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Domain/DomainControlIns.htm");
        string market = Common.GetMarket(Request["market"]);

        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "slAffDomain", AffAdmin.GenerateHTML.Domain.GenAffDomainList(string.Empty));
        TemplateHandler.Assign(ref strTpl, "slRedirectDomain", AffAdmin.GenerateHTML.Domain.Gen12BetDomainList(string.Empty));
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Add);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);

        TemplateHandler.Assign(ref strTpl, "slMarket", AffAdmin.GenerateHTML.Domain.GenMarketList(market));
        this.HtmlContent = strTpl;
    }
}