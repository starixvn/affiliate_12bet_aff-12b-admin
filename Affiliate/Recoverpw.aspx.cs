using System;
using DNAide.Utils;
using DNAide.Web;
using DNAide.Core;
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
           
            string errmsg="";         
            string email="";
            string username="";
            string firstname = "";
            string lastname = "";
            string strPreferredLanguage = string.Empty;
            bool mailOption = false;
            if (id != "" && vid != "")
            {
                string newpw=Encryption.RandomString(8);
                int result = DataHandler.Auth.ExecChangePassword(CastAide.AsInt32(id), vid, newpw, ref errmsg, ref email, ref username, ref firstname, ref lastname, ref strPreferredLanguage, ref mailOption);
                if (result == 0)
                {
                    string subject=Resources.Lang.SubjectNewPass;
                    //string body=TemplateHandler.LoadTemplate("BodyNewPassForget.htm");
                    string url = "<a href='{url}'>{url}</a>".Replace("{url}", Common.RootURL());
                    //TemplateHandler.Assign(ref body, "UsernameText", Resources.Lang.Username);
                    //TemplateHandler.Assign(ref body, "PasswordText", Resources.Lang.Password);
                    //TemplateHandler.Assign(ref body, "Dear", Resources.Lang.Dear);
                    //TemplateHandler.Assign(ref body, "Asyouhaverequested", Resources.Lang.Asyouhaverequested);
                    //TemplateHandler.Assign(ref body, "Howeveryoucanvisit", Resources.Lang.Howeveryoucanvisit);                    
                    //TemplateHandler.Assign(ref body, "Haveagreatday", Resources.Lang.Haveagreatday);
                    //TemplateHandler.Assign(ref body, "BestRegards", Resources.Lang.BestRegards);
                    //TemplateHandler.Assign(ref body, "Theteamat_12Bet", Resources.Lang.Theteamat_12Bet);


                    string strMail = TemplateHandler.LoadTemplate("Email/RecoverPass/RecoverPass_" + strPreferredLanguage + Common.EmailSuffix(mailOption) + ".htm").Replace("\r\n", "");
                    TemplateHandler.Assign(ref strMail, "firstname", firstname);
                    TemplateHandler.Assign(ref strMail, "username", username);
                    TemplateHandler.Assign(ref strMail, "password", newpw);
                    TemplateHandler.Assign(ref strMail, "url", url);
                    TemplateHandler.Assign(ref strMail, "mailadd", AppSettings.MailContact);
                    TemplateHandler.Assign(ref strMail, "rooturl", Common.RootURL());
                    TemplateHandler.Assign(ref strMail, "lang", strPreferredLanguage);
                    string strSubject = TemplateHandler.ReplaceBlock(ref strMail, "subject", "");
                    bool isok = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, email, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strMail, AppSettings.MailPwd);

                    //TemplateHandler.Assign(ref body,"username",username);
                    //TemplateHandler.Assign(ref body, "password",newpw);
                    //TemplateHandler.Assign(ref body, "firstname", firstname);
                    //TemplateHandler.Assign(ref body, "lastname", lastname);
                    //TemplateHandler.Assign(ref body, "url", url);
                    //bool isok = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, email, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, subject, body, AppSettings.MailPwd);
                    if (isok)
                    {                        
                        string tplSucc=  TemplateHandler.LoadTemplate("LoginDetailSucc.htm");
                        TemplateHandler.Assign(ref tplSucc, "Yourlogindetail", Resources.Lang.Yourlogindetail);
                        TemplateHandler.Assign(ref tplSucc, "Pleasecheckyour", Resources.Lang.Pleasecheckyour);
                        TemplateHandler.Assign(ref tplSucc, "Foranyreasonyou", Resources.Lang.Foranyreasonyou);
                        Literal1.Text = tplSucc;
                    }
                    else
                    {
                        Literal1.Text = Resources.Lang.ErrSendMail;
                    }
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
