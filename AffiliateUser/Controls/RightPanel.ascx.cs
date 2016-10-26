using System;
using System.Text;
using System.Web;
using AffiliateUser.Business.Services.Authorization;

public partial class Controls_RightPanel : RootControl
{
    protected string strUsername, strPwd, strValidate, strLogin, strSignup, strForgetPwd, errmsg, strTitleLogin;
    public string script = "";
    public string strURL = string.Empty;
    public string strURL2 = string.Empty;

    protected string _XsrfTokenFormValue;

    protected override void OnInit(EventArgs e)
    {
        _XsrfTokenFormValue = AuthHelper.GenerateXsrfFormToken(HttpContext.Current);

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GenURL();
        if (AppSettings.Session.IsAuthenticated)
        {
            ltrUserName.Text = AppSettings.Session.UserName;
            LoginPanel.Visible = false;
        }
        else
        {
            divAfterLogin.Visible = false;
        }

        script += "if($('val')) $('txtvalidate').value = $('val').value;";

        strUsername = Resources.Lang.Username;
        strPwd = Resources.Lang.Password;
        strValidate = Resources.Lang.Validation;
        strLogin = Resources.Lang.Login;
        strSignup = Resources.Lang.SignUp;
        strForgetPwd = Resources.Lang.ForgetPassword;
        strTitleLogin = Resources.Lang.TitleLogin;
        string _nl = Environment.NewLine;
        StringBuilder _s = new StringBuilder();
        _s.Append("var gMsg = {" + _nl); // global message
        _s.Append("     entryusername: \"" + Resources.Lang.Username.Replace("\"", "'") + "\"," + _nl);
        _s.Append("     entrypassword: \"" + Resources.Lang.Password.Replace("\"", "'") + "\"," + _nl);
        _s.Append("     entryvalidation: \"" + Resources.Lang.Validation.Replace("\"", "'") + "\"," + _nl);
        _s.Append("     Errmsgentryvalidcode: \"" + Resources.Lang.ErrmsgEntryValidCode.Replace("\"", "'") + "\"," + _nl);
        _s.Append("     Errmsgentryusername: \"" + Resources.Lang.ErrmsgEntryUsername.Replace("\"", "'") + "\"," + _nl);
        _s.Append("     Errmsgentrypassword: \"" + Resources.Lang.ErrmsgEntryPassword.Replace("\"", "'") + "\"," + _nl);
        _s.Append("     ErrAuthorized: \"" + Resources.Lang.ErrAuthorized.Replace("\"", "'") + "\"" + _nl);
        _s.Append("};");
        errmsg = _nl + _s.ToString() + _nl;
    }

    private void GenURL()
    {
        string strTmp = Request.Url.AbsolutePath;
        strURL = "/Content/Banner/" + Common.GetLanguage() + "/smallbanner.gif?v=" + AppSettings.JsVersion;
        if (strTmp.ToLower().IndexOf("userinfo") >= 0)
        {
            strURL = ".." + strURL;
        }
    }
}