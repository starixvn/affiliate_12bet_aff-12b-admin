using System;
using System.Data;
using DNAide.Web;
using DNAide.Data;

public partial class AddBanner : IBCPage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //-------------------------
       int bannerID = Convert.ToInt32(Context.Request["editBannerID"].Trim());
        DbResult result = DataHandler.Banner.GetBannerDetail(bannerID);
        if (result.ResultSet.Tables[0].Rows.Count > 0)
        {
            string temp = TemplateHandler.LoadTemplate("Media/MainAddBanner.htm");
            //TemplateHandler.Assign(ref temp, "submenu", Common.GenSubMenu(new string[] { "ListBanner.aspx", "" }, new string[] { Resources.Lang.ListBanner, Resources.Lang.AddBanner }, 1000, 1));
            TemplateHandler.Assign(ref temp, "formAction", "../Handler/UpBanner.ashx");
            TemplateHandler.Assign(ref temp, "AddBanner", Resources.Lang.AddBanner);
            TemplateHandler.Assign(ref temp, "BannerList", Resources.Lang.ListBanner);
            TemplateHandler.Assign(ref temp, "File", Resources.Lang.File);
            TemplateHandler.Assign(ref temp, "Category", Resources.Lang.Category);
            TemplateHandler.Assign(ref temp, "Language", Resources.Lang.Language);
            TemplateHandler.Assign(ref temp, "Currency", Resources.Lang.Currency);
            TemplateHandler.Assign(ref temp, "Type", Resources.Lang.type);
            TemplateHandler.Assign(ref temp, "ErrFileExtension", Resources.Lang.errFileExtension);
            
            DataRow dr = result.ResultSet.Tables[0].Rows[0];
            DataView dv = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;

            TemplateHandler.Assign(ref temp, "optioncate", Common.GetDataOptionBanner(dv, "slCategory", "Category", dr["category"].ToString(), false));
            TemplateHandler.Assign(ref temp, "optionlang", Common.GetDataOptionBanner(dv, "slLanguage", "Language", dr["language"].ToString(), false));
            TemplateHandler.Assign(ref temp, "optioncurrency", Common.GetDataOptionBanner(dv, "slCurrency", "Currency", dr["currency"].ToString(), false));
            TemplateHandler.Assign(ref temp, "optiontype", Common.GetDataOptionBanner(dv, "slImageType", "ImageType", dr["imagetype"].ToString(), false));
            TemplateHandler.Assign(ref temp, "bannerid", dr["bannerid"].ToString());
            TemplateHandler.Assign(ref temp, "filenamevalue", dr["filename"].ToString());

            this.HtmlContent = temp;
        }
    }
}
