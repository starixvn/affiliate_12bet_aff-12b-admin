using System;
using System.Configuration;
using System.Web;
using System.Xml;
using System.IO;
using Affiliate;
using DNAide.Core;

/// <summary>
/// Summary description for AppSettings
/// </summary>
public static class AppSettings
{
    static string _site;
    static string _12BetLink;
    static string _12BetLink_CS;
    static string _12BetLink_TH;
    static string _12BetLink_EU;
    static string _12BetLink_VI;
    


    static string _jsVersion;
    static string _serverId;
    static string _tplFolder;
    static string _themeDir;
    static string _pageHeader = string.Empty;
    static string[] _cssFiles;
    static bool _hasLogin;
    static string _pageTitle = string.Empty, _pageKeywords = string.Empty, _pageDescription = string.Empty;
    static string _affmaster;
    static string _subdomain;
    static string _forgetpwurl;
    static int _afftype;

    static string _mediaJS;
    static string _mediaJS_CS;
    static string _mediaJS_TH;
    static string _mediaJS_VI;
    static string _mediaJS_EU;

    static string _listAffUseEuLink;
    static string _lbUrl;
    static DateTime _UMStartTime;
    static DateTime _UMEndTime;
    static DateTime _StatementAvailableFrom;
    static int _pageSize;
    static string _affPersonalizedPageLink; //20101016@Peter add the link for affiliate personalized Page
    static string _affPublicKeyToken; //20101016@Peter add the public key token for affiliate personalized Page authentication
    static string _bannerUrl;
    static bool _showCasino;
    static bool _localTest;
    static string _asianAffDomain;
    static int _asianAffPort;
    static string _europeanAffDomain;
    static int _europeanAffPort;

