using System.Data;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using System.Collections;
using DNAide.Core;
using DNAide.Utils;
using System.Text;  
/// <summary>
/// Summary description for Login
/// </summary>
/// 
namespace DataHandler
{
    public static class Login
    {
        public static int ExecLogin(ref string username,string password,ref string errmsg,HttpContext hc,ref int custid,ref int roleid,string lang)
        {

            Hashtable htb = new Hashtable();
            DbResult dbr=null;
            int errcode = 0;
            
            try
            {
                htb.Add("username", username);
                htb.Add("userpwd",Encryption.CfsEnCode(password));

                htb.Add("loginip", Common.GetIPAddressOfClient());
                htb.Add("custid", 0);
                htb.Add("roleid", 0);
                htb.Add("errcode", 0);
                htb.Add("errmsg", "");
                htb.Add("language", lang);
                htb.Add("from", hc.Request.UserHostName);
                dbr = DbHelper.Execute("Aff_AdminLogin", DBType.AFFILIATE_12BET, htb);
                errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
                errmsg = dbr.Outputs["errmsg"].ToString();
                //username = dbr.Outputs["username"].ToString();
                custid = CastAide.AsInt32(dbr.Outputs["custid"]);
                roleid = CastAide.AsInt32(dbr.Outputs["roleid"]);
            }
            catch
            {
                throw; 
            }
            return errcode;
        }
        public static int LoginOnUser(HttpContext hc)
        {
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("custid", AppSettings.Session.CustID);
                htb.Add("netsessionid", hc.Session.SessionID);
                htb.Add("serverid", AppSettings.ServerId);
                htb.Add("on_userid", 0);
                htb.Add("ip", Common.GetIPAddressOfClient());
                htb.Add("subaccid", 6);
                DbResult dbr = DbHelper.Execute("Aff_CheckIn", DBType.AFFILIATE_12BET, htb);
                //DbResult dbr = DbHelper.Execute("Acc_CheckIn", htb, DBType.ON_USER);
                return CastAide.AsInt32(dbr.Outputs["on_userid"]);
            }
            catch
            {
                throw;
            }
        }

        public static bool ReCheckin(HttpContext hc)
        {
            try
            {
                var htb = new Hashtable();
                htb.Add("on_userid", AppSettings.Session.OnUser);
                htb.Add("iskickedout", 0);
                DbResult dbr = DbHelper.Execute("Aff_Recheckin", DBType.AFFILIATE_12BET, htb);
                return CastAide.AsBoolean(dbr.Outputs["iskickedout"]);  
            }
            catch
            
            {   throw; }
        }
        public static void Logout(HttpContext hc)
        {
            Hashtable htb = new Hashtable();
            htb.Add("on_userid", AppSettings.Session.OnUser);
            DbHelper.Execute("Aff_Logout", DBType.AFFILIATE_12BET, htb);
            //DbHelper.Execute("Aff_Logout", htb, DBType.ON_USER);   
        }

        public static string AssignRoles(int custid)
        {
            var result = new StringBuilder();
            var dataSet = DataHandler.UserControl.GetUserPolicy(custid);
            if (dataSet != null)
            {
                foreach (DataRow dr in dataSet.ResultSet.Tables[0].Rows)
                {
                    if (CastAide.AsInt32(dr["subfunccount"]) == 0)
                    {
                        result.Append(dr["funcname"] + "-" + ((CastAide.AsInt32(dr["actual_policy"]) > 1) ? "1" : dr["actual_policy"]) + "|");                        
                    }
                }
            }

            return result.ToString().TrimEnd('|');
        }

    }
}
