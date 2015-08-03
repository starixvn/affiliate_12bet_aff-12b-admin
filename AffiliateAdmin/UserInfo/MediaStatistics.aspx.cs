using DNAide.Web;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Data;
using System;
public partial class UserInfo_MediaStatistics : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics.htm");
        int custid = AppSettings.Session.AffId;
        
        TemplateHandler.RemoveBlock(ref strTpl, "StatisticsDetailContent");
        TemplateHandler.Assign(ref strTpl, "TrackingStatistics", Resources.Lang.TrackingStatistics);
        TemplateHandler.Assign(ref strTpl, "TrackingCode", Resources.Lang.TrackingCode);
        TemplateHandler.Assign(ref strTpl, "slTrackingCode", GenerateHTML.TrackingSystem.GetTrackingCodeList(custid,true));
        TemplateHandler.Assign(ref strTpl, "Submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "Month", Resources.Lang.Month);
        TemplateHandler.Assign(ref strTpl, "TotalClick", Resources.Lang.TotalClicks);
        TemplateHandler.Assign(ref strTpl, "UniqueClick", Resources.Lang.UniqueClicks);
        TemplateHandler.Assign(ref strTpl, "Date", Resources.Lang.Date);
        TemplateHandler.ReplaceBlock(ref strTpl, "Content", GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(custid,null));

        #region Load SubPage
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 5, 0, Resources.Lang.TrackingStatistics, "i_customer.png"));
        #endregion

        this.HtmlContent = strTpl;
        
    }
}
