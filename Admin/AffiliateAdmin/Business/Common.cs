using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Web;
using Fanex.MailDeliver.MailClient.API;
using ONEbook.UM.Models;
using ONEbook.UM.Services.Impl;

public class Common
{

    public const string System = "Affiliate";
    public const string Approved = "Approved";
    public const string Pending = "Pending";
    public const string Disapproved = "Disapproved";
    public const string Rejected = "Rejected";
    public const string RowBgOdd = "#ffffff";
    public const string RowBgEven = "#f0f0f0";
    public const string RowBgOver = "#FFF5A5";
    public const string RowBgClosed = "#d6d4d2";
    public const string RowBgPending = "#e8ded6";
    public const string NumberFormatPlaceholder = "N0";

    // ++ Added by Frank.Ngo on IRP 671 - 10 Jan 2012
    public const int NormalBanner = 0;

    public const int MediaBanner = 1;

    // -- Added by Frank.Ngo on IRP 671 - 10 Jan 2012
    public static readonly string[] CategoryMediaCasino = new string[] {
        "Casino Bonus", "Baccarat", "Blackjack" , "Slot", "Roulette", "Sic bo"
    };

    public static readonly string[] CategoryMediaSportsbook = new string[] {
        "SB Bonus", "Soccer", "Basketball" , "Tennis"
    };

    public static bool CheckHaveParentCategory(string subCategory, out string category)
    {
        foreach (string item in CategoryMediaCasino)
        {
            if (item == subCategory)
            {
                category = "Casino";
                return true;
            }
        }

        foreach (string item in CategoryMediaSportsbook)
        {
            if (item == subCategory)
            {
                category = "Sportsbook";
                return true;
            }
        }

        category = string.Empty;
        return false;
    }

    // -- Added by Frank.Ngo on IRP 671 - 10 Jan 2012

    public static string GetLangChar()
    {
        switch (AppSettings.Session.Language)
        {
            case "zh-CN":
                return "CS";

            case "en-US":
                return "E";

            default:
                return "E";
        }
    }

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
    public static string GenerateDropdownHtml(string id, IDictionary<string, string> values, string selectedValue)
    {
        var resultHtml = new StringBuilder();
        resultHtml.AppendFormat("<select id='{0}' name='{0}'>", id);
        foreach (var item in values)
        {
            var attribute = item.Key.Equals(selectedValue, StringComparison.OrdinalIgnoreCase) ? "selected" : string.Empty;
            resultHtml.AppendFormat("<option value='{0}' {1}>{2}</option>", item.Key, attribute, item.Value);
        }

        resultHtml.Append("</select>");
        return resultHtml.ToString();
    }

    public static string GetAlertMsg(int intAlertCode)
    {
        switch (intAlertCode)
        {
            case 0:
                return Resources.Lang.UpdAffSuccess;

            case 1:
                return Resources.Lang.PaymentSuccess;

            case 2:
                return Resources.Lang.AddUserSucc;

            case 3:
                return Resources.Lang.UpdateUserSucc;

            case 4:
                return Resources.Lang.DeleteUserSucc;

            case 5:
                return Resources.Lang.UpdateStaSucc;

            default:
                return Resources.Lang.UpdAffSuccess;
        }
    }

    public static string FormatNumber(object Number)
    {
        decimal decimalNumber = CastAide.AsDecimal(Number, 0m);

        return (decimalNumber >= 0 ? "$" : "-$") + Math.Abs(decimalNumber).ToString("N2");

    }

 
    public static string NoInfo(int intNumCols)
    {
        string strNoInfo = TemplateHandler.LoadTemplate("NoInfo.htm");
        TemplateHandler.Assign(ref strNoInfo, "noinfo", Resources.Lang.NoInfo);
        TemplateHandler.Assign(ref strNoInfo, "num-cols", intNumCols.ToString());
        return strNoInfo;
    }

    public static string GenNoInformation(string colspan)
    {
        string noDataTpl = TemplateHandler.LoadTemplate("NoInformation.htm");
        TemplateHandler.Assign(ref noDataTpl, "{bgcolor-nodata}", "bgcolor-nodata");
        TemplateHandler.Assign(ref noDataTpl, "{tdno}", colspan);
        TemplateHandler.Assign(ref noDataTpl, "{nodata}", Resources.Lang.NoInfo);
        return noDataTpl;
    }

