using System.Xml.Linq;
using Criterias;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;
using System.Text;
using System;
using System.Data;
using System.Linq;
public partial class Report_Performance : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/Performance.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "aff-txt", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);

        TemplateHandler.Assign(ref strTpl, "signup", Resources.Lang.SignUp);
        TemplateHandler.Assign(ref strTpl, "activemember", Resources.Lang.ActiveMember);
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "sortBy-txt", Resources.Lang.SortBy);
        TemplateHandler.Assign(ref strTpl, "desc-txt", Resources.Lang.Desc);
    }

    void GenerateHTML()
    {
        try
        {
            string strUsername = CastAide.AsString(Request["affname"]);
            string strPreferLang = Request["slPreferLang"];
            
            DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddMonths(-1));
            DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
            int sportId = CastAide.AsInt32(Request["slSport"], 1);
            int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int intPage = CastAide.AsInt32(Request["page"], 1);
            int sortby = CastAide.AsInt32(Request["sortby"], 0);
            bool isDesc = (CastAide.AsString(Request["isDesc"], string.Empty) != "ic-uncheck");

            TemplateHandler.Assign(ref strTpl, "sport-val", sportId);
            TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
            TemplateHandler.Assign(ref strTpl, "currentYear-val", DateTime.Now.Year);
            TemplateHandler.Assign(ref strTpl, "currentMonth-val", DateTime.Now.Month);
            TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/yyyy"));
            TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/yyyy"));
            TemplateHandler.Assign(ref strTpl, "page", intPage);
            TemplateHandler.Assign(ref strTpl, "all-val", "[" + Resources.Lang.All + "]");
            TemplateHandler.Assign(ref strTpl, "cssgetall", isDesc ? "ic-check" : "ic-uncheck");

            TemplateHandler.Assign(ref strTpl, "affname", String.IsNullOrEmpty(strUsername) ? "[" + Resources.Lang.All + "]" : strUsername);

            strUsername = ((string.Compare(strUsername, "[" + Resources.Lang.All + "]", true) == 0 ? string.Empty : strUsername));

            var criteria = new PerformanceCriteria
                {
                    Username = strUsername,
                    Sport = sportId,
                    Language = strPreferLang,
                    FromDate = dtmFDate,
                    ToDate = dtmTDate,
                    SortBy = sortby,
                    PageSize = intPageSize,
                    PageIndex = intPage,
                    IsDesc = isDesc
                };

            DbResult rs = DataHandler.Affiliate.PerformanceReport(criteria);

            DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            criteria.NRows = CastAide.AsInt32(rs.Outputs["nrows"]);

            if (dtb.Rows.Count > 0)
            {
                int intCount = (intPage - 1) * intPageSize + 1;
                int intTotal = 0;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");

                StringBuilder strListRow = new StringBuilder();

                int total = 0;
                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;

                    intTotal += total;

                    TemplateHandler.Assign(ref strTmp, "no", intCount);
                    intCount++;

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) 
                        ? Common.RowBgEven : Common.RowBgOdd);

                    TemplateHandler.Assign(ref strTmp, "affiliate-val", dr["Username"]);
                    TemplateHandler.Assign(ref strTmp, "affiliateid", dr["AffId"]);
                    TemplateHandler.Assign(ref strTmp, "activeplayer", dr["ActivePlayer"]);
                    TemplateHandler.Assign(ref strTmp, "qualified-member", dr["QualifiedMembers"]);
                    TemplateHandler.Assign(ref strTmp, "deposit", NumberFormat.DecFormat(dr["deposit"], 2));
                    TemplateHandler.Assign(ref strTmp, "withdrawal", NumberFormat.DecFormat(dr["withdrawal"], 2));
                    TemplateHandler.Assign(ref strTmp, "turnover", NumberFormat.DecFormat(dr["turnover"], 2));
                    TemplateHandler.Assign(ref strTmp, "winlost", NumberFormat.DecFormat(dr["winlost"], 2));
                    TemplateHandler.Assign(ref strTmp, "commission", NumberFormat.DecFormat(dr["commission"], 2));

                    strListRow.Append(strTmp);
                }

                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(4));
            }
            //Assign Paging template
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 6, criteria.NRows, SubMenu.ReportText[2], "i_report.png"));
            if (dtb.Rows.Count > 0)
            {
                TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageTop", "txtPageTop", 960, false, true));
                TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageBottom", "txtPageBottom", 960, false, false));
            }
            else
            {
                TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
                TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
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
    public int GetSignUp(DataTable dt, int affId)
    {
        try
        {
            return CastAide.AsInt32(dt.Select("AffiliateID=" + affId.ToString())[0]["TotalSignUp"]);
        }
        catch
        {
            return 0;
        }

    }
}
