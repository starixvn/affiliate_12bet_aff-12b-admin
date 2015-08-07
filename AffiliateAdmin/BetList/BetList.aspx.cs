using System;
using System.Collections.Generic;
using Criterias;
using DNAide.Core;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Web;
using AffiliateAdmin.AffBetlistService;

public partial class BetList_BetList : IBCPage
{
    protected string betlistHtmlString = string.Empty;
    protected string betlistInfo;
    protected int custId;
    protected int affId;
    protected DateTime fromDate;
    protected DateTime toDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) // Teis is the key line for avoiding the problem
        {
            // Parse arguments
            custId = int.Parse(Request["custid"]);
            affId = int.Parse(Request["affid"]);
            fromDate = DateTime.Parse(Request["fdate"]);
            toDate = DateTime.Parse(Request["tdate"]);
            bool isComm = (string.IsNullOrEmpty(Request["iscomm"]) ? false : Request["iscomm"] == "1");
            var commission = Request.ApplicationPath.TrimEnd('/') + "/Report/Commission.aspx";
            var statement = Request.ApplicationPath.TrimEnd('/') + "/UserInfo/EarningsHistory.aspx?iscomm=" + Request["iscomm"] + "&user=" + Request["ibcusername"];
            var monthdate = new DateTime(fromDate.Year, fromDate.Month, DateTime.DaysInMonth(fromDate.Year, fromDate.Month));
            var dailyEarnings = Request.ApplicationPath.TrimEnd('/') + "/UserInfo/DailyEarning.aspx?";
            dailyEarnings += "custid=" + Request["originalcustid"] + "&monthdate=" + monthdate.ToShortDateString();
            dailyEarnings += "&user=" + Request["ibcusername"] + "&slSportType=" + Request["slSportType"] + "&iscomm=" + Request["iscomm"];
            var dailyEarningDetails = Request.ApplicationPath.TrimEnd('/') + "/UserInfo/DailyEarningDetail.aspx?";
            dailyEarningDetails += "custid=" + Request["originalcustid"] + "&monthdate=" + Request["fdate"];
            dailyEarningDetails += "&originalmonthdate=" + DateTime.Today.ToShortDateString();
            dailyEarningDetails += "&user=" + Request["ibcusername"] + "&slSportType=" + Request["slSportType"] + "&iscomm=" + Request["iscomm"];

            betlistInfo = "Bet List: " + fromDate.ToShortDateString();
            // check belonging
            if (false) //custId != AppSettings.Session.CustID)
            {
                betlistHtmlString =
                    "<div><span style=\"color:Red;\">You are not authorized to view this data!</span></div>";
            }
            else
            {
                var criteria = new ValidationCriteria
                    {
                        AffId = affId,
                        MemberName = Request["UserName"] // original usernaem "12bet$username"
                    };

                var result = DataHandler.Affiliate.ValidatePermission(criteria);

                if (!result)
                {
                    betlistHtmlString = "<p style=\"font-size: 11px; font-weight: bold; margin: 5px 0;\">No information is available.</p>";
                }
                else
                {
                    var userName = Request["UserName"];
                    userName = userName.Replace("12bet$", string.Empty);
                    userName = userName.Replace("12Bet$", string.Empty);

                    var affBetlistService = new AffiliateAPI();
                    affBetlistService.Timeout = AppSettings.BetListServiceTimeout;

                    var supportedProducts = new List<Product>();
                    supportedProducts.Add(new Product {ProductId = 1, ProductName = "Sportsbook"});
                    supportedProducts.Add(new Product {ProductId = 5, ProductName = "Playtech Casino"});

                    var betlistSOAP = new BetlistSOAP
                        {
                            AffiliateName = "12bet",
                            Username = userName,
                            Product = Request["product"],
                            FromDate = fromDate,
                            ToDate = toDate,
                            ExchangeRate = 1, // no currency exchange
                            SupportedProducts = supportedProducts.ToArray()
                        };

                    betlistHtmlString += affBetlistService.GetBetlist(betlistSOAP);
                }
            }

            var subMenuHtmlString = string.Empty;
            if (!isComm)
            {
                subMenuHtmlString = SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 0, 0,
                                                          Resources.Lang.StatementHistory, "i_customer.png");
            }
            else
            {
                subMenuHtmlString = SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 5, 0,
                                                          SubMenu.ReportText[3], "i_report.png");
            }
            
            var strTemplate = TemplateHandler.LoadTemplate("UserInfo/BetList.htm");
            TemplateHandler.Assign(ref strTemplate, "submenu", subMenuHtmlString);
            if (!isComm)
            {
                TemplateHandler.Assign(ref strTemplate, "commission", string.Empty);
            }
            else
            {
                TemplateHandler.Assign(ref strTemplate, "commission", "<a href=\"" + commission + "\">Commission</a> >> ");
            }

            TemplateHandler.Assign(ref strTemplate, "statement", "<a href=\"" + dailyEarnings + "\">Statement History (" + Request["ibcusername"] + ")</a>");
            TemplateHandler.Assign(ref strTemplate, "dailyearnings", "<a href=\"" + dailyEarnings + "\">" + fromDate.ToString("MMMM yyyy") + "</a>");
            TemplateHandler.Assign(ref strTemplate, "dailyearningdetails", "<a href=\"" + dailyEarningDetails + "\">" + fromDate.ToString("dd MMMM yyyy") + "</a>");
            TemplateHandler.Assign(ref strTemplate, "username", Request["UserName"]);
            TemplateHandler.Assign(ref strTemplate, "betlistInfo", betlistInfo);


            TemplateHandler.Assign(ref strTemplate, "affid-value", affId);
            TemplateHandler.Assign(ref strTemplate, "betlistHtmlString", betlistHtmlString);
            this.HtmlContent = strTemplate;
        }
        else
        {
            betlistHtmlString = "Post back";
        }
    }
}