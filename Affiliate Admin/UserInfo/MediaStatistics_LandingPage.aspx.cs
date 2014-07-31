using System.Text;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;
using System.Collections;
using GenerateHTML;

public partial class UserInfo_MediaStatistics_LandingPage : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics_LandingPage.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
    }

    void GenerateHTML()
    {

        try
        {
            #region Generate select list
            string sportCode = CastAide.AsString(Request["sportCode"]);
            string languageCode = CastAide.AsString(Request["languageCode"]);
            ArrayList arrTarget = new ArrayList();            
            arrTarget.Add("All");
            arrTarget.Add("sportsbook");
            arrTarget.Add("casino");
            arrTarget.Add("signup");
            arrTarget.Add("promotion");

            ArrayList arrValue = new ArrayList();
            arrValue.Add("");
            arrValue.Add("sportsbook");
            arrValue.Add("casino");
            arrValue.Add("signup");
            arrValue.Add("promotion");

            if (AppSettings.Session.IsCasino)
            {
                arrTarget.Add("12betcasino");
                arrValue.Add("12betcasino");
            }

            TemplateHandler.Assign(ref strTpl, "slPage", Common.GenerateSelectObj("sportCode", arrValue, arrTarget, sportCode));

            DataView dv = DbHelper.Execute("Aff_BannerSelType", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;
            TemplateHandler.Assign(ref strTpl, "slLanguage", Common.GetDataOptionBanner(dv, "languageCode", "Language", languageCode, true));
            #endregion
            DateTime now = DateTime.Now;
            
            DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], new DateTime(now.Year, now.Month, 1));
            DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
            var deviceType = Common.GetDeviceType(Request["slDevice"]);

            TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));

            TemplateHandler.Assign(ref strTpl, "Device", Resources.Lang.Device);
            TemplateHandler.Assign(ref strTpl, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice", deviceType));

            DbResult rs = DataHandler.TrackingSystem.SelClickStatisticsByPage(dtmFDate, dtmTDate, sportCode, GetLangCode(languageCode), deviceType);

            DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            if (dtb.Rows.Count > 0)
            {

                int intCount = 0;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                string strListAffId = string.Empty;

                StringBuilder strListRow = new StringBuilder();
                int totalSignups = 0, totalDeposits = 0;
                //Hashtable hsTotal = new Hashtable();
                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;

                    intCount++;
                    totalSignups += CastAide.AsInt32(dr["Signups"]);
                    totalDeposits += CastAide.AsInt32(dr["FDM"]);

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);

                    TemplateHandler.Assign(ref strTmp, "Page-val", dr["sportcode"]);
                    TemplateHandler.Assign(ref strTmp, "LangCode-val", dr["languagecode"]);
                    TemplateHandler.Assign(ref strTmp, "Language-val", GetLanguageOriginal(dr["languagecode"].ToString()));
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                    TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());
                    TemplateHandler.Assign(ref strTmp, "DeviceType-val", deviceType);

                    strListRow.Append(strTmp);
                }

                if (!deviceType.Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    TemplateHandler.Assign(ref strRowTpl, "color", (intCount%2 == 0) ? Common.RowBgOdd : Common.RowBgEven);
                    //Assign total signups for Old URL
                    strListRow.Append(TrackingSystem.GenRowOldURL(strRowTpl, totalSignups, totalDeposits, dtmFDate,
                                                                  dtmTDate));
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
            DNAideLog.Write(ex.Message);
#if DEBUG
                throw;
#endif
        }
    }

    string GetLanguageOriginal(string strLangCode)
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


    string GetLangCode(string value)
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