    static AppSettings()
    {
        _affPersonalizedPageLink = ConfigurationManager.AppSettings["AffPersonalizedPageLink"];
        _affPublicKeyToken = ConfigurationManager.AppSettings["AffPublicKeyToken"];

        _asianAffDomain = ConfigurationManager.AppSettings["AsianAffDomain"];
        _asianAffPort = CastAide.AsInt32(ConfigurationManager.AppSettings["AsianAffPort"]);
        _europeanAffDomain = ConfigurationManager.AppSettings["EuropeanAffDomain"];
        _europeanAffPort = CastAide.AsInt32(ConfigurationManager.AppSettings["EuropeanAffPort"]);

        _site = ConfigurationManager.AppSettings["Site"];
        _12BetLink = ConfigurationManager.AppSettings["12BetLink"];
        _12BetLink_CS = ConfigurationManager.AppSettings["12BetLink_CS"];
        _12BetLink_TH = ConfigurationManager.AppSettings["12BetLink_TH"];
        _12BetLink_EU = ConfigurationManager.AppSettings["12BetLink_EU"];
        _12BetLink_VI = ConfigurationManager.AppSettings["12BetLink_VI"];
        _subdomain = ConfigurationManager.AppSettings["SubDomain"];
        _jsVersion = ConfigurationManager.AppSettings["JsVersion"];
        _serverId = ConfigurationManager.AppSettings["ServerId"];
        _tplFolder = ConfigurationManager.AppSettings["template_folder"];
        _affmaster = ConfigurationManager.AppSettings["AffMaster"];
        _afftype = CastAide.AsInt32(ConfigurationManager.AppSettings["AffType"]);
        _mediaJS = ConfigurationManager.AppSettings["MediaJS"];
        _mediaJS_CS = ConfigurationManager.AppSettings["MediaJS_CS"];
        _mediaJS_TH = ConfigurationManager.AppSettings["MediaJS_TH"];
        _mediaJS_VI = ConfigurationManager.AppSettings["MediaJS_VI"];
        _mediaJS_EU = ConfigurationManager.AppSettings["MediaJS_EU"];
        _listAffUseEuLink = ConfigurationManager.AppSettings["ListAffUseEuLink"]; 
        _lbUrl = ConfigurationManager.AppSettings["LBUrl"];
        _UMStartTime = CastAide.AsDateTime(ConfigurationManager.AppSettings["UM_StartTime"]);
        _UMEndTime = CastAide.AsDateTime(ConfigurationManager.AppSettings["UM_EndTime"]);
        _pageSize = CastAide.AsInt32(ConfigurationManager.AppSettings["PageSize"]);
        _StatementAvailableFrom = CastAide.AsDateTime(ConfigurationManager.AppSettings["StatementAvailableFrom"]);
        _themeDir = @"App_Themes/" + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Theme"]) ? "Default" : ConfigurationManager.AppSettings["Theme"]);
        //GET CSS FILES
        _cssFiles = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + _themeDir);
        for (int j = 0; j < _cssFiles.Length; ++j)
        {
            _cssFiles[j] = _cssFiles[j].Substring(_cssFiles[j].IndexOf("App_Themes")).Replace("\\", "/");
        }
        _bannerUrl = ConfigurationManager.AppSettings["BannerUrl"];
        _showCasino = (ConfigurationManager.AppSettings["ShowCasino"] == "1");
        _localTest = (ConfigurationManager.AppSettings["LocalTest"] == "1");
        initPageHeader();
    }
    static void initPageHeader()
    {
        //try
        //{
        _pageHeader = string.Empty;
        XmlDocument doc = new XmlDocument();
        doc.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"\Web.info");
        foreach (XmlNode node in doc.ChildNodes[1].ChildNodes)
        {
            if (node.Attributes["name"].Value == "title")
            {
                _pageHeader += "<title>" + node.Attributes["value"].Value + "</title>\n";
                _pageTitle = node.Attributes["value"].Value;
            }
            else if (node.Attributes["name"].Value == "keywords")
            {
                _pageHeader += "<meta name=\"keywords\" content=\"" + node.Attributes["value"].Value + "\" />\n";
                _pageKeywords = node.Attributes["value"].Value;
            }
            else if (node.Attributes["name"].Value == "description")
            {
                _pageHeader += "<meta name=\"description\" content=\"" + node.Attributes["value"].Value + "\" />\n";
                _pageDescription = node.Attributes["value"].Value;
            }
            else if (node.Attributes["name"].Value == "contenttype")
            {
                _pageHeader += "<meta http-equiv=\"Content-Type\" content=\"" + node.Attributes["value"].Value + "\" />\n";
            }
        }
        /*}
        catch
        {
        }*/
    }
    public static int PageSize
    {
        get
        {
            return _pageSize;
        }
    }
    public static string[] CssFiles
    {
        get
        {
            return _cssFiles;
        }
    }
    public static string Site
    {
        get
        {
            return _site;
        }
    }
    public static string BetLink
    {
        get
        {
            return _12BetLink;
        }
    }
    public static string BetLink_CS
    {
        get
        {
            return _12BetLink_CS;
        }
    }
    public static string BetLink_TH//Use for Thai language
    {
        get
        {
            return _12BetLink_TH;
        }
    }
    public static string BetLink_EU
    {
        get
        {
            return _12BetLink_EU;
        }
    }

    public static string BetLink_VI
    {
        get
        {
            return _12BetLink_VI;
        }
    }

    public static string SubDomain
    {
        get
        {
            return _subdomain;
        }
    }

    public static string JsVersion
    {
        get
        {
            return _jsVersion;
        }
    }
    public static string ServerId
    {
        get
        {
            return _serverId;
        }
    }
    public static string TplFolder
    {
        get
        {
            return _tplFolder;
        }
    }
    public static void InitThemeInfo()
    {

    }
    public static string HtmlHeader
    {
        get
        {
            return _pageHeader;
        }
    }
    public static string ThemeDir
    {
        get
        {
            return _themeDir;
        }
    }
    public static string PageTitle
    {
        get
        {
            return _pageTitle;
        }
    }
    public static string PageKeywords
    {
        get
        {
            return _pageKeywords;
        }
    }
    public static string PageDescription
    {
        get
        {
            return _pageDescription;
        }
    }
    public static string AffMaster
    {

        get
        {

            return _affmaster;
        }
    }
    public static int AffType
    {
        get
        {
            return _afftype;
        }
    }
    
    public static string AsianAffDomain
    {
        get
        {
            return _asianAffDomain;
        }
    }

    public static int AsianAffPort
    {
        get
        {
            return _asianAffPort;
        }
    }

    public static string EuropeanAffDomain
    {
        get
        {
            return _europeanAffDomain;
        }
    }

    public static int EuropeanAffPort
    {
        get
        {
            return _europeanAffPort;
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
            return _mediaJS;
        }
    }
    public static string MediaJS_CS
    {
        get
        {
            return _mediaJS_CS;
        }
    }
    public static string MediaJS_TH
    {
        get
        {
            return _mediaJS_TH;
        }
    }
    public static string MediaJS_VI
    {
        get
        {
            return _mediaJS_VI;
        }
    }
    public static string MediaJS_EU
    {
        get
        {
            return _mediaJS_EU;
        }
    }
    public static string ListAffUseEuLink
    {
        get
        {
            return _listAffUseEuLink;
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
            return _StatementAvailableFrom;
        }
    }
    public static string LBUrl
    {
        get
        {
            return _lbUrl;
        }
    }
    public static DateTime UMStartTime
    {
        get
        {
            return _UMStartTime;
        }
    }
    public static DateTime UMEndTime
    {
        get
        {
            return _UMEndTime;
        }
    }
    #region ---------- for Session-------------

    public static class Session
    {
        public static bool IsAllowedAffPersonalizedPage
        {
            get
            {
                return CastAide.AsBoolean(HttpContext.Current.Session["IsAllowedAffPersonalizedPage"]);
            }
            set
            {
                HttpContext.Current.Session["IsAllowedAffPersonalizedPage"] = value;
            }
        }

        public static int AffLevel
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["AffLevel"]);
            }
            set
            {
                HttpContext.Current.Session["AffLevel"] = value;
            }
        }

        public static int AdminID
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["AdminID"]);
            }
            set
            {
                HttpContext.Current.Session["AdminID"] = value;
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
        // ++ Added by Frank.Ngo IRP-671 on Jan 2012
        public static string CommGroupName
        {
            get { return CastAide.AsString(HttpContext.Current.Session["CommGroupName"]); }
            set { HttpContext.Current.Session["CommGroupName"] = value; }
        }
        // -- Added by Frank.Ngo IRP-671 on Jan 2012

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

        /*20090202@DungTong add to check where the this aff has secSuriry question yet */
        public static bool HasSecurityQuestion
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["HasSecurityQuestion"]); }
            set { HttpContext.Current.Session["HasSecurityQuestion"] = value; }
        }
        static bool fromHome = false;
        public static bool IsFromHome
        {
            get { return fromHome; }
            set { fromHome = value; }
        }
        static bool firsTimeLoginSucc = false;
        public static bool FirsTimeLoginSucc
        {
            get { return firsTimeLoginSucc; }
            set { firsTimeLoginSucc = value; }
        }
        //20100305@Johnny add to get mail option of aff(plain text or html format)
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
        //20100305@Johnny add to verify whether this user has 12betcasino
        public static bool IsCasino
        {
            get { return CastAide.AsBoolean(HttpContext.Current.Session["IsCasino"]); }
            set { HttpContext.Current.Session["IsCasino"] = value; }
        }
        //20140211@Brian add to verify whether to force password change
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


    #endregion
    #region -------------For Mail Service------------------

    public static string MailServer
    {
        get { return ConfigurationManager.AppSettings["SMTPServer"]; }
    }
    public static string MailPwd
    {
        get { return DNAide.Utils.Encryption.CfsDeCrypt(ConfigurationManager.AppSettings["PassMail"]); }
    }
    public static string FromMail
    {
        get { return ConfigurationManager.AppSettings["FromMail"]; }
    }
    public static int Port
    {
        get { return CastAide.AsInt32(ConfigurationManager.AppSettings["SMTPPort"]); }
    }

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
    #endregion
    public static string AffPersonalizedPageLink
    {
        get
        {
            return _affPersonalizedPageLink; //20101016@Peter add the link for affiliate personalized Page
        }
    }
    public static string AffPublicKeyToken
    {
        get
        {
            return _affPublicKeyToken;
        }
    }
    public static string BannerUrl
    {
        get
        {
            return _bannerUrl;
        }
    }

    public static bool ShowCasino
    {
        get
        {
            return _showCasino;
        }
    }

    public static bool LocalTest
    {
        get { return _localTest; }
    }
}
