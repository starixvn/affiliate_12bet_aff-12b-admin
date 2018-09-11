using System.Text;
using DNAide.Web;

/// <summary>
/// Summary description for SubMenu
/// </summary>
public class SubMenu
{
    public static string[] AffApprovedLink = new string[] { "../UserInfo/EarningsHistory.aspx", "../UserInfo/AffiliateUpd.aspx", "../UserInfo/note.aspx", "../UserInfo/Adjustment.aspx", "../UserInfo/MediaStatistics.aspx", "../UserInfo/MemberList.aspx", "../UserInfo/UserLog.aspx", "../UserInfo/MediaStatistics_New.aspx" };
    public static string[] AffApprovedText = new string[] { Resources.Lang.StatementHistory, Resources.Lang.Edit, Resources.Lang.Note, Resources.Lang.Adjustment, Resources.Lang.TrackingStatistics, Resources.Lang.Member, Resources.Lang.ViewLog, "Click statis New" };

    public static string[] AffPendingLink = new string[] { "AffiliateUpd.aspx?approved=0", "note.aspx?approved=0" };
    public static string[] AffPendingText = new string[] { Resources.Lang.Edit, Resources.Lang.Note };

    public static string[] ReportLink = new string[]
        {
            "../Report/MemberReport.aspx",
            "../Report/AffiliateReport.aspx"
        };

    public static string[] ReportText = new string[]
        {
            Resources.Lang.ReportDetails,
            Resources.Lang.AffiliateReport
        };

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
                string strText = (numRows == 0 ? arrText[i] : arrText[i] + " [" + numRows.ToString(Common.NumberFormatPlaceholder) + "]");
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
}
