using System;
using System.Web;
using DNAide.Web;

public partial class Add12BetDomain : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("Domain/Add12BetDomain.html");
        TemplateHandler.Assign(ref list, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref list, "12BetDomain", Resources.Lang.Domain);
        TemplateHandler.Assign(ref list, "SSL", Resources.Lang.SSL);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref list, "submit", Resources.Lang.Add);
        TemplateHandler.Assign(ref list, "cancel", Resources.Lang.Cancel);

        this.HtmlContent = list;
    }
}
