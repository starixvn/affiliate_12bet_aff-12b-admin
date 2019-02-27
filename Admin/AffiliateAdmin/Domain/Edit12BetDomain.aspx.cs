using System;
using System.Web;
using DNAide.Core;
using DNAide.Web;
public partial class Edit12BetDomain : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("Domain/Edit12BetDomain.html");
        string _12BetDomainID = Request["12BetDomainID"];
        string _12BetDomainName = Request["12BetDomainName"];

        TemplateHandler.Assign(ref list, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref list, "12BetDomain", Resources.Lang.Domain);
        TemplateHandler.Assign(ref list, "SSL", Resources.Lang.SSL);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref list, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref list, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref list, "12betdomainid", _12BetDomainID);
        TemplateHandler.Assign(ref list, "12BetDomainName", _12BetDomainName);
        TemplateHandler.Assign(ref list, "ic-ssl", CastAide.AsBoolean(Request["ssl"]) ? "ic-check" : "ic-uncheck");

        TemplateHandler.Assign(ref list, "requireDomain", Resources.Lang.ErrAffDomain);
        TemplateHandler.Assign(ref list, "updateSuccess", Resources.Lang.ErrUpdateSucc);

        this.HtmlContent = list;
    }
}
