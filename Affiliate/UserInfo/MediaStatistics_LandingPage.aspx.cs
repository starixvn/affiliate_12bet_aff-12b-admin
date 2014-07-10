using System;
using DNAide.Web;
using System.Text;
using System.Data;

public partial class UserInfo_MediaStatistics_LandingPage : RootPageLogIn
{
    protected string statisticsContent = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        statisticsContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_LandingPage.htm");

        TemplateHandler.RemoveBlock(ref statisticsContent, "StatisticsDetailContent");
        TemplateHandler.Assign(ref statisticsContent, "From", Resources.Lang.From);
        TemplateHandler.Assign(ref statisticsContent, "To", Resources.Lang.To);

        TemplateHandler.Assign(ref statisticsContent, "{TrackingStatistics}", Resources.Lang.LandingPages);
        TemplateHandler.Assign(ref statisticsContent, "{Page}", Resources.Lang.Page);
        TemplateHandler.Assign(ref statisticsContent, "{Language}", Resources.Lang.Language);
        TemplateHandler.Assign(ref statisticsContent, "{Submit}", Resources.Lang.Submit);
        TemplateHandler.Assign(ref statisticsContent, "{Site}", Resources.Lang.SiteUrl);
        TemplateHandler.Assign(ref statisticsContent, "{TotalClick}", Resources.Lang.TotalClick);
        TemplateHandler.Assign(ref statisticsContent, "{UniqueClick}", Resources.Lang.UniqueClick);
        TemplateHandler.Assign(ref statisticsContent, "{Signups}", Resources.Lang.SignUp);
        TemplateHandler.Assign(ref statisticsContent, "{FDM}", Resources.Lang.FDM);
        TemplateHandler.Assign(ref statisticsContent, "linkto", Resources.Lang.LinkTo);
        TemplateHandler.Assign(ref statisticsContent, "Device", Resources.Lang.Device);
        

        var deviceType = Common.GetDeviceType(Request["slDevice"]);
        TemplateHandler.Assign(ref statisticsContent, "DeviceType-val", deviceType);
        TemplateHandler.Assign(ref statisticsContent, "slDevice", Common.GenDeviceList("slDevice", deviceType));

        object dtmFrom = Request["fdate"];
        object dtmTo = Request["tdate"];
        string sportCode = Request["sportCode"];
        string languageCode = Request["languageCode"];
        if (dtmFrom == null || dtmTo == null)
        {
            DateTime now = DateTime.Now;
            dtmFrom = new DateTime(now.Year, now.Month, 1);
            dtmTo = now;
        }
        TemplateHandler.Assign(ref statisticsContent, "slPage", Common.GenListLink2Page("sportCode", sportCode, true));

        DataView dv = (CacheUtil.GetBannerTypesOld() as DataTable).DefaultView;
        TemplateHandler.Assign(ref statisticsContent, "slLanguage", this.GetDataOptionBanner(dv, "languageCode", "Language", languageCode, true));

        TemplateHandler.Assign(ref statisticsContent, "fdate", Common.ConvertDateTime(dtmFrom, "/", true, false));
        TemplateHandler.Assign(ref statisticsContent, "tdate", Common.ConvertDateTime(dtmTo, "/", true, false));

        TemplateHandler.ReplaceBlock(ref statisticsContent, "Content", GenerateHTML.TrackingSystem.GetStatisticsByPage(dtmFrom, dtmTo, sportCode, string.IsNullOrEmpty(languageCode) ? string.Empty : Common.GetLangCode(languageCode), deviceType));
    }

    string GetDataOptionBanner(DataView dv, string id, string type, string activeValue, bool isGenAll)
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