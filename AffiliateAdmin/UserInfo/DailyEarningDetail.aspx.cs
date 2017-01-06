using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;

public partial class DailyEarningDetail : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime _date = (Request["monthdate"] != null) ? CastAide.AsDateTime(Request["monthdate"]) : DateTime.Now.AddDays(-1);
        var currentmonthdate = new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month));
        int custID = CastAide.AsInt32(Request["custid"]);
        int sporttype = (string.IsNullOrEmpty(Request["slSportType"]) ? 1 : (CastAide.AsInt32(Request["slSportType"].ToString(), 1)));
        bool isComm = (string.IsNullOrEmpty(Request["iscomm"]) ? false : Request["iscomm"] == "1");
        string monthdate = (Request["originalmonthdate"] != null) ? Request["originalmonthdate"].ToString() : DateTime.Now.AddDays(-1).ToString();
        string username = (Request["user"] != null) ? Request["user"].ToString() : "";

        string strTemplate = TemplateHandler.LoadTemplate("UserInfo/ViewDailyDetail.htm");

        int width = 780;
        if (sporttype == 2)
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "sportbook");
            width = 250;
        }
        else if (sporttype == 3)
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "casino");
            width = 420;
        }

        //TemplateHandler.Assign(ref strTemplate, "slSportTypeValue", sporttypevalue);
        TemplateHandler.Assign(ref strTemplate, "width", width);
        TemplateHandler.Assign(ref strTemplate, "custid", custID);
        TemplateHandler.Assign(ref strTemplate, "slSports", Common.GenListSportType(sporttype));
        TemplateHandler.Assign(ref strTemplate, "monthdate", Request["monthdate"]);

        TemplateHandler.Assign(ref strTemplate, "detailearnings", Resources.Lang.DetailEarning);
        TemplateHandler.Assign(ref strTemplate, "date", _date.ToString("dd MMMM yyyy"));
        TemplateHandler.Assign(ref strTemplate, "ibcusername", Resources.Lang.IBCUsername);
        TemplateHandler.Assign(ref strTemplate, "12username", Resources.Lang.Bet12Username);

        TemplateHandler.Assign(ref strTemplate, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTemplate, "withdrawal", Resources.Lang.Withdrawal);
        TemplateHandler.Assign(ref strTemplate, "bonus", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref strTemplate, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTemplate, "adjustment", Resources.Lang.Adjustment);
        TemplateHandler.Assign(ref strTemplate, "psp", Resources.Lang.PSP);
        TemplateHandler.Assign(ref strTemplate, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTemplate, "back", Resources.Lang.Back);
        TemplateHandler.Assign(ref strTemplate, "earningMonth", _date.ToString("MMMM yyyy"));
        TemplateHandler.Assign(ref strTemplate, "user", CastAide.AsString(Request["user"]));
        TemplateHandler.Assign(ref strTemplate, "uservalue", CastAide.AsString(Request["user"]));
        TemplateHandler.Assign(ref strTemplate, "originalmonthdate", (Request["originalmonthdate"] != null) ? Request["originalmonthdate"].ToString() : DateTime.Now.AddDays(-1).ToString());

        if (isComm)
        {
            TemplateHandler.Assign(ref strTemplate, "statementurl", "DailyEarning.aspx?custid=" + custID + "&monthdate=" + currentmonthdate + "&user=" + username + "&iscomm=1&slSportType=" + sporttype);
            TemplateHandler.Assign(ref strTemplate, "commissionarea", "<a href=\"../Report/Commission.aspx\">" + Resources.Lang.Commission + "</a> >> ");
            TemplateHandler.Assign(ref strTemplate, "earningMonthurl", "DailyEarning.aspx?custid=" + custID + "&monthdate=" + currentmonthdate + "&user=" + username + "&iscomm=1&slSportType=" + sporttype);
            TemplateHandler.Assign(ref strTemplate, "statement", Resources.Lang.StatementHistory + " (" + Request["user"] + ")");
            TemplateHandler.Assign(ref strTemplate, "iscommvalue", "1");
        }
        else
        {
            TemplateHandler.Assign(ref strTemplate, "statementurl", "EarningsHistory.aspx");//"DailyEarning.aspx?custid=" + custID + "&monthdate=" + Request["originalmonthdate"].ToString() + "&user=" + Request["user"]);
            TemplateHandler.Assign(ref strTemplate, "commissionarea", "");
            TemplateHandler.Assign(ref strTemplate, "earningMonthurl", "DailyEarning.aspx?custid=" + custID + "&monthdate=" + currentmonthdate + "&user=" + Request["user"] + "&slSportType=" + sporttype);
            TemplateHandler.Assign(ref strTemplate, "statement", Resources.Lang.StatementHistory);
            TemplateHandler.Assign(ref strTemplate, "iscommvalue", "");
        }

        DataTable dtbResult = DataHandler.Affiliate.SelAffEarningDetail(custID, sporttype, _date);
        if (dtbResult.Rows.Count > 0)
        {
            string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
            TemplateHandler.Assign(ref strContent, "over", Common.RowBgOver);
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            decimal dcDeposit = 0, dcWinlost = 0, dcBonus = 0, dcAdjustment = 0, dcPSP = 0, dcWithdrawal = 0;
            foreach (DataRow dr in dtbResult.Rows)
            {
                string strTmp = strContent;
                intNo++;
                dcDeposit += CastAide.AsDecimal(dr["deposit"]);
                dcWinlost += CastAide.AsDecimal(dr["winlost"]);
                dcBonus += CastAide.AsDecimal(dr["bonus"]);
                dcAdjustment += CastAide.AsDecimal(dr["adjustment"]);
                dcPSP += CastAide.AsDecimal(dr["psp"]);
                dcWithdrawal += CastAide.AsDecimal(dr["withdrawal"]);

                TemplateHandler.Assign(ref strTmp, "color", (intNo % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                TemplateHandler.Assign(ref strTmp, "ibcusername-val", CastAide.AsString(dr["ibcusername"]));

                string strUsername2 = dr["username"].ToString();//make sure remove 12bet$ in the first position of strUsername2
                                                                //strUsername2 = (strUsername2.IndexOf("12Bet$") == 0 ? strUsername2.Substring(6, strUsername2.Length - 6) : strUsername2);

                TemplateHandler.Assign(ref strTmp, "12username-val", strUsername2);
                TemplateHandler.Assign(ref strTmp, "deposit-val", Common.FormatNumber(dr["deposit"]));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", Common.FormatNumber(dr["withdrawal"]));
                TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(dr["winlost"]));
                TemplateHandler.Assign(ref strTmp, "bonus-val", Common.FormatNumber(dr["bonus"]));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", Common.FormatNumber(dr["adjustment"]));
                TemplateHandler.Assign(ref strTmp, "psp-val", Common.FormatNumber(dr["psp"]));

                if (sporttype != 0 && sporttype != 3) // not ALL && not Casino
                {// replace content for URL displaying Betlist
                    string requestDate = _date.Month.ToString() + "%2F" + _date.Day.ToString() + "%2F" + _date.Year.ToString();
                    var url4Betlist = Request.ApplicationPath.TrimEnd('/') + "/";
                    url4Betlist += "BetList/BetList.aspx?fdate=" + requestDate + "&tdate=" + requestDate + "&slSportType=" + sporttype + "&ibcusername=" + Request["user"] + "&originalcustid=" + Request["custid"];
                    url4Betlist += "&iscomm=" + Request["iscomm"];

                    switch (sporttype)
                    {
                        case 0: // all sports
                            url4Betlist += "&product=All&CustId=" + int.Parse(dr["custid"].ToString()) + "&AffId=" + custID.ToString() + "&UserName=" + CastAide.AsString(dr["username"]);
                            break;

                        case 1: // sportsbook
                            url4Betlist += "&product=Sportsbook&CustId=" + int.Parse(dr["custid"].ToString()) + "&AffId=" + custID.ToString() + "&UserName=" + CastAide.AsString(dr["username"]);
                            break;

                        case 3: // Casino
                            url4Betlist += "&product=Casino&CustId=" + int.Parse(dr["custid"].ToString()) + "&AffId=" + custID.ToString() + "&UserName=" + CastAide.AsString(dr["username"]);
                            break;
                    }

                    string isALinkStart = "<a href=\"" + url4Betlist + "\">";

                    // Betlist's username
                    TemplateHandler.Assign(ref strTmp, "isALinkStart", isALinkStart);
                    TemplateHandler.Assign(ref strTmp, "isALinkEnd", "</a>");
                }
                else // select ALL
                {
                    TemplateHandler.Assign(ref strTmp, "isALinkStart", "");
                    TemplateHandler.Assign(ref strTmp, "isALinkEnd", "");
                }

                strBuild.Append(strTmp);
            }

            TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
            TemplateHandler.Assign(ref strTemplate, "totaldeposit", Common.FormatNumber(dcDeposit));
            TemplateHandler.Assign(ref strTemplate, "totalwithdrawal", Common.FormatNumber(dcWithdrawal));
            TemplateHandler.Assign(ref strTemplate, "totalwinlost", Common.FormatNumber(dcWinlost));
            TemplateHandler.Assign(ref strTemplate, "totalbonus", Common.FormatNumber(dcBonus));
            TemplateHandler.Assign(ref strTemplate, "totaladjustment", Common.FormatNumber(dcAdjustment));
            TemplateHandler.Assign(ref strTemplate, "totalpsp", Common.FormatNumber(dcPSP));
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", Common.NoInfo(8));
            TemplateHandler.RemoveBlock(ref strTemplate, "total");
        }

        #region Load SubPage

        if (!isComm)
            TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 0, 0, Resources.Lang.StatementHistory, "i_customer.png"));
        else
        {
            TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 5, 0, SubMenu.ReportText[3], "i_report.png"));
            TemplateHandler.Assign(ref strTemplate, "iscomm", Request["iscomm"]);
            TemplateHandler.RemoveBlock(ref strTemplate, "directory");
        }

        #endregion Load SubPage

        this.HtmlContent = strTemplate;
    }
}