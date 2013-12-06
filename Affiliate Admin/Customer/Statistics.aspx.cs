using System;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
public partial class Customer_Statistics : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Customer/Statistics.htm");
        int intCustId = AppSettings.Session.AffId;
        DateTime dtmFirstOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        if (!string.IsNullOrEmpty(Request["slMonth"]) && !string.IsNullOrEmpty(Request["slYear"]))
            dtmFirstOfMonth = new DateTime(Convert.ToInt32(Request["slYear"]), Convert.ToInt32(Request["slMonth"]), 1);
        #region Load language
        TemplateHandler.Assign(ref strTpl, "custid", intCustId.ToString());
        TemplateHandler.Assign(ref strTpl, "newsignup", Resources.Lang.NewSignup);
        TemplateHandler.Assign(ref strTpl, "dep", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTpl, "fdm", Resources.Lang.FDM);
        TemplateHandler.Assign(ref strTpl, "nondep", Resources.Lang.NonDeposit);
        TemplateHandler.Assign(ref strTpl, "totalplay", Resources.Lang.TotalPlay);
        TemplateHandler.Assign(ref strTpl, "totalactive", Resources.Lang.TotalActive);
        TemplateHandler.Assign(ref strTpl, "totaldeposit", Resources.Lang.TotalDeposit);
        TemplateHandler.Assign(ref strTpl, "turnover", Resources.Lang.Turnover);
        TemplateHandler.Assign(ref strTpl, "comm", Resources.Lang.Comm);
        TemplateHandler.Assign(ref strTpl, "commpercent", Resources.Lang.CommPercent);
        TemplateHandler.Assign(ref strTpl, "currentcomm", Resources.Lang.ComEarning);
        TemplateHandler.Assign(ref strTpl, "pendingcomm", Resources.Lang.PendingComm);
        TemplateHandler.Assign(ref strTpl, "balanceforward", Resources.Lang.BalanceForward);
        TemplateHandler.Assign(ref strTpl, "earning", Resources.Lang.EarningsHistory);
        TemplateHandler.Assign(ref strTpl, "last6month", Resources.Lang.Last6Month);
        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "username", CastAide.AsString(Request["user"]));
        TemplateHandler.Assign(ref strTpl, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTpl, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTpl, "trackingrate", Resources.Lang.TrackingRate);

        TemplateHandler.Assign(ref strTpl, "sp", Resources.Lang.SP);
        TemplateHandler.Assign(ref strTpl, "hr", Resources.Lang.HR);

        TemplateHandler.Assign(ref strTpl, "sportbook", Resources.Lang.Sportbook);
        TemplateHandler.Assign(ref strTpl, "horseracing", Resources.Lang.HorseRacing);

        #endregion

        TemplateHandler.Assign(ref strTpl, "month-sel", dtmFirstOfMonth.Month.ToString());
        TemplateHandler.Assign(ref strTpl, "year-sel", dtmFirstOfMonth.Year.ToString());
        TemplateHandler.Assign(ref strTpl, "static", Resources.Lang.Statistic);
        TemplateHandler.Assign(ref strTpl, "month", dtmFirstOfMonth.ToString("MMMM"));
        TemplateHandler.Assign(ref strTpl, "year", dtmFirstOfMonth.ToString("yyyy"));
        TemplateHandler.Assign(ref strTpl, "staticup", Resources.Lang.UpToNow);
        TemplateHandler.Assign(ref strTpl, "custid", AppSettings.Session.AffId);

        DbResult dep_result = DataHandler.Affiliate.SelAffOverview_DEPOSIT(intCustId, dtmFirstOfMonth);
        int newSignUp = CastAide.AsInt32(dep_result.Outputs["newsignup"]);
        int totalPlayer = CastAide.AsInt32(dep_result.Outputs["totalplayer"]);

        int newSignUpDep = CastAide.AsInt32(dep_result.Outputs["newsignupdep"]);
        int totalPlayerDep = CastAide.AsInt32(dep_result.Outputs["totalplayerdep"]);
        TemplateHandler.Assign(ref strTpl, "newsignup-val", newSignUp);
        TemplateHandler.Assign(ref strTpl, "totalplay-val", totalPlayer);

        //TemplateHandler.Assign(ref strTpl, "newsignupdep-val", newSignUpDep);
        //TemplateHandler.Assign(ref strTpl, "newsignupnondep-val", newSignUp - newSignUpDep);
        //TemplateHandler.Assign(ref strTpl, "totalplaynondep-val", totalPlayer - totalPlayerDep);
        //if (totalPlayerDep != 0)
        //    TemplateHandler.Assign(ref strTpl, "totalplaydep-val", totalPlayerDep);
        //else
        //    TemplateHandler.ReplaceBlock(ref strTpl, "totalplaydep", totalPlayerDep.ToString());

        //Assign info       
        DbResult result = DataHandler.Affiliate.SelStatistics(intCustId, dtmFirstOfMonth);
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembers", Resources.Lang.TotalQualifiedMembers);
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembers-val", CastAide.AsInt32(result.Outputs["totalqualifiedmembers"]));
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembersCasino-val", CastAide.AsInt32(result.Outputs["totalqualifiedcasino"]));
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembersHr-val", CastAide.AsInt32(result.Outputs["totalqualifiedmembershr"]));
        TemplateHandler.Assign(ref strTpl, "turnovermonth-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalturnover"])));

        int totalactiveplayer = CastAide.AsInt32(result.Outputs["totalactiveplayer"]);
        //TemplateHandler.Assign(ref strTpl, "totalactive-val", totalactiveplayer.ToString());

        if (totalactiveplayer == 0 || dtmFirstOfMonth.CompareTo(AppSettings.StatementAvailableFrom) < 0)
            TemplateHandler.ReplaceBlock(ref strTpl, "totalactive", totalactiveplayer.ToString());
        else
        {
            TemplateHandler.Assign(ref strTpl, "totalactive-val", totalactiveplayer.ToString());
            TemplateHandler.Assign(ref strTpl, "fdate", dtmFirstOfMonth.ToString("MM/dd/yyyy"));
        }
        TemplateHandler.Assign(ref strTpl, "totalactive-val", totalactiveplayer.ToString());
        
       
        TemplateHandler.Assign(ref strTpl, "newsignupdep-val", CastAide.AsInt32(result.Outputs["newsignupdep"], 0));
        //int newSignUpNonDep = newSignUp - CastAide.AsInt32(result.Outputs["newsignupdep"]);
        //TemplateHandler.Assign(ref strTpl, "newsignupnondep", (newSignUpNonDep > 0) ? newSignUpNonDep : 0);
        //totalplaydep
        //int totalpayerdepup = CastAide.AsInt32(result.Outputs["totalplayerdepup"]);
        TemplateHandler.Assign(ref strTpl, "totalplaydep-val", CastAide.AsInt32(result.Outputs["totalplayerdepup"]));
        //TemplateHandler.Assign(ref strTpl, "totalplaynondep", totalPlayer - totalpayerdepup);
        TemplateHandler.Assign(ref strTpl, "totaldeposit-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totaldeposit"])));
        TemplateHandler.Assign(ref strTpl, "turnover-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalturnoverup"])));
        TemplateHandler.Assign(ref strTpl, "totaldepositup-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totaldepositup"])));

        TemplateHandler.Assign(ref strTpl, "winlost-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalwinlost"])));
        TemplateHandler.Assign(ref strTpl, "winlostup-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalwinlostup"])));

        //Tracking Statistics        
        TemplateHandler.Assign(ref strTpl, "trackingrate-val", CastAide.AsInt32(result.Outputs["totaluniqueclicks"], 0) + "/" + CastAide.AsInt32(result.Outputs["totalclicks"], 0));
        TemplateHandler.Assign(ref strTpl, "trackingbysite-val", CastAide.AsInt32(result.Outputs["totaluniqueclicksbysite"], 0) + "/" + CastAide.AsInt32(result.Outputs["totalclicksbysite"], 0));
        TemplateHandler.Assign(ref strTpl, "fdate", dtmFirstOfMonth.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmFirstOfMonth.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy"));
        
        this.HtmlContent = strTpl;
    }
}
