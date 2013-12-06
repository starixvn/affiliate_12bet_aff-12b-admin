using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;

public partial class Domain_DomainControlUpd : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Domain/DomainControlUpd.htm");

        TemplateHandler.Assign(ref strTpl, "slMarket", AffAdmin.GenerateHTML.Domain.GenMarketList(Request["market"]));
        TemplateHandler.Assign(ref strTpl, "slAffDomain", AffAdmin.GenerateHTML.Domain.GenAffDomainList(Request["affdomain"]));
        TemplateHandler.Assign(ref strTpl, "slRedirectDomain", AffAdmin.GenerateHTML.Domain.Gen12BetDomainList(Request["redirectdomain"]));        
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);        
        TemplateHandler.Assign(ref strTpl, "ic-status", CastAide.AsBoolean(Request["status"]) ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTpl, "id", Request["id"]);
        this.HtmlContent = strTpl;
    }
}