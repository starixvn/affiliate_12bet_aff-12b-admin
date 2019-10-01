using System;
using System.Collections;
using System.Data;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;

namespace DataHandler
{
    public class Affiliate
    {
        public static void UpdateUser(int intAffId, int intUserId, string strFname, string strLname, string strGender, string strCountry,
            string strEmail, string strPhone, string strAddress, string strCity, string strState, string strPostal,
            string strLang, string strUrl, string strMoreUrl, string strNoSiteDesc, string strPlayerUserName, string strPayment,
            string strNetellerId, string strMoneyBookerEmail, string strBankName, string strBankAddress, string strSwiftCode, string strAccName,
            string strAccNo, string strEmailFormat, string contactApps, ref int intErrCode, ref string strErrMsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("affid", intAffId);
            hsParam.Add("userid", intUserId);
            hsParam.Add("firstname", HttpContext.Current.Server.HtmlEncode(strFname));
            hsParam.Add("lastname", HttpContext.Current.Server.HtmlEncode(strLname));
            if (strGender == "0" || strGender == "1")
                hsParam.Add("gender", strGender == "0" ? false : true);

            hsParam.Add("country", HttpContext.Current.Server.HtmlEncode(strCountry));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(strEmail));
            hsParam.Add("phone", HttpContext.Current.Server.HtmlEncode(strPhone));
            hsParam.Add("address", HttpContext.Current.Server.HtmlEncode(strAddress));
            hsParam.Add("city", HttpContext.Current.Server.HtmlEncode(strCity));
            hsParam.Add("state", HttpContext.Current.Server.HtmlEncode(strState));
            hsParam.Add("postal", HttpContext.Current.Server.HtmlEncode(strPostal));
            hsParam.Add("siteurl", HttpContext.Current.Server.HtmlEncode(strUrl));
            hsParam.Add("moresiteurls", HttpContext.Current.Server.HtmlEncode(strMoreUrl.Trim()));
            hsParam.Add("nositedesc", HttpContext.Current.Server.HtmlEncode(strNoSiteDesc.Trim()));
            hsParam.Add("ContactApps", contactApps);

            //combine
            hsParam.Add("preferredpayment", HttpContext.Current.Server.HtmlEncode(strPayment));
            hsParam.Add("player_username", HttpContext.Current.Server.HtmlEncode(strPlayerUserName));
            hsParam.Add("neteller_accountid", HttpContext.Current.Server.HtmlEncode(strNetellerId));
            hsParam.Add("moneybookers_email", HttpContext.Current.Server.HtmlEncode(strMoneyBookerEmail));
            hsParam.Add("wire_bankname", HttpContext.Current.Server.HtmlEncode(strBankName));
            hsParam.Add("wire_bankaddress", HttpContext.Current.Server.HtmlEncode(strBankAddress));
            hsParam.Add("wire_swiftcode", HttpContext.Current.Server.HtmlEncode(strSwiftCode));
            hsParam.Add("wire_accountname", HttpContext.Current.Server.HtmlEncode(strAccName));
            hsParam.Add("wire_accountno", HttpContext.Current.Server.HtmlEncode(strAccNo));
            if (strEmailFormat == "0" || strEmailFormat == "1")
                hsParam.Add("emailformat", strEmailFormat == "0" ? false : true);
            //endcombine
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("language", strLang);
            hsParam.Add("errcode", intErrCode);
            hsParam.Add("errmsg", strErrMsg);
            DbResult dbr = DbHelper.Execute("Aff_AffiliateUpdInfo", DBType.AFFILIATE_12BET, hsParam);
            intErrCode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            strErrMsg = dbr.Outputs["errmsg"].ToString();
        }

