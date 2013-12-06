using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Active12BetAccount
/// </summary>
public class Active12BetAccount
{
    /* 
         * @Check 12Player Account activated and automaticaly activate 2009-02-17
            In "payment request" page, we have to check whether member get activated or not. If not, then..
                 1. Get member information from 12bet databases.
                 2. Insert member into IBC database (SP: Dot_Dep_CustomerInsMember_Ag)
                 3. Update IBC info into 12bet database.
        */
    public static bool Check12BetUsernameByAffId(int AffId, ref string errMsg)
    {

        
        string depositUserName = string.Empty;
        bool isActivated = DataHandler.PaymentRequest.Check12BetPlayerAccountActivated(AffId, out depositUserName, out errMsg);
        if (!string.IsNullOrEmpty(errMsg))
        {/*has error */
            return false;
        }

        if (!isActivated) /*this 12 player account hadn't been activated yet */
        {
            return Active12BetUsername(depositUserName, ref errMsg);
        }
        return true;
    }

    public static bool Active12BetUsername(string depositUserName, ref string errMsg)
    {
        DepositCustomerInfo customer = DataHandler.PaymentRequest.Get12BetPlayerDepositInfo(depositUserName);
        if (customer.Success != 0)
        { /* There was an error when get customer info in Deposit DB */
            //  strErrorMsg = "There was an error when get customer info in Deposit DB";
            errMsg = Resources.Lang.errDepositDBConnectFailed;
            return false;
        }
        int ibcCustID = 0;
        string ibcUserName = string.Empty;
        DataHandler.PaymentRequest.Add12BetPlayerAccountToIBC(customer, out ibcCustID, out ibcUserName, out errMsg);
        if (!string.IsNullOrEmpty(errMsg))
        {/*has error */
            return false;
        }
        int intErrCode = 0;
        try
        {
            DataHandler.PaymentRequest.Update12BetPlayerStatusToDeposit(depositUserName, ibcCustID, ibcUserName, out intErrCode, out errMsg);
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
}
