using System;
using Affiliate.MailServices;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Web;

public partial class Recoverpw : RootPage
{
    public string forgetpassword = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            forgetpassword = Resources.Lang.ForgetPassword;
            string id = (Request["uid"] != null ? Request["uid"] : "");
            string vid = (Request["vid"] != null ? Request["vid"] : "");

            string errmsg = "";
            string email = "";
            string username = "";
            string firstname = "";
            string lastname = "";
            string strPreferredLanguage = string.Empty;
            bool mailOption = false;
            if (id != "" && vid != "")
            {
                string newpw = Encryption.RandomString(8);
                int result = DataHandler.Auth.ExecChangePassword(CastAide.AsInt32(id), vid, newpw, ref errmsg, ref email, ref username, ref firstname, ref lastname, ref strPreferredLanguage, ref mailOption);
                if (result == 0)
                {
                    string url = "<a href='{url}'>{url}</a>".Replace("{url}", Common.RootURL());

                    var mailService = new RecoverPasswordMailService
                        {
                            FirstName = firstname,
                            PreferredLanguage = strPreferredLanguage,
                            UserName = username,
                            Email = email,
                            MailOption = mailOption,
                            NewPassword = newpw,
                            Url = url
                        };

                    mailService.SendRecoverPasswordMail();

                    string tplSucc = TemplateHandler.LoadTemplate("LoginDetailSucc.htm");
                    TemplateHandler.Assign(ref tplSucc, "Yourlogindetail", Resources.Lang.Yourlogindetail);
                    TemplateHandler.Assign(ref tplSucc, "Pleasecheckyour", Resources.Lang.Pleasecheckyour);
                    TemplateHandler.Assign(ref tplSucc, "Foranyreasonyou", Resources.Lang.Foranyreasonyou);
                    Literal1.Text = tplSucc;
                }
                else
                {
                    Literal1.Text = errmsg;
                }
            }
            else
            {
                string err = Resources.Lang.ErrMsgForgetPwFail;
                TemplateHandler.Assign(ref err, "here", Common.RootURL() + "/ForgetPass.aspx");
                Literal1.Text = err;
            }
        }
    }
}