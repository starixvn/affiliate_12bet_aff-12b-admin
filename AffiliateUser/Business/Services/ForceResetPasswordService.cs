using System;
using System.Web;
using DataHandler;

namespace AffiliateUser.Business.Services
{
    public class ForceResetPasswordService
    {
        public void ForceResetPassword(HttpContext context)
        {
            context = AffHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";

            try
            {
                if (AppSettings.Session.IsInternalSite)
                {
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.Common.showError([\"" + Resources.Lang.ErrAuthorized + "\"], [\"error_item\"], \"error_item\");</script>");
                    return;
                }

                string strOldPassword = context.Request.Form["txtOldPassword"];
                string strNewPassword = context.Request.Form["txtNewPassword"];
                string strConfirmPassword = context.Request.Form["txtReNewPassword"];
                string strSecurityQuestion = context.Request.Form["slSecurityQuestion"];
                string strYourAnswer = context.Request.Form["txtYourAnswer"];

                if (string.IsNullOrEmpty(strOldPassword) || string.IsNullOrEmpty(strNewPassword))
                {
                    context.Response.Write("<script type='text/javascript'>parent.alert('" + Resources.Lang.OldPassandNewPass + "');</script>");
                    return;
                }

                if (!strNewPassword.Equals(strConfirmPassword))
                {
                    context.Response.Write("<script type='text/javascript'>parent.alert('" + Resources.Lang.PasswordAndConfirmPasswordDontMatch + "');</script>");
                    return;
                }

                int intErrCode = 0;
                int intCustId = AppSettings.Session.CustID;
                string errMsg = string.Empty;

                UserAff.ForceChangePass(intCustId, strOldPassword, strNewPassword, strSecurityQuestion, strYourAnswer, ref intErrCode, ref errMsg);

                if (intErrCode != 0)
                {
                    string strErrObj = "error_item";
                    string strMsg = Common.GetErrMsg(intErrCode, ref strErrObj);
                    context.Response.Write(
                        "<script type='text/javascript'>parent.$(\"errMsg\").show();parent.Common.showError([\"" + strMsg +
                        "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                }
                else
                {
                    AppSettings.Session.ForceChangePass = false;
                    AppSettings.Session.HasSecurityQuestion = true;
                    context.Response.Write("<script type='text/javascript'>top.location.href='../UserInfo/Account_Overview.aspx';</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.document.getElementById(\"errMsg\").show();parent.Common.showError([\"" + ex.Message + "\"], [\"error_item\"], \"error_item\");</script>");
                Fanex.Logging.LogService.Error(ex.Message);
            }
        }
    }
}