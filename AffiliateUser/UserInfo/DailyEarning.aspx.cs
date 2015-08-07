using System;
using System.Data;
using System.Text;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using AffiliateUser;

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
        if (AppSettings.Session.CommGroupName != "Korean Agent Comm")
        {
            TemplateHandler.RemoveBlock(ref strTemplate, "withdrawalhead");
            TemplateHandler.RemoveBlock(ref strTemplate, "withdrawalbody");
            TemplateHandler.RemoveBlock(ref strTemplate, "withdrawalfooter");
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
                
                StringBuilder strBuild = new StringBuilder();

                string strRowStyle = "bgcgy";//bgwhite
                string strMouseStyle = "b";//bgwhite
                int intNo = 0;
                foreach (DataRow dr in dtbResult.Rows)
                {
                    strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
                    strMouseStyle = (strMouseStyle == "b") ? "a" : "b";
                    string strTmp = strContent;
                    
                    intNo++;
                    TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                    TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);
                    TemplateHandler.Assign(ref strTmp, "no", intNo.ToString());
                    TemplateHandler.Assign(ref strTmp, "date-val", Common.ConvertDateTime(dr["winlostdate"], "/", true, false));

                    if (AppSettings.Session.HasDailyDetail)
                        TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(CastAide.AsDecimal(dr["memberwinlost"])));
                    else
                        TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(CastAide.AsDecimal(dr["memwinlostfake"])));

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
                
                MapTotalRow(result.ResultSet.Tables[1], SportType, ref strTemplate);
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
            Fanex.Logging.LogService.Error(ex.Message);
        }
    }

    private void MapTotalRow(DataTable dtbResult, int sportType, ref string strTemplate)
    {
        if (dtbResult.Rows.Count > 0)
        {
            foreach (DataRow dr in dtbResult.Rows)
            {
                if (AppSettings.Session.HasDailyDetail)
                    TemplateHandler.Assign(ref strTemplate, "totalwinlost", Common.FormatNumber(CastAide.AsDecimal(dr["memberwinlost"])));
                else
                    TemplateHandler.Assign(ref strTemplate, "totalwinlost", Common.FormatNumber(CastAide.AsDecimal(dr["memwinlostfake"])));

                if (sportType != 2)
                {
                    TemplateHandler.Assign(ref strTemplate, "totaldeposit", Common.FormatNumber(dr["deposit"]));
                    TemplateHandler.Assign(ref strTemplate, "totalwithdrawal", Common.FormatNumber(dr["withdrawal"]));
                    TemplateHandler.Assign(ref strTemplate, "totalbonus", Common.FormatNumber(dr["bonus"]));
                    TemplateHandler.Assign(ref strTemplate, "totaladjustment", Common.FormatNumber(dr["adjustment"]));
                    TemplateHandler.Assign(ref strTemplate, "totalpsp", Common.FormatNumber(dr["psp"]));
                }
            }
        }
    }
}
