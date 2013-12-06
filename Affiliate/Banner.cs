using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
using System.Configuration;
using System.Web;
namespace GenerateHTML
{
    /// <summary>
    /// Generate content for Banner
    /// Written by : Tong Hoang Dung on 24/03/2009
    /// </summary>
    public class Banner
    {
        public static string GetBannerLang(string value)
        {

            switch (value.ToLower())
            {
                case "bonus": return Resources.Lang.Bonus;
                case "tennis": return Resources.Lang.Tennis;
                case "football": return Resources.Lang.Football;
                case "poker": return Resources.Lang.Poker;
                case "image": return Resources.Lang.Image;
                case "flash": return Resources.Lang.Flash;
                case "english": return Resources.Lang.English;
                case "chinese traditional": return Resources.Lang.ChineseTraditio;
                case "chinese simplified": return Resources.Lang.ChineseSimplifi;
                case "thai": return Resources.Lang.Thai;
                case "spanish": return Resources.Lang.Spanish; 
                case "casino": return Resources.Lang.Casino;
                case "basketball": return Resources.Lang.Basketball;
                case "tennins": return Resources.Lang.Tennis;
                case "rmb": return Resources.Lang.RMB;
                case "myr": return Resources.Lang.MYR;
                case "thb": return Resources.Lang.THB;
                case "usd": return Resources.Lang.USD;
                case "eur": return Resources.Lang.EUR;
                case "gbp": return Resources.Lang.GBP;
                case "aud": return Resources.Lang.AUD;
                case "chf": return Resources.Lang.CHF;

                default:
                    return value;

            }

        }
        public static string GetTextLink(string trackingcode,DataTable language)        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
            StringBuilder textLinkList = new StringBuilder();
            string textLink = TemplateHandler.CopyBlock(htmlContent, "TextLinkList");
            string _12betLink = AppSettings.BetLink.ToString();
            
            if (string.Compare(AppSettings.Session.PreferLang, "th-TH", true) == 0)
                _12betLink = AppSettings.BetLink_TH.ToString();
            else if (string.Compare(AppSettings.Session.PreferLang, "zh-CN", true) == 0 || string.Compare(AppSettings.Session.PreferLang, "zh-TW", true) == 0)
                _12betLink = AppSettings.BetLink_CS.ToString();

            TemplateHandler.Assign(ref _12betLink, "{custid}", AppSettings.Session.CustID.ToString());
            if (!string.IsNullOrEmpty(trackingcode))
            { 
                trackingcode = "/" + trackingcode;
            }
            TemplateHandler.Assign(ref _12betLink, "{/TrackingCode}",trackingcode.Trim());
            foreach (DataRow dr in language.Rows)
            {
                string aTextLink = textLink;
                string link = _12betLink;
                TemplateHandler.Assign(ref link, "lang", Common.GetLangCode(dr["value"].ToString()));
                TemplateHandler.Assign(ref aTextLink, "language_val", GetBannerLang(dr["value"].ToString()));
                TemplateHandler.Assign(ref aTextLink, "link_val", link);
                textLinkList.Append(aTextLink);
            }
            string defaultLink = _12betLink.Replace("/{lang}", string.Empty);
            string defaultTextLink = textLink;
            TemplateHandler.Assign(ref defaultTextLink, "language_val", Resources.Lang.Default);
            TemplateHandler.Assign(ref defaultTextLink, "link_val", defaultLink);

