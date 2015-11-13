using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DNAide.Core;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Web;
using GenerateHTML;

public partial class UserInfo_MediaStatistics_LandingPage : IBCPage
{
    private string strTpl = string.Empty;
    private StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics_LandingPage.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    private void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
    }

    private void GenerateHTML()
    {
        try
        {
            #region Generate select list

            string sportCode = CastAide.AsString(Request["sportCode"]);
            string languageCode = CastAide.AsString(Request["languageCode"]);
            var deviceType = Common.GetDeviceType(Request["slDevice"]);

            TemplateHandler.Assign(ref strTpl, "slPage", Common.GeneratePagesDropdown(sportCode, deviceType));

            DataView dv = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;
            TemplateHandler.Assign(ref strTpl, "slLanguage", Common.GetDataOptionBanner(dv, "languageCode", "Language", languageCode, true));

            #endregion Generate select list

            DateTime now = DateTime.Now;

            DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], new DateTime(now.Year, now.Month, 1));
            DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
            

            TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));

            TemplateHandler.Assign(ref strTpl, "Device", Resources.Lang.Device);
            TemplateHandler.Assign(ref strTpl, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice", deviceType));

            DbResult rs = DataHandler.TrackingSystem.SelClickStatisticsByPage(dtmFDate, dtmTDate, sportCode, GetLangCode(languageCode), deviceType);
            int totalSignups = CastAide.AsInt32(rs.Outputs["totalsignups"]);
            int totalDeposits = CastAide.AsInt32(rs.Outputs["totalfdms"]);

            DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            if (dtb.Rows.Count > 0)
            {
                int intCount = 0;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                string strListAffId = string.Empty;

                var strListRow = new StringBuilder();

                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;

                    intCount++;

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);

                    TemplateHandler.Assign(ref strTmp, "Page-val", dr["sportcode"]);
                    TemplateHandler.Assign(ref strTmp, "LangCode-val", dr["languagecode"]);
                    TemplateHandler.Assign(ref strTmp, "Language-val", GetLanguageOriginal(dr["languagecode"].ToString()));
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", CastAide.AsInt32(dr["totalclick"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", CastAide.AsInt32(dr["uniqueclick"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "Signups-val", CastAide.AsInt32(dr["Signups"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "FDM-val", CastAide.AsInt32(dr["FDM"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "DeviceType-val", deviceType);

                    strListRow.Append(strTmp);
                }

                if (!deviceType.Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    TemplateHandler.Assign(ref strRowTpl, "color", (intCount % 2 == 0) ? Common.RowBgOdd : Common.RowBgEven);
                    //Assign total signups for Old URL
                    strListRow.Append(TrackingSystem.GenRowOldURL(strRowTpl, totalSignups, totalDeposits, dtmFDate, dtmTDate));
                }

                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(4));
            }
            //Assign Paging template
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 8, 0, SubMenu.AffApprovedText[7], "i_report.png"));
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
#if DEBUG
                throw;
#endif
        }
    }

    private string GetLanguageOriginal(string strLangCode)
    {
        switch (strLangCode.ToLower())
        {
            case "en": return "English";
            case "eneuro": return "English_Euro";
            case "ch": return "Chinese Traditional";
            case "cs": return "Chinese Simplified";
            case "th": return "Thai";
            case "es": return "Spanish";
            case "vn": return "Tiếng Việt";
            case "id": return "Indonesian";
            case "ko": return "Korean";
            case "jp": return "Japanese";
            default:
                return "";
        }
    }

    private string GetLangCode(string value)
    {
        switch (value.ToLower())
        {
            case "english": return "en";
            case "english (europe)": return "eneuro";
            case "chinese traditional": return "ch";
            case "chinese simplified": return "cs";
            case "thai": return "th";
            case "spanish": return "es";
            case "vietnamese": return "vn";
            case "indonesian": return "id";
            case "korean": return "ko";
            default:
                return "";
        }
    }
}