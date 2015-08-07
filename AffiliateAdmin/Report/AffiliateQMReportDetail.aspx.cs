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
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;

public partial class Report_AffiliateQMReportDetail : IBCPage
{
    private string strTpl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/AffiliateQMReportDetail.htm");
        AssignHeader();
        GenerateHTML();
        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "ibcusername", Resources.Lang.IBCUsername);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.LimitDate + AppSettings.StatementAvailableFrom.ToString("d-MMM-yy"));
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "spturnover", Resources.Lang.SPTurnover);
        TemplateHandler.Assign(ref strTpl, "isspqualified", Resources.Lang.IsSPTurnoverQualified);
        TemplateHandler.Assign(ref strTpl, "hrturnover", Resources.Lang.HRTurnover);
        TemplateHandler.Assign(ref strTpl, "ishrqualified", Resources.Lang.IsHrTurnoverQualified);
        TemplateHandler.Assign(ref strTpl, "qmreportdetailof", Resources.Lang.QMReportDetailOf);
        TemplateHandler.Assign(ref strTpl, "in", Resources.Lang.In); 

    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "Common.GetAffUsername(1166364)"
        try
        {
            int affid = CastAide.AsInt32(Request["affid"]);
            string affname = CastAide.AsString(Request["affname"]);
            int month = CastAide.AsInt32(Request["month"], DateTime.Now.Month);
            int year = CastAide.AsInt32(Request["year"], DateTime.Now.Year);
            int intPageSize = CastAide.AsInt32(Request["slPageSize"], 50);
            int intPage = CastAide.AsInt32(Request["page"], 1);

            TemplateHandler.Assign(ref strTpl, "year-val", year);
            TemplateHandler.Assign(ref strTpl, "month-val", month);
            TemplateHandler.Assign(ref strTpl, "affid-val", affid);
            TemplateHandler.Assign(ref strTpl, "affname-val", affname);

            DbResult rs = DataHandler.Affiliate.SelAffiliateQMReportDetail(affid, month, year, intPage, intPageSize);

            DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            int totalPage = CastAide.AsInt32(rs.Outputs["totalpages"]);
            int nummem = CastAide.AsInt32(rs.Outputs["nrows"]);
            if (dtb.Rows.Count > 0)
            {
                int intCount = (intPage - 1)*intPageSize;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                StringBuilder strListRow = new StringBuilder();
                //Hashtable hsTotal = new Hashtable();


                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;
                    intCount++;

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);

                    TemplateHandler.Assign(ref strTmp, "num-val", intCount);
                    TemplateHandler.Assign(ref strTmp, "ibcusername-val", dr["ibcusername"]);
                    TemplateHandler.Assign(ref strTmp, "spturnover-val", Common.FormatNumber(dr["spturnover"]));
                    TemplateHandler.Assign(ref strTmp, "isspqualified-val", (CastAide.AsInt32(dr["isspqm"])== 0 ? "" : "checked='true'"));
                    TemplateHandler.Assign(ref strTmp, "hrturnover-val", Common.FormatNumber(dr["hrturnover"]));
                    TemplateHandler.Assign(ref strTmp, "ishrqualified-val", (CastAide.AsInt32(dr["ishrqm"]) == 0 ? "false" : "checked='true'"));
                    TemplateHandler.Assign(ref strTmp, "caturnover-val", Common.FormatNumber(dr["caturnover"]));
                    TemplateHandler.Assign(ref strTmp, "iscaqualified-val", (CastAide.AsInt32(dr["iscaqm"]) == 0 ? "false" : "checked='true'"));

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
                TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", 750, false, true));
                TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageBottom", "txtPageBottom", 750, false, false));
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 3, nummem, SubMenu.ReportText[3], "i_report.png"));
            }
            else
            {
                TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
                TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 3, nummem, SubMenu.ReportText[3], "i_report.png"));
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
