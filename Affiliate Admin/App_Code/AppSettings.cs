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
    static string _internalURL;
    static string _ipCheckerURL;
    static string _websiteCheckerURL;
    static string _lbUrl;
    static string _urlAffSite;
    static DateTime _UMStartTime;
    static DateTime _UMEndTime;
    static DateTime _StatementAvailableFrom;
    static AppSettings()
    {
        _virtualDir= System.Configuration.ConfigurationManager.AppSettings["VirtualDir"];
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
        _internalURL = ConfigurationManager.AppSettings["internalURL"];
        _websiteCheckerURL = ConfigurationManager.AppSettings["WebsiteCheckerURL"];
        _lbUrl = ConfigurationManager.AppSettings["LBUrl"];
        _urlAffSite = ConfigurationManager.AppSettings["UrlAffSite"];
        _UMStartTime = CastAide.AsDateTime(ConfigurationManager.AppSettings["UM_StartTime"]);
        _UMEndTime = CastAide.AsDateTime(ConfigurationManager.AppSettings["UM_EndTime"]);
        for (int j = 0; j < _cssFiles.Length; ++j)
        {
            _cssFiles[j] = _cssFiles[j].Substring(_cssFiles[j].IndexOf("App_Themes")).Replace("\\", "/");
        }

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
    public static int PageSize
    {
        get
        {
            return CastAide.AsInt32(ConfigurationManager.AppSettings["pagesize"]);
        }
    }
    public static string InternalURL
    {
        get 
        {
            return _internalURL;
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
    public static string LBUrl
    {
        get
        {
            return _lbUrl;
        }
    }
    public static string UrlAffSite
    {
        get
        {
            return _urlAffSite;
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
                return HttpContext.Current.Session["RoleAccess"].ToString();
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
        public static int AffType
        {
            get
            {
                return CastAide.AsInt32(HttpContext.Current.Session["AffType"], 0);
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
        get { return System.Configuration.ConfigurationManager.AppSettings["mailServer"]; }
    }
    public static string MailPwd
    {
        get { return DNAide.Utils.Encryption.CfsDeCrypt(System.Configuration.ConfigurationManager.AppSettings["emailPwd"]); }
    }
    public static string MailContact
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["MailContact"]; }
    }
    public static string FromMail
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["fromMail"]; }
    }
    public static int Port
    {
        get { return DNAide.Core.CastAide.AsInt32(System.Configuration.ConfigurationManager.AppSettings["port"]); }
    }

    public static string ToMail
    {
        get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["toMail"]); }
    }

    public static string CCMail
    {
        get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["ccMail"]); }
    }

    public static string BCCMail
    {
        get { return DNAide.Core.CastAide.AsString(System.Configuration.ConfigurationManager.AppSettings["bccMail"]); }
    }
    #endregion
}
