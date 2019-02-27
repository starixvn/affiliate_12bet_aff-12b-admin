using System;
using System.Data;
using System.Text;
using AffiliateAdmin.Business.Constants;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Web;

public partial class AffRate : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Exchange/ExchangeRate.htm");
        DateTime dtmNow = DateTime.Now;

        DateTime dtmTime = DateTime.Now;
        if (!string.IsNullOrEmpty(Request["txtDate"]))
            dtmTime = CastAide.AsDateTime(Request["txtDate"]);
        else
            dtmTime = dtmNow;

        TemplateHandler.Assign(ref strTpl, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref strTpl, "date-val", dtmTime.ToString("MM/dd/yyyy"));
        //TemplateHandler.Assign(ref strTpl, "month-val", dtmTime.ToString("MM"));
        //TemplateHandler.Assign(ref strTpl, "year-val", dtmTime.ToString("yyyy"));
        TemplateHandler.Assign(ref strTpl, "exrate", Resources.Lang.ExchangeRate);
        TemplateHandler.Assign(ref strTpl, "currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref strTpl, "rate", Resources.Lang.Rate);
        TemplateHandler.Assign(ref strTpl, "buyingrate", Resources.Lang.BuyingRate);
        //TemplateHandler.Assign(ref strTpl, "month", Resources.Lang.Month);
        //TemplateHandler.Assign(ref strTpl, "year", Resources.Lang.Year);
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "isupdated", (dtmTime.DayOfYear == dtmNow.DayOfYear && dtmTime.Year == dtmNow.Year) ? "1" : "0");
        try
        {
            int intCount = 0;
            if (dtmTime <= dtmNow)
            {
                bool isHost = Common.HasHostPermission(AppSettings.Session.CustID, PermissionNames.AffExRate);

                DataTable dtbResult = DataHandler.ExchangeRate.Exchange_Sel(dtmTime);
                if (dtbResult.Rows.Count > 0)
                {
                    StringBuilder strListRow = new StringBuilder();
                    string strRow = TemplateHandler.CopyBlock(strTpl, "content");
                    TemplateHandler.Assign(ref strRow, "over", Common.RowBgOver);

                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        intCount++;
                        string strTmp = strRow;
                        TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                        TemplateHandler.Assign(ref strTmp, "no-val", intCount);
                        TemplateHandler.Assign(ref strTmp, "exid-val", dr["exchangeid"]);
                        TemplateHandler.Assign(ref strTmp, "currency-val", dr["currency"].ToString());

                        #region Hide to do not allow update exchange rate
                        if (dtmTime.DayOfYear != dtmNow.DayOfYear || dtmTime.Year != dtmNow.Year || CastAide.AsInt32(dr["exchangeid"]) == 3 || !isHost)
                            TemplateHandler.Assign(ref strTmp, "class-val", "");
                        else
                            TemplateHandler.Assign(ref strTmp, "class-val", "link popup");
                        #endregion

                        TemplateHandler.Assign(ref strTmp, "class-val", "");
                        TemplateHandler.Assign(ref strTmp, "rate-val", NumberFormat.DecFormat(dr["actualrate"], 5));
                        TemplateHandler.Assign(ref strTmp, "buyingrate-val", NumberFormat.DecFormat(dr["buyingrate"], 5));
                        strListRow.Append(strTmp);
                    }
                    TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTpl, "content", Common.NoInfo(3));
                }
            }
            else
                TemplateHandler.ReplaceBlock(ref strTpl, "content", Common.NoInfo(3));
        }
        catch (Exception ex)
        {
            AffiliateAdmin.Business.Helpers.LogHelper.WriteError(ex);
#if DEBUG
            throw;
#endif
        }
        this.HtmlContent = strTpl;
    }
}
