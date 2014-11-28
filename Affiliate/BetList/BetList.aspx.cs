using System;
using System.Collections.Generic;
using AffBetlistService;

public partial class BetList_BetList : RootPageLogIn
{
    protected string betlistHtmlString = string.Empty;
    protected string betlistInfo;
    protected int custId;
    protected int originalCustId;
    protected int affId;
    protected string UserName = string.Empty;
    protected string slSportType = string.Empty;
    protected string monthdate = string.Empty;
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
            monthdate = Request["fdate"];
            betlistInfo = "Bet List: " + fromDate.ToShortDateString();
            // check belonging
            if (false)//affId != AppSettings.Session.CustID)
            {
                betlistHtmlString =
                    "<div><span style=\"color:Red;\">You are not authorized to view this data!</span></div>";
            }
            else
            {
                //betlistHtmlString = "Member Bet List " + fromDate.ToShortDateString();
                AffiliateAPI affBetlistService = new AffiliateAPI();

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
        else
        {
            betlistHtmlString = "Post back";
        }
    }
}