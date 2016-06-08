<%@ WebHandler Language="C#" Class="ProcessLogin" %>

using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using Affiliate;
using DNAide.Web;
using Resources;

public class ProcessLogin : IHttpHandler, IRequiresSessionState
{
    private const string CheatCaptchaCode = "1508";

    public void ProcessRequest(HttpContext context)
    {
        context = AffHandler.InitializeCulture(context);
        context.Response.ContentType = "text/html";
        string message = string.Empty;

        try
        {

            string captcha = context.Request.Form["vcode"];

            if (!IsValidCaptcha(captcha, context.Request.UserHostAddress))
            {
                message = "101:" + Resources.Lang.ValidationCode;
            }
            else
            {
                string _username = context.Request.Form["un"];
                string _password = context.Request.Form["pw"];
                AppSettings.Session.IsInternalSite = false;
                var dr = DataHandler.Auth.ExecLogin(_username, _password, context);
                if (string.IsNullOrEmpty(dr.Outputs["errmsg"].ToString()))
                {
                    var afftype = (AffType)DNAide.Core.CastAide.AsInt32(dr.Outputs["afftype"], 2);

                    var currentDomain = HttpContextAnalysis.GetDomain(context);
                    var port = HttpContextAnalysis.GetPort(context);

                    bool isValidLogin = AppSettings.AffTypeList.Any(t => t == afftype);

                    if (!isValidLogin)
                    {
                        message = "103: " + Lang.NotAllowOnDomain;
                        context.Response.Write(message);
                        return;
                    }

                    AppSettings.Session.AffType = afftype;
                    AppSettings.Session.CustID = DNAide.Core.CastAide.AsInt32(dr.Outputs["custid"], -1);
                    AppSettings.Session.OnUser = DataHandler.Auth.LoginOnuser(context);
                    AppSettings.Session.UserName = _username;

                    AppSettings.Session.FirstName = DNAide.Core.CastAide.AsString(dr.Outputs["firstname"]);
                    AppSettings.Session.LastName = DNAide.Core.CastAide.AsString(dr.Outputs["lastname"]);
                    AppSettings.Session.Email = DNAide.Core.CastAide.AsString(dr.Outputs["email"]);
                    //20090202@DungTong Add to check this user has security question input
                    AppSettings.Session.HasSecurityQuestion =
                        DNAide.Core.CastAide.AsBoolean(dr.Outputs["hassecurityquestion"]);
                    AppSettings.Session.HasDailyDetail = DNAide.Core.CastAide.AsBoolean(dr.Outputs["hasdailydetail"]);
                    AppSettings.Session.MailOption = DNAide.Core.CastAide.AsBoolean(dr.Outputs["mailoption"], true);
                    //true is defined plain text
                    AppSettings.Session.PreferLang = DNAide.Core.CastAide.AsString(dr.Outputs["preferlang"], "en-US");
                    AppSettings.Session.IsCasino = DNAide.Core.CastAide.AsBoolean(dr.Outputs["iscasino"], false);
                    AppSettings.Session.ForceChangePass = DNAide.Core.CastAide.AsBoolean(dr.Outputs["forcechangepass"], false);

                    if (AppSettings.Session.Language == null)
                        AppSettings.Session.Language = "en-US";

                    AppSettings.Session.FirsTimeLoginSucc = true;
                    AppSettings.Session.CommGroupName = DNAide.Core.CastAide.AsString(dr.Outputs["commgroupname"]);

                    message = AppSettings.Session.ForceChangePass ? "99:Force to change password" : "100:OK";

                    context.Response.Cookies["Username"].Value = _username.ToUpper();

                    context.Response.Cookies["Username"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    message = "102:" + dr.Outputs["errmsg"];
                }
            }
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
            message = "102:" + ex.Message;
#if DEBUG
                        throw;
#endif
        }
        context.Response.Write(message);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private static bool IsValidCaptcha(string captcha, string userHostName)
    {
        var isProduction = AppSettings.Environment.Equals("PRODUCTION", StringComparison.OrdinalIgnoreCase);

        if (!isProduction)
        {
            return true;
        }

        if (captcha.Equals(AppSettings.Session.CaptchaCode, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return captcha.Equals(CheatCaptchaCode) && Regex.IsMatch(userHostName, AppSettings.CompanyIP);
    }
}