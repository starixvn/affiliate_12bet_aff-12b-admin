using System;
using System.Web;
using DNAide.Web;

public partial class AddAffiliateDomain : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("Domain/AddAffiliateDomain.html");        
        TemplateHandler.Assign(ref list, "AffDomain", Resources.Lang.Domain);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);        
        TemplateHandler.Assign(ref list, "submit", Resources.Lang.Add);
        TemplateHandler.Assign(ref list, "cancel", Resources.Lang.Cancel);        
        this.HtmlContent = list;

    }
}
