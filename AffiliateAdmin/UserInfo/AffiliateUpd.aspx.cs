using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Web;
using DNAide.Web;
using DNAide.Core;
using DNAide.Utils;
using DataHandler;
using AffiliateAdmin.Business.Constants;

public partial class AffiliateUpd : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/AffUpd.htm");

        int intcustid = AppSettings.Session.AffId;
        if (!Common.HasReadPermission(PermissionNames.SecurityQA))
            TemplateHandler.RemoveBlock(ref strTpl, "securityqa");
        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref strTpl, "note", Resources.Lang.Note);
        TemplateHandler.Assign(ref strTpl, "upduser", Resources.Lang.UpdUser);
        TemplateHandler.Assign(ref strTpl, "maininfo", Resources.Lang.MainInfo);
        TemplateHandler.Assign(ref strTpl, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "status", Resources.Lang.Status);
        TemplateHandler.Assign(ref strTpl, "comm", Resources.Lang.Com);
        TemplateHandler.Assign(ref strTpl, "select", Resources.Lang.Select);
        TemplateHandler.Assign(ref strTpl, "personalinfo", Resources.Lang.PersonalInfo);
        TemplateHandler.Assign(ref strTpl, "additionalinfo", Resources.Lang.AdditionalInfo);
        TemplateHandler.Assign(ref strTpl, "accinfo", Resources.Lang.AccInfo);
        TemplateHandler.Assign(ref strTpl, "fname", Resources.Lang.Fname);
        TemplateHandler.Assign(ref strTpl, "lname", Resources.Lang.Lname);
        TemplateHandler.Assign(ref strTpl, "gender", Resources.Lang.Gender);
        TemplateHandler.Assign(ref strTpl, "male", Resources.Lang.Male);
        TemplateHandler.Assign(ref strTpl, "female", Resources.Lang.Female);
        TemplateHandler.Assign(ref strTpl, "address", Resources.Lang.Address);
        TemplateHandler.Assign(ref strTpl, "city", Resources.Lang.City);
        TemplateHandler.Assign(ref strTpl, "state", Resources.Lang.State);
        TemplateHandler.Assign(ref strTpl, "postal", Resources.Lang.Postal);
        TemplateHandler.Assign(ref strTpl, "country", Resources.Lang.Country);
        TemplateHandler.Assign(ref strTpl, "email", Resources.Lang.Email);
        TemplateHandler.Assign(ref strTpl, "number", Resources.Lang.ContactNumber);
        TemplateHandler.Assign(ref strTpl, "mobile", Resources.Lang.Mobile);
        TemplateHandler.Assign(ref strTpl, "siteinfo", Resources.Lang.SiteInfo);
        TemplateHandler.Assign(ref strTpl, "siteurl", Resources.Lang.SiteUrl);
        TemplateHandler.Assign(ref strTpl, "moreurl", Resources.Lang.MoreUrl);
        TemplateHandler.Assign(ref strTpl, "preference", Resources.Lang.Preference);
        TemplateHandler.Assign(ref strTpl, "language", Resources.Lang.Language);
        TemplateHandler.Assign(ref strTpl, "paymentinfo", Resources.Lang.PaymentInfo);
        TemplateHandler.Assign(ref strTpl, "preferpayment", Resources.Lang.PreferPayment);
        TemplateHandler.Assign(ref strTpl, "12betacc", Resources.Lang.BetAcc12);
        TemplateHandler.Assign(ref strTpl, "netteller", Resources.Lang.Netteller);
        TemplateHandler.Assign(ref strTpl, "moneybooker", Resources.Lang.MoneyBooker);
        TemplateHandler.Assign(ref strTpl, "banktrans", Resources.Lang.BankTrans);
        TemplateHandler.Assign(ref strTpl, "cheque", Resources.Lang.Cheque);
        TemplateHandler.Assign(ref strTpl, "12betusername", Resources.Lang.Bet12Username);
        TemplateHandler.Assign(ref strTpl, "NonDeposit", Resources.Lang.NonDepositAcc);
        TemplateHandler.Assign(ref strTpl, "netelleraccid", Resources.Lang.NetellerAccId);
        TemplateHandler.Assign(ref strTpl, "moneybookeremail", Resources.Lang.MoneybookerEmail);
        TemplateHandler.Assign(ref strTpl, "fullname", Resources.Lang.FullName);
        TemplateHandler.Assign(ref strTpl, "bankname", Resources.Lang.Bankname);
        TemplateHandler.Assign(ref strTpl, "bankaddress", Resources.Lang.BankAddress);
        TemplateHandler.Assign(ref strTpl, "bankswift", Resources.Lang.BankSwift);
        TemplateHandler.Assign(ref strTpl, "bankaccname", Resources.Lang.BankAccname);
        TemplateHandler.Assign(ref strTpl, "bankaccnumber", Resources.Lang.BankAccnumber);
        TemplateHandler.Assign(ref strTpl, "minwithdraw", Resources.Lang.MinWithdraw);
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref strTpl, "accoverview", Resources.Lang.AccOverview);
        TemplateHandler.Assign(ref strTpl, "securequestion", Resources.Lang.securequestion);
        TemplateHandler.Assign(ref strTpl, "answer", Resources.Lang.answer);
        TemplateHandler.Assign(ref strTpl, "earning", Resources.Lang.EarningsHistory);
        TemplateHandler.Assign(ref strTpl, "addurl", Resources.Lang.AddUrl);
        TemplateHandler.Assign(ref strTpl, "emailpref", Resources.Lang.EmailPref);
        TemplateHandler.Assign(ref strTpl, "plantext", Resources.Lang.PlainText);
        TemplateHandler.Assign(ref strTpl, "html", Resources.Lang.HtmlFormat);
        TemplateHandler.Assign(ref strTpl, "ischild", Request["ischild"]);

        //Load content user
        try
        {
            string strMinWire = "0";
            string strMinNetteller = "0";
            string strMinCheque = "0";
            string strMinMoneyBooker = "0";
            string strMinPlayer = "0";

            DataTable dtbCustInfo = DataHandler.Affiliate.SelAffInfo(intcustid, ref strMinCheque, ref strMinMoneyBooker, ref strMinNetteller, ref strMinWire, ref strMinPlayer);
            if (dtbCustInfo.Rows.Count > 0)
            {
                DataRow dr = dtbCustInfo.Rows[0];
                ArrayList arrLangValue = new ArrayList();
                ArrayList arrLangText = new ArrayList();
                arrLangValue.Add("");
                arrLangValue.Add("E");                
                arrLangValue.Add("CT");
                arrLangValue.Add("CS");
                arrLangValue.Add("TH");
                arrLangText.Add("Select One");
                arrLangText.Add("English");
                arrLangText.Add("Chinese Traditional");
                arrLangText.Add("Chinese Simplified");
                arrLangText.Add("Thai");

                ArrayList arrComValue = new ArrayList();                
                for (decimal i = 20; i < 81; i++)
                {
                    decimal dc = i / 100;                    
                    arrComValue.Add(NumberFormat.DoubleFormat(dc, 2));
                }
                if (string.Compare(Status.Approved, dr["status"].ToString()) == 0)
                {
                    TemplateHandler.Assign(ref strTpl, "closed", Resources.Lang.Closed);
                    TemplateHandler.ReplaceBlock(ref strTpl, "notapproved", string.Empty);
                    //TemplateHandler.Assign(ref strTpl, "closechecked", CastAide.AsBoolean(dr["closed"]) ? "checked" : string.Empty);
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTpl, "approved", string.Empty);
                }
                if (DBNull.Value.Equals(dr["gender"]))
                {
                    TemplateHandler.Assign(ref strTpl, "male-checked", string.Empty);
                    TemplateHandler.Assign(ref strTpl, "female-checked", string.Empty);
                }
                else
                {
                    if(CastAide.AsBoolean(dr["gender"]))
                        TemplateHandler.Assign(ref strTpl, "male-checked", "checked");
                    else
                        TemplateHandler.Assign(ref strTpl, "female-checked", "checked");
                    TemplateHandler.Assign(ref strTpl, "male-checked", string.Empty);
                    TemplateHandler.Assign(ref strTpl, "female-checked", string.Empty);
                }
                
                //Email format preference
                if (DBNull.Value.Equals(dr["mailoption"]))
                {
                    TemplateHandler.Assign(ref strTpl, "plaintext-checked", string.Empty);
                    TemplateHandler.Assign(ref strTpl, "html-checked", string.Empty);
                }
                else
                {
                    if (CastAide.AsBoolean(dr["mailoption"]))
                        TemplateHandler.Assign(ref strTpl, "plaintext-checked", "checked");
                    else
                        TemplateHandler.Assign(ref strTpl, "html-checked", "checked");
                    TemplateHandler.Assign(ref strTpl, "plaintext-checked", string.Empty);
                    TemplateHandler.Assign(ref strTpl, "html-checked", string.Empty);
                }

                TemplateHandler.Assign(ref strTpl, "securequestion_val", dr["question"].ToString());
                TemplateHandler.Assign(ref strTpl, "answer_val", dr["answer"].ToString());

                TemplateHandler.Assign(ref strTpl, "custid-val", intcustid);
                TemplateHandler.Assign(ref strTpl, "username-val", dr["username"].ToString());
                TemplateHandler.Assign(ref strTpl, "fname-val", dr["firstname"].ToString());
                TemplateHandler.Assign(ref strTpl, "lname-val", dr["lastname"].ToString());
                TemplateHandler.Assign(ref strTpl, "address-val", dr["address"].ToString());
                TemplateHandler.Assign(ref strTpl, "city-val", dr["city"].ToString());
                TemplateHandler.Assign(ref strTpl, "state-val", dr["state"].ToString());
                TemplateHandler.Assign(ref strTpl, "post-val", dr["postal"].ToString());
                TemplateHandler.Assign(ref strTpl, "country-val", dr["country"].ToString());
                TemplateHandler.Assign(ref strTpl, "email-val", dr["email"].ToString());
                TemplateHandler.Assign(ref strTpl, "number-val", dr["phone"].ToString());
                TemplateHandler.Assign(ref strTpl, "mobile-val", dr["mobilephone"].ToString());
                TemplateHandler.Assign(ref strTpl, "url-val", dr["siteurl"].ToString());
                TemplateHandler.Assign(ref strTpl, "sitecontent-val", dr["nositedesc"].ToString());
                string strUrls = dr["moresiteurls"].ToString();
                if (strUrls.Trim().Length > 0)
                {
                    string[] strMoreUrls = strUrls.Split(';');
                    if (strMoreUrls.Length > 0)
                    {
                        StringBuilder strBuild = new StringBuilder();
                        string strSiteContent = TemplateHandler.CopyBlock(strTpl, "sitecontent");
                        for (int i = 0; i < strMoreUrls.Length; i++)
                        {
                            string strTmp = strSiteContent;
                            TemplateHandler.Assign(ref strTmp, "moreurl-val", strMoreUrls[i]);
                            TemplateHandler.Assign(ref strTmp, "i", (i + 2).ToString());
                            strBuild.Append(strTmp);
                        }
                        TemplateHandler.ReplaceBlock(ref strTpl, "sitecontent", strBuild.ToString());
                    }
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTpl, "sitecontent", string.Empty);
                }

                TemplateHandler.Assign(ref strTpl, "12betusername-val", dr["player_username"].ToString());
                TemplateHandler.Assign(ref strTpl, "netelleraccid-val", dr["neteller_accountid"].ToString());
                TemplateHandler.Assign(ref strTpl, "moneybookeremail-val", dr["moneybookers_email"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankname-val", dr["wire_bankname"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankaddress-val", dr["wire_bankaddress"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankswift-val", dr["wire_swiftcode"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankaccname-val", dr["wire_accountname"].ToString());
                TemplateHandler.Assign(ref strTpl, "bankaccnumber-val", dr["wire_accountno"].ToString());
                TemplateHandler.Assign(ref strTpl, "cheque_fullname", dr["cheque_fullname"].ToString());
                TemplateHandler.Assign(ref strTpl, "cheque_address", dr["cheque_address"].ToString());
                TemplateHandler.Assign(ref strTpl, "cheque_city", dr["cheque_city"].ToString());
                TemplateHandler.Assign(ref strTpl, "cheque_state", dr["cheque_state"].ToString());
                TemplateHandler.Assign(ref strTpl, "cheque_postal", dr["cheque_postal"].ToString());
                TemplateHandler.Assign(ref strTpl, "cheque_country", dr["cheque_country"].ToString());
                //method-val
                TemplateHandler.Assign(ref strTpl, "method-val", dr["preferredpayment"]);
                TemplateHandler.Assign(ref strTpl, "status-val", dr["status"].ToString());
                if (string.Compare(dr["status"].ToString(), Resources.Lang.Approved, true) != 0)
                {
                    TemplateHandler.RemoveBlock(ref strTpl, "overview");
                    TemplateHandler.RemoveBlock(ref strTpl, "earningshistory");
                }
                //Min withdraw
                TemplateHandler.Assign(ref strTpl, "minnetteller", "$" + strMinNetteller);
                TemplateHandler.Assign(ref strTpl, "minbooker", "$" + strMinMoneyBooker);
                TemplateHandler.Assign(ref strTpl, "minbank", "$" + strMinWire);
                TemplateHandler.Assign(ref strTpl, "mincheque", "$" + strMinCheque);
                TemplateHandler.Assign(ref strTpl, "minplayer", "$" + strMinPlayer);
                switch(dr["preferredpayment"].ToString().ToLower())
                {
                    case "player":
                        TemplateHandler.Assign(ref strTpl, "cssplayer", string.Empty);
                        break;
                    case "netteller":
                        TemplateHandler.Assign(ref strTpl, "cssnetteller", string.Empty);
                        break;
                    case "cheque":
                        TemplateHandler.Assign(ref strTpl, "csscheque", string.Empty);
                        break;
                    case "moneybookers":
                        TemplateHandler.Assign(ref strTpl, "cssbooker", string.Empty);
                        break;
                    case "wire":
                        TemplateHandler.Assign(ref strTpl, "cssbank", string.Empty);
                        break;
                    default:
                        TemplateHandler.Assign(ref strTpl, "cssplayer", string.Empty);
                        break;
                }
                TemplateHandler.Assign(ref strTpl, "cssplayer", "hide");
                TemplateHandler.Assign(ref strTpl, "cssnetteller", "hide");
                TemplateHandler.Assign(ref strTpl, "csscheque", "hide");
                TemplateHandler.Assign(ref strTpl, "cssbooker", "hide");
                TemplateHandler.Assign(ref strTpl, "cssbank", "hide");

            }
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
#if DEBUG
                throw;
#endif
        }
       
        #region Load SubPage
        string subPage = string.Empty;
        if (string.Compare(Request["approved"], "0", true) == 0)
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffPendingLink, SubMenu.AffPendingText, 0, 0, Resources.Lang.Edit, "i_customer.png"));
        else
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 1, 0, Resources.Lang.Edit, "i_customer.png"));
        
        TemplateHandler.Assign(ref subPage, "ContentPage", strTpl);
        #endregion

        this.HtmlContent = strTpl;

      
    }
}