    public static void InitAccessPermission()
    {
        Dictionary<string, string> pages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            DataTable dtb = DataHandler.UserControl.GetFunctionInfo().ResultSet.Tables[0];
            foreach (DataRow dr in dtb.Rows)
            {
                if (CastAide.AsInt32(dr["SubFuncCount"]) == 0)
                {
                    string strFilesName = dr["FileNames"].ToString();
                    string strFuncName = dr["FuncName"].ToString();
                    string[] strListFiles = strFilesName.Split(';');
                    for (int i = 0; i < strListFiles.Length; i++)
                    {
                        try
                        {
                            pages.Add(strListFiles[i].Trim(), strFuncName.Trim() + "-1");
                        }
                        catch { }
                    }
                }
            }
        }
        catch
        {
            throw;
        }
        HttpContext.Current.Cache.Remove("AccessPermission");
        HttpContext.Current.Cache.Add("AccessPermission", pages, null, Cache.NoAbsoluteExpiration, TimeSpan.Zero, CacheItemPriority.Normal, null);
    }

    //Get permission host per function
    public static bool HasHostPermission(int CustId, string funcName)
    {
        var result = DataHandler.Affiliate.SelPermission(CustId, funcName);
        return CastAide.AsInt32(result.Outputs["errcode"]) == 0;
    }

    public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, int width, bool hasCheckAll, bool hasPageSize)
    {
        string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
        TemplateHandler.Assign(ref strPaging, "of", Resources.Lang.Of);
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

        TemplateHandler.Assign(ref strPaging, "pagesize", pageSize);
        TemplateHandler.Assign(ref strPaging, "page", currPage);
        TemplateHandler.Assign(ref strPaging, "all", numPages);
        TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
        TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
        return strPaging;
    }

    public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, String width, bool hasCheckAll, bool hasPageSize)
    {
        string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
        TemplateHandler.Assign(ref strPaging, "of", Resources.Lang.Of);
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

        TemplateHandler.Assign(ref strPaging, "pagesize", pageSize);
        TemplateHandler.Assign(ref strPaging, "page", currPage);
        TemplateHandler.Assign(ref strPaging, "all", numPages);
        TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
        TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
        return strPaging;
    }

    public static string GenPaging(int numPages, int currPage, int pageSize, int numRows, string aID, string txtID, string[] txtButton, string[] txtButtonEvent, int width, bool hasCheckAll, bool hasPageSize, bool hasButton, bool hasExcel)
    {
        string strPaging = TemplateHandler.LoadTemplate("Paging.htm");
        TemplateHandler.Assign(ref strPaging, "of", Resources.Lang.Of);
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

    public static string GetDataOptionBanner(DataView dv, string id, string type, string activeValue, bool isGenAll)
    {
        dv.RowFilter = "type='" + type + "'";
        StringBuilder result = new StringBuilder();
        string opt = TemplateHandler.LoadTemplate("Option1.htm");
        TemplateHandler.Assign(ref opt, "id", id);
        string temp = TemplateHandler.CopyBlock(opt, "optionList");
        string tempopt = temp;
        if (isGenAll)
        {
            TemplateHandler.Assign(ref tempopt, "Optvalue", "");
            TemplateHandler.Assign(ref tempopt, "Optname", "All");
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
            TemplateHandler.Assign(ref tempopt, "Optname", dr["value"].ToString());

            if (string.Compare(activeValue, dr["value"].ToString(), true) == 0)
            {
                TemplateHandler.Assign(ref tempopt, "selected", "selected");
            }
            else
            {
                TemplateHandler.Assign(ref tempopt, "selected", "");
            }
            result.Append(tempopt);
        }
        TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
        return opt;
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
        if (string.IsNullOrEmpty(activeValue) && dt.Rows.Count > 0)
        {
            activeValue = dt.Rows[0][valueColumn].ToString();
        }
        TemplateHandler.ReplaceBlock(ref opt, "optionList", result.ToString());
        return opt;
    }

    public static string IBCDateTimeFormat(object dateCell, DTFormatType type)
    {
        switch (type)
        {
            case DTFormatType.DT24h:
                return string.Format("{0:MM/dd/yyyy HH:mm:ss}", dateCell);

            default:
                return string.Empty;
        }
    }

    public static string GetIPCheckerURL(string ip)
    {
        StringBuilder strIPCheckerURL = new StringBuilder(AppSettings.IPCheckerURL);
        strIPCheckerURL.Replace("{ip-val}", ip);
        return strIPCheckerURL.ToString();
    }

    public static string GetAffiliateUserRootUrl()
    {
        return AppSettings.Session.AffType == AffType.European ? AppSettings.EuropeanAffiliateURL : AppSettings.AsianAffiliateURL;
    }

    public static string GetMarket(string market)
    {
        return string.IsNullOrEmpty(market) ? string.Empty : market;
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
}

public enum DTFormatType
{
    DT24h = 0
};
