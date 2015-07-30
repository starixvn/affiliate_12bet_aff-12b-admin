using System;
using System.Collections;
using System.Data;
using System.Web;
using DNAide.Data;
using DNAide.Core;

/// <summary>
/// Trinh Quang Vinh
/// </summary>
namespace DataHandler
{
    public static class PaymentRequest
    {
        public static DbResult GetInfo(int CustId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", CustId);
            hsParam.Add("preferredpayment", string.Empty);            
            hsParam.Add("balbroughtforward", 0);            
            hsParam.Add("minamount", 0);
            return DbHelper.Execute("Aff_PaymentRequestSelInfoByAff", hsParam, DBType.AFFILIATE_12BET, 600);
        }
        public static int PayoutRequest(decimal Amt, int AffID,  ref Int64 intTransId, ref DateTime dtmWinlostDate, ref string strPreferredLang, ref string strErrorMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("amount", Amt);
            hsParam.Add("affid", AffID);
            hsParam.Add("language", AppSettings.Session.Language);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            DbResult result = DbHelper.Execute("Aff_PaymentRequestIns", hsParam, DBType.AFFILIATE_12BET, 600);
            intTransId = CastAide.AsInt64(result.Outputs["transid"]);
            dtmWinlostDate = CastAide.AsDateTime(result.Outputs["transdate"]);
            strErrorMsg = result.Outputs["errmsg"].ToString();
            strPreferredLang = result.Outputs["preferredlanguage"].ToString();
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }
        public static int CancelRequest(Int64 intTransId,  string Lang, ref string strPreferredLang, ref string strErrorMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("transid", intTransId);            
            hsParam.Add("userid", AppSettings.Session.CustID);            
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("language", AppSettings.Session.Language);
            hsParam.Add("status", "Cancelled");            
            DbResult result = DbHelper.Execute("Aff_PaymentRequestUpd", hsParam, DBType.AFFILIATE_12BET, 600);
            strPreferredLang = result.Outputs["preferredlanguage"].ToString();
            strErrorMsg = result.Outputs["errmsg"].ToString();          
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }        
        /// <summary>
        /// Check 12Bet player account for this affiliate is activated or not @Dung2009-02-17
        /// </summary>
        /// <param name="affiliate">affiliate id</param>
        /// <param name="depositUserName">12 bet player username for this affiliate</param>
        /// <param name="errMsg">Error Message</param>
        /// <returns>true if exists else return false</returns>
        public static bool Check12BetPlayerAccountActivated(int affiliate, out string depositUserName,out string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("language", AppSettings.Session.Language);
            hsParam.Add("affid", affiliate);
            hsParam.Add("12betplayerusername", string.Empty);
            hsParam.Add("result",false);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", "");
            DbResult result = DbHelper.Execute("Aff_CustomerCheck12BetPlayerAccountActivated", hsParam, DBType.AFFILIATE_12BET);           
            errMsg = result.Outputs["errmsg"].ToString();
            depositUserName = result.Outputs["12betplayerusername"].ToString();
            return CastAide.AsBoolean(result.Outputs["result"]);
        }
        /// <summary>
        /// Get the 12Bet Player Account information for this affiliate has the deposit_username @Dung2009-02-17
        /// </summary>
        /// <param name="deposit_username">12 bet player username</param>
        /// <returns></returns>
        public static DepositCustomerInfo Get12BetPlayerDepositInfo(string deposit_username)
        {
            DepositCustomerInfo customer = new DepositCustomerInfo();
            customer.DepositUserName = deposit_username;
            Hashtable hsParam = new Hashtable();
            hsParam.Add("deposit_username", customer.DepositUserName);
            DbResult result = DbHelper.Execute("Deposit_GetCustDataForCreateIBCACByUserName", hsParam, DBType.DEPOSIT);
            DataTable tbl = result.ResultSet.Tables[0];
            customer.Success= CastAide.AsInt32(tbl.Rows[0]["success"]);
            customer.Address = tbl.Rows[0]["address"].ToString();
            customer.AffiliateId = CastAide.AsInt32(tbl.Rows[0]["affiliateid"]);
            customer.AgentID = CastAide.AsInt32(tbl.Rows[0]["agentid"]);
           // customer.AgentType = CastAide.AsInt32(tbl.Rows[0]["agenttype"]);
            customer.Birthday =CastAide.AsDateTime(tbl.Rows[0]["birthday"]);
            customer.BonusCode = tbl.Rows[0]["bonuscode"].ToString();
          //  customer.CasinoFlag = CastAide.AsInt32(tbl.Rows[0]["casinoflag"]);
            customer.City = tbl.Rows[0]["city"].ToString();
            customer.Country = tbl.Rows[0]["country"].ToString();
            customer.Currency = CastAide.AsInt32(tbl.Rows[0]["currency"]);
           // customer.DepositCustID = CastAide.AsInt32(tbl.Rows[0]["depositcustid"]);
            customer.DepositUserId = CastAide.AsInt32(tbl.Rows[0]["deposituserid"]);
            customer.Email = tbl.Rows[0]["email"].ToString();
            customer.Gender=CastAide.AsInt32(tbl.Rows[0]["gender"]);
            customer.FirstName = tbl.Rows[0]["firstname"].ToString();
            customer.LastName = tbl.Rows[0]["lastname"].ToString();
            customer.OddsType = tbl.Rows[0]["oddstype"].ToString();
            customer.Password = tbl.Rows[0]["password"].ToString();
            customer.Phone = tbl.Rows[0]["phone"].ToString();
            customer.State = tbl.Rows[0]["state"].ToString();
            customer.ZipCode= tbl.Rows[0]["zipcode"].ToString();
            return customer;
            
        }
        /// <summary>
        /// Create a new player username for affiliate in IBC system @Dung2009-02-17
        /// </summary>
        /// <param name="customer">12Bet Player Customer Info</param>
        /// <param name="custid">return custid for new user</param>
        /// <param name="username">return username for new user</param>
        /// <param name="errmsg">return error message if has</param>
        public static void Add12BetPlayerAccountToIBC(DepositCustomerInfo customer,out int custid,out string username,out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("firstname", HttpContext.Current.Server.HtmlEncode(customer.FirstName));
            hsParam.Add("lastname", HttpContext.Current.Server.HtmlEncode(customer.LastName));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(customer.Email));
            hsParam.Add("depositusername",HttpContext.Current.Server.HtmlEncode(customer.DepositUserName));
            hsParam.Add("deposituserid", customer.DepositUserId);
            hsParam.Add("password", customer.Password);
            hsParam.Add("oddstype", HttpContext.Current.Server.HtmlEncode(customer.OddsType));
            hsParam.Add("agentid", customer.AgentID);
            hsParam.Add("affiliateid", customer.AffiliateId);
            hsParam.Add("bonuscode", HttpContext.Current.Server.HtmlEncode(customer.BonusCode));
            hsParam.Add("custid", 0);
            hsParam.Add("username", string.Empty);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result=DbHelper.Execute("Dot_Dep_CustomerInsMember_Ag_Cust", hsParam, DBType.MAIN);
            username = result.Outputs["username"].ToString();
            custid=CastAide.AsInt32(result.Outputs["custid"]);
            errmsg=result.Outputs["errmsg"].ToString();

