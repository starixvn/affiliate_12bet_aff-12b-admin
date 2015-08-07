using System;
using System.Text;
using System.Collections;
using System.Data;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;

public partial class Report_MemberStatisticsReportDetail : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/MemberStatisticsReportDetail.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
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
        TemplateHandler.Assign(ref strTpl, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTpl, "nondeposit", Resources.Lang.NonDeposit);
        TemplateHandler.Assign(ref strTpl, "totalregister", Resources.Lang.TotalRegister);
        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "totalclick", Resources.Lang.TotalClicks);
        TemplateHandler.Assign(ref strTpl, "totalunique", Resources.Lang.UniqueClicks);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.MemberStatisticsDetail);
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
    }

    void GenerateHTML()
    {
        //Get AffUsername by AffId "Common.GetAffUsername(1166364)"
        try
        {
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
            DbResult rsRegister = DataHandler.Affiliate.SelAffiliateTotalRegisterDetail(intAffId, dtmFDate, dtmTDate, isGetAll);
            DbResult rsTotal = DataHandler.Affiliate.SelAffiliateStatisticsDetail(intAffId, dtmFDate, dtmTDate, isGetAll);

            DataTable dtb = myJoinMethod(rsRegister.ResultSet.Tables[0], rsTotal.ResultSet.Tables[0], "ForeignKey", "ForeignKey");
            decimal dctotalPage = dtb.Rows.Count / intPageSize;
            int totalPage = CastAide.AsInt32(Math.Ceiling(CastAide.AsDecimal(dtb.Rows.Count) / CastAide.AsDecimal(intPageSize)));

            //Response.Write("<script type='text/javascript'>alert('" + dtb.Rows.Count.ToString() + "');</script>");
            //int totalPage = CastAide.AsInt32(rs.Outputs["totalpages"]);

            int start = (intPage - 1) * intPageSize;
            int end = (start + intPageSize <= dtb.Rows.Count ? start + intPageSize : dtb.Rows.Count);
            int nummem = dtb.Rows.Count;

            if (dtb.Rows.Count > 0)
            {
                int intCount = 0;
                //int intActive = 0, intTotal = 0, intTotalRegister = 0;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                string strListAffId = string.Empty;
                StringBuilder strListRow = new StringBuilder();
                //Hashtable hsTotal = new Hashtable();

                //Sort datatable Again
                DataView dv = new DataView(dtb);
                dv.Sort = "ForeignKey";
                dtb = dv.ToTable();

                //foreach (DataRow dr in dtb.Rows)
                for (int i = start; i < end; i++ )
                {
                    DataRow dr = dtb.Rows[i];
                    string strTmp = strRowTpl;
                    intCount++;

                    int active = CastAide.AsInt32(dr["active"]);
                    int total = CastAide.AsInt32(dr["total"]);
                    int register = CastAide.AsInt32(dr["totalregistered"]);

                    //intActive += active;
                    //intTotal += total;

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);

                    DateTime dtmCreatedDate = CastAide.AsDateTime(dr["CreatedDate"], DateTime.Now);
                    if (dr["CreatedDate"] == null)
                        dtmCreatedDate = CastAide.AsDateTime(dr["CreatDate"], DateTime.Now);

                    TemplateHandler.Assign(ref strTmp, "date-val", dtmCreatedDate.ToString("d-MMM-yyyy"));
                    TemplateHandler.Assign(ref strTmp, "active-val", active);
                    TemplateHandler.Assign(ref strTmp, "dormant-val", total - active);
                    TemplateHandler.Assign(ref strTmp, "totalregister-val", register);
                    TemplateHandler.Assign(ref strTmp, "total-val", total);
                    TemplateHandler.Assign(ref strTmp, "totalclick-val", CastAide.AsInt32(dr["TotalClick"]));
                    TemplateHandler.Assign(ref strTmp, "totalunique-val", CastAide.AsInt32(dr["UniqueClick"]));
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
            if (dtb.Rows.Count > 0)
            {
                TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(totalPage, intPage, intPageSize, 0, "aPageTop", "txtPageTop", 550, false, true));
                TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(totalPage, intPage, intPageSize, 0, "aPageBottom", "txtPageBottom", 550, false, false));
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

    #region Convert datatable to Arraylist
    public ArrayList DataSetToArrayList(int ColumnIndex, DataTable dataTable)
    {
        ArrayList output = new ArrayList();

        foreach (DataRow row in dataTable.Rows)
            output.Add(row[ColumnIndex]);

        return output;
    }
    #endregion

    #region Full outer join 2 datatables
    public DataTable myJoinMethod(DataTable LeftTable, DataTable RightTable,
            String LeftPrimaryColumn, String RightPrimaryColumn)
    {
        //first create the datatable columns 
        DataSet mydataSet = new DataSet();
        mydataSet.Tables.Add("  ");
        DataTable myDataTable = mydataSet.Tables[0];

        //add left table columns 
        DataColumn[] dcLeftTableColumns = new DataColumn[LeftTable.Columns.Count];
        LeftTable.Columns.CopyTo(dcLeftTableColumns, 0);

        foreach (DataColumn LeftTableColumn in dcLeftTableColumns)
        {
            if (!myDataTable.Columns.Contains(LeftTableColumn.ToString()))
                myDataTable.Columns.Add(LeftTableColumn.ToString());
        }

        //now add right table columns 
        DataColumn[] dcRightTableColumns = new DataColumn[RightTable.Columns.Count];
        RightTable.Columns.CopyTo(dcRightTableColumns, 0);

        foreach (DataColumn RightTableColumn in dcRightTableColumns)
        {
            if (!myDataTable.Columns.Contains(RightTableColumn.ToString()))
            {
                if (RightTableColumn.ToString() != RightPrimaryColumn)
                    myDataTable.Columns.Add(RightTableColumn.ToString());
            }
        }

        //add left-table data to mytable 
        foreach (DataRow LeftTableDataRows in LeftTable.Rows)
        {
            myDataTable.ImportRow(LeftTableDataRows);
        }

        ArrayList var = new ArrayList(); //this variable holds the id's which have joined 

        ArrayList LeftTableIDs = new ArrayList();
        LeftTableIDs = this.DataSetToArrayList(0, LeftTable);

        //import righttable which having not equal Id's with lefttable 
        foreach (DataRow rightTableDataRows in RightTable.Rows)
        {
            if (LeftTableIDs.Contains(rightTableDataRows[0]))
            {
                string wherecondition = "[" + myDataTable.Columns[0].ColumnName + "]='"
                        + rightTableDataRows[0].ToString() + "'";
                DataRow[] dr = myDataTable.Select(wherecondition);
                int iIndex = myDataTable.Rows.IndexOf(dr[0]);

                foreach (DataColumn dc in RightTable.Columns)
                {
                    if (dc.Ordinal != 0)
                        myDataTable.Rows[iIndex][dc.ColumnName.ToString().Trim()] =
                rightTableDataRows[dc.ColumnName.ToString().Trim()].ToString();
                }
            }
            else
            {
                int count = myDataTable.Rows.Count;
                DataRow row = myDataTable.NewRow();
                row[0] = rightTableDataRows[0].ToString();
                myDataTable.Rows.Add(row);
                foreach (DataColumn dc in RightTable.Columns)
                {
                    if (dc.Ordinal != 0)
                        myDataTable.Rows[count][dc.ColumnName.ToString().Trim()] =
                rightTableDataRows[dc.ColumnName.ToString().Trim()].ToString();
                }
            }
        }

        return myDataTable;
    }
    #endregion
}
