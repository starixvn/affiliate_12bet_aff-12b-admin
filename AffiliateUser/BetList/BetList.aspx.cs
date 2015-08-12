using System;
using System.Collections.Generic;
using Criterias;
using AffiliateUser.AffBetlistService;

public partial class BetList_BetList : RootPageLogIn
{
    protected string betlistHtmlString = string.Empty;
    protected string betlistInfo;
    protected int custId;
    protected int originalCustId;
    protected int affId;
    protected string UserName = string.Empty;
    protected string slSportType = string.Empty;
    protected DateTime monthdate;
    protected DateTime fromDate;
    protected DateTime toDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) // Teis is the key line for avoiding the problem
        {
            // Parse arguments
            custId = int.Parse(Request["custid"]);
            originalCustId = int.Parse(Request["originalcustid"]);
            affId = int.Parse(Request["affid"]);
            fromDate = DateTime.Parse(Request["fdate"]);
            toDate = DateTime.Parse(Request["tdate"]);
            UserName = Request["UserName"];
            slSportType = Request["slSportType"];
            monthdate = new DateTime(fromDate.Year, fromDate.Month, DateTime.DaysInMonth(fromDate.Year, fromDate.Month));
            betlistInfo = "Bet List: " + fromDate.ToShortDateString();
            // check belonging
            if (affId != AppSettings.Session.CustID)
            {
                betlistHtmlString =
                    "<div><span style=\"color:Red;\">You are not authorized to view this data!</span></div>";
            }
            else
            {
                var criteria = new ValidationCriteria
                    {
                        AffId = affId,
                        MemberName = Request["UserName"]
                    };

                var result = DataHandler.MemberInfo.ValidatePermission(criteria);

                if (false)
                {
                    betlistHtmlString =
                            "<p style=\"font-size: 11px; font-weight: bold; margin: 0 0 5px 5px;\">No information is available.</p>";
                }
                else
                {
                    //betlistHtmlString = "Member Bet List " + fromDate.ToShortDateString();
                    AffiliateAPI affBetlistService = new AffiliateAPI();
                    affBetlistService.Timeout = AppSettings.BetListServiceTimeout;

                    List<Product> supportedProducts = new List<Product>();
                    supportedProducts.Add(new Product { ProductId = 1, ProductName = "Sportsbook" });
                    supportedProducts.Add(new Product { ProductId = 5, ProductName = "Playtech Casino" });
                    var userName = Request["UserName"];
                    userName = userName.Replace("12bet$", string.Empty);
                    userName = userName.Replace("12Bet$", string.Empty);
                    BetlistSOAP betlistSOAP = new BetlistSOAP
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
        }
        else
        {
            betlistHtmlString = "Post back";
        }
    }
}
