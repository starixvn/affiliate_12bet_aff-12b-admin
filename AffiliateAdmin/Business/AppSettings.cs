using System;
using System.Configuration;
using System.Web;
using System.Xml;
using System.IO;
using DNAide.Core;
/// <summary>
/// Summary description for AppSettings
/// </summary>
public static class AppSettings
{
    static string _site;
    static string _jsVersion;
    static string _serverId;
    static string _tplFolder;
    static string _themeDir;
    static string _pageHeader = string.Empty;
    static string[] _cssFiles;
    static string _affmaster;
    static string _affMedia;
    static string _mediaUrl;
    static string _link;
    static string _link_TH;
    static string _link_CS;
    static int _imagesize;
    static string _virtualDir;
    static string _internalEuropeanAffiliateURL;
    static string _internalAsianAffiliateURL;
    static string _ipCheckerURL;
    static string _websiteCheckerURL;
    static string _urlAffSite;
    static bool _localTest;
    static DateTime _StatementAvailableFrom;
    static string _suffixSp;
    private static int _betListServiceTimeout;

    private static DateTime _ReportStartDate;

    static string _europeanAffiliateURL;
    static string _asianAffiliateURL;
    private static int _startYearOfSystem;

    static AppSettings()
    {
        _virtualDir= ConfigurationManager.AppSettings["VirtualDir"];
        _site = ConfigurationManager.AppSettings["Site"];
        _jsVersion = ConfigurationManager.AppSettings["JsVersion"];
        _serverId = ConfigurationManager.AppSettings["ServerId"];
        _tplFolder = ConfigurationManager.AppSettings["template_folder"];
        _ipCheckerURL = ConfigurationManager.AppSettings["IPCheckerURL"];
        _themeDir = @"App_Themes/" + (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Theme"]) ? "Default" : ConfigurationManager.AppSettings["Theme"]);
        //GET CSS FILES
        _cssFiles = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + _themeDir);
        _affmaster = ConfigurationManager.AppSettings["AffMaster"];
        _affMedia = ConfigurationManager.AppSettings["AffMedia"];
        _mediaUrl = ConfigurationManager.AppSettings["MediaUrl"]; 
        _link = ConfigurationManager.AppSettings["12BetLink"];
        _link_TH = ConfigurationManager.AppSettings["12BetLink_TH"];
        _link_CS = ConfigurationManager.AppSettings["12BetLink_CS"];
        _imagesize =CastAide.AsInt32(ConfigurationManager.AppSettings["imagesize"]);
        _StatementAvailableFrom = CastAide.AsDateTime(ConfigurationManager.AppSettings["StatementAvailableFrom"]);
        _ReportStartDate = CastAide.AsDateTime(ConfigurationManager.AppSettings["ReportStartDate"]);
        
        _internalEuropeanAffiliateURL = ConfigurationManager.AppSettings["internalEuropeanAffiliateURL"];
        _internalAsianAffiliateURL = ConfigurationManager.AppSettings["internalAsianAffiliateURL"];
        _localTest = ConfigurationManager.AppSettings["LocalTest"].Equals("1", StringComparison.OrdinalIgnoreCase);
        

        _websiteCheckerURL = ConfigurationManager.AppSettings["WebsiteCheckerURL"];
        _urlAffSite = ConfigurationManager.AppSettings["UrlAffSite"];

        _europeanAffiliateURL = ConfigurationManager.AppSettings["EuropeanAffiliateURL"];
        _asianAffiliateURL = ConfigurationManager.AppSettings["AsianAffiliateURL"];

        for (int j = 0; j < _cssFiles.Length; ++j)
        {
            _cssFiles[j] = _cssFiles[j].Substring(_cssFiles[j].IndexOf("App_Themes")).Replace("\\", "/");
        }

        _suffixSp = ConfigurationManager.AppSettings["SuffixSp"];

        _betListServiceTimeout = CastAide.AsInt32(ConfigurationManager.AppSettings["BetListServiceTimeout"]);

        _startYearOfSystem = string.IsNullOrEmpty(ConfigurationManager.AppSettings["StartYearOfSystem"]) 
            ? 2007 : CastAide.AsInt32(ConfigurationManager.AppSettings["StartYearOfSystem"]);

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
            }
            else if (node.Attributes["name"].Value == "keywords")
            {
                _pageHeader += "<meta name=\"keywords\" content=\"" + node.Attributes["value"].Value + "\" />\n";
            }
            else if (node.Attributes["name"].Value == "description")
            {
                _pageHeader += "<meta name=\"description\" content=\"" + node.Attributes["value"].Value + "\" />\n";
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

    public static string BannerDirectory
    {
        get
        {
            return ConfigurationManager.AppSettings["BannerDirectory"];
        }
    }

    public static string BannerUrl
    {
        get
        {
            return ConfigurationManager.AppSettings["BannerUrl"];
        }
    }

    public static string Environment
    {
        get
        {
            return ConfigurationManager.AppSettings["Environment"];
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
    public static string AffMaster
    {
        get
        {

            return _affmaster;
        }
    }
    public static string AffMedia
    {
        get
        {

            return _affMedia;
        }
    }
    public static string MediaUrl
    {
        get
        {

            return _mediaUrl;
        }
    }
    public static string Link
    {
        get
        {

            return _link;
        }
    }
    public static string Link_CS
    {
        get
        {

            return _link_CS;
        }
    }
    public static string Link_TH
    {
        get
        {

            return _link_TH;
        }
    }
    public static int ImageSize
    {
        get
        {
            return _imagesize;
        }
    }
    public static bool VirtualDir
    {
        get
        {
            return _virtualDir=="1";
        }
    }

    public static DateTime StatementAvailableFrom
    {
        get
        {
            return _StatementAvailableFrom;
        }
    }

    public static DateTime ReportStartDate
    {
        get
        {
            return _ReportStartDate;
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
            return _internalEuropeanAffiliateURL;
        }
    }
    public static string InternalAsianAffiliateURL
    {
        get
        {
            return _internalAsianAffiliateURL;
        }
    }
    public static string IPCheckerURL
    {
        get
        {
            return _ipCheckerURL;
        }
    }
    public static string WebsiteCheckerURL
    {
        get
        {
            return _websiteCheckerURL;
        }
    }

    public static string UrlAffSite
    {
        get
        {
            return _urlAffSite;
        }
    }

    public static string EuropeanAffiliateURL
    {
        get
        {
            return _europeanAffiliateURL;
        }
    }

    public static string AsianAffiliateURL
    {
        get
        {
            return _asianAffiliateURL;
        }
    }

    public static bool LocalTest
    {
        get
        {
            return _localTest;
        }
    }
    
    public static string SuffixSp
    {
        get
        {
            return _suffixSp;
        }
    }

    public static int BetListServiceTimeout
    {
        get { return _betListServiceTimeout; }
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
            get {
                return CastAide.AsInt32(HttpContext.Current.Session["RoleID"],-1);   
            
            }
            set
            {
                HttpContext.Current.Session["RoleID"] = value; 
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
        public static bool IsAuthenticated
        {
            get
            {
                return (HttpContext.Current.Session["CustID"] != null);
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

    #endregion

    #region -------------For Mail Service------------------

    public static string MailServer
    {
        get { return ConfigurationManager.AppSettings["mailServer"]; }
    }
    public static string MailPwd
    {
        get { return DNAide.Utils.Encryption.CfsDeCrypt(ConfigurationManager.AppSettings["emailPwd"]); }
    }
    public static string MailContact
    {
        get { return ConfigurationManager.AppSettings["MailContact"]; }
    }
    public static string FromMail
    {
        get { return ConfigurationManager.AppSettings["fromMail"]; }
    }
    public static int Port
    {
        get { return CastAide.AsInt32(ConfigurationManager.AppSettings["port"]); }
    }

    public static string ToMail
    {
        get { return CastAide.AsString(ConfigurationManager.AppSettings["toMail"]); }
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
        get { return _startYearOfSystem; }
    }

    #endregion
}
