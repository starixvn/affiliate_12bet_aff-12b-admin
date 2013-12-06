<%@ WebHandler Language="C#" Class="ProcessLogin" %>

using System;
using System.Web;
using System.Web.SessionState;
using DNAide.Web;
using DNAide.Data;  

public class ProcessLogin : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        context = AffHandler.InitializeCulture(context);
        context.Response.ContentType = "text/html";
        string _s = string.Empty;
        try
        {
            string _captcha = context.Request.Form["vcode"];
            string _username = context.Request.Form["un"];
            string _password = context.Request.Form["pw"];
            if (string.Compare(AppSettings.Session.CaptchaCode, _captcha, true) != 0)
            {
                _s = "101:" + Resources.Lang.ValidationCode;
            }
            else
            {
                AppSettings.Session.IsInternalSite = false;
                DbResult dr = DataHandler.Auth.ExecLogin(_username, _password, context);
                if (string.IsNullOrEmpty(dr.Outputs["errmsg"].ToString()))
                {
                    AppSettings.Session.CustID = DNAide.Core.CastAide.AsInt32(dr.Outputs["custid"], -1);
                    AppSettings.Session.OnUser = DataHandler.Auth.LoginOnuser(context);
                    AppSettings.Session.UserName = _username;
                    AppSettings.Session.FirstName = DNAide.Core.CastAide.AsString(dr.Outputs["firstname"]);
                    AppSettings.Session.LastName = DNAide.Core.CastAide.AsString(dr.Outputs["lastname"]);
                    AppSettings.Session.Email = DNAide.Core.CastAide.AsString(dr.Outputs["email"]);
                    //20090202@DungTong Add to check this user has security question input
                    AppSettings.Session.HasSecurityQuestion = DNAide.Core.CastAide.AsBoolean(dr.Outputs["hassecurityquestion"]);
                    AppSettings.Session.HasDailyDetail = DNAide.Core.CastAide.AsBoolean(dr.Outputs["hasdailydetail"]);
                    AppSettings.Session.MailOption = DNAide.Core.CastAide.AsBoolean(dr.Outputs["mailoption"], true);//true is defined plain text
                    AppSettings.Session.PreferLang = DNAide.Core.CastAide.AsString(dr.Outputs["preferlang"], "en-US");
                    AppSettings.Session.IsCasino = DNAide.Core.CastAide.AsBoolean(dr.Outputs["iscasino"], false);

                    if (AppSettings.Session.Language == null)
                        AppSettings.Session.Language = "en-US";
                    
                    _s = "100:OK";
                    AppSettings.Session.FirsTimeLoginSucc = true;
                    AppSettings.Session.CommGroupName = DNAide.Core.CastAide.AsString(dr.Outputs["commgroupname"]);
                }
                else
                {
                    _s ="102:" + dr.Outputs["errmsg"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            DNAideLog.Write(ex.Message);
            _s = "102:" + ex.Message;
#if DEBUG
            throw;
#endif
        }
        context.Response.Write(_s);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
