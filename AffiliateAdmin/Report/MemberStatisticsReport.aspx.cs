using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Criterias;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;

public partial class Report_MemberStatisticsReport : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/MemberStatisticsReport.htm");

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
        TemplateHandler.Assign(ref strTpl, "memberstatistics", Resources.Lang.MemberStatistics);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "active", Resources.Lang.Active);
        TemplateHandler.Assign(ref strTpl, "dormant", Resources.Lang.Dormant);
        TemplateHandler.Assign(ref strTpl, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTpl, "nondeposit", Resources.Lang.NonDeposit);
        TemplateHandler.Assign(ref strTpl, "totalregister", Resources.Lang.TotalRegister);
        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.LimitDate + AppSettings.StatementAvailableFrom.ToString("d-MMM-yy"));
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "Common.GetAffUsername(1166364)"

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

        var criteria = new MemberStatisticsReportCriteria
            {
                Username = strUsername,
                PreferredLanguage = strPreferLang,
                DtmFDate = dtmFDate,
                DtmTDate = dtmTDate,
                IsGetAll = isGetAll,
                AffType = 0,
                PageIndex = intPage,
                PageSize = intPageSize,
                SortBy = sortBy
            };

        DbResult rs = DataHandler.Affiliate.SelAffiliateStatistics(criteria);
        DataTable dtb = new DataTable();
        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        criteria.NRows = CastAide.AsInt32(rs.Outputs["nrows"]);

        if (dtb.Rows.Count > 0)
        {
            int intCount = (intPage - 1) * intPageSize + 1;
            int intActive = 0, intTotal = 0, intTotalRegister = 0;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strListAffId = string.Empty;


            StringBuilder strListRow = new StringBuilder();
            //Hashtable hsTotal = new Hashtable();


            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;

                int active = CastAide.AsInt32(dr["active"]);
                int total = CastAide.AsInt32(dr["total"]);

                intActive += active;
                intTotal += total;

                TemplateHandler.Assign(ref strTmp, "no", intCount);
                intCount++;

                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "affid-val", dr["affid"]);
                TemplateHandler.Assign(ref strTmp, "aff-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "active-val", active);
                TemplateHandler.Assign(ref strTmp, "dormant-val", total - active);
                TemplateHandler.Assign(ref strTmp, "total-val", total);
                TemplateHandler.Assign(ref strTmp, "totalregister-val", "{" + dr["affid"].ToString() + "}");

                strListAffId += dr["affid"].ToString() + ";";
                strListRow.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());

            strListAffId = (!string.IsNullOrEmpty(strListAffId) ? strListAffId.Substring(0, strListAffId.Length - 1) : string.Empty);
            //Get list Total Register and calculate non-Deposite here
            DbResult rsRegister = DataHandler.Affiliate.SelAffiliateTotalRegister(strListAffId, dtmFDate, dtmTDate, isGetAll);
            foreach (DataRow dr in rsRegister.ResultSet.Tables[0].Rows)
            {
                int totalRegister = CastAide.AsInt32(dr["totalregistered"]);
                intTotalRegister += totalRegister;
                TemplateHandler.Assign(ref strTpl, dr["affid"].ToString(), totalRegister);
            }
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(5));
        }

        //Assign Paging template
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 2, criteria.NRows, SubMenu.ReportText[2], "i_report.png"));
        if (dtb.Rows.Count > 0)
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageTop", "txtPageTop", 550, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageBottom", "txtPageBottom", 550, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
    }
}
