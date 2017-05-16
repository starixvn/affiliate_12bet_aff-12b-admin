using System;
using System.Text;
using System.Web;
using Affiliate.MailServices;
using DataHandler;

namespace AffiliateUser.Business.Services
{
    public class SignUpService
    {
        public void SignUp(HttpContext context)
        {
            context = AffHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            try
            {
                string strCaptcha = context.Request.Form["txtValidateRegister"] ?? string.Empty;

                if (string.Compare(AppSettings.Session.CaptchaCode, strCaptcha.Trim(), true) != 0)
                {
                    StringBuilder err = new StringBuilder("<script type='text/javascript'>parent.$('validCodeRegister').src='Authorization/LoginCaptcha.ashx?ver=");
                    err.Append(DateTime.Now.ToBinary().ToString());
                    err.Append("'; parent.$('validCode').src=parent.$('validCodeRegister').src;parent.$('txtValidateRegister').value='");
                    err.Append(Resources.Lang.Validation);
                    err.Append("';parent.$('txtvalidate').value=parent.$('txtValidateRegister').value;parent.Common.addError('txtValidateRegister','");
                    err.Append(Resources.Lang.ValidationCode);
                    err.Append("');parent.$('errMsg').show();parent.Common.showError(parent.arrError, parent.arrItem, 'error_item');</script>");
                    context.Response.Write(err.ToString());

                    return;
                }

                string strPayment = Common.GetMethodName(context.Request.Form["hdMethod"]);
                string strPlayerUserName = context.Request.Form["txtBet12UserName"];
                string errMsg = string.Empty;

                if (string.IsNullOrWhiteSpace(strPlayerUserName))
                {
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"divErr\").show();parent.Common.showError([\"Invalid player name\"], [\"txtBet12UserName\"], \"error_item\");</script>");
                    return;
                }

                if (strPayment == "Player" && !AppSettings.LocalTest)
                {
                    DepositCustomerInfo customer = DataHandler.PaymentRequest.Get12BetPlayerDepositInfo(strPlayerUserName);
                    if (customer.Success != 0)
                    {
                        context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"divErr\").show();parent.Common.showError([\"" + Resources.Lang.errDepositDBConnectFailed + "\"], [\"txtBet12UserName\"], \"error_item\");</script>");
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
                    string strErrObj = "error_item";
                    string strMsg = Common.GetErrMsg(intErrCode, ref strErrObj);
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.Common.showError([\"" + strMsg + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                }
                else
                {
                    var rdWebsite = context.Request["rdWebsite"];

                    var signupMailService = new SignupMailService
                    {
                        FirstName = strFName,
                        LastName = strLName,
                        UserName = strUsername
                    };

                    SendSignupEmail(signupMailService, strPreferLang, strEmail, rdWebsite, strSiteUrl, strMoreUrl, strNoSiteDesc);

                    context.Response.Write("<script type='text/javascript'>top.location.href='../SignUpSuccess.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                var messagex = ex.Message + "\n" + ex.StackTrace;
                context.Response.Write("<script type='text/javascript'>parent.document.getElementById(\"errMsg\").show();parent.Common.showError([\"" + messagex + "\"], [\"error_item\"], \"error_item\");</script>");
                Business.Helper.LogHelper.WriteError(ex);
            }
        }

        private static void SendSignupEmail(
        SignupMailService signupMailService,
        string strPreferLang,
        string strEmail,
        string rdWebsite,
        string strSiteUrl,
        string strMoreUrl,
        string strNoSiteDesc)
        {
            try
            {
                signupMailService.SendRegisterMail(strPreferLang, strEmail);

                signupMailService.SendCsdMail(rdWebsite, strSiteUrl, strMoreUrl, strNoSiteDesc);
            }
            catch (Exception ex)
            {
                Business.Helper.LogHelper.WriteError(ex);
            }
        }
    }
}