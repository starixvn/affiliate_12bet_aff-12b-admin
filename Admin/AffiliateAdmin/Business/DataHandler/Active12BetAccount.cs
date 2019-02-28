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
    /* 
    In "payment request" page, we have to check whether member get activated or not. If not, then..
         1. Get member information from 12bet databases.
         2. Insert member into IBC database (SP: Dot_Dep_CustomerInsMember_Ag)
         3. Update IBC info into 12bet database.
    */

    //* @Johnny: Check 12Player Account activated and automaticaly activate 2010-01-14
    public static bool Check12BetUsernameByAffId(int AffId, ref string errMsg)
    {


        string depositUserName = string.Empty;
        bool isActivated = DataHandler.Affiliate.Check12BetPlayerAccountActivated(AffId, out depositUserName, out errMsg);
        if (!string.IsNullOrEmpty(errMsg))
        {/*has error */
            return false;
        }

        if (!isActivated) /*this 12 player account hadn't been activated yet */
        {
            if (!isActivated) /*this 12 player account hadn't been activated yet */
            {
                return Active12BetUsername(depositUserName, ref errMsg);
            }
        }
        return true;
    }

    public static bool Active12BetUsername(string depositUserName, ref string errMsg)
    {
        DepositCustomerInfo customer = DataHandler.Affiliate.Get12BetPlayerDepositInfo(depositUserName);
        if (customer.Success != 0)
        { /* There was an error when get customer info in Deposit DB */
            //  strErrorMsg = "There was an error when get customer info in Deposit DB";
            errMsg = Resources.Lang.errDepositDBConnectFailed;
            return false;
        }
        int ibcCustID = 0;
        string ibcUserName = string.Empty;
        DataHandler.Affiliate.Add12BetPlayerAccountToIBC(customer, out ibcCustID, out ibcUserName, out errMsg);
        if (!string.IsNullOrEmpty(errMsg))
        {/*has error */
            return false;
        }
        int intErrCode = 0;
        try
        {
            DataHandler.Affiliate.Update12BetPlayerStatusToDeposit(depositUserName, ibcCustID, ibcUserName, out intErrCode, out errMsg);
        }
        catch
        {
            return false;
        }
        switch (intErrCode)
        {
            case 0:
                break;
            case 101:
                errMsg = Resources.Lang.errUserNotFound;
                return false;
            case 102:
                errMsg = Resources.Lang.errExistAccount;
                return false;
            default:
                return false;
                break;
        }
        return true;
    }

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
