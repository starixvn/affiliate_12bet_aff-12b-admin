using System;
using System.Data;
//using System.Linq;
using System.Text;
using DNAide.Core;
using DNAide.Web;

public partial class UserInfo_DailyEarningDetail : RootPageLogIn
{
    private System.Globalization.CultureInfo cult = System.Globalization.CultureInfo.CurrentCulture;
    public string strTemplate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        // check come from Report Commission
        if (!AppSettings.Session.HasDailyDetail)
            Response.Redirect("StatementHistory.aspx");

        strTemplate = TemplateHandler.LoadTemplate("UserInfo/DailyEarningDetail.htm");
        int custID = CastAide.AsInt32(Request["custid"]);
        // check belonging
        if (custID != AppSettings.Session.CustID) // cheated
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "showdata");
        }
        else
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "managecheat");
        }

        DateTime _date = (Request["monthdate"] != null) ? DateTime.Parse(Request["monthdate"].ToString(), cult) : DateTime.Now.AddDays(-1);
        DateTime lastDateofMonth = new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month));


        int sporttype = CastAide.AsInt32(Request["slSportType"], 0);
        int step1 = CastAide.AsInt32(Request["step1"], 0) + 1;
        int step2 = CastAide.AsInt32(Request["step2"], 0) + 1;
        bool isComm = (string.IsNullOrEmpty(Request["iscomm"]) ? false : Request["iscomm"] == "1");


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

        TemplateHandler.Assign(ref strTemplate, "width", width);
        TemplateHandler.Assign(ref strTemplate, "custid", custID);
        TemplateHandler.Assign(ref strTemplate, "step1", step1);
        TemplateHandler.Assign(ref strTemplate, "step2", step2);
        TemplateHandler.Assign(ref strTemplate, "slSports", Common.GenListSportType(sporttype, AppSettings.Session.CustID, false));
        TemplateHandler.Assign(ref strTemplate, "monthdate", Request["monthdate"]);

        TemplateHandler.Assign(ref strTemplate, "date", _date.ToString("dd MMMM yyyy"));
        TemplateHandler.Assign(ref strTemplate, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTemplate, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTemplate, "withdrawal", Resources.Lang.Withdrawal);
        TemplateHandler.Assign(ref strTemplate, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTemplate, "turnover", Resources.Lang.Turnover);
        TemplateHandler.Assign(ref strTemplate, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTemplate, "back", Resources.Lang.Back);
        TemplateHandler.Assign(ref strTemplate, "statement", Resources.Lang.StatementHistory);
        TemplateHandler.Assign(ref strTemplate, "earningMonth", Common.ConvertDateTime(_date, "-", false, false));
        TemplateHandler.Assign(ref strTemplate, "user", CastAide.AsString(Request["user"]));
        TemplateHandler.Assign(ref strTemplate, "lastdayofmonth", Common.ConvertDateTime(lastDateofMonth, "/", true, false));
        TemplateHandler.Assign(ref strTemplate, "detaildate", Common.ConvertDateTime(_date, "/", true, false));
        TemplateHandler.Assign(ref strTemplate, "DailyEarningDetail", Resources.Lang.DailyEarningDetail);
        TemplateHandler.Assign(ref strTemplate, "Submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTemplate, "SelectDate", Resources.Lang.SelectDate);

        var maxDateOfSelectedMonth = new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month));
        var betlist_breadcrum = "<a href=\"../UserInfo/StatementHistory.aspx\">" + Resources.Lang.StatementHistory + "</a> >> ";
        betlist_breadcrum += "<b><a href=\"../UserInfo/DailyEarning.aspx?monthdate=" + maxDateOfSelectedMonth.ToShortDateString() +
                             "\">" + _date.ToString("MMMM yyyy") + "</a></b>";

        TemplateHandler.Assign(ref strTemplate, "breadcrum", betlist_breadcrum);

        DataTable dtbResult = DataHandler.UserAff.SelAffEarningDetail(custID, sporttype, _date);
        if (dtbResult.Rows.Count > 0)
        {
            string strRowStyle = "bgcgy";//bgwhite
            string strMouseStyle = "b";//bgwhite

            string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            decimal dcDeposit = 0, dcWinlost = 0, dcTurnover = 0,dcWithdrawal = 0;//, dcAdjustment = 0, dcPSP = 0, ;
            foreach (DataRow dr in dtbResult.Rows)
            {
                strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
                strMouseStyle = (strMouseStyle == "b") ? "a" : "b";

                string strTmp = strContent;
                intNo++;
                dcDeposit += CastAide.AsDecimal(dr["deposit"]);
                dcWinlost += CastAide.AsDecimal(dr["winlost"]);
                dcTurnover += CastAide.AsDecimal(dr["turnover"]);
                dcWithdrawal += CastAide.AsDecimal(dr["withdrawal"]);

                TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                TemplateHandler.Assign(ref strTmp, "ibcusername-val", CastAide.AsString(dr["ibcusername"]));

                string strUsername2 = dr["username"].ToString(); //make sure remove 12bet$ in the first position of strUsername2
                strUsername2 = (strUsername2.ToLower().IndexOf("12bet$") == 0 ? strUsername2.Substring(6, strUsername2.Length - 6) : strUsername2);

                TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);
                TemplateHandler.Assign(ref strTmp, "username-val", strUsername2);
                TemplateHandler.Assign(ref strTmp, "deposit-val", Common.FormatNumber(dr["deposit"]));
                TemplateHandler.Assign(ref strTmp, "withdrawal-val", Common.FormatNumber(dr["withdrawal"]));
                TemplateHandler.Assign(ref strTmp, "turnover-val", Common.FormatNumber(dr["turnover"]));
                TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(dr["winlost"]));

                var turnover = CastAide.AsDouble(dr["turnover"], 0);
                var exrate = CastAide.AsDouble(dr["affexrate"], 1);
                var deposit = CastAide.AsDouble(dr["deposit"], 0);

                var withdrawal = CastAide.AsDouble(dr["withdrawal"], 0);


                var url = String.Format("MemDepositInfo.aspx?user={0}&date={1}&rate={2}",
                    strUsername2, _date, exrate);

                string deposit_start =
                        "<span style=\"color:#0079FF; cursor: pointer; cursor: hand;\" onclick=\"javascript:Common.openPopup('";
                deposit_start += url + "&isdeposit={0}', '" + strUsername2 + "', 1000, 580, 150, 240);\">";


                if (deposit == 0)
                {
                    TemplateHandler.Assign(ref strTmp, "deposit-start", null);
                    TemplateHandler.Assign(ref strTmp, "deposit-end", null);
                }
                else
                {
                    TemplateHandler.Assign(ref strTmp, "deposit-start", String.Format(deposit_start, 1));
                    TemplateHandler.Assign(ref strTmp, "deposit-end", "</span>");
                }


                if (withdrawal == 0)
                {

                    TemplateHandler.Assign(ref strTmp, "withdrawal-start", null);
                    TemplateHandler.Assign(ref strTmp, "withdrawal-end", null);
                }
                else
                {
                    TemplateHandler.Assign(ref strTmp, "withdrawal-start", String.Format(deposit_start, 0));
                    TemplateHandler.Assign(ref strTmp, "withdrawal-end", "</span>");
                }

                // code for displaying betlist
                if (AppSettings.Session.HasDailyDetail) // only show link to betlist in case of Korean Agent Group
                {
                    if (sporttype != 0 && sporttype != 3 && turnover > 0) // not ALL && not Casino
                    {// replace content for URL displaying Betlist
                        string requestDate = _date.Month.ToString() + "%2F" + _date.Day.ToString() + "%2F" + _date.Year.ToString();
                        var url4Betlist = Request.ApplicationPath.TrimEnd('/') + "/";
                        url4Betlist += "BetList/BetList.aspx?fdate=" + requestDate + "&tdate=" + requestDate + "&slSportType=" + sporttype + "&ibcusername=" + Request["user"] + "&originalcustid=" + Request["custid"];

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
                        TemplateHandler.Assign(ref strTmp, "isALinkEnd", "</span>");
                    }
                    else // select ALL
                    {
                        TemplateHandler.Assign(ref strTmp, "isALinkStart", "");
                        TemplateHandler.Assign(ref strTmp, "isALinkEnd", "");
                    }
                }

                strBuild.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
            TemplateHandler.Assign(ref strTemplate, "totaldeposit", Common.FormatNumber(dcDeposit));
            TemplateHandler.Assign(ref strTemplate, "totalturnover", Common.FormatNumber(dcTurnover));
            TemplateHandler.Assign(ref strTemplate, "totalwinlost", Common.FormatNumber(dcWinlost));
            TemplateHandler.Assign(ref strTemplate, "totalwithdrawal", Common.FormatNumber(dcWithdrawal));
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTemplate, "content", Common.NoInfo(8));
            TemplateHandler.RemoveBlock(ref strTemplate, "total");
        }
        TemplateHandler.Assign(ref strTemplate, "iscomm", Request["iscomm"]);
        TemplateHandler.RemoveBlock(ref strTemplate, "directory");

        this.HtmlContent = strTemplate;
    }
}