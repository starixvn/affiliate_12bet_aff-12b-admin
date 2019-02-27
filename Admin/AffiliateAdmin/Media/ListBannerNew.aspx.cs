using System;
using System.Data;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;

public partial class ListMediaBanner : IBCPage
{

    protected void Page_Load(object sender, EventArgs e)
    {

        string temp = TemplateHandler.LoadTemplate("Media/MainListBannerNew.htm");
        int numRows = 0;
        TemplateHandler.Assign(ref temp, "SubCategory", Resources.Lang.SubCategory);
        TemplateHandler.Assign(ref temp, "Category", Resources.Lang.Category);
        TemplateHandler.Assign(ref temp, "Delete-val", Resources.Lang.Delete);
        TemplateHandler.Assign(ref temp, "Language", Resources.Lang.Language);
        TemplateHandler.Assign(ref temp, "Currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref temp, "Type", Resources.Lang.type);
        TemplateHandler.Assign(ref temp, "Size", Resources.Lang.Size);
        TemplateHandler.Assign(ref temp, "AddBanner", Resources.Lang.AddBanner);
        TemplateHandler.Assign(ref temp, "BannerList", Resources.Lang.ListBanner);
        TemplateHandler.Assign(ref temp, "ConfirmDelete", Resources.Lang.ConfirmDelBanner);
        TemplateHandler.Assign(ref temp, "ErrFileExtension", Resources.Lang.errFileExtension);
        string cate = (Request["cateoption"] == null || Request["cateoption"] == "All") ? "" : Request["cateoption"];
        bool hasSubCategory = false;
        string subCategory = string.Empty;
        bool isSearchByParentCategory = false;
        string paramCategory = cate;
        var deviceType = string.IsNullOrEmpty(Request["slDevice"]) ? "0" : Request["slDevice"];

        if (cate == "Casino" || cate == "Sportsbook")
        {
            hasSubCategory = true;
            if (string.IsNullOrEmpty(Request["slSubCategoryMedia"]) || Request["slSubCategoryMedia"] == "All")
            {
                isSearchByParentCategory = true;
                subCategory = string.Empty;
            }
            else
            {
                subCategory = Request["slSubCategoryMedia"];
                paramCategory = subCategory;
            }
        }

        string lang = (Request["langoption"] == null || Request["langoption"] == "All") ? "" : Request["langoption"];
        string imgtype = (Request["typeoption"] == null || Request["typeoption"] == "All") ? "" : Request["typeoption"];
        string size = (Request["sizeoption"] == null || Request["sizeoption"] == "All") ? "" : Request["sizeoption"];
        string currency = (Request["currencyoption"] == null || Request["currencyoption"] == "All") ? "" : Request["currencyoption"];

        int afftype = CastAide.AsInt32(Request["afftypeoption"], (int)AffType.Asian);
        int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int currPage = CastAide.AsInt32(Request["page"], 1);

        DataView dv = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;

        TemplateHandler.Assign(ref temp, "optioncate", Common.GetDataOptionBanner(dv, "cateoption", "CategoryMedia", cate, true));
        if (hasSubCategory)
            TemplateHandler.Assign(ref temp, "optionsubcate", Common.GetDataOptionBanner(dv, "slSubCategoryMedia", "CategoryMedia" + cate, subCategory, true));

        TemplateHandler.Assign(ref temp, "optionlang", Common.GetDataOptionBanner(dv, "langoption", "Language", lang, true));
        TemplateHandler.Assign(ref temp, "optioncurrency", Common.GetDataOptionBanner(dv, "currencyoption", "Currency", currency, true));
        TemplateHandler.Assign(ref temp, "optiontype", Common.GetDataOptionBanner(dv, "typeoption", "ImageType", imgtype, true));
        TemplateHandler.Assign(ref temp, "optionsize", Common.GetDataOptionBanner(dv, "sizeoption", "Size", size, true));

        DataTable dt = DbHelper.Execute("Aff_AffTypeSel", DBType.AFFILIATE_12BET).ResultSet.Tables[0];
        TemplateHandler.Assign(ref temp, "afftype", Resources.Lang.AffiliateType);
        var tempAffType = afftype.ToString();
        TemplateHandler.Assign(ref temp, "optionafftype", Common.GenDropDownFromTable(dt, "TypeId", "Name", "afftypeoption", ref tempAffType, false));

        TemplateHandler.Assign(ref temp, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref temp, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice", deviceType));

        TemplateHandler.Assign(ref temp, "Images", Resources.Lang.Images);
        TemplateHandler.Assign(ref temp, "Filename", Resources.Lang.filename);
        TemplateHandler.Assign(ref temp, "ImageType", Resources.Lang.ImageType);
        TemplateHandler.Assign(ref temp, "Action", Resources.Lang.Edit);
        TemplateHandler.Assign(ref temp, "Delete", Resources.Lang.Delete);
        int minNo = 0, maxNo = 0, totalBanner = 0, totalPage = 0;
        TemplateHandler.ReplaceBlock(ref temp, "Content", GenerateHTML.Banner.GetBannerList(
            paramCategory, isSearchByParentCategory, lang, imgtype, size, currency, currPage,
            ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize, Common.NormalBanner, afftype, deviceType));
        if (totalBanner > 0)
        {
            TemplateHandler.Assign(ref temp, "paging-top", Common.GenPaging(totalPage, currPage, pageSize, totalBanner, "aPageTop", "txtPageTop", new string[] { "Delete", "Add" }, new string[] { "DeleteSelectedMedia(0, 'true');",
                                                                            "javascript:DoCal('AddBanner.aspx', 'Add Banner', 460, 450);" }, 1200, true, true, true, false));
            TemplateHandler.Assign(ref temp, "paging-bottom", Common.GenPaging(totalPage, currPage, pageSize, totalBanner, "aPageBottom", "txtPageBottom", 1200, true, false));
        }
        else
        {
            TemplateHandler.Assign(ref temp, "paging-top", "");
            TemplateHandler.Assign(ref temp, "paging-bottom", "");
        }

        TemplateHandler.Assign(ref temp, "totalMedia-val", totalBanner);
        TemplateHandler.Assign(ref temp, "minNo", minNo.ToString());
        TemplateHandler.Assign(ref temp, "maxNo", maxNo.ToString());
        TemplateHandler.Assign(ref temp, "page-val", currPage.ToString());
        TemplateHandler.Assign(ref temp, "Add-val", Resources.Lang.Add);
        this.HtmlContent = temp;
    }
}
