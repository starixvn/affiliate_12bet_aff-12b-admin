<%@ WebHandler Language="C#" Class="CheckForgetPassWord" %>

using System;
using System.Web;
using System.Web.SessionState;
using DNAide.Data;
using DNAide.Web;
using DNAide.Utils; 
public class CheckForgetPassWord : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context = AffHandler.InitializeCulture(context); 
        context.Response.ContentType = "text/html";
        string email = (context.Request["eml"] == null) ? "" : context.Request["eml"].ToString();
        string code=Encryption.RandomString(10); 
        int errcode=0;
        string errmsg="";
       
        string username = "";
        string firstname = "";
        string lastname = "";
        string preferredLanguage = string.Empty;
        bool mailOption = false;
        int idcust = DataHandler.Auth.CheckEmailForget(email, code, ref errcode, ref errmsg, ref username, ref firstname, ref lastname, ref preferredLanguage, ref mailOption);
        try
        {
            if (errcode == 0)
            {
                string strFileName = "Email/ForgetPass/ForgetPass_" + preferredLanguage + Common.EmailSuffix(mailOption) + ".htm";

                string strMail = TemplateHandler.LoadTemplate(strFileName).Replace("\r\n", "");
                TemplateHandler.Assign(ref strMail, "fname", firstname);
                TemplateHandler.Assign(ref strMail, "username", username);
                TemplateHandler.Assign(ref strMail, "url", Common.RootURL() + "/Recoverpw.aspx");
                TemplateHandler.Assign(ref strMail, "idcust", idcust.ToString());
                TemplateHandler.Assign(ref strMail, "code", code);
                TemplateHandler.Assign(ref strMail, "mailadd", AppSettings.MailContact);
                TemplateHandler.Assign(ref strMail, "rooturl", Common.RootURL());
                TemplateHandler.Assign(ref strMail, "lang", preferredLanguage);
                string strSubject = TemplateHandler.ReplaceBlock(ref strMail, "subject", "");
                bool isok = DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, email, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strMail, AppSettings.MailPwd);
                
                
                if (isok == true)
                {                    
                    context.Response.Write("Ok");
                }
                else
                {
                    context.Response.Write(Resources.Lang.CannotSendmail);
                }
            }
            else if (errcode == 1001)
            {
                context.Response.Write(Resources.Lang.ErrmsgNotExistsEmail);
            }
            else if (errcode == 1009)
            {
                context.Response.Write(Resources.Lang.ErrmsgEmailNotApprove);
            }
            else if (errcode == 1008)
            {
                context.Response.Write(Resources.Lang.ErrmsgEmailClose);
            }
            else
            {
                context.Response.Write(errmsg);
            }
        }
        catch(Exception ex)
        {
            context.Response.Write(ex.Message);
        }        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}