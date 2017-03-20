using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using AffiliateUser.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;

namespace GenerateHTML
{
    public class TrackingSystem
    {
        private readonly static IList<string> DeviceNames = new List<string> {
                "iPhone", "iPad", "iPod", "BlackBerry", "HTC", "Motorola", "Nokia"
            };

        public static string GetStatisticsAllMonthsContentList(string trackingcode)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "Content");
            DbResult result = DataHandler.TrackingSystem.SelClickStatisticsAllMonths(trackingcode);
            int cols = 4;
            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                foreach (DataTable dtbResult in result.ResultSet.Tables)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        string strTmp = strContent;
                        TemplateHandler.Assign(ref strTmp, "No-val", ++intNo);
                        TemplateHandler.Assign(ref strTmp, "TotalClick-val", CastAide.AsInt32(dr["totalclick"]).ToString(Common.NumberFormatPlaceholder));
                        TemplateHandler.Assign(ref strTmp, "UniqueClick-val", CastAide.AsInt32(dr["uniqueclick"]).ToString(Common.NumberFormatPlaceholder));

                        TemplateHandler.Assign(ref strTmp, "Month-val", Common.ConvertDateTime(dr["date"], "-", false, false));
                        TemplateHandler.Assign(ref strTmp, "MonthFull-val", dr["date"].ToString());
                        if ((intNo % 2) == 0)
                        {
                            TemplateHandler.Assign(ref strTmp, "rowstyle", "bgwhite");
                            TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "a");
                        }
                        else
                        {
                            TemplateHandler.Assign(ref strTmp, "rowstyle", "bgcgy");
                            TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "b");
                        }

                        strBuild.Append(strTmp);
                    }
                }
                if (intNo > 0)
                    return strBuild.ToString();
                return Common.NoInfo(cols);
            }
            return Common.NoInfo(cols);
        }

        public static string GetStatisticsMonthDetailContent(int month, int year, string trackingcode)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "StatisticsDetailContent");
            DbResult result = DataHandler.TrackingSystem.SelClickStatisticsDetail(month, year, trackingcode);
            int cols = 4;
            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                foreach (DataTable dtbResult in result.ResultSet.Tables)
                {
                    foreach (DataRow dr in dtbResult.Rows)
                    {
                        string strTmp = strContent;
                        TemplateHandler.Assign(ref strTmp, "No-val", ++intNo);
                        TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "Date-val", Common.ConvertDateTime(dr["date"], "-", true, false));
                        if ((intNo % 2) == 0)
                        {
                            TemplateHandler.Assign(ref strTmp, "rowstyle", "bgwhite");
                            TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "a");
                        }
                        else
                        {
                            TemplateHandler.Assign(ref strTmp, "rowstyle", "bgcgy");
                            TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "b");
                        }

                        strBuild.Append(strTmp);
                    }
                }
                if (intNo > 0)
                    return strBuild.ToString();
                return Common.NoInfo(cols);
            }
            return Common.NoInfo(cols);
        }

        public static string GetTrackingCodeList(bool isGenAll, string activedValue)
        {
            DbResult trackingCodeList = DataHandler.TrackingSystem.TrackingCodeSel();
            StringBuilder result = new StringBuilder();
            string opt = TemplateHandler.LoadTemplate("Option.htm");
            TemplateHandler.Assign(ref opt, "id", "slTrackingCode");
            string temp = TemplateHandler.CopyBlock(opt, "optionList");
            string tempopt;
            string selected = "selected";
            string noneValue = string.Empty;
            if (isGenAll)
            {
                tempopt = temp;
                TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
                TemplateHandler.Assign(ref tempopt, "Optname", "[" + Resources.Lang.All + "]");
                if (string.IsNullOrEmpty(activedValue))
                    TemplateHandler.Assign(ref tempopt, "selected", "selected");
                result.Append(tempopt);
                selected = string.Empty;
                noneValue = "[None]";
            }
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", noneValue);
            TemplateHandler.Assign(ref tempopt, "Optname", "[" + Resources.Lang.none + "]");
            if (string.IsNullOrEmpty(activedValue) && !isGenAll)
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            result.Append(tempopt);
            if (trackingCodeList.ResultSet.Tables.Count > 0)
            {
                DataTable dt = trackingCodeList.ResultSet.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    tempopt = temp;
                    if (dr["trackingcode"].ToString() != "[None]")
                    {
                        if (!string.IsNullOrEmpty(activedValue) && string.Compare(activedValue, dr["trackingcode"].ToString(), true) == 0)
                            TemplateHandler.Assign(ref tempopt, "selected", "selected");
                        TemplateHandler.Assign(ref tempopt, "Optvalue", dr["trackingcode"].ToString());
                        TemplateHandler.Assign(ref tempopt, "Optname", dr["trackingcode"].ToString());
                        TemplateHandler.Assign(ref tempopt, "selected", string.Empty);
                        result.Append(tempopt);
                    }
                }
            }

            TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
            return opt;
        }

        public static string GetStatisticsBySite(object fromdate, object todate, string deviceType)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_New.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "Content");

            TemplateHandler.Assign(ref strContent, "fdate", Common.ConvertDateTime(fromdate, "/", true, false));
            TemplateHandler.Assign(ref strContent, "tdate", Common.ConvertDateTime(todate, "/", true, false));

            DbResult result = DataHandler.TrackingSystem.SelClickStatisticsBySite(fromdate, todate, deviceType);
            int totalSignups = CastAide.AsInt32(result.Outputs["totalsignups"]);
            int totalDeposits = CastAide.AsInt32(result.Outputs["totalfdms"]);

            int cols = 5;
            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                foreach (DataRow dr in result.ResultSet.Tables[0].Rows)
                {
                    intNo++;
                    string strTmp = strContent;
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", CastAide.AsInt32(dr["totalclick"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", CastAide.AsInt32(dr["uniqueclick"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "Signups-val", CastAide.AsInt32(dr["Signups"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "FDM-val", CastAide.AsInt32(dr["FDM"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "DeviceType-val", deviceType);

                    var domain = dr["DomainReferrer"].ToString();
                    if (domain.Equals("none", StringComparison.OrdinalIgnoreCase) ||
                        domain.Equals("old url", StringComparison.OrdinalIgnoreCase))
                    {
                        TemplateHandler.ReplaceBlock(ref strTmp, "domain", domain);
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "Site-val", domain);
                    }

                    if ((intNo % 2) == 0)
                    {
                        TemplateHandler.Assign(ref strTmp, "rowstyle", "bgwhite");
                        TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "a");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "rowstyle", "bgcgy");
                        TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "b");
                    }
                    strBuild.Append(strTmp);
                }

                return strBuild.ToString();
            }

            return Common.NoInfo(cols);
        }

        public static string GetStatisticsDetail_New(DateTime fDate, DateTime tDate, string domain, string sportCode, string languageCode, int pageSize, int pageIndex, string deviceType)
        {
            DbResult result = DataHandler.TrackingSystem.SelStatisticsDetail_New(fDate, tDate, domain, sportCode, languageCode, pageSize, pageIndex, deviceType);
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_New.htm");
            string strContentTable = TemplateHandler.CopyBlock(htmlContent, "StatisticsDetail");

            TemplateHandler.Assign(ref strContentTable, "{Site}", Resources.Lang.SiteUrl);
            TemplateHandler.Assign(ref strContentTable, "{TotalClick}", Resources.Lang.TotalClick);
            TemplateHandler.Assign(ref strContentTable, "{UniqueClick}", Resources.Lang.UniqueClick);
            TemplateHandler.Assign(ref strContentTable, "{Signups}", Resources.Lang.SignUp);
            TemplateHandler.Assign(ref strContentTable, "{FDM}", Resources.Lang.FDM);

            int totalPage = CastAide.AsInt32(result.Outputs["totalpages"]);
            TemplateHandler.Assign(ref strContentTable, "paging-top", Common.GenPaging(totalPage, pageIndex, pageSize, 0, "aPageTop", "txtPageTop", 612, false, true));
            TemplateHandler.Assign(ref strContentTable, "paging-bottom", Common.GenPaging(totalPage, pageIndex, pageSize, 0, "aPageBottom", "txtPageBottom", 612, false, false));
            TemplateHandler.Assign(ref strContentTable, "page", pageIndex);
            strContentTable = strContentTable.Replace("Common.loadData", "loadDetail");
            strContentTable = strContentTable.Replace("changePage", "submitPage");

            string strContent = TemplateHandler.CopyBlock(strContentTable, "StatisticsDetailContent");
            int cols = 5;
            int intNo = 0;
            StringBuilder strBuild = new StringBuilder();
            if (result.ResultSet.Tables.Count > 0)
            {
                DataTable dtbResult = result.ResultSet.Tables[0];
                foreach (DataRow dr in dtbResult.Rows)
                {
                    intNo++;
                    string strTmp = strContent;
                    TemplateHandler.Assign(ref strTmp, "Site-val", dr["UrlReferrer"]);
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                    TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());
                    TemplateHandler.Assign(ref strTmp, "DeviceType-val", deviceType);
                    if ((intNo % 2) == 0)
                    {
                        TemplateHandler.Assign(ref strTmp, "rowstyle", "bgwhite");
                        TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "a");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "rowstyle", "bgcgy");
                        TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "b");
                    }

                    strBuild.Append(strTmp);
                }
            }
            if (intNo > 0)
            {
                TemplateHandler.ReplaceBlock(ref strContentTable, "StatisticsDetailContent", strBuild.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strContentTable, "StatisticsDetailContent", Common.NoInfo(cols));
            }
            return strContentTable;
        }

        public static string GetStatisticsByPage(object fromdate, object todate, string sportCode, string languageCode, string deviceType)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics_LandingPage.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "Content");

            TemplateHandler.Assign(ref strContent, "fdate", fromdate);
            TemplateHandler.Assign(ref strContent, "tdate", todate);

            DbResult result = DataHandler.TrackingSystem.SelClickStatisticsByPage(fromdate, todate, sportCode, languageCode, deviceType);
            int totalSignups = CastAide.AsInt32(result.Outputs["totalsignups"]);
            int totalDeposits = CastAide.AsInt32(result.Outputs["totalfdms"]);
            int cols = 5;

            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = 0;
                var strBuild = new StringBuilder();
                foreach (DataRow dr in result.ResultSet.Tables[0].Rows)
                {
                    intNo++;
                    string strTmp = strContent;
                    var sportcode = dr["sportcode"].ToString();
                    if (sportcode.Equals("none", StringComparison.OrdinalIgnoreCase) ||
                        sportcode.Equals("old url", StringComparison.OrdinalIgnoreCase))
                    {
                        TemplateHandler.ReplaceBlock(ref strTmp, "domain", sportcode);
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "Page-val", sportcode);
                    }

                    TemplateHandler.Assign(ref strTmp, "Language-val", Common.GetLanguageOriginal(dr["languagecode"].ToString()));
                    TemplateHandler.Assign(ref strTmp, "langCode-val", dr["languagecode"]);
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", CastAide.AsInt32(dr["totalclick"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", CastAide.AsInt32(dr["uniqueclick"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "Signups-val", CastAide.AsInt32(dr["Signups"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "FDM-val", CastAide.AsInt32(dr["FDM"]).ToString(Common.NumberFormatPlaceholder));
                    TemplateHandler.Assign(ref strTmp, "DeviceType-val", deviceType);

                    if ((intNo % 2) == 0)
                    {
                        TemplateHandler.Assign(ref strTmp, "rowstyle", "bgwhite");
                        TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "a");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTmp, "rowstyle", "bgcgy");
                        TemplateHandler.Assign(ref strTmp, "onmouseoutstyle", "b");
                    }
                    strBuild.Append(strTmp);
                }

                return strBuild.ToString();
            }

            return Common.NoInfo(cols);
        }

        #region for clickstatistics insert

        public static string GetIPAddressOfClient()
        {
            string ip;
            ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }
            return ip;
        }

        public static RegionInfo ResolveCountry()
        {
            CultureInfo culture = ResolveCulture();
            if (culture != null)
                return new RegionInfo(culture.LCID);

            return null;
        }

        public static CultureInfo ResolveCulture()
        {
            string[] languages = HttpContext.Current.Request.UserLanguages;

            if (languages == null || languages.Length == 0)
                return null;

            try
            {
                string language = languages[0].ToLowerInvariant().Trim();
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static string GetDeviceName(string userAgent)
        {
            string device = "Computer/Laptop";

            foreach (var name in DeviceNames)
            {
                if (userAgent.Contains(name))
                {
                    device = name;
                    break;
                }
            }

            return device;
        }

        public static string GetOSName(string userAgent)
        {
            string os = string.Empty;
            //microsoft
            if (userAgent.Contains("Windows NT 6.1"))
            {
                os = "Windows 7";
            }
            else if (userAgent.Contains("Windows NT 6.0"))
            {
                os = "Windows Vista";
            }
            else if (userAgent.Contains("Windows NT 5.2"))
            {
                os = "Windows Server 2003";
            }
            else if (userAgent.Contains("Windows NT 5.1"))
            {
                os = "Windows XP";
            }
            else if (userAgent.Contains("Windows NT 5.0"))
            {
                os = "Windows 2000";
            }
            else if (userAgent.Contains("Windows NT 4.1"))
            {
                os = "Windows 98";
            }
            else if (userAgent.Contains("Windows"))
            {
                os = "Windows";
            }
            //apple
            else if (userAgent.Contains("iPhone") ||
                        userAgent.Contains("iPad") ||
                        userAgent.Contains("iPod"))
            {
                os = "iOS";
            }
            else if (userAgent.Contains("Mac OS"))
            {
                os = "Mac OS";
            }
            //RIM
            else if (userAgent.Contains("BlackBerry"))
            {
                os = "BlackBerry";
            }
            //Google
            else if (userAgent.Contains("Android"))
            {
                os = "Android";
            }
            //Linux
            else if (userAgent.Contains("Linux"))
            {
                os = "Linux";
            }
            else if (userAgent.Contains("Ubuntu"))
            {
                os = "Ubuntu";
            }
            else
                os = "unknown";
            return os;
        }

        #endregion for clickstatistics insert
    }
}