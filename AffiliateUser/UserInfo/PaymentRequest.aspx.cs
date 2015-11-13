using System;
using System.Web;
using DNAide.Web;
using AffiliateUser;

public partial class PaymentRequest : RootPageLogIn
{
    protected string strBalance = string.Empty;
    protected string strMinAmt = string.Empty;    
    protected string strPaymentMethod = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
            ltrListRequest.Text = GenerateHTML.PaymentRequest.PaymentList(ref strBalance, ref strMinAmt,ref strPaymentMethod);
           // strPayment = DataHandler.PaymentRequest.PaymentMethodSel(AppSettings.Session.CustID);//.ToLower();
            if (string.Compare(strPaymentMethod.ToLower(), "player", true) != 0)
            {
                strPaymentMethod = Resources.Lang.ErrPaymentMethod.Replace("{method}", strPaymentMethod.Replace("Wire", Resources.Lang.BankTransfer));
            }
    }
}

