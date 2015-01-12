using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Criterias;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;

public partial class Report_Commission : IBCPage
{
    private string strTpl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/Commission.htm");
        AssignHeader();
        GenerateHTML();
        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "aff-txt", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "month-txt", Resources.Lang.Month);
        TemplateHandler.Assign(ref strTpl, "year-txt", Resources.Lang.Year);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "commission", Resources.Lang.Commission);
        TemplateHandler.Assign(ref strTpl, "aff-comm", Resources.Lang.Earning);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.LimitDate + AppSettings.StatementAvailableFrom.ToString("d-MMM-yy"));
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "sortBy-txt", Resources.Lang.SortBy);
        TemplateHandler.Assign(ref strTpl, "desc-txt", Resources.Lang.Desc);
        
    }
    private DateTime GetLastDayOfMonth(DateTime dtDate)
    {
        // create a datetime variable set to the passed in date
        DateTime dtTo = dtDate;

        // overshoot the date by a month
        dtTo = dtTo.AddMonths(1);

        // previous month
        dtTo = dtTo.AddDays(-(dtTo.Day));
        return dtTo;
    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "Common.GetAffUsername(1166364)"
        try
        {
            string strUsername = CastAide.AsString(Request["affname"]);
            int month = CastAide.AsInt32(Request["slmonth"], DateTime.Now.Month);
            int year = CastAide.AsInt32(Request["slyear"], DateTime.Now.Year);
            int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int intPage = CastAide.AsInt32(Request["page"], 1);
            int sport = CastAide.AsInt32(Request["slSport"], 1);
            string strPreferLang = Request["slPreferLang"];
            int sortBy = CastAide.AsInt32(Request["slSortBy"], 0);
            bool isDesc = (CastAide.AsString(Request["isDesc"], string.Empty) != "ic-uncheck");

            TemplateHandler.Assign(ref strTpl, "sport-val", sport);
            TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
            TemplateHandler.Assign(ref strTpl, "sortBy-val", sortBy);
            TemplateHandler.Assign(ref strTpl, "cssgetall", isDesc ? "ic-check" : "ic-uncheck");
            TemplateHandler.Assign(ref strTpl, "month-val", month);
            TemplateHandler.Assign(ref strTpl, "year-val", year);
            TemplateHandler.Assign(ref strTpl, "optionyear", Common.GenYearDropDown("slyear", year.ToString(), false, null));

            if (String.IsNullOrEmpty(strUsername))
                TemplateHandler.Assign(ref strTpl, "affname", "[" + Resources.Lang.All + "]");
            else
                TemplateHandler.Assign(ref strTpl, "affname", strUsername);
            TemplateHandler.Assign(ref strTpl, "all-val", "[" + Resources.Lang.All + "]");

            var criteria = new CommissionReportCriteria
                {
                    Username = strUsername,
                    PreferredLanguage = strPreferLang,
                    Sport = sport,
                    Month = month,
                    Year = year,
                    PageIndex = intPage,
                    PageSize = intPageSize,
                    SortBy = sortBy,
                    IsDesc = isDesc
                };

            DbResult rs = DataHandler.Affiliate.SelAffiliateCommissionReport(criteria);

            DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            criteria.NRows = CastAide.AsInt32(rs.Outputs["nrows"]);
            DateTime LastDay = GetLastDayOfMonth(new DateTime(year, month, 1));
            if (dtb.Rows.Count > 0)
            {
                int intCount = (intPage - 1) * intPageSize;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                StringBuilder strListRow = new StringBuilder();

                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;
                    intCount++;

                    if (CastAide.AsBoolean(dr["isclosed"]))
                        TemplateHandler.Assign(ref strTmp, "color", Common.RowBgClosed);
                    else
                        TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref strTmp, "monthdate_val", month.ToString() + "/" + LastDay.Day + "/" + year.ToString());
                    
                    TemplateHandler.Assign(ref strTmp, "num-val", intCount);
                    TemplateHandler.Assign(ref strTmp, "aff-val", dr["username"]);
                    TemplateHandler.Assign(ref strTmp, "affid-val", dr["affid"]);
                    TemplateHandler.Assign(ref strTmp, "commission-val", DataFormat.FormatNumber2(dr["commission"]));
                    TemplateHandler.Assign(ref strTmp, "affcomm-val", dr["affComm"]);

                    strListRow.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(12));
            }
            //Assign Paging template
            if (dtb.Rows.Count > 0)
            {
                TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageTop", "txtPageTop", 550, false, true));
                TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageBottom", "txtPageBottom", 550, false, false));
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 5, criteria.NRows, SubMenu.ReportText[3], "i_report.png"));
            }
            else
            {
                TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
                TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 5, 0, SubMenu.ReportText[3], "i_report.png"));
            }
        }
        catch (Exception ex)
        {
            DNAideLog.Write(ex.Message);
            #if DEBUG
                throw;
            #endif
        }
    }
}
