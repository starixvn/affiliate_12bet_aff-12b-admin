using System;
using DNAide.Web;

public partial class Domain_DomainControlIns : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Domain/DomainControlIns.htm");
        TemplateHandler.Assign(ref strTpl, "slMarket", AffAdmin.GenerateHTML.Domain.GenMarketList(string.Empty));
        TemplateHandler.Assign(ref strTpl, "slAffDomain", AffAdmin.GenerateHTML.Domain.GenAffDomainList(string.Empty));
        TemplateHandler.Assign(ref strTpl, "slRedirectDomain", AffAdmin.GenerateHTML.Domain.Gen12BetDomainList("0", string.Empty));
        TemplateHandler.Assign(ref strTpl, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Add);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref strTpl, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList());
        this.HtmlContent = strTpl;

    }
}