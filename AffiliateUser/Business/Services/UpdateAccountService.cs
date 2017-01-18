using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using DataHandler;

namespace AffiliateUser.Business.Services
{
    public class UpdateAccountService
    {
        public void UpdateAccount(HttpContext context)
        {
            context = AffHandler.InitializeCulture(context);
            context.Response.ContentType = "text/html";

            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"succMsg\").hide();parent.$(\"divErr\").show();parent.Common.showError([\"" + Resources.Lang.ErrAuthorized + "\"], [\"error_item\"], \"error_item\");</script>");
                return;
            }

            if (AppSettings.Session.IsInternalSite)
            {
                context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"succMsg\").hide();parent.$(\"divErr\").show();parent.Common.showError([\"" + Resources.Lang.ErrAuthorized + "\"], [\"error_item\"], \"error_item\");</script>");
                return;
            }
            try
            {
                int intErrCode = 0;
                int intCustId = AppSettings.Session.CustID;

                //Check 12BetUsername Correct or not
                string strPlayerUserName = context.Request.Form["txtBet12UserName"];
                string errMsg = string.Empty;

                DepositCustomerInfo customer = DataHandler.PaymentRequest.Get12BetPlayerDepositInfo(strPlayerUserName);
                if (customer.Success != 0)
                {
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"succMsg\").hide();parent.$(\"divErr\").show();parent.Common.showError([\"" + Resources.Lang.errDepositDBConnectFailed + "\"], [\"txtBet12UserName\"], \"error_item\");</script>");
                    return;
                }

                string strNumber = context.Request.Form["txtNumber"].ToString();
                string strMobile = context.Request.Form["txtMobile"].ToString();
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

                string strErrMsg = "";

                UserAff.UpdateUser(intCustId, strNumber, strMobile, strURL, strMoreUrl, strNoSiteDesc, AppSettings.Session.Language, strPlayerUserName, ref intErrCode, ref strErrMsg);

                if (intErrCode != 0)
                {
                    string strErrObj = "error_item";
                    string strMsg = Common.GetErrMsg(intErrCode, ref strErrObj);
                    context.Response.Write("<script type='text/javascript'>parent.$(\"errMsg\").show();parent.$(\"succMsg\").hide();parent.$(\"divErr\").show();parent.Common.showError([\"" + strMsg + "\"], [\"" + strErrObj + "\"], \"error_item\");</script>");
                }
                else
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess();</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.$('errMsg').show();parent.$('succMsg').hide();parent.$('divErr').show();parent.Common.showError([\"" + ex.Message + "\"], [\"error_item\"], \"error_item\");</script>");
                Business.Helper.LogHelper.WriteError(ex);
            }
        }
    }
}