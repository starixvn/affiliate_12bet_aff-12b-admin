using System;
using System.Text;
using System.Data;
using DNAide.Web;
using DNAide.Core;
using DataHandler;
using AffiliateUser;

public partial class MyAccount : RootPageLogIn
{
    /*protected string strFName = "";
    protected string strLName = "";*/
    protected bool isGender = true;
    protected bool hasGender = false;
    protected string strAddress = "";
    protected string strCity = "";
    protected string strState = "";
    protected string strPost = "";
    protected string strCountry = "";
    protected string strEmail = "";
    protected string strNumber = "";
    protected string strMobile = "";
    protected string strSiteUrl = "";
    protected string strMoreSiteUrl = "";
    protected string strBet12UserName = "";
    protected string strNetellerAccId = "";
    protected string strMoneyBookerEmail = "";
    protected string strChequeFullName = "";
    protected string strChequeAddress = "";
    protected string strChequeCity = "";
    protected string strChequeState = "";
    protected string strChequePostcode = "";
    protected string strChequeCountry = "";
    protected string strBankName = "";
    protected string strBankAddress = "";
    protected string strBankCode = "";
    protected string strBankAccName = "";
    protected string strBankAccNumber = "";
    protected string strPreferLang = "";
    protected string strMethod = "0";
    protected string strMinWire = "0";
    protected string strMinNetteller = "0";
    protected string strMinCheque = "0";
    protected string strMinMoneyBooker = "0";
    protected string strPayment = "0";
    protected string str12Bet = "0";
    protected string strMinPlayer = string.Empty;
    protected string strWebsites = string.Empty;
    protected string strNoSiteDesc = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Load language
        ltrContactInfo.Text = Resources.Lang.ContactInfo;
        ltrMyAcc.Text = Resources.Lang.MyAcc;
        ltrUser.Text = Resources.Lang.Username;
        ltrPass.Text = Resources.Lang.Password;
        ltrAccInfo.Text = Resources.Lang.AccInfo;
      
        ltrEmail.Text = Resources.Lang.Email;
        ltrContactNumber.Text = Resources.Lang.ContactNumber;
        ltrMobile.Text = Resources.Lang.Mobile;
        ltrPrefer.Text = Resources.Lang.PreferLang;
        ltrLang.Text = Resources.Lang.Language;
        ltrAddress.Text = Resources.Lang.Address;
        ltrCity.Text = Resources.Lang.City;
        ltrCountry.Text = Resources.Lang.Country;
        ltrPostcode.Text = Resources.Lang.PostCode;
        ltrState.Text = Resources.Lang.State;
        ltrSiteinfor.Text = Resources.Lang.SiteInfor;
        ltrSiteurl.Text = Resources.Lang.SiteUrl;
        ltrNoWebsite.Text = Resources.Lang.NoWebsite;
        ltrPaymentInfor.Text = Resources.Lang.PaymentInfor;
        ltrPaymentMethod.Text = Resources.Lang.PaymentMethod;
        ltrAccount12Bet.Text = Resources.Lang.Account12bet;
        ltr12UserName.Text = Resources.Lang.Username;// +" " + Resources.Lang.Bet12;
        #endregion

            AppSettings.RefreshPreferredLanguage();
            strPayment = Request["ispay"] == null ? "0" : "1";
            DataTable dtbMyAcc = new DataTable();
            //dtbMyAcc = UserAff.SelAffInfo(677711);//
            dtbMyAcc = UserAff.SelAffInfo(AppSettings.Session.CustID, ref strMinCheque, ref strMinMoneyBooker, ref strMinNetteller, ref strMinWire,ref strMinPlayer);
            DataRow drAcc = dtbMyAcc.Rows[0];
            #region bind data
         /*   strFName = drAcc["firstname"].ToString();
            strLName = drAcc["lastname"].ToString();*/
            hasGender = (DBNull.Value.Equals(drAcc["gender"]) ? false : true);
            isGender = CastAide.AsBoolean(drAcc["gender"], true);
            strAddress = drAcc["address"].ToString();
            strCity = drAcc["city"].ToString();
            strState = drAcc["state"].ToString();
            strPost = drAcc["postal"].ToString();
            strCountry = drAcc["country"].ToString();
            strEmail = drAcc["email"].ToString();
            strNumber = drAcc["phone"].ToString();
            strMobile = drAcc["mobilephone"].ToString();
            strSiteUrl = drAcc["siteurl"].ToString();
            strMoreSiteUrl = drAcc["moresiteurls"].ToString().Replace("\r\n", "\\n");
            strBet12UserName = drAcc["player_username"].ToString();
            strNetellerAccId = drAcc["Neteller_accountid"].ToString();
            strMoneyBookerEmail = drAcc["moneybookers_email"].ToString();
            strChequeFullName = drAcc["cheque_fullname"].ToString();
            strChequeAddress = drAcc["cheque_address"].ToString();
            strChequeCity = drAcc["cheque_city"].ToString();
            strChequeState = drAcc["cheque_state"].ToString();
            strChequePostcode = drAcc["cheque_postal"].ToString();
            strChequeCountry = drAcc["cheque_country"].ToString();
            strBankName = drAcc["wire_bankname"].ToString();
            strBankAddress = drAcc["wire_bankaddress"].ToString();
            strBankCode = drAcc["wire_swiftcode"].ToString();
            strBankAccName = drAcc["wire_accountname"].ToString();
            strBankAccNumber = drAcc["wire_accountno"].ToString();
            strPreferLang = drAcc["PreferredLanguage"].ToString();
            strMethod = Common.GetIndex(drAcc["preferredpayment"].ToString());
            str12Bet = strMethod;
            ltrUsername.Text = drAcc["username"].ToString();
            strNoSiteDesc = drAcc["NoSiteDesc"].ToString();
            strWebsites = drAcc["MoreSiteURLs"].ToString(); //Load more urls
            string strUrls = drAcc["MoreSiteURLs"].ToString();

            if (strUrls.Trim().Length > 0)
            {
                string strTpl = TemplateHandler.LoadTemplate("UserInfo/MoreUrls.htm");
                TemplateHandler.Assign(ref strTpl, "siteurl", Resources.Lang.WebsiteURL);

                string[] strMoreUrls = strUrls.Split(';');
                if (strMoreUrls.Length > 0)
                {
                    StringBuilder strBuild = new StringBuilder();
                    for (int i = 0; i < strMoreUrls.Length; i++)
                    {
                        string strTmp = strTpl;
                        TemplateHandler.Assign(ref strTmp, "moreurl-val", strMoreUrls[i]);
                        TemplateHandler.Assign(ref strTmp, "i", (i + 2).ToString());
                        strBuild.Append(strTmp);
                    }
                    strWebsites = strBuild.ToString();
                }
            }

            #endregion
    }
}

