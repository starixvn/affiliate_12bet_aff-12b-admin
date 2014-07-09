using System;
using System.Data;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;

public partial class Media_ListText :IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string temp = TemplateHandler.LoadTemplate("Media/MainListText.htm");
        int numRows = 0;
        var deviceType = string.IsNullOrEmpty(Request["optiondevice"]) ? "0" : Request["optiondevice"];
        TemplateHandler.Assign(ref temp, "Category", Resources.Lang.Category);
        TemplateHandler.Assign(ref temp, "Content", Resources.Lang.Content);
        TemplateHandler.Assign(ref temp, "Language", Resources.Lang.Language);
        TemplateHandler.Assign(ref temp, "Currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref temp, "Action", Resources.Lang.Edit);
        TemplateHandler.Assign(ref temp, "Delete", Resources.Lang.Delete);

        TemplateHandler.Assign(ref temp, "File", Resources.Lang.File);
        TemplateHandler.Assign(ref temp, "Delete-val", Resources.Lang.Delete);

        TemplateHandler.Assign(ref temp, "AddBanner", Resources.Lang.AddBanner);
        TemplateHandler.Assign(ref temp, "BannerList", Resources.Lang.ListBanner);
        TemplateHandler.Assign(ref temp, "ConfirmDelete", Resources.Lang.ConfirmDelBanner);
        TemplateHandler.Assign(ref temp, "ErrFileExtension", Resources.Lang.errFileExtension);
        string cate = (Request["cateoption"] == null || Request["cateoption"].ToString() == "All") ? "" : Request["cateoption"].ToString();
        string lang = (Request["langoption"] == null || Request["langoption"].ToString() == "All") ? "" : Request["langoption"].ToString();
        string currency = (Request["currencyoption"] == null || Request["currencyoption"].ToString() == "All") ? "" : Request["currencyoption"].ToString();
        int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int currPage = CastAide.AsInt32(Request["page"], 1);

        DataView dv = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;

        TemplateHandler.Assign(ref temp, "deviceType", deviceType);
        TemplateHandler.Assign(ref temp, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref temp, "optiondevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("optiondevice", deviceType));

        TemplateHandler.Assign(ref temp, "optioncate", Common.GetDataOptionBanner(dv, "cateoption", "Category", cate, true));
        TemplateHandler.Assign(ref temp, "optionlang", Common.GetDataOptionBanner(dv, "langoption", "Language", lang, true));
        TemplateHandler.Assign(ref temp, "optioncurrency", Common.GetDataOptionBanner(dv, "currencyoption", "Currency", currency, true));

        int minNo = 0, maxNo = 0, totalBanner = 0, totalPage = 0;
        TemplateHandler.ReplaceBlock(ref temp, "Content", GenerateHTML.Text.GetListText(cate, lang, currency, currPage, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize, deviceType));
        if (totalBanner > 0)
        {
            TemplateHandler.Assign(ref temp, "paging-top", Common.GenPaging(totalPage, currPage, pageSize, totalBanner, "aPageTop", "txtPageTop", new string[] { "Delete", "Add" }, new string[] { "DeleteSelectedText();",
                                                                            "javascript:DoCal('AddText.aspx', 'Add Text', 460, 400);" }, 1200, true, true, true, false));
            TemplateHandler.Assign(ref temp, "paging-bottom", Common.GenPaging(totalPage, currPage, pageSize, totalBanner, "aPageBottom", "txtPageBottom", 1200, true, false));
        }
        else
        {
            TemplateHandler.Assign(ref temp, "paging-top", "");
            TemplateHandler.Assign(ref temp, "paging-bottom", "");
        }
        TemplateHandler.Assign(ref temp, "submenu", SubMenu.GenSubMenu(
            new string[] { "ListText.aspx", "ListBannerNew.aspx" },
            new string[] { "Text List", "Banner List" },
            0, totalBanner, Resources.Lang.MediaGallery, "i_media1.png"));
        TemplateHandler.Assign(ref temp, "minNo", minNo.ToString());
        TemplateHandler.Assign(ref temp, "maxNo", maxNo.ToString());
        TemplateHandler.Assign(ref temp, "page-val", currPage.ToString());
        TemplateHandler.Assign(ref temp, "Add-val", Resources.Lang.Add);
        this.HtmlContent = temp;  
    }
}
