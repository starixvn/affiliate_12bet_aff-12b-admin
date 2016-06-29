<%@ WebHandler Language="C#" Class="ContactUS" %>

using System;
using System.Web;
using System.Web.SessionState;
using Affiliate.MailServices;
using DNAide.Web;
using System.Text;

public class ContactUS : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) 
    {
        context = AffHandler.InitializeCulture(context);
        context.Response.ContentType = "text/html";
        if (AppSettings.Session.IsInternalSite)
        {
            context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + Resources.Lang.ErrAuthorized + "\"], [\"error_item\"], \"error_item\");</script>");
            return;
        }      
        try
        {
            context.Response.ContentType = "text/html";
            string strCaptcha = context.Request.Form["txtValidateRegister"];
            if (!Common.IsValidCaptcha(strCaptcha, context.Request.UserHostAddress))
            {
                StringBuilder err = new StringBuilder("<script type='text/javascript'>parent.$('validCodeRegister').src='Authorization/LoginCaptcha.ashx?ver=");
                err.Append(DateTime.Now.ToBinary().ToString());
                err.Append("';if(parent.$('validCode')){ parent.$('validCode').src=parent.$('validCodeRegister').src};parent.$('txtValidateRegister').value='");
                err.Append(Resources.Lang.Validation);
                err.Append("';if(parent.$('txtvalidate')) {parent.$('txtvalidate').value=parent.$('txtValidateRegister').value};parent.AddError('txtValidateRegister','");
                err.Append(Resources.Lang.ValidationCode);
                err.Append("');parent.$('errMsg').show();parent.ShowError(parent.arrError, parent.arrItem, 'error_item');</script>");
                context.Response.Write(err.ToString());

                return;
            }


            string subject = "12Bet Affiliates - Contact Us - " + context.Request.Form["txtSubject"];
            string result = DataHandler.EmailHistory.InsertEmail(context.Request.Form["txtContent"], context.Request.Form["txtName"], context.Request.Form["txtAffID"],context.Request.Form["txtEmail"], subject);
            if(result!=string.Empty){
                context.Response.Write("<script type ='text/javascript'>parent.SendMailFail('" + result + "');</script>");
                return;
            }

            var mailService = new ContactUsMailService
                {
                    Name = context.Request.Form["txtName"],
                    Email = context.Request.Form["txtEmail"],
                    AffId = context.Request.Form["txtAffID"],
                    Content = context.Request.Form["txtContent"],
                    Subject = subject,
                };
            
            mailService.SendContactUsMail();
            
            
            string script = "<script type ='text/javascript'>";

            if (AppSettings.Session.IsFromHome)
            {
                script += "parent.window.location.href = '../ContactUs.aspx?f=h';";
            }
            else
            {
                script += "parent.SendMailSuccess();";
            }
            
            script += "</script>";
            
            context.Response.Write(script);
        }
        catch (Exception ex)
        {
            string script = "<script type ='text/javascript'>";
            
            if (ex.Message.IndexOf("timed out") > 0)
                script += "parent.SendMailSuccess();";
            else
                script += "parent.SendMailFail('"+  ex.Message.Replace("'", "").Replace(Environment.NewLine, "") +"');";
            
            script += "</script>";
            context.Response.Write(script);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
