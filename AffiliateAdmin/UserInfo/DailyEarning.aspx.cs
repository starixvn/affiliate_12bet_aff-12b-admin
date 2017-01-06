using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;

public partial class DailyEarning : IBCPage
{
    private System.Globalization.CultureInfo cult = System.Globalization.CultureInfo.CurrentCulture;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strTemplate = TemplateHandler.LoadTemplate("UserInfo/ViewDaily.htm");

        TemplateHandler.Assign(ref strTemplate, "dailyearnings", Resources.Lang.DailyEarnings);
        TemplateHandler.Assign(ref strTemplate, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref strTemplate, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTemplate, "bonus", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref strTemplate, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTemplate, "withdrawal", Resources.Lang.Withdrawal);
        TemplateHandler.Assign(ref strTemplate, "adjustment", Resources.Lang.Adjustment);
        TemplateHandler.Assign(ref strTemplate, "psp", Resources.Lang.PSP);
        TemplateHandler.Assign(ref strTemplate, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTemplate, "back", Resources.Lang.Back);
        TemplateHandler.Assign(ref strTemplate, "originalmonthdate", (Request["monthdate"] != null) ? Request["monthdate"].ToString() : DateTime.Now.AddDays(-1).ToString());
        DateTime _date = (Request["monthdate"] != null) ? DateTime.Parse(Request["monthdate"].ToString(), cult) : DateTime.Now.AddDays(-1);

        int step = CastAide.AsInt32(Request["hdStep"], 0) + 1;
        int custID = CastAide.AsInt32(Request["custid"]);
        int sporttype = (string.IsNullOrEmpty(Request["slSportType"]) ? 1 : (CastAide.AsInt32(Request["slSportType"].ToString(), 1)));
        string monthdate = (Request["monthdate"] != null) ? Request["monthdate"].ToString() : DateTime.Now.AddDays(-1).ToString();
        string username = (Request["user"] != null) ? Request["user"].ToString() : "";
        bool isComm = (string.IsNullOrEmpty(Request["iscomm"]) ? false : Request["iscomm"] == "1");

        if (isComm)
        {
            TemplateHandler.Assign(ref strTemplate, "statementurl", "DailyEarning.aspx?custid=" + custID + "&monthdate=" + monthdate + "&user=" + Request["user"] + "&iscomm=1&slSportType=" + sporttype);
            TemplateHandler.Assign(ref strTemplate, "commissionarea", "<a href=\"../Report/Commission.aspx\">" + Resources.Lang.Commission + "</a> >> ");
            TemplateHandler.Assign(ref strTemplate, "statement", Resources.Lang.StatementHistory + " (" + Request["user"] + ")");
            TemplateHandler.Assign(ref strTemplate, "iscommvalue", "1");
        }
        else
        {
            TemplateHandler.Assign(ref strTemplate, "statementurl", "EarningsHistory.aspx");//"DailyEarning.aspx?custid=" + custID + "&monthdate=" + Request["originalmonthdate"].ToString() + "&user=" + Request["user"] + "&slSportType=" + Request["slSportType"]);
            TemplateHandler.Assign(ref strTemplate, "commissionarea", "");
            TemplateHandler.Assign(ref strTemplate, "statement", Resources.Lang.StatementHistory);
            TemplateHandler.Assign(ref strTemplate, "iscommvalue", "");
        }

