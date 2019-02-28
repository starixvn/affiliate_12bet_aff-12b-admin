using System;
using System.Data;
using DNAide.Web;
using DNAide.Utils;
using DNAide.Core;

public partial class PaymentUpdate : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Payment/PaymentUpdate.htm");
        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "PaymentInfo", Resources.Lang.PaymentInfo);
        TemplateHandler.Assign(ref strTpl, "transid-tit", Resources.Lang.TransactionsId);
        TemplateHandler.Assign(ref strTpl, "daterequest", Resources.Lang.DateRequest);
        TemplateHandler.Assign(ref strTpl, "status", Resources.Lang.Status);
        TemplateHandler.Assign(ref strTpl, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "email", Resources.Lang.Email);
        TemplateHandler.Assign(ref strTpl, "fname", Resources.Lang.Fname);
        TemplateHandler.Assign(ref strTpl, "lname", Resources.Lang.Lname);
        TemplateHandler.Assign(ref strTpl, "preferred", Resources.Lang.PaymentMethod);
        TemplateHandler.Assign(ref strTpl, "amt", Resources.Lang.Amount);
        TemplateHandler.Assign(ref strTpl, "remark", Resources.Lang.Remark);
        TemplateHandler.Assign(ref strTpl, "approve", Resources.Lang.Approve);
        TemplateHandler.Assign(ref strTpl, "reject", Resources.Lang.Reject);
        TemplateHandler.Assign(ref strTpl, "update", Resources.Lang.Update);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref strTpl, "apperr", Resources.Lang.ApproveErr);
        TemplateHandler.Assign(ref strTpl, "completeddate", Resources.Lang.CompletedDate);
        TemplateHandler.Assign(ref strTpl, "UpdatedBy", Resources.Lang.UpdatedBy);
        TemplateHandler.Assign(ref strTpl, "page", Request["page"]);
        
        if (!string.IsNullOrEmpty(Request["transid"]))
        {
            Int64 intTransId = CastAide.AsInt64(Request["transid"], -1);
            DataTable dtb = DataHandler.PaymentRequest.PaymentRequestById(intTransId);
            if (dtb.Rows.Count > 0)
            {
                DataRow dr = dtb.Rows[0];
                TemplateHandler.Assign(ref strTpl, "lang", dr["preferredlanguage"]);
                TemplateHandler.Assign(ref strTpl, "transid-val", dr["transid"].ToString());
                TemplateHandler.Assign(ref strTpl, "custid-val", dr["affid"].ToString());
                TemplateHandler.Assign(ref strTpl, "daterequest-val", dr["transdate"].ToString());
                TemplateHandler.Assign(ref strTpl, "username-val", dr["username"].ToString());
                TemplateHandler.Assign(ref strTpl, "email-val", dr["email"].ToString());
                TemplateHandler.Assign(ref strTpl, "fname-val", dr["firstname"].ToString());
                TemplateHandler.Assign(ref strTpl, "lname-val", dr["lastname"].ToString());
                TemplateHandler.Assign(ref strTpl, "preferred-val", dr["PreferredPayment"].ToString());
                TemplateHandler.Assign(ref strTpl, "amt-val", Common.FormatNumber(dr["amt"]));
                TemplateHandler.Assign(ref strTpl, "completeddate-val", DataFormat.FormatDateTimeEn12(dr["completeddate"]));
                TemplateHandler.Assign(ref strTpl, "creator-val", dr["adminname"].ToString());

                if (string.Compare(dr["status"].ToString(), "pending", true) == 0)
                    TemplateHandler.ReplaceBlock(ref strTpl, "others", string.Empty);
                else
                    TemplateHandler.ReplaceBlock(ref strTpl, "pending", string.Empty);

                TemplateHandler.Assign(ref strTpl, "status-val", Common.GetStatus(dr["status"].ToString()));

                StatusGer(ref strTpl, dr);

                RemovePreferWaste(ref strTpl, dr);
            }
        }

        this.HtmlContent = strTpl;
    }

    void StatusGer(ref string strTpl, DataRow dr)
    {
        switch (dr["status"].ToString().ToLower())
        {
            case "pending":
                TemplateHandler.Assign(ref strTpl, "AdminRemark-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "AffRemark-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "submitcss", "buttonlogin");
                TemplateHandler.Assign(ref strTpl, "chkappoved", "");
                TemplateHandler.Assign(ref strTpl, "chkrejected", "");
                TemplateHandler.Assign(ref strTpl, "disable-val", "");
                break;
            case "cancelled":
                TemplateHandler.Assign(ref strTpl, "AdminRemark-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "AffRemark-val", string.Empty);
                TemplateHandler.Assign(ref strTpl, "submitcss", "disabled");
                TemplateHandler.Assign(ref strTpl, "chkappoved", string.Empty);
                TemplateHandler.Assign(ref strTpl, "chkrejected", string.Empty);
                TemplateHandler.Assign(ref strTpl, "disable-val", "disabled");
                break;
            case "rejected":
                TemplateHandler.Assign(ref strTpl, "AdminRemark-val", Common.CutLongText(dr["AdminDesc"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), 55));
                TemplateHandler.Assign(ref strTpl, "AffRemark-val", Common.CutLongText(dr["AffDesc"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), 55));
                TemplateHandler.Assign(ref strTpl, "submitcss", "disabled");
                TemplateHandler.Assign(ref strTpl, "chkappoved", string.Empty);
                TemplateHandler.Assign(ref strTpl, "chkrejected", "checked");
                TemplateHandler.Assign(ref strTpl, "disable-val", "disabled");
                break;
            case "approved":
                TemplateHandler.Assign(ref strTpl, "AdminRemark-val", Common.CutLongText(dr["AdminDesc"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), 55));
                TemplateHandler.Assign(ref strTpl, "AffRemark-val", Common.CutLongText(dr["AffDesc"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"), 55));
                TemplateHandler.Assign(ref strTpl, "submitcss", "disabled");
                TemplateHandler.Assign(ref strTpl, "chkappoved", "checked");
                TemplateHandler.Assign(ref strTpl, "chkrejected", string.Empty);
                TemplateHandler.Assign(ref strTpl, "disable-val", "disabled");
                break;
        }
    }

    #region Remove block not use
    void RemovePreferWaste(ref string strTpl, DataRow dr)
    {
        switch (dr["PreferredPayment"].ToString())
        {
            case "Player":
                TemplateHandler.Assign(ref strTpl, "12bet", Resources.Lang.Bet12Username);
                TemplateHandler.Assign(ref strTpl, "12bet-val", dr["username2"].ToString());
                TemplateHandler.RemoveBlock(ref strTpl, "netteller");
                TemplateHandler.RemoveBlock(ref strTpl, "moneybookers");
                TemplateHandler.RemoveBlock(ref strTpl, "banktransfer");
                TemplateHandler.RemoveBlock(ref strTpl, "cheque");
                break;
            case "Neteller":
                TemplateHandler.Assign(ref strTpl, "netterleracc", Resources.Lang.NetellerAccId);
                TemplateHandler.Assign(ref strTpl, "netterleracc-val", dr["neteller_accountID"].ToString());
                TemplateHandler.RemoveBlock(ref strTpl, "12bet");
                TemplateHandler.RemoveBlock(ref strTpl, "moneybookers");
                TemplateHandler.RemoveBlock(ref strTpl, "banktransfer");
                TemplateHandler.RemoveBlock(ref strTpl, "cheque");
                break;
            case "Moneybookers":
                TemplateHandler.Assign(ref strTpl, "moneybookeremail", Resources.Lang.MoneybookerEmail);
                TemplateHandler.Assign(ref strTpl, "moneybookeremail-val", dr["moneybookers_email"].ToString());
                TemplateHandler.RemoveBlock(ref strTpl, "netteller");
                TemplateHandler.RemoveBlock(ref strTpl, "12bet");
                TemplateHandler.RemoveBlock(ref strTpl, "banktransfer");
                TemplateHandler.RemoveBlock(ref strTpl, "cheque");
                break;
            case "Wire":
                TemplateHandler.Assign(ref strTpl, "bankname", Resources.Lang.Bankname);
                TemplateHandler.Assign(ref strTpl, "bankname-val", dr["wire_bankname"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankaddress", Resources.Lang.BankAddress);
                TemplateHandler.Assign(ref strTpl, "bankaddress-val", dr["wire_bankaddress"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankswiftcode", Resources.Lang.BankSwift);
                TemplateHandler.Assign(ref strTpl, "bankswiftcode-val", dr["wire_swiftcode"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankaccname", Resources.Lang.BankAccname);
                TemplateHandler.Assign(ref strTpl, "bankaccname-val", dr["wire_accountname"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankaccno", Resources.Lang.BankAccnumber);
                TemplateHandler.Assign(ref strTpl, "bankaccno-val", dr["wire_accountno"].ToString());
                TemplateHandler.RemoveBlock(ref strTpl, "netteller");
                TemplateHandler.RemoveBlock(ref strTpl, "moneybookers");
                TemplateHandler.RemoveBlock(ref strTpl, "12bet");
                TemplateHandler.RemoveBlock(ref strTpl, "cheque");
                break;
            case "Cheque":
                TemplateHandler.Assign(ref strTpl, "chequefullname", Resources.Lang.FullName);
                TemplateHandler.Assign(ref strTpl, "chequefullname-val", dr["Cheque_FullName"].ToString());
                TemplateHandler.Assign(ref strTpl, "chequeaddress", Resources.Lang.BankAddress);
                TemplateHandler.Assign(ref strTpl, "chequeaddress-val", dr["cheque_address"].ToString());
                TemplateHandler.Assign(ref strTpl, "chequecity", Resources.Lang.BankSwift);
                TemplateHandler.Assign(ref strTpl, "chequecity-val", dr["cheque_city"].ToString());
                TemplateHandler.Assign(ref strTpl, "chequestate", Resources.Lang.BankAccname);
                TemplateHandler.Assign(ref strTpl, "chequestate-val", dr["cheque_state"].ToString());
                TemplateHandler.Assign(ref strTpl, "chequepost", Resources.Lang.BankAccnumber);
                TemplateHandler.Assign(ref strTpl, "chequepost-val", dr["cheque_postal"].ToString());
                TemplateHandler.Assign(ref strTpl, "chequecountry", Resources.Lang.BankAccnumber);
                TemplateHandler.Assign(ref strTpl, "chequecountry-val", dr["cheque_country"].ToString());
                TemplateHandler.RemoveBlock(ref strTpl, "netteller");
                TemplateHandler.RemoveBlock(ref strTpl, "moneybookers");
                TemplateHandler.RemoveBlock(ref strTpl, "banktransfer");
                TemplateHandler.RemoveBlock(ref strTpl, "12bet");
                break;
        }
    }
    #endregion
}
