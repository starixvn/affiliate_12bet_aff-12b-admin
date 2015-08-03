using System;
using System.Web;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
public partial class EditAffiliateDomain : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("Domain/EditAffiliateDomain.html");
        string affDomainId= Request["affDomainID"];
        string affDomainName = Request["affDomainName"];
        

        TemplateHandler.Assign(ref list, "AffDomain", Resources.Lang.Domain);
        TemplateHandler.Assign(ref list, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);                
        TemplateHandler.Assign(ref list, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref list, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref list, "affdomainid", affDomainId);
        TemplateHandler.Assign(ref list, "AffDomainName", affDomainName);
        TemplateHandler.Assign(ref list, "ic-status", CastAide.AsBoolean(Request["status"]) ? "ic-check" : "ic-uncheck");
        
        
        this.HtmlContent = list;
    }
}
