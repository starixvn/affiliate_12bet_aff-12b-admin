using System.Data;
using System.Web;
using DNAide.Data;
using System.Collections;
using DNAide.Core;
using DNAide.Utils;  
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
                dbr = DbHelper.Execute("Aff_AdminLogin", htb, DBType.AFFILIATE_12BET);
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
                DbResult dbr = DbHelper.Execute("Aff_CheckIn", htb, DBType.AFFILIATE_12BET);
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
                Hashtable htb = new Hashtable();
                htb.Add("on_userid", AppSettings.Session.OnUser);
                htb.Add("iskickedout", 0);
                DbResult dbr = DNAide.Data.DbHelper.Execute("Aff_Recheckin", htb, DBType.AFFILIATE_12BET);
                return CastAide.AsBoolean(dbr.Outputs["iskickedout"]);  
            }
            catch
            { throw; }
        }
        public static void Logout(HttpContext hc)
        {
            Hashtable htb = new Hashtable();
            htb.Add("on_userid", AppSettings.Session.OnUser);
            DbHelper.Execute("Aff_Logout", htb, DBType.AFFILIATE_12BET);
            //DbHelper.Execute("Aff_Logout", htb, DBType.ON_USER);   
        }
        public static string AssignRoles(int custid)
        {
            string result = string.Empty;
            DbResult ds = DataHandler.UserControl.GetUserPolicy(custid);
            int counter = 0;
            if (ds != null)
            {
                foreach (DataRow dr in ds.ResultSet.Tables[0].Rows)
                {
                    counter++;
                    if (CastAide.AsInt32(dr["subfunccount"]) == 0)
                    {
                        if (counter == ds.ResultSet.Tables[0].Rows.Count)
                        {
                            result += dr["funcname"] + "-" + ((CastAide.AsInt32(dr["actual_policy"]) > 1) ? "1" : dr["actual_policy"]);
                        }
                        else
                        {
                            result += dr["funcname"] + "-" + ((CastAide.AsInt32(dr["actual_policy"]) > 1) ? "1" : dr["actual_policy"]) + "|";
                        }
                    }
                }
            }
            return result;
        }

    }
}
