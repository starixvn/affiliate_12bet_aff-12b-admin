using System;
using System.Web;
using DNAide.Data;
using DNAide.Core; 
using System.Collections;
using System.Configuration;
/// <summary>
/// Summary description for Banner
/// Author: Peter 20090328
/// </summary>
/// 
namespace DataHandler
{
    public class Banner
    {
        public static int InsBanner(string fileName, string category, string language, string currency, string imagetype, string size, string afftype, ref int errcode, ref string errmsg, int type, string deviceType)
        {
            DbResult dbr = null;
            int result = 0;          
            Hashtable htb = new Hashtable();
            var device = string.IsNullOrEmpty(deviceType) ? "0" : deviceType;

            htb.Add("filename", HttpContext.Current.Server.HtmlEncode(fileName));            

            htb.Add("bannerid", 0);
            htb.Add("afftype", afftype);
            htb.Add("devicetype", byte.Parse(device));
            htb.Add("category", HttpContext.Current.Server.HtmlEncode(category));
            htb.Add("language", HttpContext.Current.Server.HtmlEncode(language));
            htb.Add("currency", HttpContext.Current.Server.HtmlEncode(currency));
            htb.Add("imagetype", HttpContext.Current.Server.HtmlEncode(imagetype));
            htb.Add("size", HttpContext.Current.Server.HtmlEncode(size));
            htb.Add("userid", AppSettings.Session.CustID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");
            // ++ Added by Frank.Ngo on IRP 671 - 10 Jan 2012
            htb.Add("type", type);
            // -- Added by Frank.Ngo on IRP 671 - 10 Jan 2012

            dbr = DbHelper.Execute("Aff_BannerMediaIns", htb, DBType.AFFILIATE_12BET);
            errcode =CastAide.AsInt32(dbr.Outputs["errcode"]);
            result=CastAide.AsInt32(dbr.Outputs["bannerid"]);
            errmsg = dbr.Outputs["errmsg"].ToString();           
            return result;
        }

        public static string UpdBanner(int id,string filename, string category, string language,string currency, string imagetype, string size, string afftype, string deviceType)
        {                     
            var htb = new Hashtable();
            var device = string.IsNullOrEmpty(deviceType) ? "0" : deviceType;

            htb.Add("bannerid", id);
            htb.Add("filename", HttpContext.Current.Server.HtmlEncode(filename));
            htb.Add("category", HttpContext.Current.Server.HtmlEncode(category));
            htb.Add("language", HttpContext.Current.Server.HtmlEncode(language));
            htb.Add("imagetype", HttpContext.Current.Server.HtmlEncode(imagetype));
            htb.Add("size", HttpContext.Current.Server.HtmlEncode(size));
            htb.Add("currency", HttpContext.Current.Server.HtmlEncode(currency));
            htb.Add("userid", AppSettings.Session.CustID);
            htb.Add("afftype", afftype);
            htb.Add("devicetype", byte.Parse(device));
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            DbResult dbr = DbHelper.Execute("Aff_BannerUpd_1", htb, DBType.AFFILIATE_12BET);            
            return dbr.Outputs["errmsg"].ToString(); 
        }
        
        //Delete Banner
        public static string DelDataBanner(string listBannerID)
        {           
            Hashtable hsParam = new Hashtable();
            hsParam.Add("listbannerid", listBannerID);
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result = DbHelper.Execute("Aff_BannerDel_1", hsParam, DBType.AFFILIATE_12BET);
            return DNAide.Core.CastAide.AsString(result.Outputs["errmsg"]);
        }

        // Modified by Frank.Ngo on IRP 671 - 10 Jan 2012
        // add "type" parameter to indicate normal banner or media banner
        public static DbResult GetBannerList(string category,string language,string imageType,string size,string currency,int pageIndex, int pageSize, int type, int afftype, string deviceType)
        {
            var device = string.IsNullOrEmpty(deviceType) ? "0" : deviceType;
            Hashtable hsParam = new Hashtable();           
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }

            // ++ Added by Frank.Ngo on IRP 671 - 10 Jan 2012
            hsParam.Add("type", type);            
            // -- Added by Frank.Ngo on IRP 671 - 10 Jan 2012

            hsParam.Add("afftype", afftype);
            hsParam.Add("devicetype", byte.Parse(device));

            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSel_1", hsParam, DBType.AFFILIATE_12BET);
        
        }

        // Added by Frank.Ngo on IRP 671 - 10 Feb 2012
        // Get banners in parent category
        public static DbResult GetBannerListByParentCategory(string category, string language, string imageType, string size, string currency, int pageIndex, int pageSize, int type, int afftype, string deviceType)
        {
            Hashtable hsParam = new Hashtable();
            var device = string.IsNullOrEmpty(deviceType) ? "0" : deviceType;
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }

            hsParam.Add("type", type);
            hsParam.Add("afftype", afftype);
            hsParam.Add("devicetype", byte.Parse(device));
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSelByParentCate", hsParam, DBType.AFFILIATE_12BET);
        }

        public static DbResult GetBannerListOld(string category, string language, string imageType, string size, string currency, int pageIndex, int pageSize, int type)
        {
            Hashtable hsParam = new Hashtable();
            if (!string.IsNullOrEmpty(category))
            {
                hsParam.Add("category", category);
            }
            if (!string.IsNullOrEmpty(language))
            {
                hsParam.Add("language", language);
            }
            if (!string.IsNullOrEmpty(size))
            {
                hsParam.Add("size", size);
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                hsParam.Add("imagetype", imageType);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                hsParam.Add("currency", currency);
            }
            hsParam.Add("type", type);
            hsParam.Add("pagesize", pageSize);
            hsParam.Add("pageindex", pageIndex);
            hsParam.Add("totalpages", 0);
            hsParam.Add("nrows", 0);

            return DbHelper.Execute("Aff_BannerSel", hsParam, DBType.AFFILIATE_12BET);

        }

        public static DbResult GetBannerDetail(int bannerid)
        {
            Hashtable hsParam = new Hashtable();            
            hsParam.Add("bannerid", bannerid);
            return DbHelper.Execute("Aff_BannerSelDetail_1", hsParam, DBType.AFFILIATE_12BET);
        }

        public static DbResult GetBannerDetailOld(int bannerid)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("bannerid", bannerid);
            return DbHelper.Execute("Aff_BannerSelDetail", hsParam, DBType.AFFILIATE_12BET);
        }
    }
}
