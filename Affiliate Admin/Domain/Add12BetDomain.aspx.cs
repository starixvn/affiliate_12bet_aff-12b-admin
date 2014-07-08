using System;
using System.Web;
using DNAide.Web;

public partial class Add12BetDomain : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("Domain/Add12BetDomain.html");        
        TemplateHandler.Assign(ref list, "12BetDomain", Resources.Lang.Domain);
        TemplateHandler.Assign(ref list, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);        
        TemplateHandler.Assign(ref list, "submit", Resources.Lang.Add);
        TemplateHandler.Assign(ref list, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref list, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList());
        this.HtmlContent = list;
    }
}
