using System;
using System.Collections;
using DNAide.Web;
using DNAide.Data;
using System.Text;
public partial class UserInfo_MediaStatistics_New : RootPageLogIn
{
    protected string statisticsContent = string.Empty; 
    protected void Page_Load(object sender, EventArgs e)
    {
        statisticsContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_New.htm");
        TemplateHandler.RemoveBlock(ref statisticsContent, "StatisticsDetailContent");
        TemplateHandler.Assign(ref statisticsContent, "From", Resources.Lang.From);
        TemplateHandler.Assign(ref statisticsContent, "To", Resources.Lang.To);
        TemplateHandler.Assign(ref statisticsContent, "Device", Resources.Lang.Device);
        
        TemplateHandler.Assign(ref statisticsContent, "{TrackingStatistics}", Resources.Lang.Referrals);
        TemplateHandler.Assign(ref statisticsContent, "{TrackingCode}", Resources.Lang.TrackingCode);
        TemplateHandler.Assign(ref statisticsContent, "{Submit}", Resources.Lang.Submit);
        TemplateHandler.Assign(ref statisticsContent, "{Site}", Resources.Lang.SiteUrl);
        TemplateHandler.Assign(ref statisticsContent, "{TotalClick}", Resources.Lang.TotalClick);
        TemplateHandler.Assign(ref statisticsContent, "{UniqueClick}", Resources.Lang.UniqueClick);
        TemplateHandler.Assign(ref statisticsContent, "{Signups}", Resources.Lang.SignUp);
        TemplateHandler.Assign(ref statisticsContent, "{FDM}", Resources.Lang.FDM);
        TemplateHandler.Assign(ref statisticsContent, "linkto", Resources.Lang.LinkTo);

        
        object dtmFrom = Request["fdate"];
        object dtmTo = Request["tdate"];
        var deviceType = Common.GetDeviceType(Request["slDevice"]);
        TemplateHandler.Assign(ref statisticsContent, "DeviceType-val", deviceType);
        TemplateHandler.Assign(ref statisticsContent, "slDevice", Common.GenDeviceList("slDevice", deviceType));

        
        if (dtmFrom == null || dtmTo == null)
        {
            DateTime now = DateTime.Now;
            dtmFrom = new DateTime(now.Year, now.Month, 1);
            dtmTo = now;
        }

        TemplateHandler.Assign(ref statisticsContent, "fdate", Common.ConvertDateTime(dtmFrom, "/", true, false));
        TemplateHandler.Assign(ref statisticsContent, "tdate", Common.ConvertDateTime(dtmTo, "/", true, false));

        TemplateHandler.ReplaceBlock(ref statisticsContent, "Content", GenerateHTML.TrackingSystem.GetStatisticsBySite(dtmFrom, dtmTo, deviceType));
    }
}