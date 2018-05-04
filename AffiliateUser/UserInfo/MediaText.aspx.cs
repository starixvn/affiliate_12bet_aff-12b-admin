using System;
using System.Data;
using System.Linq;
using System.Text;
using AffiliateUser.Helpers;
using DNAide.Core;
using DNAide.Web;
using Resources;

public partial class UserInfo_MediaText : RootPageLogIn
{
    public string htmlContent = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        AppSettings.RefreshPreferredLanguage();
        htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaText.htm");
        TemplateHandler.Assign(ref htmlContent, "Category", Resources.Lang.Category);
        TemplateHandler.Assign(ref htmlContent, "Language", Resources.Lang.Language);
        TemplateHandler.Assign(ref htmlContent, "Currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref htmlContent, "MediaGallery", Resources.Lang.Text);
        TemplateHandler.Assign(ref htmlContent, "Search", Resources.Lang.Search);
        TemplateHandler.Assign(ref htmlContent, "TextLink", Resources.Lang.TextLink);
        TemplateHandler.Assign(ref htmlContent, "Banner", Resources.Lang.Banner);
        TemplateHandler.Assign(ref htmlContent, "TrackingCode", Resources.Lang.TrackingCode);
        TemplateHandler.Assign(ref htmlContent, "activatedTrackingCode", Resources.Lang.ActivatedTrackingCode);
        TemplateHandler.Assign(ref htmlContent, "newTrackingCode", Resources.Lang.NewTrackingCode);
        TemplateHandler.Assign(ref htmlContent, "GenerateLink", Resources.Lang.GenerateLink);

        TemplateHandler.Assign(ref htmlContent, "AvailableDomains", Resources.Lang.AvailableDomains);
        TemplateHandler.Assign(ref htmlContent, "LinkToLabel", Resources.Lang.LinkTo);

        /*Johnny:target*/
        string selectedPage = string.IsNullOrEmpty(Request["target"]) ? TargetPageHelper.TargetPageList.First() : Request["target"];

        string domain = string.IsNullOrEmpty(Request["slDomain"]) ? null : Request["slDomain"];
        DataTable dtDomain = DataHandler.MediaGallery.SelAffDomains();
        TemplateHandler.Assign(ref htmlContent, "slDomain", Common.GenDropDownFromTable(dtDomain, "DomainID", "DomainName", "slDomain", ref domain, false));

        TemplateHandler.Assign(ref htmlContent, "target", TargetPageHelper.GenerateDropdown("target", selectedPage));

        var deviceType = Common.GetDeviceType(Request["slDevice"]);
        TemplateHandler.Assign(ref htmlContent, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref htmlContent, "DeviceType-val", deviceType);
        TemplateHandler.Assign(ref htmlContent, "slDevice", Common.GenDeviceList("slDevice", deviceType));

        #region Get request information

        int pageIndex = CastAide.AsInt32(Request["page"], 1);
        int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int totalpage = 0;

        string category = string.IsNullOrEmpty(Request["slCategory"]) ? null : Request["slCategory"];
        string lang = Request["slLanguage"] == null ? Common.GetPreferLanguageName() : Request["slLanguage"];
        string currency = string.IsNullOrEmpty(Request["slCurrency"]) ? null : Request["slCurrency"];
        string trackOption = string.IsNullOrEmpty(Request["hdTrack"]) ? null : Request["hdTrack"];
        string trackingcode = string.IsNullOrEmpty(Request["slTrackingCode"]) ? "" : Request["slTrackingCode"];


        if (string.Compare(trackOption, "1", true) != 0)
        {
            TemplateHandler.Assign(ref htmlContent, "achecked", "checked");
            TemplateHandler.Assign(ref htmlContent, "nchecked", string.Empty);
            TemplateHandler.Assign(ref htmlContent, "track-val", string.Empty);
        }
        else
        {
            trackingcode = string.IsNullOrEmpty(Request["txtNewTrackingCode"]) ? "" : Request["txtNewTrackingCode"];
            TemplateHandler.Assign(ref htmlContent, "nchecked", "checked");
            TemplateHandler.Assign(ref htmlContent, "achecked", string.Empty);
            TemplateHandler.Assign(ref htmlContent, "track-val", trackingcode);
        }

        #endregion

        TemplateHandler.Assign(ref htmlContent, "slTrackingCode", GenerateHTML.TrackingSystem.GetTrackingCodeList(false, trackingcode));
        DataView dv = CacheUtil.GetBannerTypes().DefaultView;
        TemplateHandler.Assign(ref htmlContent, "slCategory", this.GetDataOptionBanner(dv, "slCategory", "Category", category, true));
        TemplateHandler.Assign(ref htmlContent, "slLanguage", this.GetDataOptionBanner(dv, "slLanguage", "Language", lang, true));
        TemplateHandler.Assign(ref htmlContent, "slCurrency", this.GetDataOptionBanner(dv, "slCurrency", "Currency", currency, true));

        /* Generate TestLinkList */
        dv.RowFilter = "type='Language'";
        DataTable dt = dv.ToTable();
        var selectedDomain = dtDomain.AsEnumerable().FirstOrDefault(row => domain.Equals(row["DomainID"].ToString()));

        if (string.IsNullOrEmpty(domain) || selectedDomain == null)
        {
            TemplateHandler.Assign(ref htmlContent, "language_val", string.Empty);
            TemplateHandler.Assign(ref htmlContent, "link_val", Lang.NoInfo);
            TemplateHandler.ReplaceBlock(ref htmlContent, "bannerlist", string.Empty);

            return;
        }

        TemplateHandler.ReplaceBlock(ref htmlContent, "TextLinkList", GenerateHTML.Banner.GetTextLink(selectedDomain, trackingcode, dt, selectedPage, lang));

        TemplateHandler.ReplaceBlock(ref htmlContent, "Content", GenerateHTML.Text.GetTextList(category, lang, currency, pageIndex, pageSize, ref totalpage, selectedDomain, trackingcode, selectedPage, deviceType));
        TemplateHandler.Assign(ref htmlContent, "PageSize-val", pageSize);
        TemplateHandler.Assign(ref htmlContent, "page", pageIndex);
        /* Generate Banner paging*/
        if (totalpage > 0)
        {
            TemplateHandler.Assign(ref htmlContent, "paging-top", Common.GenPaging(totalpage, pageIndex, pageSize, 0, "aPageTop", "txtPageTop", null, null, 0, false, true, false, false));
            TemplateHandler.Assign(ref htmlContent, "paging-bottom", Common.GenPaging(totalpage, pageIndex, pageSize, 0, "aPageBottom", "txtPageBottom", null, null, 0, false, false, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref htmlContent, "paging-top", string.Empty);
            TemplateHandler.Assign(ref htmlContent, "paging-bottom", string.Empty);
        }
    }

    private string GetDataOptionBanner(DataView dv, string id, string type, string activeValue, bool isGenAll)
    {
        dv.RowFilter = "type='" + type + "'";
        StringBuilder result = new StringBuilder();
        string opt = TemplateHandler.LoadTemplate("Option.htm");
        TemplateHandler.Assign(ref opt, "id", id);
        string temp = TemplateHandler.CopyBlock(opt, "optionList");
        string tempopt = temp;
        if (isGenAll)
        {
            TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
            TemplateHandler.Assign(ref tempopt, "Optname", Resources.Lang.All);
            if (string.IsNullOrEmpty(activeValue))
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            result.Append(tempopt);
        }
        DataTable dt = dv.ToTable();
        foreach (DataRow dr in dt.Rows)
        {
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", dr["value"].ToString());
            TemplateHandler.Assign(ref tempopt, "Optname", GenerateHTML.Banner.GetBannerLang(dr["value"].ToString()));

            if (string.Compare(activeValue, dr["value"].ToString(), true) == 0)
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            else
            {
                TemplateHandler.Assign(ref tempopt, "selected", string.Empty);
            }
            result.Append(tempopt);
        }
        TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
        return opt;
    }
}
