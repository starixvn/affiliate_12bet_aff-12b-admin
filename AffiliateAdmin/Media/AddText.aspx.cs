using System;
using System.Data;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;

public partial class Media_AddText : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-------------------------

        string temp = TemplateHandler.LoadTemplate("Media/MainAddText.htm");
        TemplateHandler.Assign(ref temp, "AddText", Resources.Lang.AddText);
        TemplateHandler.Assign(ref temp, "Text", Resources.Lang.Text);
        TemplateHandler.Assign(ref temp, "Category", Resources.Lang.Category);
        TemplateHandler.Assign(ref temp, "Language", Resources.Lang.Language);
        TemplateHandler.Assign(ref temp, "Currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref temp, "Type", Resources.Lang.type);
        TemplateHandler.Assign(ref temp, "ErrFileExtension", Resources.Lang.errFileExtension);

        DataView dv = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;

        TemplateHandler.Assign(ref temp, "optioncate", Common.GetDataOptionBanner(dv, "slCategory", "Category", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optionlang", Common.GetDataOptionBanner(dv, "slLanguage", "Language", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optioncurrency", Common.GetDataOptionBanner(dv, "slCurrency", "Currency", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optiontype",Resources.Lang.Text );

        TemplateHandler.Assign(ref temp, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref temp, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice"));

        this.HtmlContent = temp;
    }
}
