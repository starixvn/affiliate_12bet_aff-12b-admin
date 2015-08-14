using System;
using System.Collections;
using DNAide.Web;
using DNAide.Utils;
using DataHandler;

public partial class SignUp : RootPage
{
    protected string strMinWire = "0";
    protected string strMinNetteller = "0";
    protected string strMinCheque = "0";
    protected string strMinMoneyBooker = "0";
    protected string strSecureQuestion = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        throw new Exception("sdfsd");
        if (AppSettings.Session.IsAuthenticated) 
            Response.Redirect("UserInfo/Account_Overview.aspx");
        ltrRegisForm.Text = Resources.Lang.RegisForm;
        ltrCompleteForm.Text = Resources.Lang.CompleteForm;
        ltrRequireInfo.Text = Resources.Lang.RequireInfo;
        ltrExtraTextForKorean.Text = Resources.Lang.ExtraTextForKorean;
        ltrContactInfo.Text = Resources.Lang.ContactInfo;
        ltrFName.Text = Resources.Lang.FName;
        ltrLName.Text = Resources.Lang.LName;
        ltrGender.Text = Resources.Lang.Gender;
        ltrEmail.Text = Resources.Lang.Email;
        ltrReEmail.Text = Resources.Lang.ReEmail;
        ltrContactNumber.Text = Resources.Lang.ContactNumber;
        ltrMobile.Text = Resources.Lang.Mobile;
        ltrLang.Text = Resources.Lang.Language;
        ltrPrefer.Text = Resources.Lang.PreferLang;
        ltrMailingInfor.Text = Resources.Lang.MailingInfor;
        ltrAddress.Text = Resources.Lang.Address;
        ltrCity.Text = Resources.Lang.City;
        ltrCountry.Text = Resources.Lang.Country;
        ltrPostcode.Text = Resources.Lang.PostCode;
        ltrState.Text = Resources.Lang.State;
        ltrSiteinfor.Text = Resources.Lang.WebsiteInformat;
        ltrSiteurl.Text = Resources.Lang.WebsiteURL;
        ltrNoWebsite.Text = Resources.Lang.NoWebsite;
        ltrPaymentInfor.Text = Resources.Lang.PaymentInfor;
        ltrPaymentMethod.Text = Resources.Lang.PaymentMethod;
        ltrAccount12Bet.Text = Resources.Lang._12BETPlayerAcc;
        //ltrCheque.Text = Resources.Lang.Cheque;
        ltr12UserName.Text = Resources.Lang.Username;// +" " + Resources.Lang.Bet12;
        ltrAgreeto.Text = Resources.Lang.AgreeTermNCondition;
        //ltrAgreeto.Text = Resources.Lang.AgreeTo;
        //ltrTermcondition.Text = Resources.Lang.TermCondition;
        //ltrOfAff.Text = Resources.Lang.Of + " " + Resources.Lang.Bet12 + " " + Resources.Lang.Affiliate;
        try
        {
            Hashtable hsMinWidthdraw = Payment.MinWithdraw();
            strMinCheque = NumberFormat.DecFormat(hsMinWidthdraw["aff_mincheque"], 0);
            strMinMoneyBooker = NumberFormat.DecFormat(hsMinWidthdraw["aff_minmoneybookers"], 0);
            strMinNetteller = NumberFormat.DecFormat(hsMinWidthdraw["aff_minneteller"], 0);
            strMinWire = NumberFormat.DecFormat(hsMinWidthdraw["aff_minwire"], 0);
            strSecureQuestion = DNAide.Web.TemplateHandler.LoadTemplate("UserInfo/SecureQuestion_" + AppSettings.Session.Language + ".htm");
            
        }
        catch(Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
        }
    }
}
