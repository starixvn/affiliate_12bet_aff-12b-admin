using System;
using System.Data;
using System.Text;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;

public partial class DailyEarning : RootPageLogIn
{
    private System.Globalization.CultureInfo cult = System.Globalization.CultureInfo.CurrentCulture;
    protected string strListSports = "";
    protected DateTime _date;
    protected void Page_Load(object sender, EventArgs e)
    {

        _date = (Request["monthdate"] != null && Request["monthdate"]!="") ? DateTime.Parse(Request["monthdate"].ToString(), cult) : DateTime.Now.AddDays(-1);
        this.ltrDailyEarning.Text = Resources.Lang.DailyEarnings;
        string strTemplate = TemplateHandler.LoadTemplate("UserInfo/ViewDaily.htm");
        //Assign header
        TemplateHandler.Assign(ref strTemplate, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref strTemplate, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTemplate, "withdrawal", Resources.Lang.Withdrawal);
        TemplateHandler.Assign(ref strTemplate, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTemplate, "bonus", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref strTemplate, "adjustment", Resources.Lang.Adjustment);
        TemplateHandler.Assign(ref strTemplate, "psp", Resources.Lang.PSP);
        TemplateHandler.Assign(ref strTemplate, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTemplate, "monthdate", Request["monthdate"]);
        if (AppSettings.Session.HasDailyDetail)
            TemplateHandler.RemoveBlock(ref strTemplate, "nodetail");
        else
            TemplateHandler.RemoveBlock(ref strTemplate, "hasdetail");

        int SportType = CastAide.AsInt32(Request["slSportType"], 1);
        //Peter@20091231 Only enable to view SportsBook
        //int SportType = 1;
        if (SportType == 2)//if sporttype is horse racing, remove columns bonus, deposit, adjustment
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "sportbookonly");
            TemplateHandler.RemoveBlock(ref strTemplate, "totalsportbookonly");
            TemplateHandler.RemoveBlock(ref strTemplate, "sportbookonlyhead");
            TemplateHandler.Assign(ref strTemplate, "width-val", 250);

        }
        else if (SportType == 3)//Casino, remove adjustment
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "notcasino");
        }
        strListSports = Common.GenListSportType(SportType, AppSettings.Session.CustID);

        TemplateHandler.Assign(ref strTemplate, "custid", AppSettings.Session.CustID);
        TemplateHandler.Assign(ref strTemplate, "sporttype", SportType);

        try
        { 
            //Get data and assign value
            DbResult result = DataHandler.UserAff.SelAffEarningDaily(AppSettings.Session.CustID, SportType, _date);
            DataTable dtbResult = result.ResultSet.Tables[0];
            if (dtbResult.Rows.Count > 0)
            {
                string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                decimal dcDeposit = 0,dcWithdrawal=0, dcWinlost = 0, dcBonus = 0, dcAdjustment = 0, dcPSP = 0,dcRealWilost=0
                      ,oddsFeePercent= CastAide.AsDecimal(result.Outputs["oddsfeepercent"]),marketingPercent=CastAide.AsDecimal(result.Outputs["marketingpercent"])
                      , pspFeePercent = CastAide.AsDecimal(result.Outputs["pspfeepercent"]);
                string strRowStyle = "bgcgy";//bgwhite
                string strMouseStyle = "b";//bgwhite
                foreach (DataRow dr in dtbResult.Rows)
                {
                    strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
                    strMouseStyle = (strMouseStyle == "b") ? "a" : "b";
                    string strTmp = strContent;
                    /*decimal dcPercent = CastAide.AsDecimal(dr["PercentReceived"]);*/
                    intNo++;
                    decimal winlostFake=CastAide.AsDecimal(dr["memwinlostfake"]);
                    dcDeposit += CastAide.AsDecimal(dr["deposit"]);
                    dcWithdrawal += CastAide.AsDecimal(dr["withdrawal"]);                    
                    dcBonus += CastAide.AsDecimal(dr["bonus"]);
                    dcAdjustment += CastAide.AsDecimal(dr["adjustment"]);
                    dcPSP += CastAide.AsDecimal(dr["psp"]);
                    dcRealWilost += CastAide.AsDecimal(dr["memberwinlost"]);
                    if (intNo ==dtbResult.Rows.Count && Convert.ToDateTime(dr["winlostdate"]).AddDays(1).CompareTo(DateTime.Now.AddDays(-1))>0)
                    {
                        winlostFake = -(-dcRealWilost - Math.Abs(dcRealWilost) * (oddsFeePercent + marketingPercent) - dcPSP * pspFeePercent) - dcWinlost;
                    }
                    dcWinlost +=winlostFake ;
                    TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                    TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);
                    TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                    TemplateHandler.Assign(ref strTmp, "date-val", Common.ConvertDateTime(dr["winlostdate"], "/", true, false));
                    if (AppSettings.Session.HasDailyDetail)
                        TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(dr["memberwinlost"]));
                    else
                        TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(winlostFake));

                    TemplateHandler.Assign(ref strTmp, "monthdate-val", Common.ConvertDateTime(dr["winlostdate"], "/", true, false));
                    if (SportType != 2)
                    {
                        TemplateHandler.Assign(ref strTmp, "deposit-val", Common.FormatNumber(dr["deposit"]));
                        TemplateHandler.Assign(ref strTmp, "withdrawal-val", Common.FormatNumber(dr["withdrawal"]));
                        TemplateHandler.Assign(ref strTmp, "bonus-val", Common.FormatNumber(dr["bonus"]));
                        TemplateHandler.Assign(ref strTmp, "adjustment-val", Common.FormatNumber(dr["adjustment"]));
                    }
                    
                    TemplateHandler.Assign(ref strTmp, "psp-val", Common.FormatNumber(dr["psp"]));
                    strBuild.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
                if (AppSettings.Session.HasDailyDetail)
                    TemplateHandler.Assign(ref strTemplate, "totalwinlost", Common.FormatNumber(dcRealWilost));
                else
                    TemplateHandler.Assign(ref strTemplate, "totalwinlost", Common.FormatNumber(dcWinlost));

                if (SportType != 2)
                {
                    TemplateHandler.Assign(ref strTemplate, "totaldeposit", Common.FormatNumber(dcDeposit));
                    TemplateHandler.Assign(ref strTemplate, "totalwithdrawal", Common.FormatNumber(dcWithdrawal));
                    TemplateHandler.Assign(ref strTemplate, "totalbonus", Common.FormatNumber(dcBonus));
                    TemplateHandler.Assign(ref strTemplate, "totaladjustment", Common.FormatNumber(dcAdjustment));
                    TemplateHandler.Assign(ref strTemplate, "totalpsp", Common.FormatNumber(dcPSP));
                }
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTemplate, "content", Common.NoInfo(6));
                TemplateHandler.RemoveBlock(ref strTemplate, "total");
            }
            this.ltrViewDaily.Text = strTemplate;
        }
        catch (Exception ex)
        {
            DNAideLog.Write(ex.Message);
        }
    }
}
