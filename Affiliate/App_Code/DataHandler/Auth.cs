using System.Web;
using DNAide.Data;
using DNAide.Core;
using System.Collections;
using DNAide.Utils;

/// <summary>
/// Summary description for Login
/// </summary>
/// 
namespace DataHandler
{
    public class Auth
    {
        public static DbResult ExecLogin(string username, string password, HttpContext hc)
        {
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("username", username);
                htb.Add("userpwd", Encryption.CfsEnCode(password));
                htb.Add("loginip", hc.Request.ServerVariables["REMOTE_ADDR"]);
                htb.Add("serverid", CastAide.AsInt32(AppSettings.ServerId));
                htb.Add("custid", 0);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                htb.Add("firstname", "");
                htb.Add("lastname", "");
                htb.Add("email", "");
                htb.Add("language", AppSettings.Session.Language);
                htb.Add("mailoption", false);
                htb.Add("preferlang", "en-US");
                htb.Add("isallowedpersonalizedpage", 0);
                htb.Add("afflevel", 0);
                htb.Add("hasdailydetail", 0);                
                htb.Add("commgroupname", "");
                htb.Add("iscasino", 0);
                DbResult dbr = DbHelper.Execute("Aff_AffiliateLogin", htb, DBType.AFFILIATE_12BET);
                return dbr;
            }
            catch
            {
                throw;
            }
        }

