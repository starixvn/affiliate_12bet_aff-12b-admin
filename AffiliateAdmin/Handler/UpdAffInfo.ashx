<%@ WebHandler Language="C#" Class="UpdAffInfo" %>

using System;
using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Utils;
using DNAide.Core;
using DNAide.Web;

public class UpdAffInfo : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context = IBCHandler.InitializeCulture(context);
        try
        {
            int func = CastAide.AsInt32(context.Request["func"], 5);
            string strSuccessMsg = string.Empty;
            DbResult result = null;
            switch (func)
            {
                case 0://Update Status
                    string strStatus = context.Request["status"];
                    string strPayment = context.Request["hdPreferredPayment"];
                    int groupID = CastAide.AsInt32(context.Request["groupid"], -1);
                    string strPassword = string.Empty;
                    
                    if (!AppSettings.LocalTest && string.Compare(context.Request["status"], Common.Approved, true) == 0 && strPayment == "Player")
                    {
                        string strPlayerUserName = context.Request["hdPlayer_Username"];


                        DepositCustomerInfo customer =
                            DataHandler.Affiliate.Get12BetPlayerDepositInfo(strPlayerUserName);
                        if (customer.Success != 0)
                        {
                            /* There was an error when get customer info in Deposit DB */
                            //  strErrorMsg = "There was an error when get customer info in Deposit DB";
                            context.Response.Write("1@" + Resources.Lang.errDepositDBConnectFailed);
                            return;
                        }
                    }

                    if (string.Compare(strStatus, Common.Approved, true) == 0 || string.Compare(strStatus, Common.Disapproved, true) == 0)
                        strPassword = DNAide.Utils.Encryption.RandomString(6);
                    result = DataHandler.Affiliate.UpdateStatus(AppSettings.Session.AffId, groupID, context.Request["status"], strPassword);

                    string mailOption = string.IsNullOrEmpty(context.Request["mailoption"]) ? "1" : context.Request["mailoption"];
                    bool blMailOption = CastAide.AsBoolean(mailOption == "1" ? true : false);

                    if (CastAide.AsInt32(result.Outputs["errcode"], 0) == 0)
                    {
                        if (string.Compare(context.Request["status"], Common.Approved, true) == 0)
                        {
                            Common.SendEmailApproved(AppSettings.Session.AffId, context.Request["lang"], context.Request["fname"], context.Request["username"], context.Request["email"], strPassword, Common.EmailSuffix(blMailOption));
                        }
                        if (string.Compare(context.Request["status"], Common.Disapproved, true) == 0)
                            Common.SendEmailDisapproved(context.Request["lang"], context.Request["fname"], context.Request["email"], Common.EmailSuffix(blMailOption));
                    }

                    strSuccessMsg = Resources.Lang.UpdateStaSucc;
                    break;
                case 1://Close, open affiliate
                    bool isClosed = CastAide.AsBoolean(context.Request["closed"]);
                    result = DataHandler.Affiliate.AffUpdateClosedStatus(AppSettings.Session.AffId, isClosed);
                    strSuccessMsg = Resources.Lang.UpdateStaSucc;
                    break;
                case 2://Update Preferred language
                    if (string.IsNullOrEmpty(context.Request["lang"]))
                        throw new Exception(Resources.Lang.ErrLang);
                    string strPreferLang = context.Request["lang"];
                    result = DataHandler.Affiliate.UpdateLanguage(AppSettings.Session.AffId, strPreferLang);
                    strSuccessMsg = Resources.Lang.ErrLanguageSucc;
                    break;
            }
            if (CastAide.AsInt32(result.Outputs["errcode"], 0) == 0)
                context.Response.Write("0@" + strSuccessMsg);
            else
                context.Response.Write("1@" + result.Outputs["errmsg"]);
        }
        catch (Exception ex)
        {
            context.Response.Write("1@" + ex.Message);
            Fanex.Logging.LogService.Error(ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
