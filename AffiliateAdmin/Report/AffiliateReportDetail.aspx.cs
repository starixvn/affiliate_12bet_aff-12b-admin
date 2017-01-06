using System;
using System.Data;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Web;

public partial class Report_AffiliateReportDetail : IBCPage
{
    private string strTpl = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/AffiliateReportDetail.htm");

        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 1, 0, SubMenu.ReportText[1], "i_report.png"));

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    private void GenerateHTML()
    {
        string strAffName = CastAide.AsString(Request["affname"], string.Empty);
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);
        int intSportId = CastAide.AsInt32(Request["slSport"], 0);

        TemplateHandler.Assign(ref strTpl, "sportid-val", intSportId);
        TemplateHandler.Assign(ref strTpl, "affname", strAffName);
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        //TemplateHandler.Assign(ref strTpl, "turnover-total", NumberFormat.DecFormat(dcTotalTurnover, 2));

        //TemplateHandler.Assign(ref strTpl, "affid", intAffId);

        DataTable dtb = new DataTable();
        DbResult rs = null;
        rs = DataHandler.Affiliate.SelAffiliateReportByAff(strAffName, dtmFDate, dtmTDate, intSportId, isGetAll);

        if (CastAide.AsInt32(rs.Outputs["affid"]) != 0 || CastAide.AsString(Request["isclk"]) == "1")
            TemplateHandler.Assign(ref strTpl, "err-display", "none");
        else
            TemplateHandler.Assign(ref strTpl, "err-display", "block");

        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        if (dtb.Rows.Count > 0)
        {
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strDate = string.Empty;
            string strCheck = string.Empty;
            string strColor = Common.RowBgEven;
            string strRows = string.Empty;
            int intCount = 0;

            decimal dcTotalDeposit = 0, dcTotalWidthrawal = 0, dcTotalTurnover = 0, dcTotalWinloss = 0, dcTotalBonus = 0, dcTotalPsp = 0, dcTotalAdjustment = 0, dcTotalAffBF = 0, dcTotalAffWithdrawal = 0, dcTotalAffAdjustment = 0;
            StringBuilder strListRow = new StringBuilder();
            StringBuilder strRowByDate = new StringBuilder();

            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;

                decimal dcAffExRate = CastAide.AsDecimal(dr["AffExRate"]);
                decimal dcDeposit = 0, dcWidthrawal = 0, dcTurnover = 0, dcWinloss = 0, dcBonus = 0, dcPsp = 0, dcAdjustment = 0, dcAffBF = 0, dcAffWithdrawal = 0, dcAffAdjustment = 0;

                dcDeposit = CastAide.AsDecimal(dr["deposit"]);
                dcWidthrawal = CastAide.AsDecimal(dr["withdrawal"]);
                dcTurnover = CastAide.AsDecimal(dr["turnover"]);
                dcWinloss = CastAide.AsDecimal(dr["winlost"]);
                dcBonus = CastAide.AsDecimal(dr["bonus"]);
                dcPsp = CastAide.AsDecimal(dr["psp"]);
                dcAdjustment = CastAide.AsDecimal(dr["adjustment"]);
                dcAffAdjustment = CastAide.AsDecimal(dr["affadjustment"]);
                dcAffWithdrawal = CastAide.AsDecimal(dr["affwithdrawal"]);
                dcAffBF = CastAide.AsDecimal(dr["affbf"]);

                TemplateHandler.Assign(ref strTmp, "date-val", CastAide.AsDateTime(dr["Winlostdate"]).ToString("d-MMM-yy ddd"));
                TemplateHandler.Assign(ref strTmp, "currency-val", dr["Currency"]);
                TemplateHandler.Assign(ref strTmp, "units-val", NumberFormat.DecFormat(dcAffExRate, 5));

                TemplateHandler.Assign(ref strTmp, "deposit-val", NumberFormat.DecFormat(dcDeposit, 2));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", NumberFormat.DecFormat(dcWidthrawal, 2));
                TemplateHandler.Assign(ref strTmp, "turnover-val", NumberFormat.DecFormat(dcTurnover, 2));
                TemplateHandler.Assign(ref strTmp, "memwinloss-val", NumberFormat.DecFormat(dcWinloss, 2));
                TemplateHandler.Assign(ref strTmp, "bonus-val", NumberFormat.DecFormat(dcBonus, 2));
                TemplateHandler.Assign(ref strTmp, "psp-val", NumberFormat.DecFormat(dcPsp, 2));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", NumberFormat.DecFormat(dcAdjustment, 2));
                TemplateHandler.Assign(ref strTmp, "affadjustment-val", NumberFormat.DecFormat(dcAffAdjustment, 2));
                TemplateHandler.Assign(ref strTmp, "affwithdrawal-val", NumberFormat.DecFormat(dcAffWithdrawal, 2));
                TemplateHandler.Assign(ref strTmp, "affbf-val", NumberFormat.DecFormat(dcAffBF, 2));

                TemplateHandler.Assign(ref strTmp, "depositeqv-val", NumberFormat.DecFormat(dcDeposit * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "withdrawaleqv-val", NumberFormat.DecFormat(dcWidthrawal * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "turnovereqv-val", NumberFormat.DecFormat(dcTurnover * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "memwinlosseqv-val", NumberFormat.DecFormat(dcWinloss * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "bonuseqv-val", NumberFormat.DecFormat(dcBonus * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "pspeqv-val", NumberFormat.DecFormat(dcPsp * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "adjustmenteqv-val", NumberFormat.DecFormat(dcAdjustment * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "affadjustmenteqv-val", NumberFormat.DecFormat(dcAffAdjustment * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "affwithdrawaleqv-val", NumberFormat.DecFormat(dcAffWithdrawal * dcAffExRate, 2));
                TemplateHandler.Assign(ref strTmp, "affbfeqv-val", NumberFormat.DecFormat(dcAffBF * dcAffExRate, 2));

                if (string.Compare(strCheck, dr["Winlostdate"].ToString(), true) != 0)
                {
                    if (string.IsNullOrEmpty(strCheck))
                    {
                        intCount++;
                    }
                    else
                    {
                        strRows = strRowByDate.ToString();
                        TemplateHandler.Assign(ref strRows, "numrow", intCount);

                        strColor = (strColor == Common.RowBgOdd ? Common.RowBgEven : Common.RowBgOdd);
                        TemplateHandler.Assign(ref strRows, "color", strColor);

                        strListRow.Append(strRows);
                        intCount = 1;
                        strRowByDate.Remove(0, strRowByDate.Length);
                    }
                    strRowByDate.Append(strTmp);

                    strCheck = dr["Winlostdate"].ToString();
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTmp, "date", string.Empty);
                    strRowByDate.Append(strTmp);
                    intCount++;
                }

                dcTotalDeposit += dcDeposit * dcAffExRate;
                dcTotalWidthrawal += dcWidthrawal * dcAffExRate;
                dcTotalTurnover += dcTurnover * dcAffExRate;
                dcTotalWinloss += dcWinloss * dcAffExRate;
                dcTotalBonus += dcBonus * dcAffExRate;
                dcTotalPsp += dcPsp * dcAffExRate;
                dcTotalAdjustment += dcAdjustment * dcAffExRate;
                dcTotalAffAdjustment += dcAffAdjustment * dcAffExRate;
                dcTotalAffWithdrawal += dcAffWithdrawal * dcAffExRate;
                dcTotalAffBF += dcAffBF * dcAffExRate;
            }
            //Append last row
            strRows = strRowByDate.ToString();
            TemplateHandler.Assign(ref strRows, "numrow", intCount);
            strColor = (strColor == Common.RowBgOdd ? Common.RowBgEven : Common.RowBgOdd);
            TemplateHandler.Assign(ref strRows, "color", strColor);
            strListRow.Append(strRows);

            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());

            TemplateHandler.Assign(ref strTpl, "deposit-total", NumberFormat.DecFormat(dcTotalDeposit, 2));
            TemplateHandler.Assign(ref strTpl, "withdrawal-total", NumberFormat.DecFormat(dcTotalWidthrawal, 2));
            TemplateHandler.Assign(ref strTpl, "turnover-total", NumberFormat.DecFormat(dcTotalTurnover, 2));
            TemplateHandler.Assign(ref strTpl, "memwinloss-total", NumberFormat.DecFormat(dcTotalWinloss, 2));
            TemplateHandler.Assign(ref strTpl, "bonus-total", NumberFormat.DecFormat(dcTotalBonus, 2));
            TemplateHandler.Assign(ref strTpl, "psp-total", NumberFormat.DecFormat(dcTotalPsp, 2));
            TemplateHandler.Assign(ref strTpl, "adjustment-total", NumberFormat.DecFormat(dcTotalAdjustment, 2));
            TemplateHandler.Assign(ref strTpl, "affadjustment-total", NumberFormat.DecFormat(dcTotalAffAdjustment, 2));
            TemplateHandler.Assign(ref strTpl, "affwithdrawal-total", NumberFormat.DecFormat(dcTotalAffWithdrawal, 2));
            TemplateHandler.Assign(ref strTpl, "affbf-total", NumberFormat.DecFormat(dcTotalAffBF, 2));
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(21));
        }
    }

    private void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "aff-txt", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "memstatistics", Resources.Lang.MemberStatistics);
        TemplateHandler.Assign(ref strTpl, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref strTpl, "currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref strTpl, "units", Resources.Lang.Units);
        TemplateHandler.Assign(ref strTpl, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTpl, "turnover", Resources.Lang.Turnover);
        TemplateHandler.Assign(ref strTpl, "bonus", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref strTpl, "withdrawal", Resources.Lang.Withdrawal);
        TemplateHandler.Assign(ref strTpl, "adjustment", Resources.Lang.Adjustment);
        TemplateHandler.Assign(ref strTpl, "affbf", Resources.Lang.BFComm);
        TemplateHandler.Assign(ref strTpl, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTpl, "original", Resources.Lang.Original);
        TemplateHandler.Assign(ref strTpl, "usdeqv", Resources.Lang.UsdEqv);
        TemplateHandler.Assign(ref strTpl, "psp", Resources.Lang.PSP);
        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.AffiliateReportDetail + " [" + Request["affname"] + "]");
        TemplateHandler.Assign(ref strTpl, "affstatistics", Resources.Lang.AffiliateStatistics);
        TemplateHandler.Assign(ref strTpl, "err-msg", Resources.Lang.InvalidAffiliate);
        //Assign color
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
        //TemplateHandler.Assign(ref strTpl, "color", Common.RowBgOdd);
        TemplateHandler.Assign(ref strTpl, "colormonthly", Common.RowBgPending);

        //Hung added
        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
    }
}