        public static int LoginOnuser(HttpContext hc)
        {
            Hashtable p = new Hashtable();
            p.Add("custid", AppSettings.Session.CustID);
            p.Add("netsessionid", hc.Session.SessionID);
            p.Add("serverid", AppSettings.ServerId);
            p.Add("on_userid", 0);
            p.Add("ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            p.Add("subaccid", 0);
            p.Add("language", AppSettings.Session.Language);

            DbResult dr = DbHelper.Execute("Aff_CheckIn", p, DBType.AFFILIATE_12BET);
            //DbResult dr = DbHelper.Execute("Acc_Checkin", p, DBType.ON_USER);
            return CastAide.AsInt32(dr.Outputs["on_userid"]);
        }

        public static int LoginOnuser(HttpContext hc, int adminID)
        {
            Hashtable p = new Hashtable();
            p.Add("custid", AppSettings.Session.CustID);
            p.Add("netsessionid", hc.Session.SessionID);
            p.Add("serverid", AppSettings.ServerId);
            p.Add("on_userid", 0);
            p.Add("ip", hc.Request.ServerVariables["REMOTE_ADDR"]);
            p.Add("subaccid", adminID);
            p.Add("language", AppSettings.Session.Language);

            DbResult dr = DbHelper.Execute("Aff_CheckIn", p, DBType.AFFILIATE_12BET);
            //DbResult dr = DbHelper.Execute("Acc_Checkin", p, DBType.ON_USER);
            return CastAide.AsInt32(dr.Outputs["on_userid"]);
        }

        public static void Logout(int OnUserId)
        {
            Hashtable p = new Hashtable();
            p.Add("on_userid", OnUserId);
            DbHelper.Execute("Aff_Logout", p, DBType.AFFILIATE_12BET);
        }
        public static int CheckEmailForget(string email, string valicode, ref int errcode, ref string errmsg, ref string username, ref string firstname, ref string lastname, ref string PreferredLanguage, ref bool MailOption)
        {
            Hashtable htb = new Hashtable();
            DbResult dbr = null;
            int custid = 0;
            htb.Add("email", email);
            htb.Add("affid", 0);
            htb.Add("validcode", valicode);
            //htb.Add("affmaster", AppSettings.AffMaster);
            htb.Add("language", AppSettings.Session.Language);
            htb.Add("username", "");
            htb.Add("firstname", "");
            htb.Add("lastname", "");
            htb.Add("preferredlanguage", "");
            htb.Add("mailoption", false);
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");
            dbr = DbHelper.Execute("Aff_AffiliateForgotPwd", htb, DBType.AFFILIATE_12BET, 600);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            errmsg = dbr.Outputs["errmsg"].ToString();
            custid = CastAide.AsInt32(dbr.Outputs["affid"]);
            username = dbr.Outputs["username"].ToString();
            firstname = dbr.Outputs["firstname"].ToString();
            lastname = dbr.Outputs["lastname"].ToString();
            PreferredLanguage = CastAide.AsString(dbr.Outputs["preferredlanguage"], "en-US");
            MailOption = CastAide.AsBoolean(dbr.Outputs["mailoption"], false);
            return custid;
        }
        public static int ExecChangePassword(int custid, string validcode, string newpass, ref string errmsg, ref string email, ref string username, ref string firstname, ref string lastname, ref string strPreferredLanguage, ref bool mailOption)
        {
            Hashtable htb = new Hashtable();
            DbResult dbr = null;
            int errcode = 0;
            htb.Add("affid", custid);
            //htb.Add("affmaster", AppSettings.AffMaster);
            htb.Add("validcode", validcode);
            htb.Add("newpwd", Encryption.CfsEnCode(newpass));
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");
            htb.Add("email", "");
            htb.Add("username", "");
            htb.Add("firstname", "");
            htb.Add("lastname", "");
            htb.Add("language", AppSettings.Session.Language);
            dbr = DbHelper.Execute("Aff_AffiliateRecoverPwd", htb, DBType.AFFILIATE_12BET);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            errmsg = dbr.Outputs["errmsg"].ToString();
            email = dbr.Outputs["email"].ToString();
            username = dbr.Outputs["username"].ToString();
            firstname = dbr.Outputs["firstname"].ToString();
            lastname = dbr.Outputs["lastname"].ToString();
            strPreferredLanguage = dbr.Outputs["preferredlanguage"].ToString(); ;
            mailOption = CastAide.AsBoolean(dbr.Outputs["mailoption"].ToString());
            return errcode;
        }
        //20090202@DungTong Add to update security question
        public static int ExecUpdateSecurityQuestion(string question, string answer, ref string errmsg, HttpContext hc)
        {
            Hashtable htb = new Hashtable();
            DbResult dbr = null;
            int errcode = 0;
            try
            {
                htb.Add("affid", AppSettings.Session.CustID);
                htb.Add("ip", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                htb.Add("errcode", 0);
                htb.Add("errmsg", "");
                htb.Add("username", AppSettings.Session.UserName);
                htb.Add("question", question);
                htb.Add("answer", answer);
                htb.Add("language", AppSettings.Session.Language);
                htb.Add("loginip", hc.Request.ServerVariables["REMOTE_ADDR"]);
                htb.Add("serverid", CastAide.AsInt32(AppSettings.ServerId));
                dbr = DbHelper.Execute("Aff_CustomerUdpSecurityQuestion", htb, DBType.AFFILIATE_12BET);
                errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
                errmsg = dbr.Outputs["errmsg"].ToString();
            }
            catch
            {
                throw;
            }
            return errcode;
        }


        public static bool ReCheckin()
        {
            try
            {
                Hashtable p = new Hashtable();
                p.Add("on_userid", AppSettings.Session.OnUser);
                p.Add("iskickedout", 0);
                DbResult dr = DNAide.Data.DbHelper.Execute("Aff_Recheckin", p, DBType.AFFILIATE_12BET);
                return CastAide.AsBoolean(dr.Outputs["iskickedout"]);
            }
            catch
            { throw; }
        }

        public static DbResult GetAdminNetSessionID(string onuserID, HttpContext hc)
        {
            Hashtable p = new Hashtable();
            p.Add("on_userid", onuserID);
            p.Add("netsessionid", string.Empty);
            return DbHelper.Execute("Aff_OnUserSelNetSessionID", p, DBType.AFFILIATE_12BET);
        }

        public static DbResult LoginAuthentication(int adminID, string username, HttpContext hc)
        {
            Hashtable p = new Hashtable();
            p.Add("adminid", adminID);
            p.Add("username", username);
            p.Add("loginip", hc.Request.ServerVariables["REMOTE_ADDR"]);
            p.Add("serverid", CastAide.AsInt32(AppSettings.ServerId));
            p.Add("adminusername", string.Empty);
            p.Add("custid", 0);
            p.Add("errcode", 0);
            p.Add("errmsg", string.Empty);
            p.Add("firstname", "");
            p.Add("lastname", "");
            p.Add("email", "");
            p.Add("preferlang", "en-US");
            p.Add("language", AppSettings.Session.Language);
            p.Add("isallowedpersonalizedpage", 0);
            p.Add("hasdailydetail", 0);
            p.Add("afflevel", 0);
            p.Add("commgroupname", "");
            p.Add("iscasino", 0);
            return DbHelper.Execute("Aff_AffiliateInternalLogin", p, DBType.AFFILIATE_12BET);
        }
    }
}
