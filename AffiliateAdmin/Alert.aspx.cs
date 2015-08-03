using System;
using DNAide.Web;
using DNAide.Core;

public partial class Alert : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string strContent = TemplateHandler.LoadTemplate("Alert.htm");
            int intAlertCode = CastAide.AsInt32(Request["code"]);
            string strLink = (string.IsNullOrEmpty(Request["link"]) ? "Listbanner.aspx" : Request["link"]);
            if (intAlertCode > 1 && intAlertCode < 5)
            {
                TemplateHandler.Assign(ref strContent, "user","The " +  Request["user"]);
            }
            else if (intAlertCode == 5)
            {
                TemplateHandler.Assign(ref strContent, "user", string.Empty);
            }
            else
            {
                TemplateHandler.Assign(ref strContent, "user", Request["user"]);
            }
            TemplateHandler.Assign(ref strContent, "message", Common.GetAlertMsg(intAlertCode));
            TemplateHandler.Assign(ref strContent, "link", strLink);
            TemplateHandler.Assign(ref strContent, "clickhere", Resources.Lang.ClickHere);
            TemplateHandler.Assign(ref strContent, "redirect", Resources.Lang.Redirect);
            this.HtmlContent = strContent;
        }
        catch (Exception ex)
        {
            DNAideLog.Write(ex.Message);
        }
    }
}
