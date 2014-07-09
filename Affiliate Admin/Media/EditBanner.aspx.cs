using System;
using System.Data;
using DNAide.Web;
using DNAide.Data;

public partial class AddBanner : IBCPage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int bannerID = Convert.ToInt32(Context.Request["editBannerID"].Trim());
        var deviceType = Request["deviceType"];
        //Response.Redirect("EditMediaBanner.aspx?editBannerID=" + bannerID.ToString());
        DbResult result = DataHandler.Banner.GetBannerDetail(bannerID);
        if (result.ResultSet.Tables[0].Rows.Count > 0)
        {
            string temp = TemplateHandler.LoadTemplate("Media/MainAddBanner.htm");
            TemplateHandler.Assign(ref temp, "formAction", "../Handler/UpBanner.ashx");
            TemplateHandler.Assign(ref temp, "FileName", Resources.Lang.filename);
            TemplateHandler.Assign(ref temp, "AddBanner", Resources.Lang.AddBanner);
            TemplateHandler.Assign(ref temp, "BannerList", Resources.Lang.ListBanner);
            TemplateHandler.Assign(ref temp, "File", Resources.Lang.File);
            TemplateHandler.Assign(ref temp, "Category", Resources.Lang.Category);
            TemplateHandler.Assign(ref temp, "SubCategory", Resources.Lang.SubCategory);
            TemplateHandler.Assign(ref temp, "Language", Resources.Lang.Language);
            TemplateHandler.Assign(ref temp, "Currency", Resources.Lang.Currency);
            TemplateHandler.Assign(ref temp, "Type", Resources.Lang.type);
            TemplateHandler.Assign(ref temp, "ErrFileExtension", Resources.Lang.errFileExtension);

            DataRow dr = result.ResultSet.Tables[0].Rows[0];
            DataView dv = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;

            string mediaCategory = dr["category"].ToString();
            string category = string.Empty;
            if (Common.CheckHaveParentCategory(mediaCategory, out category))
            {
                    TemplateHandler.Assign(ref temp, "optioncate", Common.GetDataOptionBanner(dv, "slCategory", "CategoryMedia", category, false));
                    TemplateHandler.Assign(ref temp, "subCategory", mediaCategory);
            }
            else
            {
                TemplateHandler.Assign(ref temp, "optioncate", Common.GetDataOptionBanner(dv, "slCategory", "CategoryMedia", mediaCategory, false));
                TemplateHandler.Assign(ref temp, "subCategory", "None");
            }

            TemplateHandler.Assign(ref temp, "optionlang", Common.GetDataOptionBanner(dv, "slLanguage", "Language", dr["language"].ToString(), false));
            TemplateHandler.Assign(ref temp, "optioncurrency", Common.GetDataOptionBanner(dv, "slCurrency", "Currency", dr["currency"].ToString(), false));
            TemplateHandler.Assign(ref temp, "optiontype", Common.GetDataOptionBanner(dv, "slImageType", "ImageType", dr["imagetype"].ToString(), false));

            TemplateHandler.Assign(ref temp, "AffType", Resources.Lang.AffiliateType);
            DataTable dt = DbHelper.Execute("Aff_AffTypeSel", DBType.AFFILIATE_12BET).ResultSet.Tables[0];
            var afftype = dr["afftype"].ToString();
            TemplateHandler.Assign(ref temp, "optionafftype", Common.GenDropDownFromTable(dt, "TypeId", "Name", "slAffType", ref afftype, false));

            TemplateHandler.Assign(ref temp, "Device", Resources.Lang.Device);
            TemplateHandler.Assign(ref temp, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice", deviceType));

            TemplateHandler.Assign(ref temp, "bannerid", dr["bannerid"].ToString());
            TemplateHandler.Assign(ref temp, "oldfilenamevalue", dr["filename"].ToString());
            string fileNameValue = dr["filename"].ToString();
            int idxTemp = fileNameValue.LastIndexOf("/") + 1;
            fileNameValue = fileNameValue.Substring(idxTemp, fileNameValue.Length - idxTemp);
            TemplateHandler.Assign(ref temp, "filenamevalue", fileNameValue);
            TemplateHandler.Assign(ref temp, "size", dr["size"].ToString());
            TemplateHandler.Assign(ref temp, "oldCategory", mediaCategory);

            this.HtmlContent = temp;
        }
    }
}
