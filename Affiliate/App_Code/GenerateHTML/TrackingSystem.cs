using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Globalization;
using DataHandler;


namespace GenerateHTML
{
    /// <summary>
    /// Generate content for Tracking System
    /// Written by : Tong Hoang Dung on 19/03/2009
    /// </summary>
    public class TrackingSystem
    {
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
                        TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                        TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                        
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
                if(intNo>0)
                    return strBuild.ToString();
                return Common.NoInfo(cols);
            }
            return Common.NoInfo(cols);

        }

        public static string GetStatisticsMonthDetailContent(int month,int year,string trackingcode)
        {
            string htmlContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics.htm");
            string strContent = TemplateHandler.CopyBlock(htmlContent, "StatisticsDetailContent");
            DbResult result = DataHandler.TrackingSystem.SelClickStatisticsDetail(month,year,trackingcode);
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
            int cols = 5;
            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = 0;
                int totalSignups = 0, totalDeposits = 0;
                StringBuilder strBuild = new StringBuilder();
                foreach (DataRow dr in result.ResultSet.Tables[0].Rows)
                {
                    intNo++;
                    string strTmp = strContent;
                    totalSignups += CastAide.AsInt32(dr["Signups"]);
                    totalDeposits += CastAide.AsInt32(dr["FDM"]);
                    TemplateHandler.Assign(ref strTmp, "Site-val", dr["DomainReferrer"]);
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                    TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());

                    if (string.Compare(dr["DomainReferrer"].ToString(), "none", true) == 0)
                        TemplateHandler.ReplaceBlock(ref strTmp, "domain", dr["DomainReferrer"].ToString());

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

                if (intNo > 0)
                {
                    TemplateHandler.Assign(ref strContent, "rowstyle", (intNo % 2) == 0 ? "bgcgy" : "bgwhite");
                    TemplateHandler.Assign(ref strContent, "onmouseoutstyle", (intNo % 2) == 0 ? "b" : "a");
                    //Assign total signups for Old URL
                    strBuild.Append(GenRowOldURL(strContent, totalSignups, totalDeposits, fromdate, todate));
                    return strBuild.ToString();
                }
                return Common.NoInfo(cols);
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
            TemplateHandler.Assign(ref strContentTable, "paging-top", Common.GenPaging(totalPage, pageIndex, pageSize, 0, "aPageTop", "txtPageTop", 620, false, true));
            TemplateHandler.Assign(ref strContentTable, "paging-bottom", Common.GenPaging(totalPage, pageIndex, pageSize, 0, "aPageBottom", "txtPageBottom", 620, false, false));
            TemplateHandler.Assign(ref strContentTable, "page", pageIndex);
            strContentTable = strContentTable.Replace("LoadData", "LoadDetail");
            strContentTable = strContentTable.Replace("ChangePage", "submitPage");
            

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
            int cols = 5;
            int totalSignups = 0, totalDeposits = 0;
            if (result.ResultSet.Tables.Count > 0)
            {
                int intNo = 0;
                StringBuilder strBuild = new StringBuilder();
                foreach (DataRow dr in result.ResultSet.Tables[0].Rows)
                {
                    intNo++;
                    string strTmp = strContent;
                    totalSignups += CastAide.AsInt32(dr["Signups"]);
                    totalDeposits += CastAide.AsInt32(dr["FDM"]);

                    TemplateHandler.Assign(ref strTmp, "Page-val", dr["sportcode"]);
                    TemplateHandler.Assign(ref strTmp, "Language-val", Common.GetLanguageOriginal(dr["languagecode"].ToString()));
                    TemplateHandler.Assign(ref strTmp, "langCode-val", dr["languagecode"]);
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                    TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());
                    TemplateHandler.Assign(ref strTmp, "FDM-val", "DeviceType-val");
                    
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
                if (intNo > 0)
                {
                    TemplateHandler.Assign(ref strContent, "rowstyle", (intNo % 2) == 0 ? "bgcgy" : "bgwhite");
                    TemplateHandler.Assign(ref strContent, "onmouseoutstyle", (intNo % 2) == 0 ? "b" : "a");
                    strBuild.Append(GenRowOldURL(strContent, totalSignups, totalDeposits, fromdate, todate));
                    return strBuild.ToString();
                }
                return Common.NoInfo(cols);
            }
            return Common.NoInfo(cols);
        }

        public static string GenRowOldURL(string strRowTpl, int totalSignupsNewLink, int totalDepositsNewLink, object fromdate, object todate)
        {
            int newSignups = 0, totalDepositUsers = 0, totalClicks = 0, totalUniqueClicks = 0;
            DataHandler.TrackingSystem.SelClickStatisticsOldURL(fromdate, todate, ref totalClicks, ref totalUniqueClicks);
            DataHandler.TrackingSystem.SelAffSignupsByDate_DEPOSIT(AppSettings.Session.CustID, fromdate, todate, ref newSignups, ref totalDepositUsers);
            

            TemplateHandler.ReplaceBlock(ref strRowTpl, "domain", "Old URL");
            TemplateHandler.Assign(ref strRowTpl, "Language-val", Resources.Lang.none);
            TemplateHandler.Assign(ref strRowTpl, "TotalClick-val", totalClicks);
            TemplateHandler.Assign(ref strRowTpl, "UniqueClick-val", totalUniqueClicks);
            TemplateHandler.Assign(ref strRowTpl, "Signups-val", (newSignups - totalSignupsNewLink) > 0 ? (newSignups - totalSignupsNewLink).ToString() : "0");
            TemplateHandler.Assign(ref strRowTpl, "FDM-val", (totalDepositUsers - totalDepositsNewLink) > 0 ? (totalDepositUsers - totalDepositsNewLink).ToString() : "0");
            return strRowTpl;
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
        public static Int64 GetIPValue()
        {
            string ip = GetIPAddressOfClient();
            if (string.IsNullOrEmpty(ip) || ip == "127.0.0.1")
            {
                return 0;
            }
            else
            {
                IList<string> parts = GetStringIList(ip, ".");
                return 16777216 * int.Parse(parts[0]) + 65536 * int.Parse(parts[1]) + 256 * int.Parse(parts[2]) + int.Parse(parts[3]);
            }
        }
        public static IList<string> GetStringIList(string stringArray, string sep)
        {
            string[] separator = { sep };
            string[] result = stringArray.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            IList<string> data = new List<string>();
            foreach (string item in result)
            {
                data.Add(item);
            }
            return data;
        }
        public static string GetKeyword(Uri urlReferrer)
        {
            if (urlReferrer != null)
            {
                System.Collections.Specialized.NameValueCollection parameters = HttpUtility.ParseQueryString(urlReferrer.Query);
                if (urlReferrer.Host.Contains("google.com") ||
                    urlReferrer.Host.Contains("bing.com") ||
                    urlReferrer.Host.Contains("ask.com"))
                {
                    return parameters["q"];
                }
                else if (urlReferrer.Host.Contains("yahoo.com"))
                {
                    return parameters["p"];
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
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

            if (userAgent.Contains("iPhone"))
            {
                device = "iPhone";
            }
            if (userAgent.Contains("iPad"))
            {
                device = "iPad";
            }
            if (userAgent.Contains("iPod"))
            {
                device = "iPod";
            }

            if (userAgent.Contains("BlackBerry"))
            {
                device = "BlackBerry";
            }

            if (userAgent.Contains("HTC"))
            {
                device = "HTC";
            }

            if (userAgent.Contains("Motorola"))
            {
                device = "Motorola";
            }

            if (userAgent.Contains("Nokia"))
            {
                device = "Nokia";
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
        #endregion
    }

}
