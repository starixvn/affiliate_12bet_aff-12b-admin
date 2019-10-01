using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AffiliateAdmin.Business.DataAccess;

/// <summary>
/// Summary description for Active12BetAccount
/// </summary>
public class Active12BetAccount
{
    public static void Active12BetUsernames(string lstDepositUserNames, ref string errUserNames, ref string errMsg)
    {        
        int intErrCode = 0;
        int ibcCustID = 0;
        string ibcUserName = string.Empty;
        string errMsgTmp = string.Empty;

        try
        {
            System.Collections.Generic.List<DepositCustomerInfo> customers = DataHandler.Affiliate.Get12BetPlayersDepositInfo(lstDepositUserNames);

            foreach (DepositCustomerInfo customer in customers)
            {
                //Add to IBC
                DataHandler.Affiliate.Add12BetPlayerAccountToIBC(customer, out ibcCustID, out ibcUserName, out errMsgTmp);
                if (!string.IsNullOrEmpty(errMsgTmp))//has error
                {
                    errUserNames += "," + customer.DepositUserName + "&" + errMsgTmp;
                }
				else//Success
                {
                    //Update to Deposit
                    DataHandler.Affiliate.Update12BetPlayerStatusToDeposit(customer.DepositUserName, ibcCustID, ibcUserName, out intErrCode, out errMsgTmp);

                    if (!string.IsNullOrEmpty(errMsgTmp))//has error
                    {
                        switch (intErrCode)
                        {
                            case 0:
                                break;
                            case 101:
                                errMsgTmp = Resources.Lang.errUserNotFound; break;
                            case 102:
                                errMsgTmp = Resources.Lang.errExistAccount; break;
                        }
                        errUserNames += "," + customer.DepositUserName + "&" + errMsgTmp;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message;
        }
    }
}
