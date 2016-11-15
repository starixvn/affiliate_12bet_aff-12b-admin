using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class ChangePassService
    {
        public void ChangePass(HttpContext context)
        {
            context = IBCHandler.InitializeCulture(context);
            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + Resources.Lang.Err500 + "\");</script>");
                return;
            }

            try
            {
                string strNewPass = context.Request["NewPassword"];
                int intCustId = CastAide.AsInt32(context.Request["custid"]);
                int isAdmin = CastAide.AsInt32(context.Request["isAdmin"], 0);
                int intErrCode = 0;
                string errmsg = "";
                DataHandler.Affiliate.ResetPass(intCustId, AppSettings.Session.CustID, strNewPass, ref intErrCode, ref errmsg, isAdmin);
                if (intErrCode == 0)//send email and close this form
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess(\"" + Resources.Lang.ResetPassSuccess + "\");</script>");
                }
                else//alert error
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + errmsg + "\");</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + ex.Message + "\");</script>");
                Fanex.Logging.LogService.Error(ex.Message);
            }
        }
    }
}