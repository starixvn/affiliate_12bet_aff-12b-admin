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

public partial class CasinoPlayerTurnoverReport : IBCPage
{
    private string strTpl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/CasinoPlayerTurnoverReport.htm");
        AssignFilter();
        AssignHeader();
        GenerateHTML();
        this.HtmlContent = strTpl;
    }

    void AssignFilter()
    {
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.LimitDate + AppSettings.ReportStartDate.ToString("d-MMM-yy"));
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "username-txt", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "ibcusername-txt", Resources.Lang.IBCUsername);
        TemplateHandler.Assign(ref strTpl, "registereddate-txt", Resources.Lang.RegisteredDate);
        TemplateHandler.Assign(ref strTpl, "turnover-txt", Resources.Lang.Turnover);
        TemplateHandler.Assign(ref strTpl, "winloss-txt", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTpl, "deposit-txt", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTpl, "bonus-txt", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref strTpl, "affiliate-txt", Resources.Lang.Affiliate);
    }

    void GenerateHTML()
    {
        try
        {
            #region Get paras and assign value to input fields.

            const int sportId = 3; // Casino

            DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-31));
            if (dtmFDate < AppSettings.ReportStartDate)
                dtmFDate = AppSettings.ReportStartDate;
            DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);

            string strPreferLang = string.IsNullOrEmpty(Request["slPreferLang"]) ? null : Request["slPreferLang"];
            bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);
            int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int intPage = CastAide.AsInt32(Request["page"], 1);

            TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
            TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref strTpl, "sportid-val", sportId);
            TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
            TemplateHandler.Assign(ref strTpl, "page", intPage); 

            #endregion

            var criteria = new CasinoPlayerTurnoverReportCriteria
                {
                    PreferredLanguage = strPreferLang, 
                    DtmFDate = dtmFDate,
                    DtmTDate = dtmTDate,
                    PageIndex = intPage,
                    PageSize = intPageSize,
                    IsGetAll = isGetAll
                };

            DbResult rs = DataHandler.Report.CasinoPlayerTurnoverReport(criteria);

            DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            criteria.NRows = CastAide.AsInt32(rs.Outputs["nrows"]);

            if (dtb.Rows.Count > 0)
            {
                int intCount = (intPage - 1) * intPageSize;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                StringBuilder strListRow = new StringBuilder();

                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;
                    intCount++;

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);

                    TemplateHandler.Assign(ref strTmp, "num-val", intCount);
                    TemplateHandler.Assign(ref strTmp, "username-val", dr["username"]);
                    TemplateHandler.Assign(ref strTmp, "ibcusername-val", dr["ibcusername"]);
                    TemplateHandler.Assign(ref strTmp, "registereddate-val", Common.FormatDateNoHour(dr["registereddate"]));
                    TemplateHandler.Assign(ref strTmp, "turnover-val", Common.FormatNumber(dr["turnover"]));
                    TemplateHandler.Assign(ref strTmp, "winloss-val", Common.FormatNumber(dr["winloss"]));
                    TemplateHandler.Assign(ref strTmp, "deposit-val", Common.FormatNumber(dr["deposit"]));
                    TemplateHandler.Assign(ref strTmp, "bonus-val", Common.FormatNumber(dr["bonus"]));
                    TemplateHandler.Assign(ref strTmp, "affiliate-val", dr["affusername"]);

                    strListRow.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(9));
            }
            //Assign Paging template
            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageTop", "txtPageTop", 850, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(criteria.TotalPages, intPage, intPageSize, criteria.NRows, "aPageBottom", "txtPageBottom", 850, false, false));
            const int subMenuIndex = 8;

            if (dtb.Rows.Count > 0)
            {
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, subMenuIndex, criteria.NRows, SubMenu.ReportText[subMenuIndex], "i_report.png"));
            }
            else
            {
                TemplateHandler.Assign(ref strTpl, "pagingcss", "hide");
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, subMenuIndex, 0, SubMenu.ReportText[subMenuIndex], "i_report.png"));
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
