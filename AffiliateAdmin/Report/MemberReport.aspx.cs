using System;
using System.Collections;
using System.Text;
using System.Data;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;

public partial class MemberReport : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("Report/MemberReport.htm");
        
        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }
    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "tdate-txt", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "memreport", Resources.Lang.ReportDetails);
        TemplateHandler.Assign(ref strTpl, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref strTpl, "currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref strTpl, "units", Resources.Lang.Units);
        TemplateHandler.Assign(ref strTpl, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTpl, "turnover", Resources.Lang.Turnover);
        TemplateHandler.Assign(ref strTpl, "bonus", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref strTpl, "memwinlost", Resources.Lang.MemberWinlost);
        TemplateHandler.Assign(ref strTpl, "affwinlost", Resources.Lang.AffWinlost);
        TemplateHandler.Assign(ref strTpl, "housewinlost", Resources.Lang.HouseWinLost);
        TemplateHandler.Assign(ref strTpl, "affnetpos", Resources.Lang.AffNetWinLoss);
        TemplateHandler.Assign(ref strTpl, "housenetwinlost", Resources.Lang.HouseNetWinLost);
        TemplateHandler.Assign(ref strTpl, "original", Resources.Lang.Original);
        TemplateHandler.Assign(ref strTpl, "usdeqv", Resources.Lang.UsdEqv);
        TemplateHandler.Assign(ref strTpl, "subtotal", Resources.Lang.SubTotal);
        TemplateHandler.Assign(ref strTpl, "back", Resources.Lang.Back);
        TemplateHandler.Assign(ref strTpl, "datelimit", Resources.Lang.LimitDate + AppSettings.StatementAvailableFrom.ToString("d-MMM-yy"));
        //Assign color
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
        TemplateHandler.Assign(ref strTpl, "color", Common.RowBgOdd);
        TemplateHandler.Assign(ref strTpl, "colormonthly", Common.RowBgPending);
        
        //Hung added
        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
        
    }

    void GenerateHTML()
    {
        try
        {
            int intCustId = 0;
            string strMenuText = string.Empty, strUsername = string.Empty;
            if (string.Compare(Request["isaff"], "1", true) == 0)
            {
                if (!string.IsNullOrEmpty(Request["custid"]) && !string.IsNullOrEmpty(Request["username"]))
                {
                    intCustId = CastAide.AsInt32(Request["custid"]);
                    strUsername = CastAide.AsString(Request["username"]);
                }
                else
                {
                    intCustId = AppSettings.Session.AffId;
                    strUsername = AppSettings.Session.AffUsername;
                }
                TemplateHandler.Assign(ref strTpl, "pagepadd", string.Empty);
                TemplateHandler.Assign(ref strTpl, "username", strUsername);
                TemplateHandler.Assign(ref strTpl, "username-val", strUsername + " --- ");
                TemplateHandler.Assign(ref strTpl, "custid", intCustId);
                strMenuText = Resources.Lang.ReportDetails + " [" + strUsername + "]";
                //TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 1, 0, Resources.Lang.Report, "i_customer.png"));
            }
            else
            {
                TemplateHandler.Assign(ref strTpl, "pagepadd", "padl0");
                TemplateHandler.Assign(ref strTpl, "username", string.Empty);
                TemplateHandler.Assign(ref strTpl, "username-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "custid", string.Empty);
                strMenuText = Resources.Lang.ReportDetails;
            }
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.ReportLink, SubMenu.ReportText, 0, 0, SubMenu.ReportText[0], "i_report.png"));

            TemplateHandler.Assign(ref strTpl, "isaff-val", Request["isaff"]);
                       
            DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddDays(-1));
            TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
            if (dtmFDate < AppSettings.StatementAvailableFrom)
                dtmFDate = AppSettings.StatementAvailableFrom;
            DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
            TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
            
            DataTable dtb = new DataTable();
            DbResult rs = null;
            if (intCustId != 0)
            {
                TemplateHandler.Assign(ref strTpl, "hwidth", "170");
                rs = DataHandler.Affiliate.SelMemberReportByAff(intCustId, dtmFDate, dtmTDate,0);
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "username", string.Empty);
                TemplateHandler.Assign(ref strTpl, "hwidth", "100");
                rs = DataHandler.Affiliate.SelMemberReport(dtmFDate, dtmTDate,0);
            }
            if(rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];
            if (dtb.Rows.Count > 0)
            {
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                string strSubTpl = TemplateHandler.ReplaceBlock(ref strTpl, "subtotal", "");
                string strTotal = TemplateHandler.ReplaceBlock(ref strTpl, "total", "");
                string strDate = string.Empty;
                int intCount = 0, numRow = 0;
                decimal dcDeposit = 0, dcTurnover = 0, dcBonus = 0, dcMemWinloss = 0, dcAffWinloss = 0, dcHouseWinloss = 0, dcAffNetPos = 0, dcHouseNetWinloss = 0;
                Hashtable hsTotal = new Hashtable();//Use for Total monthly
                ArrayList arr = new ArrayList();
                strListRow = new StringBuilder();
                StringBuilder strRow = new StringBuilder();
                int intMonth = 0;
                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmpDate = CastAide.AsDateTime(dr["Winlostdate"]).ToString("d-MMM-yy ddd");
                    string strTmp = TemplateHandler.CopyBlock(strRowTpl, "currency");
                    string strNewRow = string.Empty;
                    //Load 1 row without date value
                    decimal dcActualRate = CastAide.AsDecimal(dr["actualrate"]);

                    //Add new 1 column here
                    TemplateHandler.Assign(ref strTmp, "currency-val", dr["Currency"].ToString());
                    TemplateHandler.Assign(ref strTmp, "units-val", NumberFormat.DecFormat(dcActualRate, 5));
                    TemplateHandler.Assign(ref strTmp, "deposit-val", NumberFormat.DecFormat(dr["deposit"], 2));
                    TemplateHandler.Assign(ref strTmp, "depositeqv-val", Common.FormatNumber(CastAide.AsDecimal(dr["deposit"]) * dcActualRate));
                    TemplateHandler.Assign(ref strTmp, "turnover-val", NumberFormat.DecFormat(dr["Turnover"], 2));
                    TemplateHandler.Assign(ref strTmp, "turnovereqv-val", Common.FormatNumber(CastAide.AsDecimal(dr["Turnover"]) * dcActualRate));
                    TemplateHandler.Assign(ref strTmp, "bonus-val", NumberFormat.DecFormat(dr["Bonus"], 2));
                    TemplateHandler.Assign(ref strTmp, "bonuseqv-val", Common.FormatNumber(CastAide.AsDecimal(dr["Bonus"]) * dcActualRate));
                    TemplateHandler.Assign(ref strTmp, "memwinloss-val", NumberFormat.DecFormat(dr["MemberWinLost"], 2));
                    TemplateHandler.Assign(ref strTmp, "memwinlosseqv-val", Common.FormatNumber(CastAide.AsDecimal(dr["MemberWinLost"]) * dcActualRate));
                    TemplateHandler.Assign(ref strTmp, "affwinloss-val", NumberFormat.DecFormat(dr["AffiliateWinLost"], 2));
                    TemplateHandler.Assign(ref strTmp, "affwinlosseqv-val", Common.FormatNumber(CastAide.AsDecimal(dr["AffiliateWinLost"]) * dcActualRate));
                    TemplateHandler.Assign(ref strTmp, "housewinloss-val", NumberFormat.DecFormat(dr["HouseWinLost"], 2));
                    TemplateHandler.Assign(ref strTmp, "housewinlosseqv-val", Common.FormatNumber(CastAide.AsDecimal(dr["HouseWinLost"]) * dcActualRate));
                    TemplateHandler.Assign(ref strTmp, "affnetpos-val", NumberFormat.DecFormat(dr["AffiliateNetWinLost"], 2));
                    TemplateHandler.Assign(ref strTmp, "affnetposeqv-val", Common.FormatNumber(CastAide.AsDecimal(dr["AffiliateNetWinLost"]) * dcActualRate));
                    TemplateHandler.Assign(ref strTmp, "housenetwinloss-val", NumberFormat.DecFormat(dr["HouseNetWinLost"], 2));
                    TemplateHandler.Assign(ref strTmp, "housenetwinlosseqv-val", Common.FormatNumber(CastAide.AsDecimal(dr["HouseNetWinLost"]) * dcActualRate));

                    numRow++;                    
                    if (string.Compare(strDate, strTmpDate, true) != 0)//New row
                    {
                        //hsTotal = new Hashtable();
                        #region insert old row to list rows
                        string strOldRow = strRow.ToString();
                        TemplateHandler.Assign(ref strOldRow, "numrow", intCount.ToString());
                        strRow = new StringBuilder();
                        strListRow.Append(strOldRow);
                        #endregion

                        if (!string.IsNullOrEmpty(strDate))//insert subtotal row for list rows before
                        {
                            GenerateSubTotal(strRow, strSubTpl, intCount, ref dcDeposit, ref dcTurnover, ref dcBonus, ref dcMemWinloss, ref dcAffWinloss, ref dcHouseWinloss, ref dcAffNetPos, ref dcHouseNetWinloss);//Sub total                            
                        }
                        strNewRow = strRowTpl;
                        TemplateHandler.Assign(ref strNewRow, "date-val", strTmpDate);
                        TemplateHandler.ReplaceBlock(ref strNewRow, "currency", strTmp);
                        strRow.Append(strNewRow);

                        strDate = strTmpDate;
                        intCount = 0;
                    }
                    dcDeposit += CastAide.AsDecimal(dr["deposit"]) * dcActualRate;
                    dcTurnover += CastAide.AsDecimal(dr["Turnover"]) * dcActualRate;
                    dcBonus += CastAide.AsDecimal(dr["Bonus"]) * dcActualRate;
                    dcMemWinloss += CastAide.AsDecimal(dr["MemberWinLost"]) * dcActualRate;
                    dcAffWinloss += CastAide.AsDecimal(dr["AffiliateWinLost"]) * dcActualRate;
                    dcHouseWinloss += CastAide.AsDecimal(dr["HouseWinLost"]) * dcActualRate;
                    dcAffNetPos += CastAide.AsDecimal(dr["AffiliateNetWinLost"]) * dcActualRate;
                    dcHouseNetWinloss += CastAide.AsDecimal(dr["HouseNetWinLost"]) * dcActualRate;

                    int intCurrMonth = CastAide.AsDateTime(dr["Winlostdate"]).Month;
                    if (intMonth != intCurrMonth)
                    {
                        //Assign Monthly row
                        if(intMonth != 0)
                            GenerateMonthlyRow(strTotal, hsTotal);
                        intMonth = intCurrMonth;
                        hsTotal = new Hashtable();
                    }
                    if (hsTotal.ContainsKey(dr["Currency"]))
                    {
                        Hashtable hsCurrency = (Hashtable)hsTotal[dr["Currency"]];
                        hsCurrency["Deposit"] = CastAide.AsDecimal(hsCurrency["Deposit"]) + (CastAide.AsDecimal(dr["Deposit"]) * dcActualRate);
                        hsCurrency["Turnover"] = CastAide.AsDecimal(hsCurrency["Turnover"]) + (CastAide.AsDecimal(dr["Turnover"]) * dcActualRate);
                        hsCurrency["Bonus"] = CastAide.AsDecimal(hsCurrency["Bonus"]) + (CastAide.AsDecimal(dr["Bonus"]) * dcActualRate);
                        hsCurrency["MemberWinLost"] = CastAide.AsDecimal(hsCurrency["MemberWinLost"]) + (CastAide.AsDecimal(dr["MemberWinLost"]) * dcActualRate);
                        hsCurrency["AffiliateWinLost"] = CastAide.AsDecimal(hsCurrency["AffiliateWinLost"]) + (CastAide.AsDecimal(dr["AffiliateWinLost"]) * dcActualRate);
                        hsCurrency["HouseWinLost"] = CastAide.AsDecimal(hsCurrency["HouseWinLost"]) + (CastAide.AsDecimal(dr["HouseWinLost"]) * dcActualRate);
                        hsCurrency["AffiliateNetWinLost"] = CastAide.AsDecimal(hsCurrency["AffiliateNetWinLost"]) + (CastAide.AsDecimal(dr["AffiliateNetWinLost"]) * dcActualRate);
                        hsCurrency["HouseNetWinLost"] = CastAide.AsDecimal(hsCurrency["HouseNetWinLost"]) + (CastAide.AsDecimal(dr["HouseNetWinLost"]) * dcActualRate);

                        hsTotal[dr["Currency"]] = hsCurrency;
                    }
                    else
                    {
                        Hashtable hsCurrency = new Hashtable();
                        hsCurrency.Add("Deposit", CastAide.AsDecimal(dr["Deposit"]) * dcActualRate);
                        hsCurrency.Add("Turnover", CastAide.AsDecimal(dr["Turnover"]) * dcActualRate);
                        hsCurrency.Add("Bonus", CastAide.AsDecimal(dr["Bonus"]) * dcActualRate);
                        hsCurrency.Add("MemberWinLost", CastAide.AsDecimal(dr["MemberWinLost"]) * dcActualRate);
                        hsCurrency.Add("AffiliateWinLost", CastAide.AsDecimal(dr["AffiliateWinLost"]) * dcActualRate);
                        hsCurrency.Add("HouseWinLost", CastAide.AsDecimal(dr["HouseWinLost"]) * dcActualRate);
                        hsCurrency.Add("AffiliateNetWinLost", CastAide.AsDecimal(dr["AffiliateNetWinLost"]) * dcActualRate);
                        hsCurrency.Add("HouseNetWinLost", CastAide.AsDecimal(dr["HouseNetWinLost"]) * dcActualRate);
                        hsCurrency["Currency"] = dr["Currency"];
                        //hsCurrency.Add("Rate", CastAide.AsDecimal(dr["actualrate"]));
                        hsTotal.Add(dr["Currency"], hsCurrency);
                    }

                    if (string.IsNullOrEmpty(strNewRow))//New row but is not first row of date
                        strRow.Append("<tr class=\"nowrap\" bgcolor=\"" + Common.RowBgOdd + "\" onmouseout=\"bgColor='" + Common.RowBgOdd + "'\" onmouseover=\"bgColor='" + Common.RowBgOver + "'\">" + strTmp + "</tr>");
                    intCount++;
                    if (numRow == dtb.Rows.Count)
                    {
                        GenerateSubTotal(strRow, strSubTpl, intCount, ref dcDeposit, ref dcTurnover, ref dcBonus, ref dcMemWinloss, ref dcAffWinloss, ref dcHouseWinloss, ref dcAffNetPos, ref dcHouseNetWinloss);//Sub total
                        GenerateMonthlyRow(strTotal, hsTotal);//Monthly for last row
                    }
                }
                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "content", Common.NoInfo(19));
                TemplateHandler.ReplaceBlock(ref strTpl, "subtotal", string.Empty);
                TemplateHandler.ReplaceBlock(ref strTpl, "total", string.Empty);
            }
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
            #if DEBUG
                        throw;
            #endif
        }
    }

    void GenerateMonthlyRow(string strTotal, Hashtable hsTotal)
    {
        string strMonthly = strTotal;
        string strMonthlyTotal = TemplateHandler.ReplaceBlock(ref strMonthly, "monthly-total", string.Empty);
        TemplateHandler.Assign(ref strMonthly, "monthly-val", Resources.Lang.TotalMonthly);
        TemplateHandler.Assign(ref strMonthly, "numrow", hsTotal.Keys.Count.ToString());
        decimal dcDeposit = 0, dcTurnoverTotal = 0, dcBonusTotal = 0, dcMemTotal = 0, dcAffTotal = 0, dcHouseTotal = 0, dcAffNetTotal = 0, dcHouseNetTotal = 0;
        
        ArrayList arrItem = new ArrayList();
        foreach (DictionaryEntry de in hsTotal)
        {
            arrItem.Add(de.Key);
        }
        arrItem.Sort();
        
        for (int i = 0; i < arrItem.Count; i++)
        {
            string strTmpMonthly = strMonthly;
            if (i != 0)
                TemplateHandler.RemoveBlock(ref strTmpMonthly, "monthly");
            //string strKey = hsTotal.;
            Hashtable hsCurrency = (Hashtable)hsTotal[arrItem[i]];
            //decimal dcRate = CastAide.AsDecimal(hsCurrency["Rate"]);
            TemplateHandler.Assign(ref strTmpMonthly, "currency-val", hsCurrency["Currency"].ToString());
            //TemplateHandler.Assign(ref strTmpMonthly, "rate-val", NumberFormat.DecFormat(hsCurrency["Rate"], 5));

            dcDeposit += CastAide.AsDecimal(hsCurrency["Deposit"]);
            dcTurnoverTotal += CastAide.AsDecimal(hsCurrency["Turnover"]);
            dcBonusTotal += CastAide.AsDecimal(hsCurrency["Bonus"]);
            dcMemTotal += CastAide.AsDecimal(hsCurrency["MemberWinLost"]);
            dcAffTotal += CastAide.AsDecimal(hsCurrency["AffiliateWinLost"]);
            dcHouseTotal += CastAide.AsDecimal(hsCurrency["HouseWinLost"]);
            dcAffNetTotal += CastAide.AsDecimal(hsCurrency["AffiliateNetWinLost"]);
            dcHouseNetTotal += CastAide.AsDecimal(hsCurrency["HouseNetWinLost"]);

            TemplateHandler.Assign(ref strTmpMonthly, "deposit-val", Common.FormatNumber(CastAide.AsDecimal(hsCurrency["Deposit"])));
            TemplateHandler.Assign(ref strTmpMonthly, "turnover-val", Common.FormatNumber(CastAide.AsDecimal(hsCurrency["Turnover"])));
            TemplateHandler.Assign(ref strTmpMonthly, "bonus-val", Common.FormatNumber(CastAide.AsDecimal(hsCurrency["Bonus"])));
            TemplateHandler.Assign(ref strTmpMonthly, "memwinloss-val", Common.FormatNumber(CastAide.AsDecimal(hsCurrency["MemberWinLost"])));
            TemplateHandler.Assign(ref strTmpMonthly, "affwinloss-val", Common.FormatNumber(CastAide.AsDecimal(hsCurrency["AffiliateWinLost"])));
            TemplateHandler.Assign(ref strTmpMonthly, "housewinloss-val", Common.FormatNumber(CastAide.AsDecimal(hsCurrency["HouseWinLost"])));
            TemplateHandler.Assign(ref strTmpMonthly, "affnetpos-val", Common.FormatNumber(CastAide.AsDecimal(hsCurrency["AffiliateNetWinLost"])));
            TemplateHandler.Assign(ref strTmpMonthly, "housenetwinloss-val", Common.FormatNumber(CastAide.AsDecimal(hsCurrency["HouseNetWinLost"])));
            strListRow.Append(strTmpMonthly);
        }
        TemplateHandler.Assign(ref strMonthlyTotal, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strMonthlyTotal, "deposit-total", Common.FormatNumber(dcDeposit));
        TemplateHandler.Assign(ref strMonthlyTotal, "turnover-total", Common.FormatNumber(dcTurnoverTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "bonus-total", Common.FormatNumber(dcBonusTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "memwinloss-total", Common.FormatNumber(dcMemTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "affwinloss-total", Common.FormatNumber(dcAffTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "housewinloss-total", Common.FormatNumber(dcHouseTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "affnetpos-total", Common.FormatNumber(dcAffNetTotal));
        TemplateHandler.Assign(ref strMonthlyTotal, "housenetwinloss-total", Common.FormatNumber(dcHouseNetTotal));
        strListRow.Append(strMonthlyTotal);
    }

    void GenerateSubTotal(StringBuilder strRow, string strSubTpl, int intCount, ref decimal dcDeposit, ref decimal dcTurnover, ref decimal dcBonus, ref decimal dcMemWinloss, ref decimal dcAffWinloss, ref decimal dcHouseWinloss, ref decimal dcAffNetPos, ref decimal dcHouseNetWinloss)
    {
        string strOldRow = strRow.ToString();
        TemplateHandler.Assign(ref strOldRow, "numrow", intCount.ToString());
        strListRow.Append(strOldRow);
        string strSubTmp = strSubTpl;
        TemplateHandler.Assign(ref strSubTmp, "deposit-val", Common.FormatNumber(dcDeposit));
        TemplateHandler.Assign(ref strSubTmp, "turnover-val", Common.FormatNumber(dcTurnover));
        TemplateHandler.Assign(ref strSubTmp, "bonus-val", Common.FormatNumber(dcBonus));
        TemplateHandler.Assign(ref strSubTmp, "memwinloss-val", Common.FormatNumber(dcMemWinloss));
        TemplateHandler.Assign(ref strSubTmp, "affwinloss-val", Common.FormatNumber(dcAffWinloss));
        TemplateHandler.Assign(ref strSubTmp, "housewinloss-val", Common.FormatNumber(dcHouseWinloss));
        TemplateHandler.Assign(ref strSubTmp, "affnetpos-val", Common.FormatNumber(dcAffNetPos));
        TemplateHandler.Assign(ref strSubTmp, "housenetwinloss-val", Common.FormatNumber(dcHouseNetWinloss));
        strListRow.Append(strSubTmp);
        dcTurnover = 0;
        dcBonus = 0;
        dcMemWinloss = 0;
        dcAffWinloss = 0;
        dcHouseWinloss = 0;
        dcAffNetPos = 0;
        dcHouseNetWinloss = 0;
        dcDeposit = 0;
    }
}
