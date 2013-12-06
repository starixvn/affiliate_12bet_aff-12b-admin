using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using System.Data;
using DNAide.Utils;

public partial class Report_PerformanceReportByAffiliate : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/PerformanceReportByAffiliate.htm");
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
        GenerateHTML();

        this.HtmlContent = strTpl;
    }
    void GenerateHTML()
    {
        try
        {
            int affId = CastAide.AsInt32(Request["affId"]);
            int sportID = CastAide.AsInt32(Request["sportid"]);
            DbResult rs = DataHandler.Affiliate.PerformanceReportByAffiliate(sportID, affId);

            DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            if (dtb.Rows.Count > 0)
            {

                int intCount = 0;
                int intTotal = 0;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                string strListAffId = string.Empty;


                StringBuilder strListRow = new StringBuilder();
                //Hashtable hsTotal = new Hashtable();


                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;

                    int total = 0;// CastAide.AsInt32(dr["Active"]);

                    intTotal += total;

                    TemplateHandler.Assign(ref strTmp, "no", intCount);
                    intCount++;

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0)
                        ? Common.RowBgEven : Common.RowBgOdd);

                    TemplateHandler.Assign(ref strTmp, "month",  DateTime.Parse(dr["WinLostDate"].ToString()).ToString("MMM-yyyy"));
                    TemplateHandler.Assign(ref strTmp, "activeplayer", dr["ActivePlayer"]);
                    TemplateHandler.Assign(ref strTmp, "qualified-member", NumberFormat.DecFormat(dr["QualifiedMember"], 0));
                    TemplateHandler.Assign(ref strTmp, "deposit", NumberFormat.DecFormat(dr["MemberDeposit"], 2));
                    TemplateHandler.Assign(ref strTmp, "withdrawal", NumberFormat.DecFormat(dr["MemberWithdrawal"], 2));
                    TemplateHandler.Assign(ref strTmp, "turnover", NumberFormat.DecFormat(dr["MemberTurnover"], 2));
                    TemplateHandler.Assign(ref strTmp, "winlost", NumberFormat.DecFormat(dr["MemberWinlost"], 2));
                    TemplateHandler.Assign(ref strTmp, "commission", NumberFormat.DecFormat(dr["commission"], 2));

                    strListRow.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());


            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(7));
            }
        }
        catch
        {
        }
    }
}
