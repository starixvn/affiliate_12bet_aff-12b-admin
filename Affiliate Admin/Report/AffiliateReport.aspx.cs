using System;
using System.Text;
using System.Data;
using Criterias;
using DNAide.Web;
using DNAide.Utils;
using DNAide.Core;
using DNAide.Data;

public partial class Report_AffiliateReport : IBCPage
{
    string strTpl = string.Empty;
    public  DateTime now = DateTime.Now;
    StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/AffiliateReport.htm");


        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "memberstatistics", Resources.Lang.MemberStatistics);
        TemplateHandler.Assign(ref strTpl, "affiliatestatistics", Resources.Lang.AffiliateStatistics);
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
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "trackingrate", Resources.Lang.OldTrackingRate);
        TemplateHandler.Assign(ref strTpl, "newtrackingrate", Resources.Lang.NewTrackingRate);
        TemplateHandler.Assign(ref strTpl, "signup", Resources.Lang.SignUp);
        TemplateHandler.Assign(ref strTpl, "fdm", Resources.Lang.FDM);
        TemplateHandler.Assign(ref strTpl, "activemember", Resources.Lang.ActiveMember);
        TemplateHandler.Assign(ref strTpl, "qm", Resources.Lang.QM);

        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "psp", Resources.Lang.PSP);
        TemplateHandler.Assign(ref strTpl, "back", Resources.Lang.Back);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.LimitDate + AppSettings.StatementAvailableFrom.ToString("d-MMM-yy"));
        //Assign color
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);

        //Hung added
        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
    }
    
    void GenerateHTML()
    {
        try
        {
            var dtmFirstOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], dtmFirstOfMonth);
            DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], dtmFirstOfMonth);

            bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check");
            string strPreferLang = Request["slPreferLang"];
            int intSportId = CastAide.AsInt32(Request["slSport"], 0);
            int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            int intPage = CastAide.AsInt32(Request["page"], 1);

            TemplateHandler.Assign(ref strTpl, "lang-val", strPreferLang);
            TemplateHandler.Assign(ref strTpl, "currentYear-val", DateTime.Now.Year);
            TemplateHandler.Assign(ref strTpl, "currentMonth-val", DateTime.Now.Month);
            TemplateHandler.Assign(ref strTpl, "sportid-val", Request["slSport"]);
            TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/yyyy"));
            TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/yyyy"));
            TemplateHandler.Assign(ref strTpl, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
            TemplateHandler.Assign(ref strTpl, "page", intPage);

            TemplateHandler.Assign(ref strTpl, "fdate-full", GetFirstDayOfMonth(dtmFDate).ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref strTpl, "tdate-full", GetLastDayOfMonth(dtmTDate).ToString("MM/dd/yyyy"));
            //TemplateHandler.Assign(ref strTpl, "turnover-total", NumberFormat.DecFormat(dcTotalTurnover, 2));

            DataTable dtb = new DataTable();
            DbResult rs = null;

            var criteria = new AffiliateReportCriteria
                {
                    PreferredLanguage = strPreferLang,
                    FromDate = dtmFDate,
                    ToDate = dtmTDate,
                    SportId = intSportId,
                    IsGetAll = isGetAll,
                    PageIndex = intPage,
                    PageSize = intPageSize
                };

            rs = DataHandler.Affiliate.SelAffiliateReport(criteria);

            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];

            criteria.NRows = CastAide.AsInt32(rs.Outputs["nrows"]);

            if (dtb.Rows.Count > 0)
            {
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                string strDate = string.Empty;
                int intCount = 0;
                decimal dcTotalDeposit = 0, dcTotalWidthrawal = 0, dcTotalTurnover = 0, dcTotalWinloss = 0, dcTotalBonus = 0, dcTotalPsp = 0, dcTotalAdjustment = 0, dcTotalAffBF = 0, dcTotalAffWithdrawal = 0, dcTotalAffAdjustment = 0,
                    dcTotalUniqueClick = 0, dcTotalClick = 0,
                    dcTotalNewUniqueClick = 0, dcTotalNewClick = 0,
                    dcTotalSignup = 0, dcTotalFDM = 0,
                    dcTotalActiveMember = 0, dcTotalQM = 0;

                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 1, dtb.Rows.Count, SubMenu.ReportText[1], "i_report.png"));

                strListRow = new StringBuilder();
                decimal trackingRate;
                decimal newTrackingRate;

                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;
                    intCount++;
                    decimal dcDeposit = 0, dcWidthrawal = 0, dcTurnover = 0, dcWinloss = 0, dcBonus = 0, dcPsp = 0, dcAdjustment = 0, dcAffBF = 0, dcAffWithdrawal = 0, dcAffAdjustment = 0,
                        dcUniqueClick = 0, dcClick = 0,
                        dcNewUniqueClick = 0, dcNewClick = 0,
                        dcSignup = 0, dcFDM = 0,
                        dcActiveMember = 0, dcQM = 0;

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

                    dcUniqueClick = CastAide.AsDecimal(dr["OldUniqueclick"]);
                    dcClick = CastAide.AsDecimal(dr["OldTotalclick"]);
                    dcNewUniqueClick = CastAide.AsDecimal(dr["NewUniqueclick"]);
                    dcNewClick = CastAide.AsDecimal(dr["NewTotalclick"]);
                    dcSignup = CastAide.AsDecimal(dr["TotalSignup"]);
                    dcFDM = CastAide.AsDecimal(dr["TotalFDM"]);
                    dcActiveMember = CastAide.AsDecimal(dr["TotalActiveMember"]);
                    dcQM = CastAide.AsDecimal(dr["TotalQualifiedMember"]);

                    trackingRate = dcClick == 0 ? 0 : dcUniqueClick/dcClick;
                    newTrackingRate = dcNewClick == 0 ? 0 : dcNewUniqueClick / dcNewClick;

                    //affcomm = 0;//CastAide.AsDecimal(dr["affcomm"]);

                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref strTmp, "affid", dr["affid"]);
                    TemplateHandler.Assign(ref strTmp, "aff-val",  dr["username"]);
                    TemplateHandler.Assign(ref strTmp, "deposit-val", NumberFormat.DecFormat(dcDeposit, 2));
                    TemplateHandler.Assign(ref strTmp, "withdrawal-val", NumberFormat.DecFormat(dcWidthrawal, 2));
                    TemplateHandler.Assign(ref strTmp, "turnover-val", NumberFormat.DecFormat(dcTurnover, 2));
                    TemplateHandler.Assign(ref strTmp, "winlost-val", NumberFormat.DecFormat(dcWinloss, 2));
                    TemplateHandler.Assign(ref strTmp, "bonus-val", NumberFormat.DecFormat(dcBonus, 2));
                    TemplateHandler.Assign(ref strTmp, "psp-val", NumberFormat.DecFormat(dcPsp, 2));
                    TemplateHandler.Assign(ref strTmp, "adjustment-val", NumberFormat.DecFormat(dcAdjustment, 2));
                    TemplateHandler.Assign(ref strTmp, "affadjustment-val", NumberFormat.DecFormat(dcAffAdjustment, 2));
                    TemplateHandler.Assign(ref strTmp, "affwithdrawal-val", NumberFormat.DecFormat(dcAffWithdrawal, 2));
                    //TemplateHandler.Assign(ref strTmp, "affcomm-val", NumberFormat.DecFormat(affcomm, 2));
                    TemplateHandler.Assign(ref strTmp, "affbf-val", NumberFormat.DecFormat(dcAffBF, 2));

                    TemplateHandler.Assign(ref strTmp, "trackingrate-val", string.Format("{0:P2}", trackingRate));
                    TemplateHandler.Assign(ref strTmp, "newtrackingrate-val", string.Format("{0:P2}", newTrackingRate));
                    TemplateHandler.Assign(ref strTmp, "signup-val", dcSignup);
                    TemplateHandler.Assign(ref strTmp, "fdm-val", dcFDM);
                    TemplateHandler.Assign(ref strTmp, "activemember-val", dcActiveMember);
                    TemplateHandler.Assign(ref strTmp, "qm-val", dcQM);

                    strListRow.Append(strTmp);

                    dcTotalDeposit += dcDeposit;
                    dcTotalWidthrawal += dcWidthrawal;
                    dcTotalTurnover += dcTurnover;
                    dcTotalWinloss += dcWinloss;
                    dcTotalBonus += dcBonus;
                    dcTotalPsp += dcPsp;
                    dcTotalAdjustment += dcAdjustment;
                    dcTotalAffAdjustment += dcAffAdjustment;
                    dcTotalAffWithdrawal += dcAffWithdrawal;
                    dcTotalAffBF += dcAffBF;
                    //dcTotalAffComm += affcomm;

                    dcTotalClick += dcClick;
                    dcTotalUniqueClick += dcUniqueClick;
                    dcTotalNewClick += dcNewClick;
                    dcTotalNewUniqueClick += dcNewUniqueClick;
                    dcTotalSignup += dcSignup;
                    dcTotalFDM += dcFDM;
                    dcTotalActiveMember += dcActiveMember;
                    dcTotalQM += dcQM;
                }
                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());

                TemplateHandler.Assign(ref strTpl, "deposit-total", NumberFormat.DecFormat(dcTotalDeposit, 2));
                TemplateHandler.Assign(ref strTpl, "withdrawal-total", NumberFormat.DecFormat(dcTotalWidthrawal, 2));
                TemplateHandler.Assign(ref strTpl, "turnover-total", NumberFormat.DecFormat(dcTotalTurnover, 2));
                TemplateHandler.Assign(ref strTpl, "winlost-total", NumberFormat.DecFormat(dcTotalWinloss, 2));
                TemplateHandler.Assign(ref strTpl, "bonus-total", NumberFormat.DecFormat(dcTotalBonus, 2));
                TemplateHandler.Assign(ref strTpl, "psp-total", NumberFormat.DecFormat(dcTotalPsp, 2));
                TemplateHandler.Assign(ref strTpl, "adjustment-total", NumberFormat.DecFormat(dcTotalAdjustment, 2));
                TemplateHandler.Assign(ref strTpl, "affadjustment-total", NumberFormat.DecFormat(dcTotalAffAdjustment, 2));
                TemplateHandler.Assign(ref strTpl, "affwithdrawal-total", NumberFormat.DecFormat(dcTotalAffWithdrawal, 2));
                //TemplateHandler.Assign(ref strTpl, "affcomm-total", NumberFormat.DecFormat(dcTotalAffComm, 2));
                TemplateHandler.Assign(ref strTpl, "affbf-total", NumberFormat.DecFormat(dcTotalAffBF, 2));

                trackingRate = dcTotalClick == 0 ? 0 : dcTotalUniqueClick / dcTotalClick;
                newTrackingRate = dcTotalNewClick == 0 ? 0 : dcTotalNewUniqueClick / dcTotalNewClick;

                TemplateHandler.Assign(ref strTpl, "trackingrate-total", string.Format("{0:P2}", trackingRate));
                TemplateHandler.Assign(ref strTpl, "newtrackingrate-total", string.Format("{0:P2}", newTrackingRate));
                TemplateHandler.Assign(ref strTpl, "signup-total", dcTotalSignup);
                TemplateHandler.Assign(ref strTpl, "fdm-total", dcTotalFDM);
                TemplateHandler.Assign(ref strTpl, "activemember-total", dcTotalActiveMember);
                TemplateHandler.Assign(ref strTpl, "qm-total", dcTotalQM);
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(11));
                TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 1, 0, SubMenu.ReportText[1], "i_report.png"));
            }

            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(criteria.TotalPages, criteria.PageIndex, criteria.PageSize, criteria.NRows, "aPageTop", "txtPageTop", string.Empty, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(criteria.TotalPages, criteria.PageIndex, criteria.PageSize, criteria.NRows, "aPageBottom", "txtPageBottom", string.Empty, false, false));

        }
        catch (Exception ex)
        {
            DNAideLog.Write(ex.Message);
            #if DEBUG
                throw;
            #endif
        }
    }

    private DateTime GetLastDayOfMonth(DateTime date)
    {
        date = GetFirstDayOfMonth(date);
        date = date.AddMonths(1).AddDays(-1);
        return date > DateTime.Now ? DateTime.Now : date;
    }

    private DateTime GetFirstDayOfMonth(DateTime date)
    {
        var firstDay = new DateTime(date.Year, date.Month, 1);
        if (firstDay < AppSettings.StatementAvailableFrom)
            firstDay = AppSettings.StatementAvailableFrom;

        return firstDay;
    }
}
