using System;
using System.Web;
using DataHandler;

namespace AffiliateUser.Business.Services
{
    public class ChangePassService
    {
        public void ChangePass(HttpContext context)
        {
            context = AffHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";

            if (AppSettings.Session.IsInternalSite)
            {
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + Resources.Lang.ErrAuthorized + "\"], [\"error_item\"], \"error_item\");</script>");
                return;
            }

            if (string.IsNullOrEmpty(context.Request.Form["txtCurrPass"]) || string.IsNullOrEmpty(context.Request.Form["txtNewPass"]))
            {
                context.Response.Write("<script type='text/javascript'>parent.alert('" + Resources.Lang.OldPassandNewPass + "');</script>");
                return;
            }

            try
            {
                string strOldPass = context.Request.Form["txtCurrPass"];
                string strNewPass = context.Request.Form["txtNewPass"];
                int intErrCode = 0;
                UserAff.ChangePassword(AppSettings.Session.CustID, strNewPass, strOldPass, AppSettings.Session.CustID, AppSettings.Session.Language, ref intErrCode);
                if (intErrCode == 0)
                {
                    context.Response.Write("<script type='text/javascript'>parent.alert('" + Resources.Lang.PwdChanged + "');parent.location.href='../UserInfo/MyAccount.aspx';</script>");
                    return;
                }

                if (intErrCode == 1001)
                {
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.ShowError([\"" + Resources.Lang.OldPassErr + "\"], [\"txtCurrPass\"], \"error_item\");</script>");
                    return;
                }

                context.Response.Write("<script type='text/javascript'>parent.alert(\"" + Resources.Lang.ChangePassErr + "\");</scrip>");
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.alert(\"" + ex.Message + "\");</scrip>");
                Fanex.Logging.LogService.Error(ex.Message);
            }
        }
    }
}