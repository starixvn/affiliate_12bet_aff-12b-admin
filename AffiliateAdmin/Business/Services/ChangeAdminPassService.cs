using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class ChangeAdminPassService
    {
        public void ChangeAdminPass(HttpContext context)
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
                string strOldPass = context.Request["OldPass"];
                string strNewPass = context.Request["NewPassword"];
                int intCustId = CastAide.AsInt32(context.Request["custid"]);
                int intErrCode = DataHandler.Account.ChangePass(AppSettings.Session.CustID, strOldPass, strNewPass, Common.GetLangChar());
                if (intErrCode == 0)
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateSucess('" + Resources.Lang.UpdateSucc + "');parent.location.href='../Home/Home.aspx';</script>");///Alert.aspx?user=" + Resources.Lang.Password + "&code=0&link=UserInfo/AffiliateList.aspx?status=approved';</script>");
                }
                else//alert error
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + Resources.Lang.OldPassIncorrect + "\");</script>");
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