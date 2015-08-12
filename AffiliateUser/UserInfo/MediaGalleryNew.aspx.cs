namespace AffiliateUser
{
    using System;
    using System.Data;
    using System.Text;
    using DNAide.Core;
    using DNAide.Web;
    using Resources;

    public partial class MediaGallery : RootPageLogIn
    {
        public string htmlContent = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppSettings.RefreshPreferredLanguage();
            htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaBanner.htm");
            /*Request to re-enable media gallerry again 20100917 by Peter */
            /* if (string.Compare(AppSettings.Session.PreferLang, "zh-CN", true) == 0 || string.Compare(AppSettings.Session.PreferLang, "zh-TW", true) == 0)
                 TemplateHandler.RemoveBlock(ref htmlContent, "listbanner");*/
            TemplateHandler.Assign(ref htmlContent, "SubCategory", Resources.Lang.SubCategory);
            TemplateHandler.Assign(ref htmlContent, "Category", Resources.Lang.Category);
            TemplateHandler.Assign(ref htmlContent, "Language", Resources.Lang.Language);
            TemplateHandler.Assign(ref htmlContent, "ImageType", Resources.Lang.ImageType);
            TemplateHandler.Assign(ref htmlContent, "Size", Resources.Lang.Size);
            TemplateHandler.Assign(ref htmlContent, "Currency", Resources.Lang.Currency);
            TemplateHandler.Assign(ref htmlContent, "MediaGallery", Resources.Lang.Media);
            TemplateHandler.Assign(ref htmlContent, "Search", Resources.Lang.Search);
            TemplateHandler.Assign(ref htmlContent, "TextLink", Resources.Lang.TextLink);
            TemplateHandler.Assign(ref htmlContent, "Banner", Resources.Lang.Banner);
            TemplateHandler.Assign(ref htmlContent, "TrackingCode", Resources.Lang.TrackingCode);
            TemplateHandler.Assign(ref htmlContent, "activatedTrackingCode", Resources.Lang.ActivatedTrackingCode);
            TemplateHandler.Assign(ref htmlContent, "newTrackingCode", Resources.Lang.NewTrackingCode);
            TemplateHandler.Assign(ref htmlContent, "GenerateLink", Resources.Lang.GenerateLink);

            TemplateHandler.Assign(ref htmlContent, "AvailableDomains", Resources.Lang.AvailableDomains);
            TemplateHandler.Assign(ref htmlContent, "LinkToLabel", Resources.Lang.LinkTo);

            var deviceType = Common.GetDeviceType(Request["slDevice"]);
            string targetRequest = GetTargetRequest(Request["target"], deviceType);

            string domain = string.IsNullOrEmpty(Request["slDomain"]) ? null : Request["slDomain"];
            DataTable dtDomain = DataHandler.MediaGallery.SelAffDomains(deviceType);
            TemplateHandler.Assign(ref htmlContent, "slDomain", Common.GenDropDownFromTable(dtDomain, "DomainName", "DomainName", "slDomain", ref domain, false));

            TemplateHandler.Assign(ref htmlContent, "target", Common.GenListLink2Page(targetRequest, deviceType));

            #region Get request information

            int pageIndex = CastAide.AsInt32(Request["page"], 1);
            int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int totalpage = 0;

            string category = string.IsNullOrEmpty(Request["slCategory"]) ? null : Request["slCategory"];
            bool hasSubCategory = false;
            string subCategory = string.Empty;
            bool isSearchByParentCategory = false;
            string paramCategory = category;
            if (category == "Casino" || category == "Sportsbook")
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

            string lang = Request["slLanguage"] == null ? Common.GetPreferLanguageName() : Request["slLanguage"];
            string currency = string.IsNullOrEmpty(Request["slCurrency"]) ? null : Request["slCurrency"];
            string type = string.IsNullOrEmpty(Request["slImageType"]) ? null : Request["slImageType"];
            string size = string.IsNullOrEmpty(Request["slSize"]) ? null : Request["slSize"];
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

            #endregion Get request information

            TemplateHandler.Assign(ref htmlContent, "slSubCategory", "");
            TemplateHandler.Assign(ref htmlContent, "slTrackingCode", GenerateHTML.TrackingSystem.GetTrackingCodeList(false, trackingcode));
            DataView dv = CacheUtil.GetBannerTypes().DefaultView;
            TemplateHandler.Assign(ref htmlContent, "slCategory", this.GetDataOptionBanner(dv, "slCategory", "CategoryMedia", category, true));
            if (hasSubCategory)
                //    TemplateHandler.Assign(ref htmlContent, "slSubCategory", this.GetDataOptionBanner(dv, "slSubCategoryMedia", "CategoryMedia" + category, subCategory, true));
                TemplateHandler.Assign(ref htmlContent, "currentSubCategory", subCategory);
            else
                TemplateHandler.Assign(ref htmlContent, "currentSubCategory", "none");

            TemplateHandler.Assign(ref htmlContent, "slLanguage", this.GetDataOptionBanner(dv, "slLanguage", "Language", lang, true));
            TemplateHandler.Assign(ref htmlContent, "slCurrency", this.GetDataOptionBanner(dv, "slCurrency", "Currency", currency, true));
            TemplateHandler.Assign(ref htmlContent, "slImageType", this.GetDataOptionBanner(dv, "slImageType", "ImageType", type, true));
            TemplateHandler.Assign(ref htmlContent, "slSize", this.GetDataOptionBanner(dv, "slSize", "Size", size, true));

            TemplateHandler.Assign(ref htmlContent, "Device", Resources.Lang.Device);
            TemplateHandler.Assign(ref htmlContent, "DeviceType-val", deviceType);
            TemplateHandler.Assign(ref htmlContent, "slDevice", Common.GenDeviceList("slDevice", deviceType));

            /* Generate TestLinkList */
            dv.RowFilter = "type='Language'";
            DataTable dt = dv.ToTable();

            if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(targetRequest))
            {
                TemplateHandler.Assign(ref htmlContent, "language_val", string.Empty);
                TemplateHandler.Assign(ref htmlContent, "link_val", Lang.NoInfo);
                TemplateHandler.ReplaceBlock(ref htmlContent, "bannerlist", string.Empty);

                return;
            }

            TemplateHandler.ReplaceBlock(ref htmlContent, "TextLinkList",
                                            GenerateHTML.Banner.GetTextLink(domain, trackingcode, dt, targetRequest, deviceType, lang));

            TemplateHandler.ReplaceBlock(ref htmlContent, "Content",
                                            GenerateHTML.Banner.GetBannerList(paramCategory, isSearchByParentCategory, lang,
                                                                            type, size,
                                                                            currency, pageIndex, pageSize, ref totalpage,
                                                                            domain, trackingcode, targetRequest,
                                                                            Common.NormalBanner,
                                                                            (int)AppSettings.Session.AffType, deviceType));
            TemplateHandler.Assign(ref htmlContent, "PageSize-val", pageSize);
            TemplateHandler.Assign(ref htmlContent, "page", pageIndex);
            /* Generate Banner paging*/
            if (totalpage > 0)
            {
                TemplateHandler.Assign(ref htmlContent, "paging-top",
                                        Common.GenPaging(totalpage, pageIndex, pageSize, 0, "aPageTop", "txtPageTop",
                                                        null, null, 0, false, true, false, false));
                TemplateHandler.Assign(ref htmlContent, "paging-bottom",
                                        Common.GenPaging(totalpage, pageIndex, pageSize, 0, "aPageBottom",
                                                        "txtPageBottom", null, null, 0, false, false, false, false));
            }
            else
            {
                TemplateHandler.Assign(ref htmlContent, "paging-top", string.Empty);
                TemplateHandler.Assign(ref htmlContent, "paging-bottom", string.Empty);
            }
        }

        private string GetTargetRequest(string target, string deviceType)
        {
            if (!string.IsNullOrEmpty(target))
                return target;

            if (deviceType.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                return "home";
            }

            return "sportsbook";
        }

        /// <summary>
        /// Common function to generate dropdown list in media gallery page by Peter 20090328
        /// </summary>
        /// <param name="dv">DataView of returned DataTable</param>
        /// <param name="type">type in ("Category","Language","Currency","Type","Size"))</param>
        /// <param name="activeValue">current selected value in the dropdownlist. string.Empty mean All</param>
        /// <returns>string for dropdownlist</returns>
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
}