            //wirite log if the user creating processed successfully
            if(string.IsNullOrEmpty(errmsg))
            {
                int refNo =0;// CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator", AppSettings.Session.CustID);
                logParam.Add("affid", AppSettings.Session.CustID);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Dot_Dep_CustomerInsMember_Ag");
                logParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                logParam.Add("refno", refNo);
                logParam.Add("details", "Add 12Bet player account to IBC DepositCustID=" + customer.DepositUserId);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE_12BET);
            }
            
        }
        /// <summary>
        ///  Update status of this 12betplayer in deposit db to mark it as active @Dung2009-02-17
        /// </summary>
        /// <param name="depositUserName">Deposit username</param>
        /// <param name="ibcCustid">IBC cust ID for this Deposit username </param>
        /// <param name="ibcUserName">IBC user name for this Deposit username</param>
        /// <param name="errcode">return error code if has -- 0 : No error occur</param>
        /// <param name="errmsg">return string equivalent of this error code --- string.Empty : no error occur </param>
        public static void Update12BetPlayerStatusToDeposit(string depositUserName,int ibcCustid,string ibcUserName,out int errcode,out string errmsg)
        {           
            Hashtable hsParam = new Hashtable();
            hsParam.Add("depusername", HttpContext.Current.Server.HtmlEncode(depositUserName));
            hsParam.Add("ibccustid", ibcCustid);
            hsParam.Add("ibcusername", HttpContext.Current.Server.HtmlEncode(ibcUserName));
            hsParam.Add("errorcode", 0);
            hsParam.Add("errormsg", string.Empty);
            DbResult result = DbHelper.Execute("Deposit_MemberIBCDetail", hsParam, DBType.DEPOSIT);
            errcode = CastAide.AsInt32(result.Outputs["errorcode"]);
            errmsg = result.Outputs["errormsg"].ToString();

            //wirite log if the user updating processed successfully
            if (string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator", AppSettings.Session.CustID);
                logParam.Add("affid", AppSettings.Session.CustID);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Deposit_MemberIBCDetail");
                logParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                logParam.Add("refno", refNo);
                logParam.Add("details", "Update 12Bet player status to deposit DepositCustID=" + ibcCustid);
                DbHelper.Execute("Aff_WriteLog", logParam, DBType.AFFILIATE_12BET);
            }
        }

    }
}
