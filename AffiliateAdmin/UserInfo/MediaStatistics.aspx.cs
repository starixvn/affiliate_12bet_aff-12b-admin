using System;
using DNAide.Web;
public partial class UserInfo_MediaStatistics : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics.htm");
        var affId = string.IsNullOrEmpty(Request["affId"]) ? AppSettings.Session.AffId : Convert.ToInt32(Request["affId"]);

        TemplateHandler.RemoveBlock(ref strTpl, "StatisticsDetailContent");
        TemplateHandler.Assign(ref strTpl, "TrackingStatistics", Resources.Lang.TrackingStatistics);
        TemplateHandler.Assign(ref strTpl, "TrackingCode", Resources.Lang.TrackingCode);
        TemplateHandler.Assign(ref strTpl, "slTrackingCode", GenerateHTML.TrackingSystem.GetTrackingCodeList(affId, true));
        TemplateHandler.Assign(ref strTpl, "Submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "Month", Resources.Lang.Month);
        TemplateHandler.Assign(ref strTpl, "TotalClick", Resources.Lang.TotalClicks);
        TemplateHandler.Assign(ref strTpl, "UniqueClick", Resources.Lang.UniqueClicks);
        TemplateHandler.Assign(ref strTpl, "Date", Resources.Lang.Date);
        TemplateHandler.ReplaceBlock(ref strTpl, "Content", GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(affId, null));
        TemplateHandler.Assign(ref strTpl, "affId-val", affId);

        #region Load SubPage
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 5, 0, Resources.Lang.TrackingStatistics, "i_customer.png"));
        #endregion

        this.HtmlContent = strTpl;

    }
}
