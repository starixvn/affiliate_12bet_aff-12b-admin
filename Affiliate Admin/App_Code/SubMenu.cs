using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DNAide.Web;

/// <summary>
/// Summary description for SubMenu
/// </summary>
public class SubMenu
{
    public static string[] AffLink = new string[] { "../UserInfo/AffiliateList.aspx?status=approved&favor=1&isclk=1", "../UserInfo/AffiliateList.aspx?status=approved", "../UserInfo/AffiliateList.aspx?status=pending", "../UserInfo/AffiliateList.aspx?status=disapproved",
                                                    "../UserInfo/AffiliateList.aspx?status=approved&isClosed=true", "../Payment/PaymentRequest.aspx", "../GroupSetting/GroupSetting.aspx", "../Others/EmailHistory.aspx" };
    public static string[] AffText = new string[] { Resources.Lang.MyFavorite, Resources.Lang.Approved, Resources.Lang.Pending, Resources.Lang.Disapproved, Resources.Lang.Closed, Resources.Lang.PaymentRequest, Resources.Lang.CommGroup, Resources.Lang.EmailHistory };

    public static string[] MediaAffLink = new string[] { "../UserInfo/MediaAffList.aspx?favor=1&isclk=1", "../UserInfo/MediaAffList.aspx", "../UserInfo/MediaAffList.aspx?isClosed=true", "../Report/MediaReportDetail.aspx",
                                                    "../Report/AffiliateReport.aspx?type=2", "../Report/MemberStatisticsReport.aspx?type=2", "../Report/SignUpActive.aspx?type=2", "../Report/Commission.aspx?type=2" };
    public static string[] MediaAffText = new string[] { Resources.Lang.MyFavorite, Resources.Lang.MediaAff, Resources.Lang.Closed, Resources.Lang.ReportDetails, Resources.Lang.AffiliateReport, Resources.Lang.MemberStatistics };

    public static string[] AffApprovedLink = new string[] { "../UserInfo/EarningsHistory.aspx", "../UserInfo/AffiliateUpd.aspx", "../UserInfo/note.aspx", "../UserInfo/PlayersDetail.aspx", "../UserInfo/Adjustment.aspx", "../UserInfo/MediaStatistics.aspx", "../UserInfo/MemberList.aspx", "../UserInfo/UserLog.aspx", "../UserInfo/MediaStatistics_New.aspx" };
    public static string[] AffApprovedText = new string[] { Resources.Lang.StatementHistory, Resources.Lang.Edit, Resources.Lang.Note, Resources.Lang.PlayersDetail, Resources.Lang.Adjustment, Resources.Lang.TrackingStatistics, Resources.Lang.Member, Resources.Lang.ViewLog, "Click statis New" };

    public static string[] AffPendingLink = new string[] { "AffiliateUpd.aspx?approved=0", "note.aspx?approved=0" };
    public static string[] AffPendingText = new string[] { Resources.Lang.Edit, Resources.Lang.Note };

    public static string[] MediaCustomerLink = new string[] { "../UserInfo/EarningsHistory.aspx", "../UserInfo/AffiliateUpd.aspx", "../UserInfo/note.aspx", "../UserInfo/PlayersDetail.aspx", "../UserInfo/MediaGallery.aspx", "../UserInfo/MediaStatistics.aspx", "../UserInfo/MemberList.aspx", "../UserInfo/UserLog.aspx" };
    public static string[] MediaCustomerText = new string[] { Resources.Lang.StatementHistory, Resources.Lang.Edit, Resources.Lang.Note, Resources.Lang.PlayersDetail, Resources.Lang.MediaGallery, Resources.Lang.TrackingStatistics, Resources.Lang.Member, Resources.Lang.ViewLog };

    public static string[] AdminLink = new string[] { "ListUser.aspx", "ListDepartment.aspx"};
    public static string[] AdminText = new string[] { Resources.Lang.AdministratorList , Resources.Lang.DepartmentList};