            textLinkList.Append(defaultTextLink);
            return textLinkList.ToString();
        }
        public static string GetBannerList(string category, string language, string imageType, string size, string currency, int pageIndex, int pagesize, ref int totalpage,string trackingcode)
        {

            DbResult result = null;
            int nRows = 0;
            result = DataHandler.UserAff.SelAffBanner(category, language, imageType, size, currency, pageIndex, pagesize, Common.NormalBanner);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            //int pagesize = AppSettings.PageSize;
            int cols = 9;
            
            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("UserInfo/MediaGallery.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");                
                totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 3;
                string signUpLink = AppSettings.BetLink.ToString();

                if (string.Compare(AppSettings.Session.PreferLang, "th-TH", true) == 0)
                    signUpLink = AppSettings.BetLink_TH.ToString();
                else if (string.Compare(AppSettings.Session.PreferLang, "zh-CN", true) == 0 || string.Compare(AppSettings.Session.PreferLang, "zh-TW", true) == 0)
                    signUpLink = AppSettings.BetLink_CS.ToString();

                TemplateHandler.Assign(ref signUpLink, "{custid}", AppSettings.Session.CustID.ToString());
                if (!string.IsNullOrEmpty(trackingcode))
                {
                    trackingcode = "/" + trackingcode;
                }
                TemplateHandler.Assign(ref signUpLink, "{/TrackingCode}", trackingcode.Trim());
                string _12betLink=signUpLink;
                if (_12betLink.IndexOf("http://") >= 0)
                 {
                         _12betLink = _12betLink.Substring(7);
                }
        
                 if (_12betLink.IndexOf("/") > 0)
                 {
                     _12betLink = _12betLink.Substring(0, _12betLink.IndexOf("/"));
                 }
                 string flContent = HttpContext.Current.Server.HtmlEncode(TemplateHandler.LoadTemplate("UserInfo/FlashCode.htm"));
               

                string strMedia = AppSettings.MediaJS;
                string src = ConfigurationManager.AppSettings["MediaUrl"].ToString();

                if (string.Compare(AppSettings.Session.PreferLang, "th-TH", true) == 0)
                {
                    strMedia = AppSettings.MediaJS_TH;
                    src = ConfigurationManager.AppSettings["MediaUrl_TH"].ToString();
                }
                else if (string.Compare(AppSettings.Session.PreferLang, "zh-TW", true) == 0 || string.Compare(AppSettings.Session.PreferLang, "zh-CN", true) == 0)
                {
                    strMedia = AppSettings.MediaJS_CS;
                    src = ConfigurationManager.AppSettings["MediaUrl_CS"].ToString();
                }

                TemplateHandler.Assign(ref flContent, "src", strMedia);

                foreach (DataRow dr in dtbResult.Rows)
                {
                    counter++;
                    string rowText = content;
                    string aSignUpLink = signUpLink;
                    TemplateHandler.Assign(ref aSignUpLink, "lang", Common.GetLangCode(dr["language"].ToString()));
                    string tmpSrc = src + dr["filename"].ToString();
                    //string src = ((string.Compare(AppSettings.Session.Language, "zh-CN", true) == 0 || string.Compare(AppSettings.Session.Language, "zh-TW", true) == 0) ? ConfigurationManager.AppSettings["MediaUrl_CS"].ToString() : ConfigurationManager.AppSettings["MediaUrl"].ToString()) + dr["filename"].ToString();
                    string []strSize = dr["size"].ToString().Split(new char[]{'x'});
                    if (System.IO.Path.GetExtension(dr["filename"].ToString()) == ".swf")
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "ImageContent");
                        string flashContent = flContent;
                        TemplateHandler.Assign(ref rowText, "FlashContent_val", flashContent);
                        TemplateHandler.Assign(ref rowText, "custid_val", AppSettings.Session.CustID.ToString());
                        TemplateHandler.Assign(ref rowText, "srcnoswf_val", tmpSrc.Replace(".swf", string.Empty));

                    }
                    else
                    {
                        TemplateHandler.RemoveBlock(ref rowText, "FlashContent");
                        TemplateHandler.Assign(ref rowText, "12betlink_val", _12betLink);                       
                    }
                    TemplateHandler.Assign(ref rowText, "src_val", tmpSrc);
                    TemplateHandler.Assign(ref rowText, "signuplink_val", aSignUpLink);
                    TemplateHandler.Assign(ref rowText, "width_val", strSize[0]);
                    TemplateHandler.Assign(ref rowText, "height_val", strSize[1]);
                    contentList.Append(rowText);

                }

                //if (totalpage > 0)
                //{
                //    contentList.Append(Common.GenPagingFooter("", totalpage, pageIndex, cols));                
                //}
                return contentList.ToString();

            }
            else
            {
                return Common.NoInfo(cols);
            }

        }
    }
}
