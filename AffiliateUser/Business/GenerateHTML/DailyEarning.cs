using System;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;

/// <summary>
/// Thai@20121105
/// </summary>
namespace GenerateHTML
{
    public class DailyEarning
    {
        public static string DailyEarningList(int sportType, DateTime date)
        {
            string strTemplate = TemplateHandler.LoadTemplate("UserInfo/ViewDaily.htm");
            //Assign header
            TemplateHandler.Assign(ref strTemplate, "date", Resources.Lang.Date);
            TemplateHandler.Assign(ref strTemplate, "deposit", Resources.Lang.Deposit);
            TemplateHandler.Assign(ref strTemplate, "winlost", Resources.Lang.Winlost);
            TemplateHandler.Assign(ref strTemplate, "bonus", Resources.Lang.Bonus);
            TemplateHandler.Assign(ref strTemplate, "adjustment", Resources.Lang.Adjustment);
            TemplateHandler.Assign(ref strTemplate, "psp", Resources.Lang.PSP);
            TemplateHandler.Assign(ref strTemplate, "total", Resources.Lang.Total);

            if (sportType == 2)//if sporttype is horse racing, remove columns bonus, deposit, adjustment
            {
                TemplateHandler.RemoveBlock(ref strTemplate, "sportbookonly");
                TemplateHandler.RemoveBlock(ref strTemplate, "totalsportbookonly");
                TemplateHandler.RemoveBlock(ref strTemplate, "sportbookonlyhead");
                TemplateHandler.Assign(ref strTemplate, "width-val", 250);
            }
            else if (sportType == 3)//Casino, remove adjustment
            {
                TemplateHandler.RemoveBlock(ref strTemplate, "notcasino");
                TemplateHandler.RemoveBlock(ref strTemplate, "notcasinototal");
                TemplateHandler.RemoveBlock(ref strTemplate, "notcasinohead");                
            }

            //Get data and assign value
            var result = DataHandler.UserAff.SelAffEarningDaily(AppSettings.Session.CustID, sportType, date);
            DataTable dtbResult = result.ResultSet.Tables[0];
            if (dtbResult.Rows.Count > 0)
            {
                string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                decimal dcDeposit = 0, dcWinlost = 0, dcBonus = 0, dcAdjustment = 0, dcPSP = 0, dcRealWilost = 0
                      , oddsFeePercent = CastAide.AsDecimal(result.Outputs["oddsfeepercent"]), marketingPercent = CastAide.AsDecimal(result.Outputs["marketingpercent"])
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
                    decimal winlostFake = CastAide.AsDecimal(dr["memwinlostfake"]);
                    dcDeposit += CastAide.AsDecimal(dr["deposit"]);
                    dcBonus += CastAide.AsDecimal(dr["bonus"]);
                    dcAdjustment += CastAide.AsDecimal(dr["adjustment"]);
                    dcPSP += CastAide.AsDecimal(dr["psp"]);
                    dcRealWilost += CastAide.AsDecimal(dr["memberwinlost"]);
                    if (intNo == dtbResult.Rows.Count && Convert.ToDateTime(dr["winlostdate"]).AddDays(1).CompareTo(DateTime.Now.AddDays(-1)) > 0)
                    {
                        winlostFake = -(-dcRealWilost - Math.Abs(dcRealWilost) * (oddsFeePercent + marketingPercent) - dcPSP * pspFeePercent) - dcWinlost;
                    }
                    dcWinlost += winlostFake;
                    TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                    TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);
                    TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                    TemplateHandler.Assign(ref strTmp, "date-val", Common.ConvertDateTime(dr["winlostdate"], "/", true, false));
                    TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(winlostFake));
                    if (sportType != 2)//Horse racing
                    {
                        TemplateHandler.Assign(ref strTmp, "deposit-val", Common.FormatNumber(dr["deposit"]));
                        TemplateHandler.Assign(ref strTmp, "bonus-val", Common.FormatNumber(dr["bonus"]));
                        if (sportType != 3)//Casino
                        {
                            TemplateHandler.Assign(ref strTmp, "adjustment-val", Common.FormatNumber(dr["adjustment"]));
                        }
                    }                    
                    
                    strBuild.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
                TemplateHandler.Assign(ref strTemplate, "totalwinlost", Common.FormatNumber(dcWinlost));
                if (sportType != 2)//Horse racing
                {
                    TemplateHandler.Assign(ref strTemplate, "totaldeposit", Common.FormatNumber(dcDeposit));
                    TemplateHandler.Assign(ref strTemplate, "totalbonus", Common.FormatNumber(dcBonus));
                    if (sportType != 3)//Casino
                    {
                        TemplateHandler.Assign(ref strTemplate, "totaladjustment", Common.FormatNumber(dcAdjustment));
                    }                    
                }
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTemplate, "content", Common.NoInfo(6));
                TemplateHandler.RemoveBlock(ref strTemplate, "total");
            }
            
            return strTemplate;
        }      
    }
}
