using System.Collections;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;


/// <summary>
/// Summary description for Domain
/// </summary>
namespace DataHandler
{
    public class Domain
    {
        public Domain()
        {
        }

        public static DbResult MarketSel()
        {
            return DbHelper.Execute("Aff_MarketSel", DBType.AFFILIATE_12BET);
        }

        public static DbResult DomainAffSel()
        {
            return DbHelper.Execute("Aff_DomainAffSel", DBType.AFFILIATE_12BET);
        }

        public static DbResult DomainControlSel(string marketId)
        {
            Hashtable hs = new Hashtable();
            if (!string.IsNullOrEmpty(marketId))
                hs.Add("marketid", marketId);
            return DbHelper.Execute("Aff_DomainControlSel_xtest", DBType.AFFILIATE_12BET, hs);
        }

        public static bool DomainControlIns(string marketId, string affDomainId, string redirectDomainId, bool status, ref int errCode, ref string errMsg)
        {
            Hashtable hs = new Hashtable();
            hs.Add("marketid", marketId);
            hs.Add("affdomainid", affDomainId);
            hs.Add("12betdomainid", redirectDomainId);
            hs.Add("status", status);
            hs.Add("creator", AppSettings.Session.CustID);
            hs.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hs.Add("userid", AppSettings.Session.CustID);
            hs.Add("errcode", 0);
            hs.Add("errmsg", string.Empty);
            DbResult rs = DbHelper.Execute("Aff_DomainControlIns_xtest", DBType.AFFILIATE_12BET, hs);
            errCode = CastAide.AsInt32(rs.Outputs["errcode"]);
            errMsg = rs.Outputs["errmsg"].ToString();

            return (errCode == 0);
        }

        public static bool DomainControlUpd(int id, string marketId, string affDomainId, string redirectDomainId, bool status, ref int errCode, ref string errMsg)
        {
            Hashtable hs = new Hashtable();
            hs.Add("id", id);
            hs.Add("marketid", marketId);
            hs.Add("affdomainid", affDomainId);
            hs.Add("12betdomainid", redirectDomainId);
            hs.Add("status", status);
            hs.Add("userid", AppSettings.Session.CustID);
            hs.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hs.Add("errcode", 0);
            hs.Add("errmsg", string.Empty);
            DbResult rs = DbHelper.Execute("Aff_DomainControlUpd_xtest", DBType.AFFILIATE_12BET, hs);
            errCode = CastAide.AsInt32(rs.Outputs["errcode"]);
            errMsg = rs.Outputs["errmsg"].ToString();

            return (errCode == 0);
        }

        public static bool DomainControlDel(int Id, ref int errCode, ref string errMsg)
        {
            Hashtable hs = new Hashtable();
            hs.Add("id", Id);
            hs.Add("userid", AppSettings.Session.CustID);
            hs.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hs.Add("errcode", 0);
            hs.Add("errmsg", string.Empty);
            DbResult rs = DbHelper.Execute("Aff_DomainControlDel", DBType.AFFILIATE_12BET, hs);
            errCode = CastAide.AsInt32(rs.Outputs["errcode"]);
            errMsg = rs.Outputs["errmsg"].ToString();

            return (errCode == 0);
        }

        public static DbResult GetListAffDomain()
        {
            return DbHelper.Execute("Aff_DomainAffSel", DBType.AFFILIATE_12BET);
        }
        public static DbResult UpdateAffDomain(int domainID, string domainName, int userID, bool isSSL)
        {
            Hashtable htb = new Hashtable();
            htb.Add("domainid", domainID);
            htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
            htb.Add("IsSSL", isSSL);
            htb.Add("userid", userID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_DomainAffUpd", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult AddAffDomain(string domainName, int userID, bool isSSL)
        {
            Hashtable htb = new Hashtable();
            htb.Add("domainid", 0);
            htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
            htb.Add("IsSSL", isSSL);
            htb.Add("userid", userID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_DomainAffIns", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult DelAffDomain(int domainID, string domainName, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("domainid", domainID);
            htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
            htb.Add("userid", userID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_DomainAffDel", DBType.AFFILIATE_12BET, htb);
        }

        public static DbResult GetList12BetDomain()
        {
            return DbHelper.Execute("Aff_Domain12BetSel_xtest", DBType.AFFILIATE_12BET);
        }
        public static DbResult Update12BetDomain(int domainID, string domainName, bool ssl, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("domainid", domainID);
            htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
            htb.Add("isssl", ssl);
            htb.Add("userid", userID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_Domain12BetUpd_xtest", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult Add12BetDomain(string domainName, bool ssl, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("domainid", 0);
            htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
            htb.Add("isssl", ssl);
            htb.Add("userid", userID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_Domain12BetIns_xtest", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult Del12BetDomain(int domainID, string domainName, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("domainid", domainID);
            htb.Add("userid", userID);
            htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            return DbHelper.Execute("Aff_Domain12BetDel", DBType.AFFILIATE_12BET, htb);
        }
    }
}