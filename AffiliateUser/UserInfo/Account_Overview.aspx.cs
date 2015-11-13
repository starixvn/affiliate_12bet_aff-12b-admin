using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;
using AffiliateUser.Business.DataAccess;
using DataHandler;
using DNAide.Core;
using DNAide.Web;

public partial class Account_Overview : RootPageLogIn
{
    protected string strMonths = string.Empty;
    protected string strYears = string.Empty;
    protected string slMonth = string.Empty;
    protected string slYear = string.Empty;
    protected string strTierSetting = string.Empty;
    public const int MAXQM = 1000000000;
    public const Int64 MAXAMOUNT = 100000000000000;
    public string flashPath = string.Empty;
    public string showPopup = string.Empty;
    public DateTime selectedMonth;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Load language
        ltrAccOverview.Text = Resources.Lang.AccOverview;
        ltrNewSignup.Text = Resources.Lang.NewSignup;
        ltrTotalPlay.Text = Resources.Lang.TotalPlay;
        ltrComm.Text = Resources.Lang.CurrentCommissiSetting;
        BalanceBroughtText.Text = Resources.Lang.BalForward;
        ltrTotalActive.Text = Resources.Lang.TotalActive;
        ltrTotalDeposit.Text = Resources.Lang.TotalDeposit;
        ltrTurnover.Text = Resources.Lang.TurnoverUpToNow;
        ltrDepositUp.Text = Resources.Lang.TotalDeposit;

        #endregion

        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        var dtmFirstOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        selectedMonth = CastAide.AsDateTime(Request["fmonth"], dtmFirstOfMonth);

        strMonths = dtmFirstOfMonth.Month.ToString();
        strYears = dtmFirstOfMonth.Year.ToString();

        GenMonthSel();
        slYear = Common.GenYearDropDown("slYear", strYears, false, null);

        ltrStatic.Text = GetStatisticsFor(selectedMonth);
        ltrStaticUp.Text = Resources.Lang.Statistic + " " + Resources.Lang.UpToNow;

        var dep_result = UserAff.SelAffOverview_DEPOSIT(AppSettings.Session.CustID, selectedMonth);
        ltrSignup.Text = CastAide.AsInt32(dep_result.Outputs["newsignup"], 0).ToString(Common.NumberFormatPlaceholder);
        ltrTotalPlayer.Text = CastAide.AsInt32(dep_result.Outputs["totalplayer"], 0).ToString(Common.NumberFormatPlaceholder);

        var result = UserAff.SelAffOverview(AppSettings.Session.CustID, selectedMonth);
        ltrTotalActiveVal.Text = CastAide.AsInt32(result.Outputs["totalactiveplayer"], 0).ToString(Common.NumberFormatPlaceholder);
        ltrTotalDepositVal.Text = Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totaldeposit"]));

        ltrDepositUpVal.Text = Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totaldepositup"]));
        ltrTurnoverVal.Text = Common.FormatNumber(CastAide.AsDecimal(result.Outputs["totalturnoverup"]));

        BalanceBroughtVal.Text = Common.FormatNumber(result.Outputs["balbroughtforward"]);
        ltrTurnoverMonth.Text = Common.FormatNumber(result.Outputs["totalturnover"]);
        //With totalQM=1--1, it means this month did not have the theory of QM
        int totalQM = CastAide.AsInt32(result.Outputs["totalqualifiedmembers"], -1);
        int totalCAQM = CastAide.AsInt32(result.Outputs["totalqualifiedcasino"], -1);
        ltrTotalQualifiedMembers.Text = (totalQM == -1 || totalCAQM == -1) ? "N/A" : totalQM.ToString(Common.NumberFormatPlaceholder) + "/" + totalCAQM.ToString(Common.NumberFormatPlaceholder);

        //set flash path
        string lang = AppSettings.Session.Language;
        switch (lang)
        {
            case "vi-VN":
                flashPath = "../Flash/Vietnamese/vn.swf";
                break;
            case "es-ES":
                flashPath = "../Flash/Spanish/spanish.swf";
                break;
            case "zh-CN":
                flashPath = "../Flash/Simplified/sc.swf";
                break;
            case "zh-TW":
                flashPath = "../Flash/Tradition/tc.swf";
                break;
            case "id-ID":
                flashPath = "../Flash/Indonesian/indo.swf";
                break;
            case "th-TH":
                flashPath = "../Flash/Thai/thai.swf";
                break;
            default:
                flashPath = "../Flash/English/en.swf";
                break;
        }
        if (AppSettings.Session.IsAuthenticated && AppSettings.Session.FirsTimeLoginSucc)
        {
            showPopup = "";
            AppSettings.Session.FirsTimeLoginSucc = false;
        }
        else
        {
            showPopup = "none";
        }

