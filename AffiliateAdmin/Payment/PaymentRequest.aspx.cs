using System;
using System.Text;
using System.Data;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;
using DNAide.Utils;

public partial class PaymentRequest : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Payment/PaymentRequest.htm");
        #region assign text
        TemplateHandler.Assign(ref strTpl, "PaymentMethod", Resources.Lang.PaymentMethod);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "paymentrequest", Resources.Lang.PaymentRequest);
        TemplateHandler.Assign(ref strTpl, "transid", Resources.Lang.TransId);
        TemplateHandler.Assign(ref strTpl, "transdate", Resources.Lang.DateRequest);
        TemplateHandler.Assign(ref strTpl, "uname", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "refno", Resources.Lang.RefNo);
        TemplateHandler.Assign(ref strTpl, "amt", Resources.Lang.Amount);
        TemplateHandler.Assign(ref strTpl, "status", Resources.Lang.Status);
        TemplateHandler.Assign(ref strTpl, "payment", Resources.Lang.PaymentMethod);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "all", Resources.Lang.All);
        TemplateHandler.Assign(ref strTpl, "bet12", Resources.Lang.Bet12Username);
        TemplateHandler.Assign(ref strTpl, "netteller", Resources.Lang.Netteller);
        TemplateHandler.Assign(ref strTpl, "moneybooker", Resources.Lang.MoneyBooker);
        TemplateHandler.Assign(ref strTpl, "banktrans", Resources.Lang.BankTrans);
        TemplateHandler.Assign(ref strTpl, "cheque", Resources.Lang.Cheque);
        TemplateHandler.Assign(ref strTpl, "processing", Resources.Lang.Proccessing);
        TemplateHandler.Assign(ref strTpl, "approved", Resources.Lang.Approved);
        TemplateHandler.Assign(ref strTpl, "rejected", Resources.Lang.Rejected);
        TemplateHandler.Assign(ref strTpl, "cancelled", Resources.Lang.Cancelled);
        TemplateHandler.Assign(ref strTpl, "fdate-txt", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "to-txt", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "dateerr", Resources.Lang.DateErr);
        TemplateHandler.Assign(ref strTpl, "getall", Resources.Lang.GetAll);
        TemplateHandler.Assign(ref strTpl, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "searchby", Resources.Lang.SearchBy);
        TemplateHandler.Assign(ref strTpl, "others", Resources.Lang.Others);
        //TemplateHandler.Assign(ref strTpl, "submenu", Common.GenSubMenu(new string[] { "#" }, new string[] { Resources.Lang.PaymentRequest + " [{nummem}]" }, 801, 0));
        #endregion

        string strSearchBy = string.Empty;
        string strFrom = string.Empty;
        string strTo = string.Empty;
        string strUsername = string.Empty;
        bool isGetAll = false;
        string strStatus = string.Empty;
        int page = string.IsNullOrEmpty(Request["page"]) ? 1 : CastAide.AsInt32(Request["page"], 1);
        int iPagesize = string.IsNullOrEmpty(Request["slPageSize"]) ? 20 : CastAide.AsInt32(Request["slPageSize"], 1);

        strSearchBy = string.IsNullOrEmpty(Request["slSearch"]) ? "0" : Request["slSearch"];
        isGetAll = Request["chkGetAll"] == "on" ? true : false;
        strUsername = Request["txtUsername"];
        strStatus = Request["slStatus"];
        DateTime dtmNow = DateTime.Now;
        dtmNow = GenStartOfDate(dtmNow);
        DateTime dtmFrom = CastAide.AsDateTime(Request["fdate"], dtmNow.AddDays(-1));
        DateTime dtmTo = CastAide.AsDateTime(Request["tdate"], dtmNow);

        TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
        TemplateHandler.Assign(ref strTpl, "search-val", strSearchBy); 
        TemplateHandler.Assign(ref strTpl, "completeddate", Resources.Lang.CompletedDate);
        TemplateHandler.Assign(ref strTpl, "creator", Resources.Lang.Creator);
        TemplateHandler.Assign(ref strTpl, "username-val", strUsername);
        TemplateHandler.Assign(ref strTpl, "page", page);
        TemplateHandler.Assign(ref strTpl, "pagesize", iPagesize);
        try
        {
            TemplateHandler.Assign(ref strTpl, "isgetall", isGetAll ? "checked" : string.Empty);
            TemplateHandler.Assign(ref strTpl, "fdate", dtmFrom.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref strTpl, "tdate", dtmTo.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref strTpl, "curStatus", strStatus);//get current status selected
            //Response.Write("<script type='text/javascript'>alert('" + isGetAll.ToString() + "');</script>");
            DbResult rs = DataHandler.PaymentRequest.PaymentRequestList(strSearchBy, strUsername, strStatus, dtmFrom, dtmTo, isGetAll, page, iPagesize);

            int intMaxPage = CastAide.AsInt32(rs.Outputs["totalpages"]);
            int intNumRows = CastAide.AsInt32(rs.Outputs["nrows"]);
            int intPageId = page;
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffLink, SubMenu.AffText, 5, intNumRows, Resources.Lang.PaymentRequest, "i_pay_re.png"));
            
            if (intNumRows > 0)
            {
                DataTable dtb= rs.ResultSet.Tables[0];
                string strContentTpl = TemplateHandler.CopyBlock(strTpl, "content");
                StringBuilder strContent = new StringBuilder();
                int intRowNo = (intPageId - 1) * iPagesize;
                TemplateHandler.Assign(ref strContentTpl, "over", Common.RowBgOver);
                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strContentTpl;
                    intRowNo++;

                    TemplateHandler.Assign(ref strTmp, "color", GetRowStyle(dr["status"].ToString()));
                    TemplateHandler.Assign(ref strTmp, "no", intRowNo);
                    TemplateHandler.Assign(ref strTmp, "transid-val", dr["transid"].ToString());
                    TemplateHandler.Assign(ref strTmp, "transdate-val", Common.IBCDateTimeFormat(dr["transdate"],DTFormatType.DT24h));
                    TemplateHandler.Assign(ref strTmp, "uname-val", dr["username"].ToString());
                    TemplateHandler.Assign(ref strTmp, "amt-val", Common.FormatNumber(dr["amt"]));
                    TemplateHandler.Assign(ref strTmp, "status-val", Common.GerStatus(dr["status"].ToString()));
                    if (dr["status"].ToString().ToLower() == "pending")
                    {
                        TemplateHandler.Assign(ref strTmp, "completeddate-val", "--");
                        TemplateHandler.Assign(ref strTmp, "creator-val", "--");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "completeddate-val", Common.IBCDateTimeFormat(dr["completeddate"],DTFormatType.DT24h));
                        TemplateHandler.Assign(ref strTmp, "creator-val", dr["creator"].ToString());
                    }
                    strContent.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strTpl, "content", strContent.ToString());
                TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(intMaxPage, intPageId, iPagesize, intNumRows, "aPageTop", "txtPageTop", 800, false, true));
                TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(intMaxPage, intPageId, iPagesize, intNumRows, "aPageBottom", "txtPageBottom", 800, false, false));
            }
            else
            {
                TemplateHandler.Assign(ref strTpl, "paging-top", "");
                TemplateHandler.Assign(ref strTpl, "paging-bottom", "");
                TemplateHandler.ReplaceBlock(ref strTpl, "content", Common.NoInfo(8));
            }
            TemplateHandler.Assign(ref strTpl, "nummem", intNumRows);
            this.HtmlContent = strTpl;
        }
        catch(Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
#if DEBUG
            throw;
#endif
        }
    }

    DateTime GenStartOfDate(DateTime dtmDate)
    {
        return CastAide.AsDateTime(dtmDate.ToString("MM/dd/yyyy"), dtmDate);
    }

    string GetRowStyle(string strStatus)
    {
        switch (strStatus.ToLower())
        {
            case "cancelled":
            case "rejected":
                return Common.RowBgClosed;//return "Closed";
            case "approved":
                return Common.RowBgOdd;// return "Approved";
            case "pending":
                return Common.RowBgPending;//return "Pending";
            default:
                return Common.RowBgPending;
        }
    }
}