        public static void ResetPass(int intCustId, int intUserId, string strNewPass, ref int intErrCode, ref string errmsg, int isAdmin)
        {
            Hashtable hsParam = new Hashtable();
            DbResult result;
            if (isAdmin == 0)
            {
                hsParam.Add("affid", intCustId);
                hsParam.Add("affpwd", Encryption.CfsEnCode(strNewPass));
                hsParam.Add("userid", intUserId);
                hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                hsParam.Add("errcode", "0");
                hsParam.Add("errmsg", "");
                //result = DbHelper.ExecuteCmd("Aff_AffiliateUpdPwd", DBType.AFFILIATE_12BET, hsParam);
                result = DbHelper.Execute("Aff_AffiliateUpdPwd", DBType.AFFILIATE_12BET, hsParam);
            }
            else
            {
                hsParam.Add("userid", intUserId);
                hsParam.Add("custid", intCustId);
                hsParam.Add("userpwd", Encryption.CfsEnCode(strNewPass));
                hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                hsParam.Add("errcode", "0");
                hsParam.Add("errmsg", "");
                result = DbHelper.Execute("Aff_AdminUpdPwd", DBType.AFFILIATE_12BET, hsParam);
            }
            intErrCode = CastAide.AsInt32(result.Outputs["errcode"]);
            errmsg = result.Outputs["errmsg"].ToString();
        }

