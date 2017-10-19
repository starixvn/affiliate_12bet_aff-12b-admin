using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using Affiliate;
using AffiliateUser.Business.Helper;
using DNAide.Core;

/// <summary>
/// Summary description for AppSettings
/// </summary>
public static class AppSettings
{
    public static string Environment
    {
        get
        {
            return ConfigurationManager.AppSettings["Environment"];
        }
    }

    public static int PageSize
    {
        get
        {
            return CastAide.AsInt32(GetValue("PageSize"));
        }
    }

    public static string[] CssFiles
    {
        get
        {
            var cssFiles = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + ThemeDir);
            for (int j = 0; j < cssFiles.Length; ++j)
            {
                cssFiles[j] = cssFiles[j].Substring(cssFiles[j].IndexOf("App_Themes")).Replace("\\", "/");
            }

            return cssFiles;
        }
    }

    public static string BetLink
    {
        get
        {
            return GetValue("12BetLink");
        }
    }

    public static string BetLink_CS
    {
        get
        {
            return GetValue("12BetLink_CS");
        }
    }

    public static string BetLink_TH
    {
        get
        {
            return GetValue("12BetLink_TH");
        }
    }

    public static string BetLink_EU
    {
        get
        {
            return GetValue("12BetLink_EU");
        }
    }

    public static string BetLink_VI
    {
        get
        {
            return GetValue("12BetLink_VI");
        }
    }

    public static string JsVersion
    {
        get
        {
            return GetValue("JsVersion");
        }
    }

    public static string ServerId
    {
        get
        {
            return GetValue("ServerId");
        }
    }

    public static string HtmlHeader
    {
        get
        {
            var pageHeader = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"\Web.info");
            foreach (XmlNode node in doc.ChildNodes[1].ChildNodes)
            {
                if (node.Attributes["name"].Value == "title")
                {
                    pageHeader += "<title>" + node.Attributes["value"].Value + "</title>\n";
                }
                else if (node.Attributes["name"].Value == "keywords")
                {
                    pageHeader += "<meta name=\"keywords\" content=\"" + node.Attributes["value"].Value + "\" />\n";
                }
                else if (node.Attributes["name"].Value == "description")
                {
                    pageHeader += "<meta name=\"description\" content=\"" + node.Attributes["value"].Value + "\" />\n";
                }
                else if (node.Attributes["name"].Value == "contenttype")
                {
                    pageHeader += "<meta http-equiv=\"Content-Type\" content=\"" + node.Attributes["value"].Value + "\" />\n";
                }
            }

            return pageHeader;
        }
    }

    public static string ThemeDir
    {
        get
        {
            var theme = GetValue("Theme");
            return @"App_Themes/" + (string.IsNullOrEmpty(theme) ? "Default" : theme);
        }
    }

    public static string PageTitle
    {
        get
        {
            return "12BET Affiliate Program";
        }
    }

    public static string AffMaster
    {
        get
        {
            return GetValue("AffMaster");
        }
    }

    public static List<AffType> AffTypeList
    {
        get
        {
            var affTypes = GetValue("AffTypeList");
            return affTypes.Split(',').Select(t => (AffType)CastAide.AsInt32(t, 2)).ToList();
        }
    }

    public static AffType AffType
    {
        get
        {
            return AffTypeList.FirstOrDefault();
        }
    }

    public static string DefaultPreferLang
    {
        get
        {
            var language = GetValue("PreferLang");
            return string.IsNullOrEmpty(language) ? string.Empty : language;
        }
    }

    public static bool HasLogin
    {
        get
        {
            return (Session.CustID > 0);
        }
    }

    public static string MediaJS
    {
        get
        {
            return GetValue("MediaJS");
        }
    }

    public static string MediaJS_CS
    {
        get
        {
            return GetValue("MediaJS_CS");
        }
    }

    public static string MediaJS_TH
    {
        get
        {
            return GetValue("MediaJS_TH");
        }
    }

    public static string MediaJS_VI
    {
        get
        {
            return GetValue("MediaJS_VI");
        }
    }

    public static string MediaJS_EU
    {
        get
        {
            return GetValue("MediaJS_EU");
        }
    }

    public static string ListAffUseEuLink
    {
        get
        {
            return GetValue("ListAffUseEuLink");
        }
    }

    public static string PhoneContact
    {
        get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["PhoneContact"]); }
    }

    public static DateTime StatementAvailableFrom
    {
        get
        {
            return CastAide.AsDateTime(GetValue("StatementAvailableFrom"));
        }
    }

    public static string CompanyIP
    {
        get
        {
            return GetValue("CompanyIP");
        }
    }

    #region ---------- for Session-------------

    public static class Session
    {
        // Prevent Session Fixation
        public static string AuthToken
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["AuthToken"], string.Empty);
            }
            set
            {
                HttpContext.Current.Session["AuthToken"] = value;
            }
        }

        public static string Language
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["Culture"], "en-US");
            }
            set
            {
                HttpContext.Current.Session["Culture"] = value;
            }
        }

        public static string Flag
        {
            get
            {
                string f = CastAide.AsString(HttpContext.Current.Session["Flag"], "en-US");
                return f;
            }
            set
            {
                HttpContext.Current.Session["Flag"] = value;
            }
        }

        public static string CaptchaCode
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["CaptchaCode"]);
            }
            set
            {
                HttpContext.Current.Session["CaptchaCode"] = value;
            }
        }

        public static string TrackingCode
        {
            get
            {
                if (HttpContext.Current.Session["TrackingCode"] != null)
                    return CastAide.AsString(HttpContext.Current.Session["TrackingCode"]);
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["TrackingCode"] = value;
            }
        }

        public static int CustID
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["CustID"], -1);
            }
            set
            {
                HttpContext.Current.Session["CustID"] = value;
            }
        }

        public static int OnUser
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["On_User"], -1);
            }
            set
            {
                HttpContext.Current.Session["On_User"] = value;
            }
        }

        public static bool IsAuthenticated
        {
            get
            {
                return (HttpContext.Current.Session["CustID"] != null);
            }
        }

        public static string UserName
        {
            get { return CastAide.AsString(HttpContext.Current.Session["UserName"]); }
            set { HttpContext.Current.Session["UserName"] = value; }
        }

        public static string FirstName
        {
            get { return CastAide.AsString(HttpContext.Current.Session["FirstName"]); }
            set { HttpContext.Current.Session["FirstName"] = value; }
        }

        public static string LastName
        {
            get { return CastAide.AsString(HttpContext.Current.Session["LastName"]); }
            set { HttpContext.Current.Session["LastName"] = value; }
        }

        public static string Email
        {
            get { return CastAide.AsString(HttpContext.Current.Session["Email"]); }
            set { HttpContext.Current.Session["Email"] = value; }
        }

        public static string CommGroupName
        {
            get { return CastAide.AsString(HttpContext.Current.Session["CommGroupName"]); }
            set { HttpContext.Current.Session["CommGroupName"] = value; }
        }

        public static bool IsInternalSite
        {
            get
            {
                try
                {
                    return CastAide.AsBoolean(HttpContext.Current.Session["IsInternalSite"]);
                }
                catch
                {
                    return false;
                }
            }
            set { HttpContext.Current.Session["IsInternalSite"] = value; }
        }

        public static bool HasDailyDetail
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["HasDailyDetail"]); }
            set { HttpContext.Current.Session["HasDailyDetail"] = value; }
        }

        public static bool HasSecurityQuestion
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["HasSecurityQuestion"]); }
            set { HttpContext.Current.Session["HasSecurityQuestion"] = value; }
        }

        private static bool fromHome = false;

        public static bool IsFromHome
        {
            get { return fromHome; }
            set { fromHome = value; }
        }

        private static bool firsTimeLoginSucc = false;

        public static bool FirsTimeLoginSucc
        {
            get { return firsTimeLoginSucc; }
            set { firsTimeLoginSucc = value; }
        }

        public static bool MailOption
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["MailOption"]); }
            set { HttpContext.Current.Session["MailOption"] = value; }
        }

        public static string PreferLang
        {
            get { return CastAide.AsString(HttpContext.Current.Session["PreferLang"], "en-US"); }

            set { HttpContext.Current.Session["PreferLang"] = value; }
        }

        public static bool IsCasino
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["IsCasino"]); }
            set { HttpContext.Current.Session["IsCasino"] = value; }
        }

        public static bool ForceChangePass
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["ForceChangePass"]); }
            set { HttpContext.Current.Session["ForceChangePass"] = value; }
        }

        public static AffType AffType
        {
            get
            {
                return (AffType)CastAide.AsInt32(HttpContext.Current.Session["AffType"], -1);
            }
            set
            {
                HttpContext.Current.Session["AffType"] = value;
            }
        }
    }

    #endregion ---------- for Session-------------

    #region -------------For Mail Service------------------

    public static string ToMail
    {
        get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["RegAlertMail"]); }
    }

    public static string CCMail
    {
        get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["CCMail"]); }
    }

    public static string BCCMail
    {
        get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["BCCMail"]); }
    }

    public static string MailContact
    {
        get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["MailContact"]); }
    }

    #endregion -------------For Mail Service------------------

    public static string BannerUrl
    {
        get
        {
            return UrlHelper.GetUrlFromHostNamePattern(GetValue("BannerUrl"));
        }
    }

    public static string MediaUrl
    {
        get { return UrlHelper.GetUrlFromHostNamePattern(GetValue("MediaUrl")); }
    }

    public static string MediaUrlCS
    {
        get { return UrlHelper.GetUrlFromHostNamePattern(GetValue("MediaUrl_CS")); }
    }

    public static string MediaUrlTH
    {
        get { return UrlHelper.GetUrlFromHostNamePattern(GetValue("MediaUrl_TH")); }
    }

    public static string MediaUrlVI
    {
        get { return UrlHelper.GetUrlFromHostNamePattern(GetValue("MediaUrl_VI")); }
    }

    public static string MediaUrlEU
    {
        get { return UrlHelper.GetUrlFromHostNamePattern(GetValue("MediaUrl_EU")); }
    }

    public static bool ShowCasino
    {
        get
        {
            return GetValue("ShowCasino") == "1";
        }
    }

    public static bool LocalTest
    {
        get { return GetValue("LocalTest") == "1"; }
    }

    public static int BetListServiceTimeout
    {
        get { return CastAide.AsInt32(GetValue("BetListServiceTimeout")); }
    }

    public static int StartYearOfSystem
    {
        get
        {
            var startYear = GetValue("StartYearOfSystem");
            return string.IsNullOrEmpty(startYear) ? 2007 : CastAide.AsInt32(startYear);
        }
    }

    public static string RedirectionEuHost
    {
        get { return ConfigurationManager.AppSettings["RedirectionEuHost"] ?? string.Empty; }
    }

    public static string RedirectionEuCountryList
    {
        get
        {
            return ConfigurationManager.AppSettings["RedirectionEuCountryList"] ?? string.Empty;
        }
    }

    public static string RedirectionEuTarget
    {
        get { return ConfigurationManager.AppSettings["RedirectionEuTarget"] ?? string.Empty; }
    }

    public static void RefreshPreferredLanguage()
    {
        AppSettings.Session.PreferLang = DataHandler.UserAff.GetPreferredLanguage(AppSettings.Session.CustID);
    }

    public static string GetValue(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }
}