using System;
using DNAide.Web;
public partial class AccessDenied : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.HtmlContent = TemplateHandler.LoadTemplate("AccessDenied.html");
    }
}
