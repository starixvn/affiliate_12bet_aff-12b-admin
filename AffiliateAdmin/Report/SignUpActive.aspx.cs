using System;
using System.Data;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using Criterias;
using DNAide.Core;
using DNAide.Web;

public partial class Report_SignUpActive : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/SignUpActiveReport.htm");

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
    }

    void GenerateHTML()
    {
        string strUsername = CastAide.AsString(Request["affname"]);
        string strPreferLang = Request["slPreferLang"];
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int sortBy = CastAide.AsInt32(Request["slSortBy"], 0);

        TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
        TemplateHandler.Assign(ref strTpl, "sortBy-val", sortBy);
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTpl, "affname", strUsername);

        var criteria = new SignUpActiveCriteria
            {
                DtmFDate = dtmFDate,
                DtmTDate = dtmTDate,
                IsGetAll = isGetAll,
                Username = strUsername,
                PreferredLanguage = strPreferLang,
                PageSize = intPageSize,
                PageIndex = intPage,
                SortBy = sortBy
            };

        DbResult rs = DataHandler.Affiliate.ActiveMemberReport(criteria);

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

            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;

                int total = CastAide.AsInt32(dr["fdm"]);

                intTotal += total;

                TemplateHandler.Assign(ref strTmp, "no", intCount);
                intCount++;

                TemplateHandler.Assign(ref strTmp, "aff-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "affid-val", dr["affid"]);
                TemplateHandler.Assign(ref strTmp, "unique-val", dr["UniqueClicks_Old"]);
                TemplateHandler.Assign(ref strTmp, "total-val", dr["TotalClicks_Old"]);
                TemplateHandler.Assign(ref strTmp, "uniquenew-val", dr["UniqueClicks_New"]);
                TemplateHandler.Assign(ref strTmp, "totalnew-val", dr["TotalClicks_New"]);
                TemplateHandler.Assign(ref strTmp, "fdm-val", total);
                if (CastAide.AsBoolean(dr["isclosed"]))
                    TemplateHandler.Assign(ref strTmp, "color", Common.RowBgClosed);
                else
                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);

                //sign up
                TemplateHandler.Assign(ref strTmp, "signup-val", dr["Signup"]);

                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(5));
        }
        //Assign Paging template
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 4, criteria.NRows, SubMenu.ReportText[2], "i_report.png"));
        if (dtb.Rows.Count > 0)
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageTop", "txtPageTop", 710, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageBottom", "txtPageBottom", 710, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
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