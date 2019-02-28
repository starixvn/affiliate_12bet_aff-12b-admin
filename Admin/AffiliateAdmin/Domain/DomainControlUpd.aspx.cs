using System;
using DNAide.Core;
using DNAide.Web;

public partial class Domain_DomainControlUpd : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Domain/DomainControlUpd.htm");

        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "slMarket", AffAdmin.GenerateHTML.Domain.GenMarketList(Request["market"]));
        TemplateHandler.Assign(ref strTpl, "slAffDomain", AffAdmin.GenerateHTML.Domain.GenAffDomainList(Request["affdomain"]));
        TemplateHandler.Assign(ref strTpl, "slRedirectDomain", AffAdmin.GenerateHTML.Domain.Gen12BetDomainList(Request["redirectdomain"]));
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref strTpl, "ic-status", CastAide.AsBoolean(Request["status"]) ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTpl, "id", Request["id"]);
        TemplateHandler.Assign(ref strTpl, "redirectDomain", Request["redirectdomain"]);

        this.HtmlContent = strTpl;
    }
}