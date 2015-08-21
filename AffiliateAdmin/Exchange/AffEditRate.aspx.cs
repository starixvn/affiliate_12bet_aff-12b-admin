using System;
using DNAide.Web;
using DNAide.Core;

public partial class AffEditRate : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Exchange/AffEditRate.htm");
        TemplateHandler.Assign(ref strTpl, "exrate", Resources.Lang.ExchangeRate);
        TemplateHandler.Assign(ref strTpl, "curr", Resources.Lang.Currency);
        TemplateHandler.Assign(ref strTpl, "rate", Request["column"]== "buyingrate" ? Resources.Lang.BuyingRate : Resources.Lang.Rate);
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "column", Request["column"]);
        try
        {
            if (string.IsNullOrEmpty(Request["exid"]) || string.IsNullOrEmpty(Request["curr"]) || string.IsNullOrEmpty(Request["rate"]))
                this.HtmlContent = "Invalid information";
            int exId = CastAide.AsInt32(Request["exid"], -1);
            TemplateHandler.Assign(ref strTpl, "curr-val", Request["curr"]);
            TemplateHandler.Assign(ref strTpl, "rate-val", Request["rate"]);
            TemplateHandler.Assign(ref strTpl, "exid-val", exId.ToString());
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
#if DEBUG
                throw;
#endif
        }
        this.HtmlContent = strTpl;
    }
}
