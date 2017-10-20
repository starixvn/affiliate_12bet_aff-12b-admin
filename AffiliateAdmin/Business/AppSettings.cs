using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Xml;
using DNAide.Core;

/// <summary>
/// Summary description for AppSettings
/// </summary>
public static class AppSettings
{
    public static string[] BannerFolders
    {
        get
        {
            return GetValue("BannerFolders").Split(';');
        }
    }

    public static string BannerUrl
    {
        get
        {
            return GetValue("BannerUrl");
        }
    }

    public static string Environment
    {
        get
        {
            return GetValue("Environment");
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

    public static string Site
    {
        get
        {
            return GetValue("Site");
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

    public static string TplFolder
    {
        get
        {
            return GetValue("template_folder");
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
            return @"App_Themes/" + (string.IsNullOrEmpty(GetValue("Theme")) ? "Default" : GetValue("Theme"));
        }
    }

    public static string AffMaster
    {
        get
        {
            return GetValue("AffMaster");
        }
    }

    public static string Link
    {
        get
        {
            return GetValue("12BetLink");
        }
    }

    public static string Link_TH
    {
        get
        {
            return GetValue("12BetLink_TH");
        }
    }

    public static int ImageSize
    {
        get
        {
            return CastAide.AsInt32(GetValue("imagesize"));
        }
    }

    public static bool VirtualDir
    {
        get
        {
            return GetValue("VirtualDir") == "1";
        }
    }

    public static DateTime StatementAvailableFrom
    {
        get
        {
            return CastAide.AsDateTime(GetValue("StatementAvailableFrom"));
        }
    }

    public static DateTime ReportStartDate
    {
        get
        {
            return CastAide.AsDateTime(GetValue("ReportStartDate"));
        }
    }

    public static int PageSize
    {
        get
        {
            return CastAide.AsInt32(ConfigurationManager.AppSettings["pagesize"]);
        }
    }

    public static string InternalEuropeanAffiliateURL
    {
        get
        {
            return GetValue("internalEuropeanAffiliateURL");
        }
    }

    public static string InternalAsianAffiliateURL
    {
        get
        {
            return GetValue("internalAsianAffiliateURL");
        }
    }

    public static string IPCheckerURL
    {
        get
        {
            return GetValue("IPCheckerURL");
        }
    }

    public static string WebsiteCheckerURL
    {
        get
        {
            return GetValue("WebsiteCheckerURL");
        }
    }

    public static string EuropeanAffiliateURL
    {
        get
        {
            return GetValue("EuropeanAffiliateURL");
        }
    }

    public static string AsianAffiliateURL
    {
        get
        {
            return GetValue("AsianAffiliateURL");
        }
    }

    public static bool LocalTest
    {
        get
        {
            return GetValue("LocalTest") == "1";
        }
    }

    public static string SuffixSp
    {
        get
        {
            return GetValue("SuffixSp");
        }
    }

    public static int BetListServiceTimeout
    {
        get
        {
            return CastAide.AsInt32(GetValue("BetListServiceTimeout"));
        }
    }

    public static string GetValue(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    #region ---------- for Session-------------

    public static class Session
    {
        public static string Language
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["Culture"]);
            }
            set
            {
                HttpContext.Current.Session["Culture"] = value;
            }
        }

        public static int RoleID
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["RoleID"], -1);
            }
            set
            {
                HttpContext.Current.Session["RoleID"] = value;
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
                return CastAide.AsInt32(HttpContext.Current.Session["On_UserID"], -1);
            }
            set
            {
                HttpContext.Current.Session["On_UserID"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                return HttpContext.Current.Session["UserName"].ToString();
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }

        public static string RoleAccess
        {
            get
            {
                var value = HttpContext.Current.Session["RoleAccess"];
                return value == null ? string.Empty : value.ToString();
            }
            set
            {
                HttpContext.Current.Session["RoleAccess"] = value;
            }
        }

        public static int AffId
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["AffId"]);
            }
            set
            {
                HttpContext.Current.Session["AffId"] = value;
            }
        }

        public static string AffUsername
        {
            get
            {
                return CastAide.AsString(HttpContext.Current.Session["AffUsername"], string.Empty);
            }
            set
            {
                HttpContext.Current.Session["AffUsername"] = value;
            }
        }

        public static AffType AffType
        {
            get
            {
                return (AffType)CastAide.AsInt32(HttpContext.Current.Session["AffType"], 0);
            }
            set
            {
                HttpContext.Current.Session["AffType"] = value;
            }
        }

        public static bool IsCasino
        {
            get
            {
                return CastAide.AsBoolean(HttpContext.Current.Session["IsCasino"], false);
            }
            set
            {
                HttpContext.Current.Session["IsCasino"] = value;
            }
        }
    }

    #endregion ---------- for Session-------------

    #region -------------For Mail Service------------------
    public static string MailContact
    {
        get { return ConfigurationManager.AppSettings["MailContact"]; }
    }

    public static string CCMail
    {
        get { return CastAide.AsString(ConfigurationManager.AppSettings["ccMail"]); }
    }

    public static string BCCMail
    {
        get { return CastAide.AsString(ConfigurationManager.AppSettings["bccMail"]); }
    }

    public static int StartYearOfSystem
    {
        get
        {
            return string.IsNullOrEmpty(GetValue("StartYearOfSystem")) ? 2007 : CastAide.AsInt32(GetValue("StartYearOfSystem"));
        }
    }

    #endregion -------------For Mail Service------------------
}