    public static string[] ReportLink = new string[]
        {
            "../Report/MemberReport.aspx", 
            "../Report/AffiliateReport.aspx", 
            "../Report/MemberStatisticsReport.aspx", 
            "../Report/AffiliateQMReport.aspx", 
            "../Report/SignUpActive.aspx", 
            "../Report/Commission.aspx", 
            "../Report/Performance.aspx",
            "../Report/SBPlayerTurnoverReport.aspx",
            "../Report/CasinoPlayerTurnoverReport.aspx"
        };

    public static string[] ReportText = new string[]
        {
            Resources.Lang.ReportDetails, 
            Resources.Lang.AffiliateReport, 
            Resources.Lang.MemberStatistics, 
            Resources.Lang.QMReport, 
            Resources.Lang.SignUpActive, 
            Resources.Lang.Commission, 
            Resources.Lang.Performance,
            Resources.Lang.SBPlayerTurnoverReport,
            Resources.Lang.CasinoPlayerTurnoverReport
        };

    public static string[] MemberLink = new string[] { "MemberListAll.aspx", "MemberActiveList.aspx" };
    public static string[] MemberText = new string[] { Resources.Lang.Member, Resources.Lang.ActiveMember};

    public static string[] DomainLink = new string[] { "AffiliateDomain.aspx", "12BetDomain.aspx", "DomainControl.aspx" };
    public static string[] DomainText = new string[] { "Affiliate Domains", "12Bet Domains", "Domain Control" };

    //@VinhTrinh
    public static string GenSubMenu(string[] arrUrl, string[] arrText, int currMenu, int numRows, string strPageName, string iconFilePath)
    {
        string strTpl = TemplateHandler.LoadTemplate("SubMenu1.htm");
        string strMenuItem = TemplateHandler.CopyBlock(strTpl, "menu");
        TemplateHandler.Assign(ref strTpl, "pagename", string.Empty);
        TemplateHandler.Assign(ref strTpl, "ThemePath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "iconFilePath-val", iconFilePath);
        StringBuilder strBuild = new StringBuilder();
        for (int i = 0; i < arrText.Length; i++)
        {
            string strTmp = strMenuItem;
            TemplateHandler.Assign(ref strTmp, "url", arrUrl[i]);
            if (i == currMenu)
            {
                string strText = (numRows == 0 ? arrText[i] : arrText[i] + " [" + numRows.ToString() + "]");
                TemplateHandler.Assign(ref strTmp, "text", strText);
            }
            else
                TemplateHandler.Assign(ref strTmp, "text", arrText[i]);

            TemplateHandler.Assign(ref strTmp, "curr", (i == currMenu ? "id=\"current\"" : string.Empty));
            strBuild.AppendLine(strTmp);
        }
        TemplateHandler.ReplaceBlock(ref strTpl, "menu", strBuild.ToString());
        return strTpl;
    }
    //@VinhTrinh
    public static string GenSubMenu(string[] arrUrl, string[] arrText, int currMenu, string strText, string strPageName, string iconFilePath)
    {
        string strTpl = TemplateHandler.LoadTemplate("SubMenu1.htm");
        string strMenuItem = TemplateHandler.CopyBlock(strTpl, "menu");
        TemplateHandler.Assign(ref strTpl, "pagename", string.Empty);
        TemplateHandler.Assign(ref strTpl, "ThemePath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "iconFilePath-val", iconFilePath);
        StringBuilder strBuild = new StringBuilder();
        for (int i = 0; i < arrText.Length; i++)
        {
            string strTmp = strMenuItem;
            TemplateHandler.Assign(ref strTmp, "url", arrUrl[i]);
            if (i == currMenu)
            {
                TemplateHandler.Assign(ref strTmp, "curr", (i == currMenu ? "id=\"current\"" : string.Empty));                
                TemplateHandler.Assign(ref strTmp, "text", arrText[i] + "(" + strText + ")");
            }
            else
                TemplateHandler.Assign(ref strTmp, "text", arrText[i]);

            strBuild.AppendLine(strTmp);
        }
        TemplateHandler.ReplaceBlock(ref strTpl, "menu", strBuild.ToString());
        return strTpl;
    }
}
