using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Affiliate;
using DNAide.Core;
using DNAide.Web;
using ONEbook.UM.Models;
using ONEbook.UM.Services.Impl;
using Resources;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    private const string CheatCaptchaCode = "1508";

    public const string Male = "Male";
    public const string Female = "Female";
    public const string NumberFormatPlaceholder = "N0";
    private const int TimeCacheUM = 10;//second

    // ++ Added by Frank.Ngo on IRP 671 - Jan 2012
    public const int NormalBanner = 0;
    public const int MediaBanner = 1;
    // -- Added by Frank.Ngo on IRP 671 - Jan 2012

    public Common()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetIPAddress()
    {
        var context = System.Web.HttpContext.Current;
        string result = null;
        var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                result = addresses[0];
            }
        }

        result = result ?? context.Request.ServerVariables["REMOTE_ADDR"];

        if (result == "::1")
        {
            result = "127.0.0.1";
        }

        return result;
    }

    public static string GetLangCode(string value)
    {
        switch (value.ToLower())
        {
            case "english": return "en";
            case "english (europe)": return "eneuro";
            case "chinese traditional": return "ch";
            case "chinese simplified": return "cs";
            case "thai": return "th";
            case "spanish": return "es";
            case "vietnamese": return "vn";
            case "indonesian": return "id";
            case "korean": return "ko";
            case "japanese": return "jp";
            case "republic of india": return "hien";
            default:
                return "en";
        }
    }

    public static string GetBannerFlash()
    {
        string folder = AppSettings.AffTypeList.Any(t => t == AffType.European) ? AffType.European.ToString() : GetLanguage();
        return string.Format("Content/Banner/{0}/{1}?v={2}", folder, "bigbanner.gif", AppSettings.JsVersion);
    }

    public static string GetLanguage()
    {
        switch (AppSettings.Session.Language.ToLower())
        {
            case "en-us":
                return "English";
            case "zh-cn":
                return "Simplified";
            case "zh-tw":
                return "Tradition";
            case "th-th":
                return "Thai";
            case "es-es":
                return "Spanish";
            case "vi-vn":
                return "Vietnamese";
            case "id-id":
                return "Indonesian";
            case "ko-kr":
                return "Korean";
            case "ja-jp":
                return "Japan";
            default:
                return "English";
        }
    }

    public static string GetPreferLanguageName()
    {
        switch (AppSettings.Session.PreferLang.ToLower())
        {
            case "en-us":
                return "English";
            case "en-gb":
                return "English (Europe)";
            case "zh-cn":
                return "Chinese Simplified";
            case "zh-tw":
                return "Chinese Traditional";
            case "th-th":
                return "Thai";
            case "es-es":
                return "Spanish";
            case "vi-vn":
                return "Vietnamese";
            case "id-id":
                return "Indonesian";
            case "ko-kr":
                return "Korean";
            case "ja-jp":
                return "Japan";
            default:
                return "English";
        }
    }

    public static string GetLanguageText(string strLangCode)
    {
        switch (strLangCode.ToLower())
        {
            case "en-us":
                return Resources.Lang.English;
            case "zh-cn":
                return Resources.Lang.ChineseSimplifi;
            case "zh-tw":
                return Resources.Lang.ChineseTraditio;
            case "th-th":
                return Resources.Lang.Thai;
            case "es-es":
                return Resources.Lang.Spanish;
            case "vi-vn":
                return Resources.Lang.Vietnamese;
            case "id-id":
                return Resources.Lang.Indonesian;
            case "ko-kr":
                return Resources.Lang.Korean;
            case "ja-jp":
                return Resources.Lang.Japanese;
            default:
                return Resources.Lang.English;
        }
    }

    public static string GetLanguageOriginal(string strLangCode)
    {
        switch (strLangCode.ToLower())
        {
            case "en": return "English";
            case "eneuro": return "English_Euro";
            case "ch": return "繁體中文";
            case "cs": return "简体中文";
            case "th": return "ภาษาไทย";
            case "es": return "Español";
            case "vn":
            case "vi": return "Tiếng Việt";
            case "id": return "Indonesian";
            case "ko": return "한국어";
            case "ja":
            case "jp": return "日本";
            case "hien": return "Republic of India";
            default:
                return strLangCode;
        }
    }

    public static string GetIndex(string strType)
    {
        switch (strType)
        {
            case "Player": return "0";
            case "Neteller": return "1";
            case "Moneybookers": return "2";
            case "Wire": return "3";
            case "Cheque": return "4";
            default: return "0";
        }
    }
    public static string GetMethodName(string strValue)
    {
        switch (strValue)
        {
            case "0": return "Player";
            case "1": return "Neteller";
            case "2": return "Moneybookers";
            case "3": return "Wire";
            case "4": return "Cheque";
            default: return "Player";
        }
    }
    public static string GetErrMsg(int intErrCode, ref string strErrObj)
    {
        switch (intErrCode)
        {
            case 1:
                return Resources.Lang.Err1;
            case 2:
                return Resources.Lang.Err2;
            case 3:
                return Resources.Lang.Err3;
            case 4:
                return Resources.Lang.Err4;
            case 5:
                return Resources.Lang.Err5;
            case 101:
                return Resources.Lang.Err101;
            case 102:
                return Resources.Lang.Err102;
            case 103:
                return Resources.Lang.Err103;
            case 104:
                return Resources.Lang.Err104;
            case 105:
                return Resources.Lang.Err105;
            case 107:
                return Resources.Lang.Err107;
            case 111:
                return Resources.Lang.Err111;
            case 112:
                return Resources.Lang.Err112;
            case 113:
                return Resources.Lang.Err113;
            case 114:
                return Resources.Lang.Err114;
            case 115:
                return Resources.Lang.Err115;
            case 116:
                return Resources.Lang.Err116;
            case 100:
                return Resources.Lang.Err100;
            case 999:
                return Resources.Lang.Err999;
            case 1001:
                return Resources.Lang.Err1001;
            case 1002:
                strErrObj = "txtEmail";
                return Resources.Lang.Err1002;
            case 1003:
                strErrObj = "txtBet12UserName";
                return Resources.Lang.Err1003;
            case 1004:
                return Resources.Lang.Err1004;
            case 1005:
                return Resources.Lang.Err1005;
            case 1006:
                return Resources.Lang.Err1006;
            case 3001:
                return Resources.Lang.Err3001;
            case 3002:
                return Resources.Lang.Err3002;
            case 3003:
                return Resources.Lang.Err3003;
            case 3004:
                return Resources.Lang.Err3004;
            case 3006:
                return Resources.Lang.Err3006;
            case 4001:
                return Resources.Lang.Err4001;
            case 4002:
                return Resources.Lang.Err4002;
            case 4003:
                return Resources.Lang.Err4003;
            case 4004:
                return Resources.Lang.Err4004;
            case 4005:
                return Resources.Lang.Err4005;
            case 201:
                return Resources.Lang.Err201;
            case 202:
                return Resources.Lang.Err202;
            case 203:
                return Resources.Lang.Err203;
            case 204:
                return Resources.Lang.Err204;
            default:
                return Resources.Lang.ErrDefault;
        }
    }
    public static string FormatNumber(object Number)
    {
        decimal dcNumber = CastAide.AsDecimal(Number);
        CultureInfo info = new CultureInfo("en-US");
        if (dcNumber == 0)
            return "$0.00";
        if (dcNumber >= 0)
        {
            return "$" + string.Format(info, "{0:#,##0.00}", dcNumber); //FormatHandler.FormatNumber20(dcNumber, "en-US");
        }
        else
        {
            dcNumber = dcNumber * -1;
            return "-$" + string.Format(info, "{0:#,##0.00}", dcNumber);
        }
    }

    public static string GenerateSelectObj(string strID, ArrayList arrValue, ArrayList arrText, string strValueSelected, bool hasTextAll)
    {
        StringBuilder strBuild = new StringBuilder();
        strBuild.Append("<select id=\"" + strID + "\" name=\"" + strID + "\">");

        if (hasTextAll)
        {
            strBuild.Append("<option value=\"\">" + Resources.Lang.All + "</option>");
        }

        for (int i = 0; i < arrValue.Count; i++)
        {
            if (string.Compare(strValueSelected, arrValue[i].ToString()) == 0)
                strBuild.Append("<option value=\"" + arrValue[i].ToString() + "\" selected>" + arrText[i].ToString() + "</option>");
            else
                strBuild.Append("<option value=\"" + arrValue[i].ToString() + "\">" + arrText[i].ToString() + "</option>");
        }
        strBuild.Append("</select>");
        return strBuild.ToString();
    }

    public static string NoInfo(int intNumCols)
    {
        string strNoInfo = TemplateHandler.LoadTemplate("NoInfo.html");
        TemplateHandler.Assign(ref strNoInfo, "noinfo", Resources.Lang.NoInfo);
        TemplateHandler.Assign(ref strNoInfo, "num-cols", intNumCols.ToString());
        return strNoInfo;
    }

    public static string GerStatus(object strStatus)
    {
        switch (strStatus.ToString().ToLower())
        {
            case "running":
            case "pending":
                return Resources.Lang.Proccessing;
            case "completed":
                return Resources.Lang.Approved;
            case "cancelled":
                return Resources.Lang.Cancelled;
            case "approved":
                return Resources.Lang.Approved;
            case "rejected":
                return Resources.Lang.Rejected;
            case "current":
                return Resources.Lang.Current;
            default:
                return strStatus.ToString();
        }
    }
    /// <summary>
    /// Split long string into many smaller lines DungTong@20090319
    /// </summary>
    /// <param name="str"></param>
    /// <param name="nChars"></param>
    /// <returns></returns>
    public static string BreakLongString(string strContent, int numChar)
    {
        StringBuilder strBuild = new StringBuilder();
        int txtLength = strContent.Length;
        for (int i = 0; i <= (txtLength / numChar); i++)
        {
            string strTmp = strContent.Substring(i * numChar, ((i + 1) * numChar > txtLength ? txtLength - (i * numChar) : numChar));
            int start = strTmp.IndexOf(" ");
            if (start < 0)
            {
                strTmp = strTmp + " ";
            }
            else
            {
                int last = strTmp.LastIndexOf(" ");
                if (last < (numChar / 2))//the space is too far with last character
                {
                    strTmp = strTmp + " ";
                }
            }
            strBuild.Append(strTmp);
        }
        return strBuild.ToString();
    }
    public static string RootURL()
    {
        return VirtualPathUtility.ToAbsolute("~/");
    }

    public static string GetFullDomain()
    {
        return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
    }

    public static string HTMLEncode(string text)
    {
        return text.Replace("\r\n", "").Replace("'", "&rsquo;");
    }

    /// <summary>
    /// Generate paging header and footer include button at top of table VinhTrinh@20090612
    /// </summary>
    /// <param name="str"></param>
    /// <param name="nChars"></param>
    /// <returns></returns>
    public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, int width, bool hasCheckAll, bool hasPageSize)
    {
        string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
        TemplateHandler.Assign(ref strPaging, "select", Resources.Lang.Select);
        TemplateHandler.Assign(ref strPaging, "alltext", Resources.Lang.All);
        TemplateHandler.Assign(ref strPaging, "pagetext", Resources.Lang.Page);
        TemplateHandler.Assign(ref strPaging, "pagesizetext", Resources.Lang.PageSize);
        TemplateHandler.Assign(ref strPaging, "of", Resources.Lang.Of.ToLower());
        TemplateHandler.Assign(ref strPaging, "fpage", 1);
        TemplateHandler.Assign(ref strPaging, "lpage", numPages);
        TemplateHandler.Assign(ref strPaging, "ppage", currPage - 1);
        TemplateHandler.Assign(ref strPaging, "npage", currPage + 1);
        TemplateHandler.Assign(ref strPaging, "maxpage", numPages);
        TemplateHandler.Assign(ref strPaging, "aId", aID);
        TemplateHandler.Assign(ref strPaging, "txtID", txtID);
        TemplateHandler.Assign(ref strPaging, "width", width);

        if (!hasCheckAll)
            TemplateHandler.RemoveBlock(ref strPaging, "selectall");
        if (!hasPageSize)
            TemplateHandler.RemoveBlock(ref strPaging, "pagesize");

        TemplateHandler.Assign(ref strPaging, "page", currPage);
        TemplateHandler.Assign(ref strPaging, "all", numPages);
        TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
        TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
        return strPaging;
    }
    public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, string[] txtButton, string[] txtButtonEvent, int width, bool hasCheckAll, bool hasPageSize, bool hasButton, bool hasExcel)
    {
        string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
        TemplateHandler.Assign(ref strPaging, "select", Resources.Lang.Select);
        TemplateHandler.Assign(ref strPaging, "alltext", Resources.Lang.All);
        TemplateHandler.Assign(ref strPaging, "pagetext", Resources.Lang.Page);
        TemplateHandler.Assign(ref strPaging, "pagesizetext", Resources.Lang.PageSize);
        TemplateHandler.Assign(ref strPaging, "of", Resources.Lang.Of.ToLower());
        TemplateHandler.Assign(ref strPaging, "fpage", 1);
        TemplateHandler.Assign(ref strPaging, "lpage", numPages);
        TemplateHandler.Assign(ref strPaging, "ppage", currPage - 1);
        TemplateHandler.Assign(ref strPaging, "npage", currPage + 1);
        TemplateHandler.Assign(ref strPaging, "maxpage", numPages);
        TemplateHandler.Assign(ref strPaging, "aId", aID);
        TemplateHandler.Assign(ref strPaging, "txtID", txtID);
        TemplateHandler.Assign(ref strPaging, "width", width != 0 ? width.ToString() : "100%");

        if (!hasCheckAll)
            TemplateHandler.RemoveBlock(ref strPaging, "selectall");
        if (!hasPageSize)
            TemplateHandler.RemoveBlock(ref strPaging, "pagesize");
        TemplateHandler.Assign(ref strPaging, "pagesize", pageSize);
        TemplateHandler.Assign(ref strPaging, "page", currPage);
        TemplateHandler.Assign(ref strPaging, "all", numPages);
        if (!hasButton)
            TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
        else
        {
            string strButton = TemplateHandler.CopyBlock(strPaging, "headbutton");
            StringBuilder strBuild = new StringBuilder();
            for (int i = 0; i < txtButton.Length; i++)
            {
                string strTmp = strButton;
                TemplateHandler.Assign(ref strTmp, "txtheadbutton", txtButton[i]);
                TemplateHandler.Assign(ref strTmp, "buttonevent", txtButtonEvent[i]);
                strBuild.Append(strTmp);
            }
            TemplateHandler.ReplaceBlock(ref strPaging, "headbutton", strBuild.ToString());
        }
        if (!hasExcel)
            TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
        return strPaging;
    }
    public static string CutLongText(string strText, int maxLength)
    {
        if (strText.Length <= maxLength)
            return strText;
        if (AppSettings.Session.Language != Language.ENGLISH)
            return strText.Substring(0, maxLength) + "...";
        int end = strText.LastIndexOf(" ", maxLength);
        return strText.Substring(0, end) + "...";
    }
    ///Johnny@20100317: Get suffix for sending mail template
    public static string EmailSuffix(bool MailOption)
    {
        return (MailOption ? "" : "_html");
    }

    public static string ConvertDateTime(object dtmTime, string slash, bool isFullDate, bool hasTime)
    {
        CultureInfo culture = new CultureInfo("en-US");
        DateTime Tmp1 = Convert.ToDateTime(dtmTime, culture);
        if (AppSettings.Session.Language != "th-TH")
        {
            if (isFullDate)
                return Tmp1.ToString("MM" + slash + "dd" + slash + "yyyy") + (hasTime ? " " + Tmp1.ToString("hh:mm:ss") : string.Empty);
            else
                return Tmp1.ToString("MMMM" + slash + "yyyy");
        }
        if (isFullDate)
            return Tmp1.ToString("MM" + slash + "dd" + slash) + Tmp1.ToString("yyyy", culture) + (hasTime ? " " + Tmp1.ToString("hh:mm:ss") : string.Empty);
        else
            return Tmp1.ToString("MMMM" + slash) + Tmp1.ToString("yyyy", culture);
    }

    public static bool IsAffInListEuLink()
    {
        //List Affiliate in web.config and Affiliate use English Euro, Spanish language
        string[] strTmp = AppSettings.Session.UserName.ToLower().Split(':');
        string strUsername = strTmp[0].Trim();
        return (AppSettings.ListAffUseEuLink.ToLower().IndexOf(strUsername) >= 0 || AppSettings.Session.PreferLang.ToLower() == "es-es" || AppSettings.Session.PreferLang.ToLower() == "en-gb");
    }
    public static string FormatStringHtml(string value)
    {
        return HttpUtility.HtmlDecode(value.Replace("\r\n", "<br/>"));
    }
    public static string FormatStringJavascript(string value)
    {
        return HttpUtility.HtmlDecode(value).Replace("<br/>", "\r\n");
    }

    public static string GetDataOptionBanner(DataView dv, string id, string type, string activeValue, bool isGenAll)
    {
        dv.RowFilter = "type='" + type + "'";
        StringBuilder result = new StringBuilder();
        string opt = TemplateHandler.LoadTemplate("Option.htm");
        TemplateHandler.Assign(ref opt, "id", id);
        string temp = TemplateHandler.CopyBlock(opt, "optionList");
        string tempopt = temp;
        if (isGenAll)
        {
            TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
            TemplateHandler.Assign(ref tempopt, "Optname", Resources.Lang.All);
            if (string.IsNullOrEmpty(activeValue))
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            result.Append(tempopt);
        }
        DataTable dt = dv.ToTable();
        foreach (DataRow dr in dt.Rows)
        {
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", dr["value"].ToString());
            TemplateHandler.Assign(ref tempopt, "Optname", GenerateHTML.Banner.GetBannerLang(dr["value"].ToString()));

            if (string.Compare(activeValue, dr["value"].ToString(), true) == 0)
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            else
            {
                TemplateHandler.Assign(ref tempopt, "selected", string.Empty);
            }
            result.Append(tempopt);
        }
        TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
        return opt;
    }

    public static string GenListLink2Page(string id, string targetRequest, string deviceType, bool hasTextAll = false)
    {
        var arrTarget = new ArrayList();
        if (deviceType.Equals("0", StringComparison.OrdinalIgnoreCase))
        {
            arrTarget.Add("sportsbook");
            arrTarget.Add("casino");
            arrTarget.Add("signup");
            arrTarget.Add("promotion");
            if (AppSettings.Session.IsCasino)
            {
                arrTarget.Add("12betcasino");
            }

            arrTarget.Add("numbergame");
            arrTarget.Add("keno");
            arrTarget.Add("virtualsports");
            arrTarget.Add("playtech");
            arrTarget.Add("livecasino");
        }
        else
        {
            arrTarget.Add("home");
            arrTarget.Add("signup");
            arrTarget.Add("sportsbook");
            arrTarget.Add("numbergame");
            arrTarget.Add("promotion");
        }

        return GenerateSelectObj(id, arrTarget, arrTarget, targetRequest, hasTextAll);
    }

    public static string GenDropDownFromTable(DataTable dt, string valueColumn, string nameColumn, string id, ref string activeValue, bool isGenAll)
    {
        StringBuilder result = new StringBuilder();
        string opt = TemplateHandler.LoadTemplate("Option.htm");
        TemplateHandler.Assign(ref opt, "id", id);
        string temp = TemplateHandler.CopyBlock(opt, "optionList");
        string tempopt = temp;
        if (isGenAll)
        {
            TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
            TemplateHandler.Assign(ref tempopt, "Optname", Resources.Lang.All);
            if (string.IsNullOrEmpty(activeValue))
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            result.Append(tempopt);
        }

        if (dt.Rows.Count == 0)
            return Lang.none;

        foreach (DataRow dr in dt.Rows)
        {
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", dr[valueColumn].ToString());
            TemplateHandler.Assign(ref tempopt, "Optname", GenerateHTML.Banner.GetBannerLang(dr[nameColumn].ToString()));

            if (string.Compare(activeValue, dr[valueColumn].ToString(), true) == 0)
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            else
            {
                TemplateHandler.Assign(ref tempopt, "selected", string.Empty);
            }
            result.Append(tempopt);
        }
        if (string.IsNullOrEmpty(activeValue) && dt.Rows.Count > 0)
        {
            activeValue = dt.Rows[0][valueColumn].ToString();
        }
        TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
        return opt;
    }

    public static string GenDropDownFromTable2(DataTable dt, string valueColumn, string nameColumn, string id, ref string activeValue, bool isGenAll, string labelAll)
    {
        var result = new StringBuilder();
        string opt = TemplateHandler.LoadTemplate("Option.htm");
        TemplateHandler.Assign(ref opt, "id", id);
        string temp = TemplateHandler.CopyBlock(opt, "optionList");
        string tempopt = temp;
        if (isGenAll)
        {
            TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
            TemplateHandler.Assign(ref tempopt, "Optname", string.IsNullOrEmpty(labelAll) ? Resources.Lang.All : labelAll);
            if (string.IsNullOrEmpty(activeValue))
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            result.Append(tempopt);
        }
        foreach (DataRow dr in dt.Rows)
        {
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", dr[valueColumn].ToString());
            TemplateHandler.Assign(ref tempopt, "Optname", dr[nameColumn].ToString());

            if (string.Compare(activeValue, dr[valueColumn].ToString(), true) == 0)
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            else
            {
                TemplateHandler.Assign(ref tempopt, "selected", string.Empty);
            }
            result.Append(tempopt);
        }

        TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
        return opt;
    }

    public static string GenPartialDropDown(DataTable dt, string valueColumn, string nameColumn, string id, ref string activeValue, bool isGenAll, string labelAll)
    {
        var result = new StringBuilder();
        string opt = TemplateHandler.LoadTemplate("OnlyOption.htm");
        string temp = TemplateHandler.CopyBlock(opt, "optionList");
        string tempopt = temp;

        if (isGenAll)
        {
            TemplateHandler.Assign(ref tempopt, "Optvalue", string.Empty);
            TemplateHandler.Assign(ref tempopt, "Optname", string.IsNullOrEmpty(labelAll) ? Resources.Lang.All : labelAll);
            if (string.IsNullOrEmpty(activeValue))
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            result.Append(tempopt);
        }
        foreach (DataRow dr in dt.Rows)
        {
            tempopt = temp;
            TemplateHandler.Assign(ref tempopt, "Optvalue", dr[valueColumn].ToString());
            TemplateHandler.Assign(ref tempopt, "Optname", dr[nameColumn].ToString());

            if (string.Compare(activeValue, dr[valueColumn].ToString(), true) == 0)
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            else
            {
                TemplateHandler.Assign(ref tempopt, "selected", string.Empty);
            }
            result.Append(tempopt);
        }

        TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());

        return opt;
    }

    public static string GenYearDropDown(string id, string activeValue, bool isGenAll, string labelAll, bool fullDropDown = true)
    {
        var table = GetYearDataTable("name", "value");

        if (fullDropDown)
        {
            return GenDropDownFromTable2(table, "value", "name", id, ref activeValue, isGenAll, labelAll);
        }

        return GenPartialDropDown(table, "value", "name", id, ref activeValue, isGenAll, labelAll);
    }

    public static DataTable GetYearDataTable(string nameColumn, string valueColumn)
    {
        var dt = new DataTable();

        dt.Columns.Add(nameColumn, typeof(string));
        dt.Columns.Add(valueColumn, typeof(string));

        var startYear = AppSettings.StartYearOfSystem;
        var currentYear = DateTime.Now.Year;

        for (int i = startYear; i <= currentYear; i++)
        {
            dt.Rows.Add(i.ToString(), i.ToString());
        }

        return dt;
    }

    public static string GenListSportType(int intCurrent, int affID, bool autoReload = true)
    {
        StringBuilder strBuild = new StringBuilder();

        var rs = DataHandler.UserAff.SportTypeSelByAff(affID);
        if (rs.ResultSet.Tables[0].Rows.Count > 1)
        {
            strBuild.Append(Resources.Lang.SportType + "&nbsp;&nbsp;&nbsp;");
            strBuild.Append("<select id=\"slSportType\" name=\"slSportType\"" + (autoReload ? " onchange=\"Common.loadData(1);\"" : string.Empty) + ">");
            if (intCurrent == 0)
            {
                strBuild.Append("<option value=\"0\" selected>" + Resources.Lang.All + "</option>");
            }
            else
                strBuild.Append("<option value=\"0\">" + Resources.Lang.All + "</option>");
            //Get List Sport type

            DataTable dtb = rs.ResultSet.Tables[0];
            foreach (DataRow dr in dtb.Rows)
            {
                int intId = CastAide.AsInt32(dr["SportId"], 0);
                if (intId == intCurrent)
                    strBuild.Append("<option value=\"" + intId.ToString() + "\" selected>" + dr["SportName"].ToString() + "</option>");
                else
                    strBuild.Append("<option value=\"" + intId.ToString() + "\">" + dr["SportName"].ToString() + "</option>");
            }
            strBuild.Append("</select>");
            strBuild.Append("<div class=\"clear\"></div><br />");
        }

        return strBuild.ToString();
    }

    public static string GenDeviceList(string id, string selectedDevice = "0", bool hasAll = false)
    {
        var table = new DataTable("table");
        table.Columns.Add(new DataColumn("value", Type.GetType("System.String")));
        table.Columns.Add(new DataColumn("name", Type.GetType("System.String")));

        var newRow = table.NewRow();
        newRow["value"] = "0";
        newRow["name"] = Lang.PCDevice;
        table.Rows.Add(newRow);

        newRow = table.NewRow();
        newRow["value"] = "1";
        newRow["name"] = Lang.MobileDevice;
        table.Rows.Add(newRow);

        return GenDropDownFromTable(table, "value", "name", id, ref selectedDevice, hasAll);
    }

    public static string GetDeviceType(string deviceType)
    {
        return string.IsNullOrEmpty(deviceType) ? "0" : deviceType;
    }

    public static T ByteArrayToObject<T>(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        var structuredObject = (T)binForm.Deserialize(memStream);

        return structuredObject;
    }

    public static List<T> ByteArrayToListObjects<T>(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        var listStructuredObjects = (List<T>)binForm.Deserialize(memStream);

        return listStructuredObjects;
    }

    public static UnderMaintenanceTime GetUnderMaintenanceTime()
    {
        var maintenaceService = new MaintenanceService();

        return maintenaceService.GetUnderMaintenanceTime();
    }

    public static bool IsUnderMaintenance()
    {
        var maintenaceService = new MaintenanceService();

        return maintenaceService.IsUnderMaintenance();
    }

    public static bool IsValidCaptcha(string captcha, string userHostName)
    {
        var isProduction = AppSettings.Environment.Equals("PRODUCTION", StringComparison.OrdinalIgnoreCase);

        if (!isProduction)
        {
            return true;
        }

        if (captcha.Equals(AppSettings.Session.CaptchaCode, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return captcha.Equals(CheatCaptchaCode) && Regex.IsMatch(userHostName, AppSettings.CompanyIP);
    }
}

public class IBCException : Exception
{
    public IBCException() : base() { }
    public IBCException(string message) : base(message) { }
    public IBCException(string message, Exception ex) : base(message, ex) { }
    protected IBCException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}

