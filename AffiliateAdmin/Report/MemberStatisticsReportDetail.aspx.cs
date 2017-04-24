using System;
using System.Collections;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;

public partial class Report_MemberStatisticsReportDetail : IBCPage
{
    private string strTpl = string.Empty;
    private StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/MemberStatisticsReportDetail.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    private void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref strTpl, "aff-txt", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "memberstatistics", Resources.Lang.MemberStatistics);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "active", Resources.Lang.Active);
        TemplateHandler.Assign(ref strTpl, "dormant", Resources.Lang.Dormant);
        TemplateHandler.Assign(ref strTpl, "deposit", Resources.Lang.FirstDeposit);
        TemplateHandler.Assign(ref strTpl, "nondeposit", Resources.Lang.NonDeposit);
        TemplateHandler.Assign(ref strTpl, "totalregister", Resources.Lang.TotalRegister);
        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "totalclick", Resources.Lang.TotalClicks);
        TemplateHandler.Assign(ref strTpl, "totalunique", Resources.Lang.UniqueClicks);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.MemberStatisticsDetail);
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
    }

    private void GenerateHTML()
    {
        //Get AffUsername by AffId "Common.GetAffUsername(1166364)"
        int intAffId = CastAide.AsInt32(Request["affid"]);
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int intPage = CastAide.AsInt32(Request["page"], 1);

        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTpl, "affname", Request["affname"]);
        TemplateHandler.Assign(ref strTpl, "affid", intAffId);

        //Get data from Deposit site
        var result = DataHandler.Affiliate.SelAffiliateStatisticsDetail(intAffId, dtmFDate, dtmTDate, isGetAll);
        var dataTable = result.ResultSet.Tables[0];

        int start = (intPage - 1) * intPageSize;
        int end = (start + intPageSize <= dataTable.Rows.Count ? start + intPageSize : dataTable.Rows.Count);
        int nummem = dataTable.Rows.Count;

        if (dataTable.Rows.Count > 0)
        {
            int intCount = 0;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strListAffId = string.Empty;
            StringBuilder strListRow = new StringBuilder();

            foreach (DataRow dr in dataTable.Rows)
            {
                string strTmp = strRowTpl;
                intCount++;
                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);

                DateTime dtmCreatedDate = CastAide.AsDateTime(dr["CreatedDate"], DateTime.Now);
                if (dr["CreatedDate"] == null)
                    dtmCreatedDate = CastAide.AsDateTime(dr["CreatDate"], DateTime.Now);

                TemplateHandler.Assign(ref strTmp, "date-val", dtmCreatedDate.ToString("d-MMM-yyyy"));
                TemplateHandler.Assign(ref strTmp, "active-val", CastAide.AsInt32(dr["active"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "dormant-val", CastAide.AsInt32(dr["dormant"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "totalregister-val", CastAide.AsInt32(dr["totalregistered"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "total-val", CastAide.AsInt32(dr["total"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "totalclick-val", CastAide.AsInt32(dr["TotalClick"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "totalunique-val", CastAide.AsInt32(dr["UniqueClick"]).ToString(Common.NumberFormatPlaceholder));
                strListRow.Append(strTmp);
            }

            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(7));
        }

        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 2, nummem, SubMenu.ReportText[2], "i_report.png"));
        //Assign Paging template
        if (dataTable.Rows.Count > 0)
        {
            int totalPage = CastAide.AsInt32(Math.Ceiling(CastAide.AsDecimal(dataTable.Rows.Count) / CastAide.AsDecimal(intPageSize)));
            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(totalPage, intPage, intPageSize, 0, "aPageTop", "txtPageTop", 550, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(totalPage, intPage, intPageSize, 0, "aPageBottom", "txtPageBottom", 550, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
    }

    #region Convert datatable to Arraylist

    public ArrayList DataSetToArrayList(int ColumnIndex, DataTable dataTable)
    {
        ArrayList output = new ArrayList();

        foreach (DataRow row in dataTable.Rows)
            output.Add(row[ColumnIndex]);

        return output;
    }

    #endregion Convert datatable to Arraylist
}