        strTierSetting = GenCommGroupSettingDetails();
    }

    private string GetStatisticsFor(DateTime dtmFirstOfMonth)
    {
        var statisticsFor = Resources.Lang.Statistic + " " + Resources.Lang.For + " " +
                         Common.ConvertDateTime(dtmFirstOfMonth, "-", false, false);

        if (AppSettings.Session.Language.Equals("ko-KR", StringComparison.OrdinalIgnoreCase))
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime Tmp1 = Convert.ToDateTime(dtmFirstOfMonth, culture);

            statisticsFor = Resources.Lang.For + " " + Resources.Lang.Statistic + " " +
                             string.Format("{0} {1} {2}", dtmFirstOfMonth.Year, Resources.Lang.Year,
                                           Tmp1.ToString("MMMM"));
        }

        return statisticsFor;
    }

    private void GenMonthSel()
    {
        slMonth = TemplateHandler.LoadTemplate("SelMonth.htm");
        TemplateHandler.Assign(ref slMonth, "jan", Resources.Lang.January);
        TemplateHandler.Assign(ref slMonth, "feb", Resources.Lang.February);
        TemplateHandler.Assign(ref slMonth, "mar", Resources.Lang.March);
        TemplateHandler.Assign(ref slMonth, "apr", Resources.Lang.April);
        TemplateHandler.Assign(ref slMonth, "may", Resources.Lang.May);
        TemplateHandler.Assign(ref slMonth, "jun", Resources.Lang.June);
        TemplateHandler.Assign(ref slMonth, "jul", Resources.Lang.July);
        TemplateHandler.Assign(ref slMonth, "aug", Resources.Lang.August);
        TemplateHandler.Assign(ref slMonth, "sep", Resources.Lang.September);
        TemplateHandler.Assign(ref slMonth, "oct", Resources.Lang.October);
        TemplateHandler.Assign(ref slMonth, "nov", Resources.Lang.November);
        TemplateHandler.Assign(ref slMonth, "dec", Resources.Lang.December);
    }

    private String GenCommGroupSettingDetails()
    {
        string tbl_CommSetting = TemplateHandler.LoadTemplate("UserInfo/CommGroupSetting.htm");
        DbResult result = null;

        //Assign text header of table
        TemplateHandler.Assign(ref tbl_CommSetting, "Sport", Resources.Lang.Sport);
        TemplateHandler.Assign(ref tbl_CommSetting, "Tier", Resources.Lang.Tier);
        TemplateHandler.Assign(ref tbl_CommSetting, "Commission", Resources.Lang.Comm);
        TemplateHandler.Assign(ref tbl_CommSetting, "QM", Resources.Lang.QualifyingMem);
        TemplateHandler.Assign(ref tbl_CommSetting, "ComNetWin", Resources.Lang.NetWinLoss);
        TemplateHandler.Assign(ref tbl_CommSetting, "MinAmount_Explain", Resources.Lang.MinAmt_explain);
        TemplateHandler.Assign(ref tbl_CommSetting, "MinQM_Explain", Resources.Lang.MinQM_explain);

        result = CommGroup.GetCommGroupSettingByAffId(AppSettings.Session.CustID);

        if (result.ResultSet.Tables.Count < 5 || result.ResultSet.Tables[0].Rows.Count <= 0
            || result.ResultSet.Tables[1].Rows.Count <= 0
            || result.ResultSet.Tables[3].Rows.Count <= 0
            || result.ResultSet.Tables[4].Rows.Count <= 0)
        {
            return string.Empty;
        }

        DataTable SportType = result.ResultSet.Tables[3];
        DataTable GroupSetting = result.ResultSet.Tables[1];

        string mainRow = TemplateHandler.CopyBlock(tbl_CommSetting, "Mainrow");
        //string deductionOption = TemplateHandler.CopyBlock(tbl_CommSetting, "DeductionOption");
        string tierRow = TemplateHandler.CopyBlock(tbl_CommSetting, "subrow");
        var ret = new StringBuilder();
        int noSports = 0;

        foreach (DataRow sport in SportType.Rows)
        {
            if (String.Compare(CastAide.AsString(sport["SportName"]), "Horse Racing", true) != 0 && (AppSettings.ShowCasino || String.Compare(CastAide.AsString(sport["SportName"]), "Casino", true) != 0))
            {
                if (GroupSetting.Select("SportType=" + sport["SportID"]).Length >= 1)
                {
                    string _sportSetting = mainRow;
                    TemplateHandler.Assign(ref _sportSetting, "sports-type", sport["SportName"]);

                    noSports++;
                    TemplateHandler.Assign(ref _sportSetting, "class-main", (noSports % 2 == 0) ? "tbl_cs" : "tbl_spb");
                    TemplateHandler.Assign(ref _sportSetting, "class", (noSports % 2 == 0) ? "tbl_01" : "tbl_td_01");

                    var tierList = new StringBuilder();

                    //Load Tier Setting
                    DataRow[] Tiers = GroupSetting.Select("SportType=" + sport["SportID"]);

                    int noTier = 0;
                    int totalShownTier = Tiers.Length;
                    foreach (DataRow tier in Tiers)
                    {
                        if (CastAide.AsDouble(tier["Commission"]) != 0)
                        {
                            if (noTier == 0)
                            {
                                TemplateHandler.Assign(ref _sportSetting, "tier-no1", ++noTier);
                                TemplateHandler.Assign(ref _sportSetting, "color", "#F4E9E9");
                                //Only allow to delete last tier

                                TemplateHandler.Assign(ref _sportSetting, "minam-val1", Common.FormatNumber(Convert.ToInt64(tier["MinAmount"])));//( String.Format("{0:0.00}", Convert.ToInt64(tier["MinAmount"])));
                                TemplateHandler.Assign(ref _sportSetting, "minqm-val1", Convert.ToInt32(tier["MinQM"]));
                                if (noTier <= totalShownTier - 1)
                                {
                                    TemplateHandler.Assign(ref _sportSetting, "maxqm-val1", " - " + GetMaxQMDisplay(tier["MaxQM"]));
                                    TemplateHandler.Assign(ref _sportSetting, "maxam-val1", " - " + Common.FormatNumber(Convert.ToInt64((tier["MaxAmount"])) + 0.99));
                                }
                                else
                                {
                                    TemplateHandler.Assign(ref _sportSetting, "maxqm-val1", GetMaxQMDisplay(tier["MaxQM"]));
                                    TemplateHandler.Assign(ref _sportSetting, "maxam-val1", GetMaxAmountDisplay(tier["MaxAmount"]));
                                }
                                TemplateHandler.Assign(ref _sportSetting, "Commission-val1", (Convert.ToDouble(tier["Commission"]) * 100).ToString() + "%");
                            }
                            else
                            {
                                string _tierRow = tierRow;
                                TemplateHandler.Assign(ref _tierRow, "tier-no", ++noTier);
                                TemplateHandler.Assign(ref _tierRow, "class", (noSports % 2 == 0) ? "tbl_01" : "tbl_td_01");
                                if (noTier % 2 != 0)
                                {
                                    TemplateHandler.Assign(ref _tierRow, "color", "#F4E9E9");
                                }
                                //Only allow to delete last tier

                                TemplateHandler.Assign(ref _tierRow, "minam-val", Common.FormatNumber(Convert.ToInt64(tier["MinAmount"])));//String.Format("{0:0.00}", Convert.ToInt64(tier["MinAmount"])));
                                TemplateHandler.Assign(ref _tierRow, "minqm-val", Convert.ToInt32(tier["MinQM"]));
                                if (noTier <= totalShownTier - 1)
                                {
                                    TemplateHandler.Assign(ref _tierRow, "maxam-val", " - " + Common.FormatNumber(Convert.ToInt64((tier["MaxAmount"])) + 0.99));
                                    TemplateHandler.Assign(ref _tierRow, "maxqm-val", " - " + GetMaxQMDisplay(tier["MaxQM"]));
                                }
                                else
                                {
                                    TemplateHandler.Assign(ref _tierRow, "maxam-val", GetMaxAmountDisplay(tier["MaxAmount"]));
                                    TemplateHandler.Assign(ref _tierRow, "maxqm-val", GetMaxQMDisplay(tier["MaxQM"]));
                                }
                                TemplateHandler.Assign(ref _tierRow, "Commission-val", (Convert.ToDouble(tier["Commission"]) * 100).ToString() + "%");
                                tierList.Append(_tierRow);
                            }
                        }
                        else
                        {
                            totalShownTier--;
                        }
                    }
                    TemplateHandler.Assign(ref _sportSetting, "tierno-val", noTier);

                    if (tierList.Length == 0)
                    {
                        //TemplateHandler.ReplaceBlock(ref _sportSetting, "Mainrow", Common.NoInfo(5));
                        if (noTier == 1)
                            TemplateHandler.ReplaceBlock(ref _sportSetting, "subrow", string.Empty);
                        else
                            _sportSetting = Common.NoInfo(5);

                    }
                    else
                    {
                        TemplateHandler.ReplaceBlock(ref _sportSetting, "subrow", tierList.ToString());
                    }

                    if (tierList.Length == 0)
                    {
                        TemplateHandler.ReplaceBlock(ref _sportSetting, "TierRow", Common.NoInfo(7));
                    }
                    else
                    {
                        TemplateHandler.ReplaceBlock(ref _sportSetting, "TierRow", tierList.ToString());
                    }
                    ret.Append(_sportSetting);
                    if (tierList.Length == 0)
                        break;
                }
            }
        }
        TemplateHandler.ReplaceBlock(ref tbl_CommSetting, "Mainrow", ret.ToString());
        return tbl_CommSetting;
    }
    public static string GetMaxQMDisplay(object max)
    {
        if (Convert.ToInt32(max) == MAXQM)
        {
            return "+";
        }
        return Convert.ToInt32(max).ToString();
    }
    public static string GetMaxAmountDisplay(object max)
    {
        if (Convert.ToInt64(max) == MAXAMOUNT)
        {
            return "+";
        }
        return String.Format("{0:0.00}", Convert.ToDouble(max) + 0.99);
    }
}
