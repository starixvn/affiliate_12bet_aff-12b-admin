using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.DTO;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class UpdateAccountService
    {
        public void Update(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);

            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + Resources.Lang.Err500 + "\", \"strErrObj\");</script>");
                return;
            }

            try
            {
                var contactAppList = GetContactAppList(context);
                if (!IsValidAppContactList(contactAppList))
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + Resources.Lang.RequireIMAccount + "\", \"error_item\");</script>");
                    return;
                }

                var serializer = new JavaScriptSerializer();
                var contactAppJson = serializer.Serialize(contactAppList);
                //Check 12BetUsername Correct or not
                string strPayment = context.Request.Form["slPayment"];
                string strPlayerUserName = context.Request.Form["txtBet12UserName"];
                string errMsg = string.Empty;

                if (!AppSettings.LocalTest && strPayment.Equals("Player", StringComparison.OrdinalIgnoreCase))
                {
                    DepositCustomerInfo customer = DataHandler.Affiliate.Get12BetPlayerDepositInfo(strPlayerUserName);
                    if (customer.Success != 0)
                    { /* There was an error when get customer info in Deposit DB */
                      //  strErrorMsg = "There was an error when get customer info in Deposit DB";
                        context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + Resources.Lang.errDepositDBConnectFailed + "\", \"txtBet12UserName\");</script>");
                        return;
                    }
                }

                int intErrCode = 0;
                string strGender = context.Request.Form["rdGender"];
                string strEmail = context.Request.Form["txtEmail"];
                string strPhone = context.Request.Form["txtNumber"];
                string strAddress = context.Request.Form["txtAddress"];
                string strCity = context.Request.Form["txtCity"];
                string strPostCode = context.Request.Form["txtPost"];
                string strState = context.Request.Form["txtState"];
                string strEmailFormat = context.Request.Form["rdFormat"];
                string strErrMsg = "";
                string strURL = context.Request.Form["txtSiteUrl"];
                string strMoreUrl = context.Request.Form["hdMoreurl"];
                string strNoSiteDesc = context.Request.Form["txtNoWebsite"];
                string strNetellerAccId = context.Request.Form["txtNetellerAccId"];
                string strMoneyBookerEmail = context.Request.Form["txtMoneyBookerEmail"];
                string strBankName = context.Request.Form["txtBankName"];
                string strBankAddress = context.Request.Form["txtBankAddress"];
                string strSwiftCode = context.Request.Form["txtBankCode"];
                string strAccName = context.Request.Form["txtBankAccName"];
                string strAccNo = context.Request.Form["txtBankAccNumber"];
                string strCheque_FullName = context.Request.Form["txtChequeFullName"];
                string strCheque_Address = context.Request.Form["txtChequeAddress"];
                string strCheque_City = context.Request.Form["txtChequeCity"];
                string strCheque_State = context.Request.Form["txtChequeState"];
                string strCheque_Postal = context.Request.Form["txtChequePostcode"];
                string strCheque_Country = context.Request.Form["txtChequeCountry"];
                //Remove Prefer language and set to default English
                string strUsername = context.Request.Form["hdUsername"];
                string strFname = context.Request.Form["txtFName"];
                string strLname = context.Request.Form["txtLName"];
                string strCountry = context.Request["selCountry"];
                string strStatus = string.Empty;
                int intCustId = CastAide.AsInt32(context.Request.Form["hdCustid"]);
                int intReload = 0;

                DataHandler.Affiliate.UpdateUser(intCustId, AppSettings.Session.CustID, strFname, strLname, strGender, strCountry,
                    strEmail, strPhone, strAddress, strCity, strState, strPostCode, Common.GetLangChar(),
                    strURL, strMoreUrl, strNoSiteDesc, strPlayerUserName, strPayment, strNetellerAccId, strMoneyBookerEmail,
                    strBankName, strBankAddress, strSwiftCode, strAccName, strAccNo, strCheque_FullName, strCheque_Address,
                    strCheque_City, strCheque_State, strCheque_Postal, strCheque_Country, strEmailFormat, contactAppJson,
                    ref intErrCode, ref strErrMsg);
                if (intErrCode != 0)
                {
                    string strErrObj = "error_item";
                    string strMsg = strErrMsg;
                    if (intErrCode == 500)
                    {
                        context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + strErrMsg + "\", \"strErrObj\");</script>");
                    }
                    else
                    {
                        context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + strMsg + "\", \"" + strErrObj + "\");</script>");
                    }
                }
                else
                {
                    context.Response.Write("<script type='text/javascript'>parent.UpdateSuccess(\"" + strUsername + " " + Resources.Lang.UpdAffSuccess + "\", \"../Alert.aspx?code=0&user=" + strUsername + "&link=UserInfo/AffiliateList.aspx?status=" + strStatus + "\", " + intReload + ");</script>");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + ex.Message + "\", \"error_item\");</script>");
                AffiliateAdmin.Business.Helpers.LogHelper.WriteError(ex);
            }
        }

        private static IEnumerable<ContactApp> GetContactAppList(HttpContext context)
        {
            const string ContactAppKey = "ContactApps";
            var result = new List<ContactApp>();

            var form = context.Request.Form;
            var contactAppKeys = form.AllKeys.Where(k => k.StartsWith(ContactAppKey));
            var length = contactAppKeys.Count() / 3;
            for (var i = 0; i < length; i++)
            {
                var key = $"{ContactAppKey}[{i}]";
                result.Add(new ContactApp
                {
                    Name = form[$"{key}.Name"],
                    Value = form[$"{key}.Value"],
                    Order = Convert.ToInt32(form[$"{key}.Order"])
                });
            }

            return result;
        }

        private static bool IsValidAppContactList(IEnumerable<ContactApp> contactAppList)
        {
            return contactAppList != null && !contactAppList.All(c => string.IsNullOrEmpty(c.Value));
        }
    }
}