        int width = 600;
        if (sporttype == 2)
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "sportbook");
            width = 250;
        }
        else if (sporttype == 3)
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "notcasino");
        }
        else if (sporttype == 3)
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "casino");
            width = 400;
        }

        TemplateHandler.Assign(ref strTemplate, "custid", custID);
        TemplateHandler.Assign(ref strTemplate, "sporttype", sporttype);
        TemplateHandler.Assign(ref strTemplate, "monthdate", monthdate);
        TemplateHandler.Assign(ref strTemplate, "user", username);
        TemplateHandler.Assign(ref strTemplate, "uservalue", username);
        TemplateHandler.Assign(ref strTemplate, "earningMonth", _date.ToString("MMMM yyyy"));
        TemplateHandler.Assign(ref strTemplate, "width", width);

        TemplateHandler.Assign(ref strTemplate, "slSports", Common.GenListSportType(sporttype));

        DataTable dtbResult = DataHandler.Affiliate.SelAffEarningDaily(custID, sporttype, _date);
        if (dtbResult.Rows.Count > 0)
        {
            string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
            TemplateHandler.Assign(ref strContent, "over", Common.RowBgOver);
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            decimal dcWinlost = 0, dcDeposit = 0, dcBonus = 0, dcAdjust = 0, dcPSP = 0, dcWithdrawal = 0;
            foreach (DataRow dr in dtbResult.Rows)
            {
                string strTmp = strContent;
                intNo++;
                dcWinlost += CastAide.AsDecimal(dr["MemberWinlost"]);
                dcDeposit += CastAide.AsDecimal(dr["deposit"]);
                dcBonus += CastAide.AsDecimal(dr["bonus"]);
                dcAdjust += CastAide.AsDecimal(dr["adjustment"]);
                dcPSP += CastAide.AsDecimal(dr["psp"]);
                dcWithdrawal += CastAide.AsDecimal(dr["withdrawal"]);

                TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                TemplateHandler.Assign(ref strTmp, "date-val", CastAide.AsDateTime(dr["winlostdate"]).ToString("dd MMMM yyyy"));
                TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(dr["MemberWinlost"]));
                TemplateHandler.Assign(ref strTmp, "deposit-val", Common.FormatNumber(dr["deposit"]));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", Common.FormatNumber(dr["withdrawal"]));
                TemplateHandler.Assign(ref strTmp, "bonus-val", Common.FormatNumber(dr["bonus"]));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", Common.FormatNumber(dr["adjustment"]));
                TemplateHandler.Assign(ref strTmp, "psp-val", Common.FormatNumber(dr["psp"]));
                TemplateHandler.Assign(ref strTmp, "custid", custID.ToString());
                TemplateHandler.Assign(ref strTmp, "monthdate-val", CastAide.AsDateTime(dr["winlostdate"]).ToShortDateString());
                TemplateHandler.Assign(ref strTmp, "omonth", Request["monthdate"].ToString());

                if (isComm)
                {
                    TemplateHandler.Assign(ref strTmp, "showIsComm", "&iscomm=1");
                }
                else
                {
                    TemplateHandler.Assign(ref strTmp, "showIsComm", "");
                }

                strBuild.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
            TemplateHandler.Assign(ref strTemplate, "totalwinlost", Common.FormatNumber(dcWinlost));
            TemplateHandler.Assign(ref strTemplate, "totaldeposit", Common.FormatNumber(dcDeposit));
            TemplateHandler.Assign(ref strTemplate, "totalwithdrawal", Common.FormatNumber(dcWithdrawal));
            TemplateHandler.Assign(ref strTemplate, "totalbonus", Common.FormatNumber(dcBonus));
            TemplateHandler.Assign(ref strTemplate, "totaladjustment", Common.FormatNumber(dcAdjust));
            TemplateHandler.Assign(ref strTemplate, "totalpsp", Common.FormatNumber(dcPSP));
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", Common.NoInfo(7));
            TemplateHandler.RemoveBlock(ref strTemplate, "total");
        }

        #region Load SubPage

        if (!isComm)
            TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 0, 0, Resources.Lang.StatementHistory, "i_customer.png"));
        else
        {
            TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 5, 0, SubMenu.ReportText[3], "i_report.png"));
            TemplateHandler.Assign(ref strTemplate, "iscommvalue", Request["iscomm"]);
            TemplateHandler.RemoveBlock(ref strTemplate, "directory");
        }

        #endregion Load SubPage

        this.HtmlContent = strTemplate;
    }
}