        //@Vinh
        public static DbResult SelPermission(int CustId, string funcName)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("custid", CustId);
            hsParam.Add("funcname", funcName);
            hsParam.Add("system", Common.System);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_Con_CheckPermission", DBType.AFFILIATE_12BET, hsParam);
        }

        //Vinh@20090908
        public static DbResult SportTypeSel()
        {
            return DbHelper.Execute("Aff_SportTypeSel", DBType.AFFILIATE_12BET);
        }

        //@Johnny, 2010 Jan 14: Check 12Bet Exist in Deposit_Customer table or not
        public static DepositCustomerInfo Get12BetPlayerDepositInfo(string deposit_username)
        {
            DepositCustomerInfo customer = new DepositCustomerInfo();
            customer.DepositUserName = deposit_username;
            Hashtable hsParam = new Hashtable();
            hsParam.Add("deposit_username", customer.DepositUserName);
            DbResult result = DbHelper.Execute("Deposit_GetCustDataForCreateIBCACByUserName", DBType.DEPOSIT, hsParam);
            DataTable tbl = result.ResultSet.Tables[0];
            customer.Success = CastAide.AsInt32(tbl.Rows[0]["success"]);
            customer.Address = tbl.Rows[0]["address"].ToString();
            customer.AffiliateId = CastAide.AsInt32(tbl.Rows[0]["affiliateid"]);
            customer.AgentID = CastAide.AsInt32(tbl.Rows[0]["agentid"]);
            // customer.AgentType = CastAide.AsInt32(tbl.Rows[0]["agenttype"]);
            customer.Birthday = CastAide.AsDateTime(tbl.Rows[0]["birthday"]);
            customer.BonusCode = tbl.Rows[0]["bonuscode"].ToString();
            //  customer.CasinoFlag = CastAide.AsInt32(tbl.Rows[0]["casinoflag"]);
            customer.City = tbl.Rows[0]["city"].ToString();
            customer.Country = tbl.Rows[0]["country"].ToString();
            customer.Currency = CastAide.AsInt32(tbl.Rows[0]["currency"]);
            // customer.DepositCustID = CastAide.AsInt32(tbl.Rows[0]["depositcustid"]);
            customer.DepositUserId = CastAide.AsInt32(tbl.Rows[0]["deposituserid"]);
            customer.Email = tbl.Rows[0]["email"].ToString();
            customer.Gender = CastAide.AsInt32(tbl.Rows[0]["gender"]);
            customer.FirstName = tbl.Rows[0]["firstname"].ToString();
            customer.LastName = tbl.Rows[0]["lastname"].ToString();
            customer.OddsType = tbl.Rows[0]["oddstype"].ToString();
            customer.Password = tbl.Rows[0]["password"].ToString();
            customer.Phone = tbl.Rows[0]["phone"].ToString();
            customer.State = tbl.Rows[0]["state"].ToString();
            customer.ZipCode = tbl.Rows[0]["zipcode"].ToString();
            return customer;
        }

        //@Thai, 2012 July 27: Get list of deposit customers
        public static System.Collections.Generic.List<DepositCustomerInfo> Get12BetPlayersDepositInfo(string lstDepositUserNames)
        {
            System.Collections.Generic.List<DepositCustomerInfo> customers = new System.Collections.Generic.List<DepositCustomerInfo>();
            Hashtable hsParam = new Hashtable();

            hsParam.Add("deposit_usernames", lstDepositUserNames);
            DbResult rs = DbHelper.Execute("Deposit_GetCustDataForCreateIBCACByUserNames", DBType.DEPOSIT, hsParam);

            DataTable tbl = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                tbl = rs.ResultSet.Tables[0];

            if (tbl.Rows.Count > 0)
            {
                foreach (DataRow dr in tbl.Rows)
                {
                    DepositCustomerInfo customer = new DepositCustomerInfo();
                    customer.DepositUserName = dr["username"].ToString();
                    customer.Address = dr["address"].ToString();
                    customer.AffiliateId = CastAide.AsInt32(dr["affiliateid"]);
                    customer.AgentID = CastAide.AsInt32(dr["agentid"]);
                    // customer.AgentType = CastAide.AsInt32(dr["agenttype"]);
                    customer.Birthday = CastAide.AsDateTime(dr["birthday"]);
                    customer.BonusCode = dr["bonuscode"].ToString();
                    //  customer.CasinoFlag = CastAide.AsInt32(dr["casinoflag"]);
                    customer.City = dr["city"].ToString();
                    customer.Country = dr["country"].ToString();
                    customer.Currency = CastAide.AsInt32(dr["currency"]);
                    // customer.DepositCustID = CastAide.AsInt32(dr["depositcustid"]);
                    customer.DepositUserId = CastAide.AsInt32(dr["deposituserid"]);
                    customer.Email = dr["email"].ToString();
                    customer.Gender = CastAide.AsInt32(dr["gender"]);
                    customer.FirstName = dr["firstname"].ToString();
                    customer.LastName = dr["lastname"].ToString();
                    customer.OddsType = dr["oddstype"].ToString();
                    customer.Password = dr["password"].ToString();
                    customer.Phone = dr["phone"].ToString();
                    customer.State = dr["state"].ToString();
                    customer.ZipCode = dr["zipcode"].ToString();

                    customers.Add(customer);
                }
            }

            return customers;
        }

        //@Johnny, 2010 Jan 14: Add 1 customer to our DB with username2=depositusername
        public static void Add12BetPlayerAccountToIBC(DepositCustomerInfo customer, out int custid, out string username, out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("firstname", HttpContext.Current.Server.HtmlEncode(customer.FirstName));
            hsParam.Add("lastname", HttpContext.Current.Server.HtmlEncode(customer.LastName));
            hsParam.Add("email", HttpContext.Current.Server.HtmlEncode(customer.Email));
            hsParam.Add("depositusername", HttpContext.Current.Server.HtmlEncode(customer.DepositUserName));
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
            DbResult result = DbHelper.Execute("Dot_Dep_CustomerInsMember_Ag_Cust", DBType.CustDB, hsParam);
            username = result.Outputs["username"].ToString();
            custid = CastAide.AsInt32(result.Outputs["custid"]);
            errmsg = result.Outputs["errmsg"].ToString();

            //wirite log if the user creating processed successfully
            if (string.IsNullOrEmpty(errmsg))
            {
                int refNo = CastAide.AsInt32(result.Outputs["refno"]);

                Hashtable logParam = new Hashtable();
                logParam.Add("creator", AppSettings.Session.CustID);
                logParam.Add("affid", AppSettings.Session.CustID);
                logParam.Add("typename", "Member");
                logParam.Add("spname", "Dot_Dep_CustomerInsMember_Ag_Cust");
                logParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
                logParam.Add("refno", refNo);
                logParam.Add("details", "Add 12Bet player account to IBC DepositCustID=" + customer.DepositUserId);
                DbHelper.Execute("Aff_WriteLog", DBType.AFFILIATE_12BET, logParam);
            }
        }

        //@Johnny, 2010 Jan 14: Update AccStatus = 2 in Deposit_Customer table in Deposit_DB.
        public static void Update12BetPlayerStatusToDeposit(string depositUserName, int ibcCustid, string ibcUserName, out int errcode, out string errmsg)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("depusername", HttpContext.Current.Server.HtmlEncode(depositUserName));
            hsParam.Add("ibccustid", ibcCustid);
            hsParam.Add("ibcusername", HttpContext.Current.Server.HtmlEncode(ibcUserName));
            hsParam.Add("errorcode", 0);
            hsParam.Add("errormsg", string.Empty);
            DbResult result = DbHelper.Execute("Deposit_MemberIBCDetail", DBType.DEPOSIT, hsParam);
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
                DbHelper.Execute("Aff_WriteLog", DBType.AFFILIATE_12BET, logParam);
            }
        }
    }
}