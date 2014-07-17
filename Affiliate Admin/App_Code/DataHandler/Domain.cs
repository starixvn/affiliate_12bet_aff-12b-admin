using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DNAide.Data;
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
            return DbHelper.Execute("Aff_MarketSel", null, DBType.AFFILIATE_12BET);
        }

        public static DbResult DomainAffSel()
        {
            return DbHelper.Execute("Aff_DomainAffSel", null, DBType.AFFILIATE_12BET);
        }

        public static DbResult Domain12BetSel()
        {
            return DbHelper.Execute("Aff_Domain12BetSel", null, DBType.AFFILIATE_12BET);
        }

        public static DbResult DomainControlSel(string marketId, string deviceType)
        {
            Hashtable hs = new Hashtable();
            if(!string.IsNullOrEmpty(marketId))
                hs.Add("marketid", marketId);
            var device = string.IsNullOrEmpty(deviceType) ? 0 : byte.Parse(deviceType);
            hs.Add("devicetype", device);
            return DbHelper.Execute("Aff_DomainControlSel", hs, DBType.AFFILIATE_12BET);
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
            DbResult rs = DbHelper.Execute("Aff_DomainControlIns", hs, DBType.AFFILIATE_12BET);
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
            DbResult rs = DbHelper.Execute("Aff_DomainControlUpd", hs, DBType.AFFILIATE_12BET);
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
            DbResult rs = DbHelper.Execute("Aff_DomainControlDel", hs, DBType.AFFILIATE_12BET);
            errCode = CastAide.AsInt32(rs.Outputs["errcode"]);
            errMsg = rs.Outputs["errmsg"].ToString();

            return (errCode == 0);
        }

        public static DbResult GetListAffDomain()
        {
            DbResult dbr = null;
            try
            {

                dbr = DbHelper.Execute("Aff_DomainAffSel", null, DBType.AFFILIATE_12BET);
            }
            catch
            {

                throw;
            }
            return dbr;
        }
        public static DbResult UpdateAffDomain(int domainID, string domainName, int userID)
        {

            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", domainID);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("userid", userID);
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                // htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_DomainAffUpd", htb, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult AddAffDomain(string domainName, int userID)
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", 0);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("userid", userID);
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_DomainAffIns", htb, DBType.AFFILIATE_12BET);

            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult DelAffDomain(int domainID, string domainName, int userID)
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", domainID);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("userid", userID);
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //  htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_DomainAffDel", htb, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
            return dr;
        }

        public static DbResult GetList12BetDomain(string deviceType)
        {
            DbResult dbr = null;
            try
            {
                var hs = new Hashtable();
                var device = string.IsNullOrEmpty(deviceType) ? 0 : byte.Parse(deviceType);
                hs.Add("devicetype", device);
                dbr = DbHelper.Execute("Aff_Domain12BetSel", hs, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }

            return dbr;
        }
        public static DbResult Update12BetDomain(int domainID, string domainName, string deviceType, bool ssl, int userID)
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                var device = string.IsNullOrEmpty(deviceType) ? 0 : byte.Parse(deviceType);
                htb.Add("domainid", domainID);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("devicetype", device);
                htb.Add("isssl", ssl);
                htb.Add("userid", userID);
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                dr = DbHelper.Execute("Aff_Domain12BetUpd", htb, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult Add12BetDomain(string domainName, string deviceType, bool ssl, int userID)
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                var device = string.IsNullOrEmpty(deviceType) ? 0 : byte.Parse(deviceType);
                htb.Add("domainid", 0);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("devicetype", device);
                htb.Add("isssl", ssl);
                htb.Add("userid", userID);
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_Domain12BetIns", htb, DBType.AFFILIATE_12BET);

            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult Del12BetDomain(int domainID, string domainName, int userID)
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("domainid", domainID);
                htb.Add("userid", userID);
                htb.Add("domainname", HttpContext.Current.Server.HtmlEncode(domainName));
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //  htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_Domain12BetDel", htb, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
            return dr;
        }
    }
}