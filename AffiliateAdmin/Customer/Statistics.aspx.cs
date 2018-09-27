using System;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;
public partial class Customer_Statistics : IBCPage
{


    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Customer/Statistics.htm");
        int intCustId = AppSettings.Session.AffId;
        var dtmFirstOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var selectedMonth = CastAide.AsDateTime(Request["fmonth"], dtmFirstOfMonth);

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
        TemplateHandler.Assign(ref strTpl, "trackingrate", Resources.Lang.TrackingRate);
        TemplateHandler.Assign(ref strTpl, "trackingrateforpc", Resources.Lang.TrackingRateForPC);
        TemplateHandler.Assign(ref strTpl, "trackingrateformobile", Resources.Lang.TrackingRateForMobile);

        TemplateHandler.Assign(ref strTpl, "sp", Resources.Lang.SP);
        TemplateHandler.Assign(ref strTpl, "hr", Resources.Lang.HR);

        TemplateHandler.Assign(ref strTpl, "sportbook", Resources.Lang.Sportbook);
        TemplateHandler.Assign(ref strTpl, "horseracing", Resources.Lang.HorseRacing);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);

        #endregion
        TemplateHandler.Assign(ref strTpl, "static", Resources.Lang.Statistic);
        TemplateHandler.Assign(ref strTpl, "staticup", Resources.Lang.UpToNow);
        TemplateHandler.Assign(ref strTpl, "custid", AppSettings.Session.AffId);
        TemplateHandler.Assign(ref strTpl, "fmonth", selectedMonth.ToString("MMM-yyyy"));
        TemplateHandler.Assign(ref strTpl, "currentYear-val", DateTime.Now.Year);
        TemplateHandler.Assign(ref strTpl, "currentMonth-val", DateTime.Now.Month);

        var playerDetailUrl = VirtualPathUtility.ToAbsolute("~/Portal/UserPanel/PlayerDetail/Index?isFromStatistics=true");
        TemplateHandler.Assign(ref strTpl, "playerDetailUrl", playerDetailUrl);

        var playerDetailByMonthUrl = $"{playerDetailUrl}&monthDate={selectedMonth.ToString("MM/dd/yyyy")}";
        TemplateHandler.Assign(ref strTpl, "playerDetailByMonthUrl", playerDetailByMonthUrl);

        //Assign info
        DbResult result = DataHandler.Affiliate.SelStatistics(intCustId, selectedMonth);
        int newSignUp = CastAide.AsInt32(result.Outputs["newsignup"], 0);
        int totalPlayer = CastAide.AsInt32(result.Outputs["totalplayer"], 0);
        TemplateHandler.Assign(ref strTpl, "newsignup-val", newSignUp.ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "totalplay-val", totalPlayer.ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembers", Resources.Lang.TotalQualifiedMembers);
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembers-val", CastAide.AsInt32(result.Outputs["totalqualifiedmembers"]).ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "totalqualifiedmembersHr-val", CastAide.AsInt32(result.Outputs["totalqualifiedmembershr"]).ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "turnovermonth-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalturnover"])));

        int totalactiveplayer = CastAide.AsInt32(result.Outputs["totalactiveplayer"]);

        if (totalactiveplayer == 0 || dtmFirstOfMonth.CompareTo(AppSettings.StatementAvailableFrom) < 0)
            TemplateHandler.ReplaceBlock(ref strTpl, "totalactive", totalactiveplayer.ToString(Common.NumberFormatPlaceholder));
        else
        {
            TemplateHandler.Assign(ref strTpl, "totalactive-val", totalactiveplayer.ToString(Common.NumberFormatPlaceholder));
            TemplateHandler.Assign(ref strTpl, "fdate", selectedMonth.ToString("MM/dd/yyyy"));
        }

        TemplateHandler.Assign(ref strTpl, "totalactive-val", totalactiveplayer.ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "newsignupdep-val", CastAide.AsInt32(result.Outputs["newsignupdep"], 0).ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "totalplaydep-val", CastAide.AsInt32(result.Outputs["totalplayerdepup"]).ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "totaldeposit-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totaldeposit"])));
        TemplateHandler.Assign(ref strTpl, "turnover-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalturnoverup"])));
        TemplateHandler.Assign(ref strTpl, "totaldepositup-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totaldepositup"])));
        TemplateHandler.Assign(ref strTpl, "winlost-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalwinlost"])));
        TemplateHandler.Assign(ref strTpl, "winlostup-val", Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalwinlostup"])));

        //Tracking Statistics
        TemplateHandler.Assign(ref strTpl, "trackingrate-val", CastAide.AsInt32(result.Outputs["totaluniqueclicks"], 0).ToString(Common.NumberFormatPlaceholder) + "/" + CastAide.AsInt32(result.Outputs["totalclicks"], 0).ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "trackingforpcbysite-val", CastAide.AsInt32(result.Outputs["totaluniqueclicksbysite"], 0).ToString(Common.NumberFormatPlaceholder) + "/" + CastAide.AsInt32(result.Outputs["totalclicksbysite"], 0).ToString(Common.NumberFormatPlaceholder));
        TemplateHandler.Assign(ref strTpl, "trackingformobilebysite-val", CastAide.AsInt32(result.Outputs["totaluniqueclicksbymobile"], 0).ToString(Common.NumberFormatPlaceholder) + "/" + CastAide.AsInt32(result.Outputs["totalclicksbymobile"], 0).ToString(Common.NumberFormatPlaceholder));

        TemplateHandler.Assign(ref strTpl, "fdate", selectedMonth.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", selectedMonth.AddMonths(1).AddDays(-1).ToString("MM/dd/yyyy"));

        this.HtmlContent = strTpl;
    }
}
