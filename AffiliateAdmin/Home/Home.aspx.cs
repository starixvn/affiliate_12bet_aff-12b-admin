using System;
using System.Data;
using DNAide.Core;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Web;

public partial class Home : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("Home.htm");

        TemplateHandler.Assign(ref content, "submenu", SubMenu.GenSubMenu(new string[] { "Home.aspx" }, new string[] { Resources.Lang.Home }, 0, 0, Resources.Lang.Home, "i_home.png"));
        TemplateHandler.Assign(ref content, "HomeDesc", Resources.Lang.HomeDesc);
        TemplateHandler.Assign(ref content, "TaskWaitingApp", Resources.Lang.TaskWaitingApp);
        TemplateHandler.Assign(ref content, "Affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref content, "PaymentRequest", Resources.Lang.PaymentRequest);
        TemplateHandler.Assign(ref content, "AffStatisticsAll", Resources.Lang.AffStatisticsAll);
        TemplateHandler.Assign(ref content, "AffInPro", Resources.Lang.AffInPro);
        TemplateHandler.Assign(ref content, "Pending", Resources.Lang.Pending);
        TemplateHandler.Assign(ref content, "Approved", Resources.Lang.Approved);
        TemplateHandler.Assign(ref content, "Disapproved", Resources.Lang.Disapproved);
        TemplateHandler.Assign(ref content, "Closed", Resources.Lang.Closed);
        TemplateHandler.Assign(ref content, "MemStatAll", Resources.Lang.MemStatAll);
        TemplateHandler.Assign(ref content, "Deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref content, "NonDeposit", Resources.Lang.NonDeposit);
        TemplateHandler.Assign(ref content, "TotalDeposit", Resources.Lang.TotalDeposit);
        TemplateHandler.Assign(ref content, "TotalWinlost", Resources.Lang.TotalWinlost);
        TemplateHandler.Assign(ref content, "TotalBonus", Resources.Lang.TotalBonus);
        TemplateHandler.Assign(ref content, "TotalPSPFees", Resources.Lang.TotalPSPFees);
        TemplateHandler.Assign(ref content, "TotalAdjustment", Resources.Lang.Adjustment);
        TemplateHandler.Assign(ref content, "TotalNetWinlost", Resources.Lang.TotalNetWinlost);
        TemplateHandler.Assign(ref content, "MemInAffPro", Resources.Lang.MemInAffPro);
        TemplateHandler.Assign(ref content, "BannerStatc", Resources.Lang.BannerStatc);
        TemplateHandler.Assign(ref content, "AffMonthStatc", Resources.Lang.AffMonthStatc);
        TemplateHandler.Assign(ref content, "MemStatcMonth", Resources.Lang.MemStatcMonth);
        TemplateHandler.Assign(ref content, "BannerInPro", Resources.Lang.BannerInPro);
        TemplateHandler.Assign(ref content, "ImageBanner", Resources.Lang.ImageBanner);
        TemplateHandler.Assign(ref content, "FlashBanner", Resources.Lang.FlashBanner);
        TemplateHandler.Assign(ref content, "Username", Resources.Lang.Username);
        TemplateHandler.Assign(ref content, "LoggedIn", Resources.Lang.LoggedIn);
        TemplateHandler.Assign(ref content, "LastRequest", Resources.Lang.LastRequest);
        TemplateHandler.Assign(ref content, "Role", Resources.Lang.Role);
        TemplateHandler.Assign(ref content, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref content, "WeHave", Resources.Lang.WeHave);

        //dynamic data render
        Decimal dcPSPToday = DataHandler.Member.GetPSPToday();
        DbResult rs = DataHandler.Home.GetGenInforFromAff();
        DataTable dtb = rs.ResultSet.Tables[0];
        DataRow dr = dtb.Rows[0];

        Decimal dcPSPAll = CastAide.AsDecimal(dr["PSPFeeAll"]);
        Decimal dcPSPMonth = CastAide.AsDecimal(dr["PSPFeeMonth"]);

        TemplateHandler.Assign(ref content, "AffNum-val", CastAide.AsInt32(dr["PendingAllNum"], 0));
        TemplateHandler.Assign(ref content, "PaymentReqNum-val", CastAide.AsInt32(dr["PaymentReqNum"], 0));

        TemplateHandler.Assign(ref content, "AffInProAll-val", CastAide.AsInt32(dr["AffInProAllNum"], 0));
        TemplateHandler.Assign(ref content, "PendingAll-val", CastAide.AsInt32(dr["PendingAllNum"], 0));
        TemplateHandler.Assign(ref content, "ApprovedAll-val", CastAide.AsInt32(dr["ApprovedAllNum"], 0));
        TemplateHandler.Assign(ref content, "DisapprovedAll-val", CastAide.AsInt32(dr["DisapprovedAllNum"], 0));

        TemplateHandler.Assign(ref content, "Deposit-val", CastAide.AsInt32(dr["DepositMemAllNum"], 0));
        TemplateHandler.Assign(ref content, "TotalDepositAll-val", Common.FormatNumber(dr["DepositAll"]));
        TemplateHandler.Assign(ref content, "TotalWinlostAll-val", Common.FormatNumber(dr["WinlostAll"]));
        TemplateHandler.Assign(ref content, "TotalBonusAll-val", Common.FormatNumber(dr["BonusAll"]));
        TemplateHandler.Assign(ref content, "TotalPSPFeesAll-val", Common.FormatNumber(dcPSPAll + dcPSPToday));
        TemplateHandler.Assign(ref content, "TotalAdjustmentAll-val", Common.FormatNumber(dr["AdjustmentAll"]));
        TemplateHandler.Assign(ref content, "TotalNetWinlostAll-val", Common.FormatNumber(dr["NetWinlostAll"]));

        TemplateHandler.Assign(ref content, "BannerInPro-val", CastAide.AsInt32(dr["BannerNum"], 0));
        TemplateHandler.Assign(ref content, "ImageBanner-val", CastAide.AsInt32(dr["ImageBannerNum"], 0));
        TemplateHandler.Assign(ref content, "FlashBanner-val", CastAide.AsInt32(dr["FlashBannerNum"], 0));

        TemplateHandler.Assign(ref content, "AffInProMoth-val", CastAide.AsInt32(dr["AffInProMonthNum"], 0));
        TemplateHandler.Assign(ref content, "PendingMonth-val", CastAide.AsInt32(dr["PendingMonthNum"], 0));
        TemplateHandler.Assign(ref content, "ApprovedMonth-val", CastAide.AsInt32(dr["ApprovedMonthNum"], 0));
        TemplateHandler.Assign(ref content, "DisapprovedMonth-val", CastAide.AsInt32(dr["DisapprovedMonthNum"], 0));
        TemplateHandler.Assign(ref content, "ClosedMonth-val", CastAide.AsInt32(dr["ClosedMonthNum"], 0));
        TemplateHandler.Assign(ref content, "ClosedAllNum-val", CastAide.AsInt32(dr["ClosedAllNum"], 0));

        TemplateHandler.Assign(ref content, "DepositMonth-val", CastAide.AsInt32(dr["DepositMemMonthNum"], 0));
        TemplateHandler.Assign(ref content, "TotalDepositMonth-val", Common.FormatNumber(dr["DepositMonth"]));
        TemplateHandler.Assign(ref content, "TotalWinlostMonth-val", Common.FormatNumber(dr["WinlostMonth"]));
        TemplateHandler.Assign(ref content, "TotalBonusMonth-val", Common.FormatNumber(dr["BonusMonth"]));
        TemplateHandler.Assign(ref content, "TotalPSPFeesMonth-val", Common.FormatNumber(dcPSPMonth + dcPSPToday));
        TemplateHandler.Assign(ref content, "TotalAdjustmentMonth-val", Common.FormatNumber(dr["AdjustmentMonth"]));
        TemplateHandler.Assign(ref content, "TotalNetWinlostMonth-val", Common.FormatNumber(dr["NetWinlostMonth"]));

        DbResult rs1 = DataHandler.Home.GetGenInforFromDeposit();
        DataTable dtb1 = rs1.ResultSet.Tables[0];
        DataRow dr1 = dtb1.Rows[0];
        int nonDepositMemAllNum, NonDepositMemMonthNum;
        nonDepositMemAllNum = CastAide.AsInt32(dr1["MemInProAllNum"], 0) - CastAide.AsInt32(dr["DepositMemAllNum"], 0);
        NonDepositMemMonthNum = CastAide.AsInt32(dr1["MemInProMonthNum"], 0) - CastAide.AsInt32(dr["DepositMemMonthNum"], 0);


        TemplateHandler.Assign(ref content, "MemInAffProAll-val", CastAide.AsInt32(dr1["MemInProAllNum"], 0));
        TemplateHandler.Assign(ref content, "NonDeposit-val", nonDepositMemAllNum);
        TemplateHandler.Assign(ref content, "MemInAffProMonth-val", CastAide.AsInt32(dr1["MemInProMonthNum"], 0));
        TemplateHandler.Assign(ref content, "NonDepositMonth-val", NonDepositMemMonthNum);

        int pagesize = CastAide.AsInt32(Request["pagesize"], 20);
        int page = CastAide.AsInt32(Request["page"], 1);
        int Nrows = 0, totalpages=0;
        int NrowOnCurPage = 0;
        DbResult result;
        DataTable oldtb = new DataTable() ;

        while (NrowOnCurPage == 0)
        {
            result = DataHandler.Home.GetOnlineList(pagesize, page);
            totalpages = CastAide.AsInt32(result.Outputs["totalpages"]);
            Nrows = CastAide.AsInt32(result.Outputs["NRows"]);
            oldtb = result.ResultSet.Tables[0];
            if(page == 1)//Break out if page is equal 1
                NrowOnCurPage = -1;
            else
                NrowOnCurPage = oldtb.Rows.Count;
            if (NrowOnCurPage == 0)
            {
                page = totalpages;
            }
        }
        string olRows = string.Empty;
        string olRow = TemplateHandler.CopyBlock(content, "OLRow");
        int No = 0;
        foreach (DataRow r in oldtb.Rows)
        {
            string temp = olRow;
            TemplateHandler.Assign(ref temp, "No-val", ++No + (page-1)*pagesize);
            TemplateHandler.Assign(ref temp, "logged-in-val", Common.IBCDateTimeFormat(r["LoggedIn"],DTFormatType.DT24h));
            TemplateHandler.Assign(ref temp, "lastrequest-val", Common.IBCDateTimeFormat(r["LastRequest"],DTFormatType.DT24h));
            TemplateHandler.Assign(ref temp, "IP-val", r["IP"].ToString());
            TemplateHandler.Assign(ref temp, "ipchecker-url", Common.GetIPCheckerURL(r["IP"].ToString()));
            if (r["RoleName"].ToString() == "Admin")
            {
                TemplateHandler.Assign(ref temp, "Username-val", r["username"].ToString());
                TemplateHandler.Assign(ref temp, "Type-val", Resources.Lang.Administrator);
            }
            else
            {
                TemplateHandler.Assign(ref temp, "Username-val", "<a href=\"../Customer/AffDefault.aspx?user=" + r["username"].ToString() + "\"> " + r["username"].ToString() + "</a>");
                TemplateHandler.Assign(ref temp, "Type-val", Resources.Lang.Affiliate);
            }
            if ((No % 2) == 0)
                TemplateHandler.Assign(ref temp, "color", Common.RowBgEven);
            else
                TemplateHandler.Assign(ref temp, "color", Common.RowBgOdd);
            TemplateHandler.Assign(ref temp, "over", Common.RowBgOver);
            olRows += temp;
        }
        TemplateHandler.ReplaceBlock(ref content, "OLRow", olRows);
        TemplateHandler.Assign(ref content, "paging-top", Common.GenPaging(totalpages, page, pagesize, Nrows, "aPageTop", "txtPageTop", 950, false, true));
        TemplateHandler.Assign(ref content, "paging-bottom", Common.GenPaging(totalpages, page, pagesize, Nrows, "aPageBottom", "txtPageBottom", 950, false, false));
        TemplateHandler.Assign(ref content, "pagesize", pagesize);
        TemplateHandler.Assign(ref content, "page", page);
        TemplateHandler.Assign(ref content, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "60" : Request["slTime"]);
        this.HtmlContent = content;
    }
}
