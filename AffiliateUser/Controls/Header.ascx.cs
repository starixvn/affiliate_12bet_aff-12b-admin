using System;
using System.Text;
using DNAide.Web;

public partial class Controls_Header : RootControl
{
    public string Logout = string.Empty, displayLogout = "none";
    protected string strErrMsg = string.Empty;
    protected string strLogin = string.Empty;
    protected string LogoutDisplay = string.Empty;
    public string hotnews = string.Empty;
    public string langClass = string.Empty;
    public string selectedLang = string.Empty;
    public string gmt;
    public string year;
    public string month;
    public string day;
    public string hour;
    public string minute;
    public string second;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AppSettings.Session.IsAuthenticated)
        {
            A1.Visible = false;
            LogoutDisplay = "none";
        }
        else
        {
            strLogin = "setInterval(function(){ KickedOut();} ,30000);";
        }

        InitializeHotNews();
        Logout = Resources.Lang.Logout;
        //hlkLogout.Text = Resources.Lang.Logout;
        string navTpl = TemplateHandler.LoadTemplate("Controls/MainNavigator.htm");
        TemplateHandler.Assign(ref navTpl, "langcss", GetLangCss());
        string strUrl = Request.Url.AbsoluteUri.ToString().ToLower();
        string strCurrent = "id=\"current\"";
        if (strUrl.IndexOf("programdetails.aspx") >= 0)
        {
            TemplateHandler.Assign(ref navTpl, "prog", strCurrent);
        }
        else if (strUrl.IndexOf("faqs.aspx") >= 0)
        {
            TemplateHandler.Assign(ref navTpl, "faq", strCurrent);
        }
        else if (strUrl.IndexOf("contactus.aspx") >= 0)
        {
            TemplateHandler.Assign(ref navTpl, "cont", strCurrent);
        }
        else
        {
            TemplateHandler.Assign(ref navTpl, "home", strCurrent);
        }
        TemplateHandler.Assign(ref navTpl, "prog", string.Empty);
        TemplateHandler.Assign(ref navTpl, "faq", string.Empty);
        TemplateHandler.Assign(ref navTpl, "cont", string.Empty);
        TemplateHandler.Assign(ref navTpl, "home", string.Empty);

        TemplateHandler.Assign(ref navTpl, "my-home", Resources.Lang.Home);
        TemplateHandler.Assign(ref navTpl, "sign-up", Resources.Lang.SignUp);
        TemplateHandler.Assign(ref navTpl, "prog-details", Resources.Lang.ProgDetails);
        TemplateHandler.Assign(ref   navTpl, "faqs", Resources.Lang.Faqs);
        TemplateHandler.Assign(ref navTpl, "contact-us", Resources.Lang.ContactUs);
        TemplateHandler.Assign(ref navTpl, "about-us", Resources.Lang.AboutUs);
        TemplateHandler.Assign(ref navTpl, "home-url", Common.RootURL());
        if (AppSettings.Session.IsAuthenticated)
        {
            TemplateHandler.RemoveBlock(ref navTpl, "sign-up");
            displayLogout = "";
        }
        string lang = AppSettings.Session.Language;
        switch (lang)
        {
            case "vi-VN":
                langClass = "fTV";
                selectedLang = "Tiếng Việt";
                break;
            case "es-ES":
                langClass = "fSpain";
                selectedLang = "Español";
                break;
            case "zh-CN":
                langClass = "fChinese";
                selectedLang = "中文简体";
                break;
            case "zh-TW":
                langClass = "fChinese";
                selectedLang = "中文繁体";
                break;
            case "id-ID":
                langClass = "fIndo";
                selectedLang = "Indonesia";
                break;
            case "th-TH":
                langClass = "fThai";
                selectedLang = "ภาษาไทย";
                break;
            case "ko-KR":
                langClass = "fKr";
                selectedLang = "한국어";
                break;
            case "ja-JP":
                langClass = "fJapan";
                selectedLang = "日本";
                break;
            case "hi-IN":
                langClass = "fIndia";
                selectedLang = "Indian";
                break;
            default:
                langClass = "fEng";
                selectedLang = "English";
                break;
        }

        string _nl = Environment.NewLine;
        string strIsRequire = Resources.Lang.IsRequired.Replace("\"", "'");
        string strIsInvalid = Resources.Lang.IsInvalid.Replace("\"", "'");
        StringBuilder _s = new StringBuilder();
        _s.Append("var ErrMsg = {" + _nl); // global message
        _s.Append("Term: \"" + Resources.Lang.TermErr.Replace("\"", "'") + "\"," + _nl);
        _s.Append("     Errmsgentryvalidcode: \"" + Resources.Lang.ErrmsgEntryValidCode.Replace("\"", "'") + "\"," + _nl);
        _s.Append("SecurityQuestion: \"" + Resources.Lang.errSecQuestionEmpty + "\"," + _nl);
        _s.Append("ContactNumber: \"" + Resources.Lang.errContactNumberEmpty + "\"," + _nl);
        _s.Append("MobileNumber: \"" + Resources.Lang.errMobileNumberEmpty + "\"," + _nl);
        _s.Append("YourAnswer: \"" + Resources.Lang.errYourAnswer + "\"," + _nl);
        _s.Append("FName: \"" + Resources.Lang.FirstNameIsRequired + "\"," + _nl);
        _s.Append("LName: \"" + Resources.Lang.LastNameIsRequired + "\"," + _nl);
        _s.Append("ErrGender: \"" + Resources.Lang.ErrGender + "\"," + _nl);
        _s.Append("Email: \"" + Resources.Lang.EmailAddressIsRequired + "\"," + _nl);
        _s.Append("EmailValid: \"" + Resources.Lang.EmailAddressIsInvalid + "\"," + _nl);
        _s.Append("EmailMatch: \"" + Resources.Lang.Emailaddressest.Replace("\"", "'") + "\"," + _nl);
        _s.Append("Address: \"" + Resources.Lang.AddressIsRequired + "\"," + _nl);
        _s.Append("City: \"" + Resources.Lang.CityIsRequired + "\"," + _nl);
        _s.Append("State: \"" + Resources.Lang.StateIsRequired + "\"," + _nl);
        _s.Append("PostCode : \"" + Resources.Lang.PostcodeZiIsRequired + "\"," + _nl);
        _s.Append("Country : \"" + Resources.Lang.CountryErr.Replace("\"", "'") + "\"," + _nl);
        _s.Append("SiteUrl : \"" + Resources.Lang.WebsiteURLIsRequired + "\"," + _nl);
        _s.Append("PreferLang : \"" + Resources.Lang.PreferLangErr.Replace("\"", "'") + "\"," + _nl);
        _s.Append("UserName12Bet : \"" + Resources.Lang.BETUserNameIsRequired + "\"," + _nl);
        _s.Append("Neteller : \"" + Resources.Lang.NetellerAccount + "\"," + _nl);
        _s.Append("Moneybooker : \"" + Resources.Lang.MoneyBooker.Replace("\"", "'") + " " + Resources.Lang.Email1 + " " + strIsRequire + "\"," + _nl);
        _s.Append("MoneybookerValid : \"" + Resources.Lang.MoneybookersEma + "\"," + _nl);
        _s.Append("BankName : \"" + Resources.Lang.BankNameisrequi + "\"," + _nl);
        _s.Append("BankAddress : \"" + Resources.Lang.BankAddressisre + "\"," + _nl);
        _s.Append("BankCode : \"" + Resources.Lang.BankABA_Swift_SIsRequired + "\"," + _nl);
        _s.Append("BankAccName : \"" + Resources.Lang.BankAccountNameIsRequired + "\"," + _nl);
        _s.Append("BankAccNumber : \"" + Resources.Lang.BankAccountNumbIsRequired + "\"," + _nl);
        string strCheque = Resources.Lang.Cheque.Replace("\"", "'");
        _s.Append("ChequeFullname : \"" + Resources.Lang.ChequeFullNamei + "\"," + _nl);
        _s.Append("ChequeAddress : \"" + Resources.Lang.ChequeAddressis + "\"," + _nl);
        _s.Append("ChequeCity : \"" + Resources.Lang.ChequeCityisreq + "\"," + _nl);
        _s.Append("ChequeState : \"" + Resources.Lang.ChequeStateisre + "\"," + _nl);
        _s.Append("ChequePostcode : \"" + Resources.Lang.ChequePostcode_ + "\"," + _nl);
        _s.Append("OldPass : \"" + Resources.Lang.OldPasswordisre + "\"," + _nl);
        _s.Append("NewPass : \"" + Resources.Lang.NewPasswordisre + "\"," + _nl);
        _s.Append("RePass : \"" + Resources.Lang.ReenterNewPassw + "\"," + _nl);

        _s.Append("ChequeCountry : \"" + Resources.Lang.ChequeCountryis + "\"," + _nl);

        _s.Append("Name: \"" + Resources.Lang.NameIsInvalid + "\"," + _nl);
        _s.Append("Comment: \"" + Resources.Lang.CommentsIsInvalid + "\"," + _nl);
        _s.Append("Subject: \"" + Resources.Lang.SubjectIsInvalid + "\"," + _nl);
        _s.Append("RootUrl : \"" + Common.RootURL() + "\"," + _nl);

        _s.Append("language: \"" + "\"" + _nl);



        _s.Append("};");
        strErrMsg = _s.ToString();
        //Response.Write("<script type='text/javascript'>" + _nl + _s.ToString() + _nl + "</script>" + _nl);


        this.MainNavHtml.Text = navTpl;

        DateTime dt = DataHandler.ServerDateTime.GetCurrentDBTime(out gmt);
        year = dt.Year.ToString();
        month = dt.Month.ToString();
        day = dt.Day.ToString();
        hour = dt.Hour.ToString();
        minute = dt.Minute.ToString();
        second = dt.Second.ToString();

    }
    private void InitializeHotNews()
    {
        bool isImportant;
        string latestews = CacheUtil.GetLatestHotNews(out isImportant);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + latestews + "');", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString("N"), "alert('" + latestews + "');", true);
        
        string textStyle = "text_hotnews";
        StringBuilder news = new StringBuilder();
        string url = String.Format(Common.RootURL() + "/UserInfo/Message.aspx");
        if (!string.IsNullOrEmpty(latestews))
        {
            //imgHotNews.Src = "../App_Themes/Default/Images/hotnews_" + AppSettings.Session.Language + ".gif";
            if (isImportant)
            {
                textStyle += "_important";
            }

            if (AppSettings.Session.CustID == -1)
            {
                news.Append("<span class=\"" + textStyle + "\" id=\"topnews\">" + latestews + "</span>");
                //news.Append(string.Format("<div class={0}><marquee onMouseover='this.scrollAmount=2;' onMouseout='this.scrollAmount=3;' scrollAmount='3'>", textStyle));
                //news.Append(latestews);
                //news.Append("</marquee></div>");
            }
            else
            {
                news.Append("<a class=\"" + textStyle + "\" id=\"topnews\" href=\"" + url + "\">" + latestews + "</a>");
                //string url = String.Format(Common.RootURL() + "/UserInfo/Message.aspx");

                //news.Append("<a class='link_hotnews' href='");
                //news.Append(url);
                //news.Append("'>");
                //news.Append(string.Format("<div class={0}><marquee style='cursor:pointer;' onMouseover='this.scrollAmount=2;' onMouseout='this.scrollAmount=3;' scrollAmount='3'>", textStyle));
                //news.Append(latestews);
                //news.Append("</marquee></div>");
                //news.Append("</a>");
            }
        }
        //else
        //    imgHotNews.Visible = false;
        hotnews = news.ToString();
    }
    protected string GetLangCss()
    {
        string langchar = Common.GetLanguage();
        return (langchar == "Spanish" || langchar == "Vietnamese") ? "short" : string.Empty;
    }
    protected string FocusLang(string strLang)
    {
        if (string.Compare(AppSettings.Session.Language, strLang, true) == 0)
            return "selected";
        else
            return string.Empty;
    }

}
