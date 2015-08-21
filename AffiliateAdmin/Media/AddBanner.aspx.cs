using System;
using System.Data;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;

public partial class AddBanner : IBCPage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string temp = TemplateHandler.LoadTemplate("Media/MainAddBanner.htm");
        TemplateHandler.Assign(ref temp, "formAction", "../Handler/AddNewBannerV2.ashx");
        TemplateHandler.Assign(ref temp, "FileName", Resources.Lang.filename);
        TemplateHandler.Assign(ref temp, "AddBanner", Resources.Lang.AddBanner);
        TemplateHandler.Assign(ref temp, "BannerList", Resources.Lang.ListBanner);
        TemplateHandler.Assign(ref temp, "File", Resources.Lang.File);
        TemplateHandler.Assign(ref temp, "Category", Resources.Lang.Category);
        TemplateHandler.Assign(ref temp, "SubCategory", "Sub Category");
        TemplateHandler.Assign(ref temp, "Language", Resources.Lang.Language);
        TemplateHandler.Assign(ref temp, "Currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref temp, "Type", Resources.Lang.type);
        TemplateHandler.Assign(ref temp, "Size", Resources.Lang.Size);
        TemplateHandler.Assign(ref temp, "AffType", Resources.Lang.AffiliateType);
        TemplateHandler.Assign(ref temp, "ErrFileExtension", Resources.Lang.errFileExtension);

        DataView dv = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;

        TemplateHandler.Assign(ref temp, "optioncate", Common.GetDataOptionBanner(dv, "slCategory", "CategoryMedia", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optionlang", Common.GetDataOptionBanner(dv, "slLanguage", "Language", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optioncurrency", Common.GetDataOptionBanner(dv, "slCurrency", "Currency", string.Empty, false));
        TemplateHandler.Assign(ref temp, "optiontype", Common.GetDataOptionBanner(dv, "slImageType", "ImageType", string.Empty, false));
        TemplateHandler.Assign(ref temp, "filenamevalue", string.Empty);


        TemplateHandler.Assign(ref temp, "afftype", Resources.Lang.AffiliateType);
        DataTable dt = DbHelper.Execute("Aff_AffTypeSel", DBType.AFFILIATE_12BET).ResultSet.Tables[0];
        var afftype = string.Empty;
        TemplateHandler.Assign(ref temp, "optionafftype", Common.GenDropDownFromTable(dt, "TypeId", "Name", "slAffType", ref afftype, false));

        TemplateHandler.Assign(ref temp, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref temp, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice"));

        this.HtmlContent = temp;
        
    }
}
