using System;
using System.Web;

namespace AffiliateUser.Business.Services
{
    public class UpdateSecurityQuestionService
    {
        public void UpdateSecurityQuestion(HttpContext context)
        {
            context = AffHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";
            if (string.IsNullOrEmpty(context.Request.Form["slSecurityQuestion"]) || string.IsNullOrEmpty(context.Request.Form["txtAnswer"]))
            {
                string strErrObj = "error_item";
                string strMsg = "Invalid data are sent to the server!";
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + strMsg + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                return;
            }
            try
            {
                string strQuestion = context.Request.Form["slSecurityQuestion"];
                string strAnswer = context.Request.Form["txtAnswer"];
                int intErrCode = 0;
                string errmsg = string.Empty;
                intErrCode = DataHandler.Auth.ExecUpdateSecurityQuestion(strQuestion, strAnswer, ref errmsg, context);
                if (intErrCode == 0)
                {
                    AppSettings.Session.HasSecurityQuestion = true;
                    context.Response.Write("<script type='text/javascript'>parent.alert('" + Resources.Lang.SecurityQuestionUpdated + "');parent.location.href='" + Common.RootURL() + "';</script>");
                    return;
                }
                else
                {
                    string strErrObj = "error_item";
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + errmsg + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                    return;
                }
            }
            catch (Exception ex)
            {
                string strErrObj = "error_item";
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + ex.Message + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                Fanex.Logging.LogService.Error(ex.Message);
            }
        }
    }
}