using System;
using System.Data;
using System.Text;
using Criterias;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;

public partial class Report_AffiliateQMReport : IBCPage
{
    private string strTpl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/AffiliateQMReport.htm");
        AssignHeader();
        GenerateHTML();
        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "aff-txt", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "month-txt", Resources.Lang.Month);
        TemplateHandler.Assign(ref strTpl, "year-txt", Resources.Lang.Year);
        TemplateHandler.Assign(ref strTpl, "sortby-txt", Resources.Lang.SortBy);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "sportbook", Resources.Lang.Sportbook);
        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.LimitDate + AppSettings.StatementAvailableFrom.ToString("d-MMM-yy"));
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
    }

    void GenerateHTML()
    {
        try
        {
            string strUsername = CastAide.AsString(Request["affname"]);
            var selectedMonth = CastAide.AsDateTime(Request["fmonth"], DateTime.Now);

            int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int intPage = CastAide.AsInt32(Request["page"], 1);
            string strPreferLang = Request["slPreferLang"];
            int sortBy = CastAide.AsInt32(Request["slSortBy"], 0);
            
            TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
            TemplateHandler.Assign(ref strTpl, "sortBy-val", sortBy);
            TemplateHandler.Assign(ref strTpl, "page", intPage);
            TemplateHandler.Assign(ref strTpl, "fmonth", selectedMonth.ToString("MM/yyyy"));
            TemplateHandler.Assign(ref strTpl, "currentYear-val", DateTime.Now.Year);
            TemplateHandler.Assign(ref strTpl, "currentMonth-val", DateTime.Now.Month);

            if (String.IsNullOrEmpty(strUsername))
                TemplateHandler.Assign(ref strTpl, "affname", "[" + Resources.Lang.All + "]");
            else
                TemplateHandler.Assign(ref strTpl, "affname", strUsername);
            TemplateHandler.Assign(ref strTpl, "all-val", "[" + Resources.Lang.All + "]");

            var criteria = new QMReportCriteria
                {
                    Username = strUsername,
                    PreferredLanguage = strPreferLang,
                    Month = selectedMonth.Month,
                    Year = selectedMonth.Year,
                    PageIndex = intPage,
                    PageSize = intPageSize,
                    SortBy = sortBy
                };

            DbResult rs = DataHandler.Affiliate.SelAffiliateQMReport(criteria);

            var dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            criteria.NRows = CastAide.AsInt32(rs.Outputs["nrows"]);

            if (dtb.Rows.Count > 0)
            {
                int intCount = (intPage - 1) * intPageSize;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                var strListRow = new StringBuilder();

                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;
                    intCount++;

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref strTmp, "num-val", intCount);
                    TemplateHandler.Assign(ref strTmp, "aff-val", dr["username"]);
                    TemplateHandler.Assign(ref strTmp, "affid-val", dr["affid"]);
                    TemplateHandler.Assign(ref strTmp, "sportbook-val", CastAide.AsInt32(dr["spqm"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "casino-val", CastAide.AsInt32(dr["csqm"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "total-val", dr["totalqm"]);
                    TemplateHandler.Assign(ref strTmp, "month-val", selectedMonth.Month);
                    TemplateHandler.Assign(ref strTmp, "year-val", selectedMonth.Year);

                    strListRow.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(12));
            }
            
            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageTop", "txtPageTop", 550, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageBottom", "txtPageBottom", 550, false, false));
            if (dtb.Rows.Count > 0)
            {
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 3, criteria.NRows, SubMenu.ReportText[3], "i_report.png"));
            }
            else
            {
                TemplateHandler.Assign(ref strTpl, "pagingcss", "hide");
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 3, 0, SubMenu.ReportText[3], "i_report.png"));
            }

        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
            #if DEBUG
                throw;
            #endif
        }
    }
}
