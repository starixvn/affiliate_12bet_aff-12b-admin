using System;
using System.Data;
using DNAide.Web;
using DNAide.Data;

public partial class AddBanner : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //-------------------------

        string temp = TemplateHandler.LoadTemplate("Media/MainAddBannerOld.htm");
        //TemplateHandler.Assign(ref temp, "submenu", Common.GenSubMenu(new string[] { "ListBanner.aspx", "" }, new string[] { Resources.Lang.ListBanner, Resources.Lang.AddBanner }, 1000, 1));
        TemplateHandler.Assign(ref temp, "AddBanner", Resources.Lang.AddBanner);
        TemplateHandler.Assign(ref temp, "BannerList", Resources.Lang.ListBanner);
        TemplateHandler.Assign(ref temp, "File", Resources.Lang.File);
        TemplateHandler.Assign(ref temp, "Category", Resources.Lang.Category);
        TemplateHandler.Assign(ref temp, "Language", Resources.Lang.Language);
        TemplateHandler.Assign(ref temp, "Currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref temp, "Type", Resources.Lang.type);
        TemplateHandler.Assign(ref temp, "Size", Resources.Lang.Size);
        TemplateHandler.Assign(ref temp, "ErrFileExtension", Resources.Lang.errFileExtension);

        DataView dv = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;

        TemplateHandler.Assign(ref temp, "optioncate", Common.GetDataOptionBanner(dv, "slCategory", "Category", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optionlang", Common.GetDataOptionBanner(dv, "slLanguage", "Language", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optioncurrency", Common.GetDataOptionBanner(dv, "slCurrency", "Currency", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optiontype", Common.GetDataOptionBanner(dv, "slImageType", "ImageType", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optionsize", Common.GetDataOptionBanner(dv, "slSize", "Size", string.Empty, false));


        this.HtmlContent = temp;

    }
}
