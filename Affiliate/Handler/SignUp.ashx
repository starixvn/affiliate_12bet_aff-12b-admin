<%@ WebHandler Language="C#" Class="SignUp" %>

using System;
using System.Web;
using System.Web.SessionState;
using DataHandler;
using DNAide.Web;
using DNAide.Core;
using System.Text;

public class SignUp : IHttpHandler, IRequiresSessionState
{    
    public void ProcessRequest (HttpContext context) {
        context = AffHandler.InitializeCulture(context);     
        context.Response.ContentType = "text/html";
        try
        {
            string strCaptcha = context.Request.Form["txtValidateRegister"];
            if (string.Compare(AppSettings.Session.CaptchaCode, strCaptcha.Trim(), true) != 0)
            {
                StringBuilder err = new StringBuilder("<script type='text/javascript'>parent.$('validCodeRegister').src='Authorization/LoginCaptcha.ashx?ver=");
                err.Append(DateTime.Now.ToBinary().ToString());
                err.Append("'; parent.$('validCode').src=parent.$('validCodeRegister').src;parent.$('txtValidateRegister').value='");
                err.Append(Resources.Lang.Validation);
                err.Append("';parent.$('txtvalidate').value=parent.$('txtValidateRegister').value;parent.AddError('txtValidateRegister','");
                err.Append(Resources.Lang.ValidationCode);
                err.Append("');parent.$('errMsg').show();parent.ShowError(parent.arrError, parent.arrItem, 'error_item');</script>");
                context.Response.Write(err.ToString());

                return;
            }
            
            string strPayment = Common.GetMethodName(context.Request.Form["hdMethod"]);
            string strPlayerUserName = context.Request.Form["txtBet12UserName"];
            string errMsg = string.Empty;

            if (strPayment == "Player")
            {
                DepositCustomerInfo customer = DataHandler.PaymentRequest.Get12BetPlayerDepositInfo(strPlayerUserName);
                if (customer.Success != 0)
                { /* There was an error when get customer info in Deposit DB */
                    //  strErrorMsg = "There was an error when get customer info in Deposit DB";
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"divErr\").show();parent.ShowError([\"" + Resources.Lang.errDepositDBConnectFailed + "\"], [\"txtBet12UserName\"], \"error_item\");</script>"); 
                    return;
                }
            }            
             
            int intErrCode = 0;
            int intCustId = 0;
            bool Gender = (context.Request.Form["rdGender"] == "0" ? false : true);
            string strPreferLang = context.Request.Form["slPreferLang"];
            string strFName = context.Request.Form["txtFName"];
            string strLName = context.Request.Form["txtLName"];
            string strEmail = context.Request.Form["txtEmail"];
            string strPhone = context.Request.Form["txtNumber"];
            string strMobile = context.Request.Form["txtMobile"];
            string strAddress = context.Request.Form["txtAddress"];
            string strCity = context.Request.Form["txtCity"];
            string strCountry = context.Request.Form["selCountry"];
            string strPostCode = context.Request.Form["txtPost"];
            
            string strState = context.Request.Form["txtState"];
            string strSiteUrl = context.Request.Form["txtSiteUrl"];
            string strErrMsg = "";            
            //Remove prefer lang and user English as default
            string strQuestion = context.Request.Form["slSecurityQuestion"];
            string strAnswer = context.Request.Form["txtAnswer"];
            string strURL = context.Request.Form["txtSiteUrl"];
            string strMoreUrl = context.Request.Form["hdMoreurl"];
            string strNoSiteDesc = context.Request.Form["txtNoWebsite"];

            if (context.Request["rdWebsite"] == "0")
                strNoSiteDesc = string.Empty;
            else
            {
                strURL = string.Empty;
                strMoreUrl = string.Empty;
            }
            
            
            string strUsername = string.Empty;
            UserAff.CreateUser(strFName, strLName, Gender, strEmail, strPhone, strMobile, strAddress, strCity, strState, strPostCode, strCountry, AppSettings.Session.Language, ref intCustId, strPayment, strPlayerUserName, strPreferLang, strURL, strMoreUrl, strNoSiteDesc, strQuestion, strAnswer, ref strUsername, ref intErrCode, ref strErrMsg);
            if (intErrCode != 0)
            {
                //throw new Exception(strErrMsg);
                string strErrObj = "error_item";
                string strMsg = Common.GetErrMsg(intErrCode, ref strErrObj);
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + strMsg + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
            }
            else
            {
                string strFileName = string.Empty;
                string strCurrLang = AppSettings.Session.Language;
                if (strPreferLang.ToUpper() == string.Empty)
                    strPreferLang = "en-US";
                strFileName = "Email/SignupMail/SignupMail_" + strPreferLang + Common.EmailSuffix(true) + ".htm";                
               
                string strRegisMail = TemplateHandler.LoadTemplate(strFileName).Replace("\r\n", "");
                TemplateHandler.Assign(ref strRegisMail, "fname", strFName);
                TemplateHandler.Assign(ref strRegisMail, "lname", strLName);
                TemplateHandler.Assign(ref strRegisMail, "mailadd", AppSettings.MailContact);
                TemplateHandler.Assign(ref strRegisMail, "rooturl", Common.RootURL());
                TemplateHandler.Assign(ref strRegisMail, "lang", strPreferLang);
                string strRegisSubject = TemplateHandler.ReplaceBlock(ref strRegisMail, "subject", "");
                DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strRegisSubject, strRegisMail, AppSettings.MailPwd);

                string strCsdMail = TemplateHandler.LoadTemplate("CsdMail.htm").Replace("\r\n", "");
                TemplateHandler.Assign(ref strCsdMail, "fname", strFName);
                TemplateHandler.Assign(ref strCsdMail, "lname", strLName);
                TemplateHandler.Assign(ref strCsdMail, "username", strUsername);
                TemplateHandler.Assign(ref strCsdMail, "mailadd", AppSettings.MailContact);

                if (context.Request["rdWebsite"] == "0")
                {
                    TemplateHandler.Assign(ref strCsdMail, "siteurl", strSiteUrl);
                    TemplateHandler.RemoveBlock(ref strCsdMail, "nositeurl");
                    if (strMoreUrl == "")
                        TemplateHandler.ReplaceBlock(ref strCsdMail, "moreurl", "");
                    else
                        TemplateHandler.Assign(ref strCsdMail, "moreurl", strMoreUrl.Replace("\\n", ","));
                }
                else
                {
                    TemplateHandler.RemoveBlock(ref strCsdMail, "siteurl");
                    TemplateHandler.Assign(ref strCsdMail, "nositedesc", strNoSiteDesc);
                }
               
                string strCsdSubject = TemplateHandler.ReplaceBlock(ref strCsdMail, "subject", "");
                DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, AppSettings.ToMail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strCsdSubject, strCsdMail, AppSettings.MailPwd);
                context.Response.Write("<script type='text/javascript'>top.location.href='../SignUpSuccess.aspx';</script>");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("<script type='text/javascript'>parent.document.getElementById(\"errMsg\").show();parent.ShowError([\"" + ex.Message + "\"], [\"error_item\"], \"error_item\");</script>");
            DNAideLog.Write(ex.Message);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}