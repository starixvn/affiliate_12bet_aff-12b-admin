<%@ WebHandler Language="C#" Class="CheckForgetPassWord" %>

using System;
using System.Web;
using System.Web.SessionState;
using Affiliate;
using Affiliate.MailServices;
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
                var mailService = new ForgetPasswordMailService
                    {
                        PreferLanguage = preferredLanguage,
                        Code = code,
                        Email = email,
                        FirstName = firstname,
                        IdCust = idcust,
                        MailOption = mailOption,
                        UserName = username
                    };
                
                mailService.SendForgetPassword();

                context.Response.Write("Ok");
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
            context.Response.Write(Resources.Lang.MailServerNotAvailable);
            Fanex.Logging.LogService.Error(ex.Message, ex);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}