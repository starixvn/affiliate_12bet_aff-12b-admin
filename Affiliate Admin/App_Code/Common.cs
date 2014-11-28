using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Caching;
using LoadBalancerLibrary;
using System.Xml;
using System.Data.OleDb;
//using OfficeOpenXml;
using Sunplus.UMControllerLibrary;
//using System.Linq;
using Fanex.MailDeliver.MailClient.API;


/// <summary>
/// Summary description for Common
/// </summary>
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
    private const int TimeCacheUM = 10;

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
    
    public static string GetLangCharFor12Bet(string preferredLang)
    {
        switch (preferredLang)
        {
            case "zh-CN":
                return "cs";
            case "zh-TW":
                return "ct";
            case "en-US":
                return "en";
            case "en-GB":
                return "en";
            case "id-ID":
                return "id";
            case "th-TH":
                return "th";
            case "vi-VN":
                return "vi";
            case "es-ES":
                return "es";
            case "ko-KR":
                return "ko";
            case "ja-JP":
                return "ja";
            default:
                return string.Empty;
        }
    }

    public static string GerFullTextLang(object strLang)
    {
        switch (strLang.ToString().ToLower())
        {
            case "en-us":
                return Resources.Lang.English;
            case "en-gb":
                return Resources.Lang.EnglishEU;
            case "zh-tw":
                return Resources.Lang.ChineseTraditional;
            case "zh-cn":
                return Resources.Lang.ChineseSimplified;
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

    public static string GetLangCode(string value)
    {
        switch (value.ToLower())
        {
            case "english": return "en";
            case "english_euro": return "en";
            case "chinese traditional": return "ct";
            case "chinese simplified": return "cs";
            case "thai": return "th";
            case "spanish": return "es";
            case "vietnamese": return "vi";
            case "indonesian": return "id";
            case "korean": return "ko";
            case "japanese": return "ja";
            default:
                return "en";
        }
    }

    public static void UploadImage(string path, HtmlInputFile inputfile,string newname,ref int errcode)
    {

        // Errcode
        //  - 0 : successful
        //  - 1 : filename is not exists
        //  - 2 : filename is exists
        //  - 3 : 
       
        string filetype = "";
        string file = "";
        if (inputfile.Value != "")
        {
            file = inputfile.Value.Substring(inputfile.Value.LastIndexOf("\\") + 1);
            filetype = file.Substring(file.LastIndexOf(".") + 1);
            if (File.Exists(path + "//" + file))
            {
                errcode = 2;
            }
            else
            {
                inputfile.PostedFile.SaveAs(path + "//" + newname + "." + filetype);
                errcode = 0;
            }
        }
        else
        {
            errcode = 1;
        }
    }
    public static void UploadFile(string file,string newfolder,string idname,ref string errmsg)
    {
        try
        {
            
            string type = Path.GetExtension(file.Substring(file.LastIndexOf("\\") + 1));
            string filename = file.Substring(file.LastIndexOf("\\") + 1);
            string p = HttpContext.Current.Server.MapPath("../Banner//");
             string path ="";
            //---------
             if (Directory.Exists(p + newfolder) == false)
             {
                 Directory.CreateDirectory(p + newfolder);
                
             }
             path = p + newfolder + "\\" + idname + type;
            //---------
           
            Stream s = File.OpenRead(file);
            byte[] buffer = new byte[s.Length];
            s.Read(buffer, 0, (int)s.Length);
            int len = (int)s.Length;
            s.Dispose();
            s.Close();

            if (len <= 3000000)
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Write(buffer, 0, len);

                Bitmap bmp=null;                
                if (Path.GetExtension(filename).Equals(".gif"))
                {
                     bmp = new Bitmap(fs);
                    bmp.Save(fs, ImageFormat.Gif);
                    bmp.Dispose();
                }
                else if (Path.GetExtension(filename).Equals(".png"))
                {
                     bmp = new Bitmap(fs);
                    bmp.Save(fs, ImageFormat.Png);
                    bmp.Dispose();
                }
                else if (Path.GetExtension(filename).Equals(".jpg"))
                {
                     bmp = new Bitmap(fs);
                    bmp.Save(fs, ImageFormat.Jpeg);
                    bmp.Dispose();
                }
                
                else
                {
                    errmsg = Resources.Lang.ErrmsgFile ;
                }
                
                fs.Dispose();
                fs.Close();
            }
            else
            {
                errmsg = Resources.Lang.ErrmsgSizeFile;
            }
        }
        catch 
        {
            
            throw;
        }
    }
    public static void PostFile(string path,string file,HttpContext context)
    {
        // Get size of POST data
        int nPostSize = context.Request.TotalBytes;
        if (nPostSize == 0) return;

        byte[] bytesPostData;
        int nReadSize = 1024;
        FileStream fsData = new FileStream(file, FileMode.Create);
        BinaryWriter wData = new BinaryWriter(fsData);

        // Read POST data in 1K chunks
        int nBytesRead = 0;
        for (int i = 0; i < (nPostSize / nReadSize); i++)
        {
            bytesPostData = context.Request.BinaryRead(nReadSize);

            wData.Write(bytesPostData);
            nBytesRead = nBytesRead + nReadSize;
        }

        // Read remaining fraction of 1K
        nReadSize = nPostSize - nBytesRead;
        if (nReadSize != 0)
        {
            bytesPostData = context.Request.BinaryRead(nReadSize);
            nBytesRead = nBytesRead + nReadSize;
        }
        wData.Close();
        fsData.Close(); 
    }
    public static bool IsValidExtension(string fileName,ref string ext){
        string[] arr = fileName.Split('\\');
        string[] arr1 = arr[arr.Length - 1].Split('.');
        ext = arr1[arr1.Length - 1].ToLower();
        return ext.Equals("gif") == true || ext.Equals("jpg") == true || ext.Equals("png") == true || ext.Equals("jpeg") == true || ext.Equals("swf") == true;
    }
    public static void UploadFile(HttpContext context, string newfolder, string path, ref int errcode, string id)
    {
        try
        {
            errcode = 0;
            string file="";
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                if (string.IsNullOrEmpty(context.Request.Files[i].FileName)==false)
                {
                    file = context.Request.Files[i].FileName;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(file))
            {
                if (Directory.Exists(path + newfolder) == false)
                {
                    Directory.CreateDirectory(path + newfolder);

                }                
                string strDst;
                //strSrc = context.Request.Files[0].FileName;
                string Extension = Path.GetExtension(file);
                if (Extension.ToLower().Equals(".gif") == true || Extension.ToLower().Equals(".jpg") == true || Extension.ToLower().Equals(".png") == true || Extension.ToLower().Equals(".jpeg") == true || Extension.ToLower().Equals(".swf") == true)
                {
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                        {
                            if (context.Request.Files[i].ContentLength <= AppSettings.ImageSize)
                            {
                                int nPos1 = file.LastIndexOf('\\');
                                int nPos2 = file.LastIndexOf('/');
                                //strDst = path + file.Substring(Math.Max(nPos1, nPos2) + 1);
                                strDst = path + newfolder + "\\" + id + Extension;
                                context.Request.Files[i].SaveAs(strDst);
                                break;
                            }
                            else
                            {
                                errcode = 3;
                            }
                        }
                    }
                }
                else
                {
                    // Extension fail
                    errcode = 2;
                }
            }
            else
            {
                // file is null
                errcode = 1;
            }
        }
        catch 
        {
            
            throw;
        }
    }

    public static void UploadFileWithAutoDetectSize(HttpContext context, string newFolder, string path, string fileName, ref int errcode, out string size, ref string strDst)
    {
        size = string.Empty;
        try
        {
            errcode = 0;
            string file = "";
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                {
                    file = context.Request.Files[i].FileName;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(file))
            {
                if (Directory.Exists(path + newFolder) == false)
                {
                    Directory.CreateDirectory(path + newFolder);

                }
                //string strDst;
                //strSrc = context.Request.Files[0].FileName;
                string Extension = Path.GetExtension(file);
                if (Extension.ToLower().Equals(".gif") == true || Extension.ToLower().Equals(".jpg") == true || Extension.ToLower().Equals(".png") == true || Extension.ToLower().Equals(".jpeg") == true || Extension.ToLower().Equals(".swf") == true)
                {
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                        {
                            if (context.Request.Files[i].ContentLength <= AppSettings.ImageSize)
                            {
                                int nPos1 = file.LastIndexOf('\\');
                                int nPos2 = file.LastIndexOf('/');
                                //strDst = path + file.Substring(Math.Max(nPos1, nPos2) + 1);
                                strDst = path + newFolder + "\\" + fileName;
                                context.Request.Files[i].SaveAs(strDst);
                                if (!Extension.ToLower().Equals(".swf"))
                                {
                                    Bitmap bitmap = new Bitmap(context.Request.Files[0].InputStream);
                                    size = string.Format("{0}x{1}", bitmap.Width, bitmap.Height);
                                }
                                else
                                {
                                    SwfInfo swfInfo = new SwfInfo(strDst);
                                    size = string.Format("{0}x{1}", swfInfo.Width, swfInfo.Height);
                                }
                                break;
                            }
                            else
                            {
                                errcode = 3;
                            }
                        }
                    }
                }
                else
                {
                    // Extension fail
                    errcode = 2;
                }
            }
            else
            {
                // file is null
                errcode = 1;
            }
        }
        catch
        {

            throw;
        }
    }

    public static void DeleteFile(string path)
    {
        FileInfo file = new FileInfo(path);
        if (file.Exists)
            file.Delete();    
    }

    public static string GenerateSelectObj(string strID, ArrayList arrValue, ArrayList arrText, string strValueSelected)
    {
        StringBuilder strBuild = new StringBuilder();
        strBuild.Append("<select id=\"" + strID + "\" name=\"" + strID + "\">");
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
        decimal dcNumber = CastAide.AsDecimal(Number);
        if (dcNumber >= 0)
        {
            return "$" + NumberFormat.DecFormat(dcNumber, 2);
        }
        else
        {
            dcNumber = dcNumber * -1;
            return "-$" + NumberFormat.DecFormat(dcNumber, 2);
        }
    }
    
    public static string FormatNumber1(object Number,bool isMoneyAmount)
    {
        decimal dcNumber = CastAide.AsDecimal(Number);
        if (isMoneyAmount)
        {
            return NumberFormat.DecFormat(dcNumber, 0);
        }
        return NumberFormat.DecFormat(dcNumber, 0);
    }
    
    public static string FormatDate(object dtmDate)
    {
        DateTime date = CastAide.AsDateTime(dtmDate);
        return date.ToString("MM/dd/yyyy HH:mm:ss");
    }
    
    public static string FormatDateNoHour(object dtmDate)
    {
        if (string.IsNullOrEmpty(dtmDate.ToString()))
            return "N/A";
        DateTime date = CastAide.AsDateTime(dtmDate);
        return date.ToString("MM/dd/yyyy");
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
            case 500:
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
            case 1007:
                return Resources.Lang.Err1007;
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
            case 5001:
                return Resources.Lang.Err5001;
            case 30010:
                return Resources.Lang.Err30010;
            case 30011:
                return Resources.Lang.Err30011;
            case 30012:
                return Resources.Lang.Err30012;
            case 30014:
                return Resources.Lang.Err30014;
            case 30015:
                return Resources.Lang.Err30015;
            default:
                return GetErrorText(intErrCode.ToString());
        }
    }
    
    public static string NoInfo(int intNumCols)
    {
        string strNoInfo = TemplateHandler.LoadTemplate("NoInfo.htm");
        TemplateHandler.Assign(ref strNoInfo, "noinfo", Resources.Lang.NoInfo);
        TemplateHandler.Assign(ref strNoInfo, "num-cols", intNumCols.ToString());
        return strNoInfo;
    }
    
    public static string NoInfo(int width, string strMsg)
    {
        string strNoInfo = TemplateHandler.LoadTemplate("NoInfo1.htm");
        TemplateHandler.Assign(ref strNoInfo, "width", width);
        TemplateHandler.Assign(ref strNoInfo, "msg", strMsg);
        return strNoInfo;
    }
    
    /// <summary>
    /// Create date time selector
    /// </summary>
    /// <param name="txtDateId"></param>
    /// <param name="currentPos"></param>
    /// <param name="triggerId"></param>
    /// <param name="currentDate"></param>
    /// <param name="noJs"></param>
    /// <param name="showTime"></param>
    /// <returns></returns>
    public static string GenCalendarControl(string txtDateId, string currentPos, string triggerId, string currentDate, bool noJs, bool showTime)
    {
        try
        {
            if (string.IsNullOrEmpty(txtDateId) || string.IsNullOrEmpty(triggerId)) return null;
            string js = (noJs ? "" : TemplateHandler.LoadTemplate("CalendarJs.html"));
            string txtClass = showTime ? "txtdate-show-time" : "txtdate-no-time";
            string singleClick = showTime ? "false" : "true";
            TemplateHandler.Assign(ref js, "{CurrentPos}", currentPos);
            return TemplateHandler.LoadHtml("Calendar.html",
                new string[] { "{txtDate}", "{themepath}", "{triggerId}", "{currentDate}", "{JS}", "{ShowTime}", "{TxtDateCls}", "{SecFormat}", "{IsSingleClick}", "{submit}" },
                new string[] { txtDateId, currentPos + AppSettings.ThemeDir, triggerId, currentDate, js, showTime.ToString().ToLower(), txtClass, (showTime ? " %H:%M:%S" : ""), singleClick, Resources.Lang.Submit });
        }
        catch { throw; }
    }
    
    public static string GenCalendarControl(string txtDateId, string currentPos, string triggerId, string currentDate, bool noJs)
    {
        try
        {
            return GenCalendarControl(txtDateId, currentPos, triggerId, currentDate, noJs, false);
        }
        catch { throw; }
    }
    
    /// <summary>
    /// Create date/time range selector
    /// </summary>
    /// <param name="currentPos"></param>
    /// <param name="noDayReset"></param>
    /// <param name="fdateId"></param>
    /// <param name="tdateId"></param>
    /// <param name="fdate"></param>
    /// <param name="tdate"></param>
    /// <param name="noJs"></param>
    /// <param name="showTime"></param>
    /// <returns></returns>
    public static string CreateDateRangeSelector(string currentPos, int noDayReset, string fdateId, string tdateId, DateTime fdate, DateTime tdate, bool noJs, bool showTime, bool blShowToday)
    {
        try
        {
            string dateRangeTpl = TemplateHandler.LoadTemplate("DateRangeSelect.html");
            TemplateHandler.Assign(ref dateRangeTpl, "{CurrentPos}", currentPos);
            TemplateHandler.Assign(ref dateRangeTpl, "{DayNoReset}", noDayReset.ToString());
            TemplateHandler.Assign(ref dateRangeTpl, "{Fdate}", fdateId);
            TemplateHandler.Assign(ref dateRangeTpl, "{Tdate}", tdateId);
            TemplateHandler.Assign(ref dateRangeTpl, "{submit}", Resources.Lang.Submit);
            TemplateHandler.Assign(ref dateRangeTpl, "{today_now}", Resources.Lang.Today);
            TemplateHandler.Assign(ref dateRangeTpl, "{yesterday}", Resources.Lang.Yesterday);
            TemplateHandler.Assign(ref dateRangeTpl, "fdatetext", Resources.Lang.From);
            TemplateHandler.Assign(ref dateRangeTpl, "tdatetext", Resources.Lang.To);
            string strShowToday = blShowToday ? "" : "hidden";
            TemplateHandler.Assign(ref dateRangeTpl, "{hide}", strShowToday);
            string fdateStr = showTime ? DateFormat.ShortDateFormat(fdate) : DateFormat.ShortDateFormat(fdate);
            string tdateStr = showTime ? DateFormat.ShortDateFormat(tdate) : DateFormat.ShortDateFormat(tdate);

            TemplateHandler.Assign(ref dateRangeTpl, "{FromDateCal}", GenCalendarControl(fdateId, currentPos, fdateId + "_tg", fdateStr, noJs, showTime));
            TemplateHandler.Assign(ref dateRangeTpl, "{ToDateCal}", GenCalendarControl(tdateId, currentPos, tdateId + "_tg", tdateStr, true, showTime));
            return dateRangeTpl;
        }
        catch { throw; }
    }

    public static string CreateDateRangeSelector(string currentPos, int noDayReset, string fdateId, string tdateId, DateTime fdate, DateTime tdate, bool blShowToday)
    {
        try
        {
            return CreateDateRangeSelector(currentPos, noDayReset, fdateId, tdateId, fdate, tdate, false, false, blShowToday);
        }
        catch { throw; }
    }

    public static string CreateDateRangeSelector(string currentPos, int noDayReset, string fdateId, string tdateId)
    {
        try
        {
            return CreateDateRangeSelector(currentPos, noDayReset, fdateId, tdateId, DateTime.Now.AddDays(-noDayReset), DateTime.Now, false);
        }
        catch { throw; }
    }
    
    public static string CreateDateRangeSelector(string currentPos, string fdateId, string tdateId)
    {
        try
        {
            return CreateDateRangeSelector(currentPos, 1, fdateId, tdateId);
        }
        catch { throw; }
    }
    
    public static string GerStatus(object strStatus)
    {
        switch (strStatus.ToString().ToLower())
        {           
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
            default:
                return strStatus.ToString();
        }
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
        //Application["AccessPermission"] = pages;
        HttpContext.Current.Cache.Remove("AccessPermission");
        HttpContext.Current.Cache.Add("AccessPermission", pages, null, Cache.NoAbsoluteExpiration, TimeSpan.Zero, CacheItemPriority.Normal, null);
    }

    //Get permission host per function
    public static bool isHost(int CustId, string funcName)
    {
        bool isTrue = false;
        DbResult result = DataHandler.Affiliate.SelPermission(CustId, funcName);
        if (CastAide.AsInt32(result.Outputs["errcode"]) == 0)
            isTrue = true;
        return isTrue;
    }
    
    //Get permission host per function
    public static bool isRead(string funcName)
    {
        string roleAccess = HttpContext.Current.Session["RoleAccess"] == null ? string.Empty : HttpContext.Current.Session["RoleAccess"].ToString();
        if (roleAccess.IndexOf(funcName + "-1") >= 0)
            return true;
        else
            return false;
    }

    // get Error message according to error code
    public static string GetErrorText(string key)
    {
        key = "Err" + key;
        object text = HttpContext.GetGlobalResourceObject("Lang", key);
        if (text == null) return key;        
        return text.ToString();
    }

    public static string GenPagingFooter(string url, int pageNum, int cpage, int colsNo)
    {
        try
        {
            string pageIndexTpl = TemplateHandler.LoadTemplate("PageIndex.html");
            string footerHtml = TemplateHandler.LoadTemplate("Paging.html");
            string GotoPageHtml = TemplateHandler.LoadTemplate("GotoPage.html");
            TemplateHandler.Assign(ref footerHtml, "{CurrentPage}", cpage.ToString());
            TemplateHandler.Assign(ref footerHtml, "{Colspan}", colsNo.ToString());
            string pageList = string.Empty;

            int startpage, endpage;
            startpage = cpage - 3;
            if (startpage < 1)
                startpage = 0;

            endpage = startpage + 5;
            if (endpage > pageNum)
                endpage = pageNum;

            if ((endpage - startpage) < 5 && (cpage < pageNum) && (pageNum - 5 > 0))
            {
                startpage = pageNum - 5;
                endpage = pageNum;
            }

            if ((endpage + 1) == pageNum)
                endpage = pageNum;

            if (cpage >= 4)
            {
                string pageIndex = (string)pageIndexTpl.Clone();
                TemplateHandler.Assign(ref pageIndex, "{Url}", url);
                TemplateHandler.Assign(ref pageIndex, "{CurrentPage}", string.Empty);
                TemplateHandler.Assign(ref pageIndex, "{PageText}", "1");
                TemplateHandler.Assign(ref pageIndex, "{Page}", "1");
                if (cpage == 4 || pageNum == 6)
                    pageList += pageIndex;
                else
                    pageList += pageIndex + " ... ";
            }

            if ((endpage == cpage) && (endpage - 5) > 0)
                startpage = endpage - 5;

            for (int i = startpage; i < endpage; ++i)
            {
                string pageIndex = (string)pageIndexTpl.Clone();

                TemplateHandler.Assign(ref pageIndex, "{Url}", url);
                if (cpage == (i + 1))
                {
                    pageIndex = "&nbsp;<strong style=\"padding:0 1px;\">" + cpage.ToString() + "</strong>";
                }
                else
                {
                    TemplateHandler.Assign(ref pageIndex, "{CurrentPage}", string.Empty);
                    TemplateHandler.Assign(ref pageIndex, "{PageText}", "" + (i + 1));
                    TemplateHandler.Assign(ref pageIndex, "{Page}", "" + (i + 1));
                }
                pageList += pageIndex;
            }

            if ((endpage + 1) < pageNum)
            {
                string pageIndex = (string)pageIndexTpl.Clone();
                TemplateHandler.Assign(ref pageIndex, "{Url}", url);
                TemplateHandler.Assign(ref pageIndex, "{CurrentPage}", string.Empty);
                TemplateHandler.Assign(ref pageIndex, "{PageText}", " " + pageNum);
                TemplateHandler.Assign(ref pageIndex, "{Page}", " " + pageNum);
                pageList += " ... " + pageIndex;
            }

            if (pageList.Length > 0)
            {
                TemplateHandler.Assign(ref footerHtml, "{PageList}", pageList.Remove(pageList.Length - 1));
                TemplateHandler.Assign(ref footerHtml, "{GotoPage}", GotoPageHtml);
                TemplateHandler.Assign(ref footerHtml, "{Url}", url);
            }
            else
            {
                TemplateHandler.Assign(ref footerHtml, "{PageList}", "<strong>1</strong>");
                TemplateHandler.Assign(ref footerHtml, "{Url}", "");
            }
            return footerHtml;
        }
        catch { throw; }
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
        TemplateHandler.Assign(ref strPaging, "of", Resources.Lang.Of);
        TemplateHandler.Assign(ref strPaging, "fpage", 1);
        TemplateHandler.Assign(ref strPaging, "lpage", numPages);
        TemplateHandler.Assign(ref strPaging, "ppage", currPage - 1);
        TemplateHandler.Assign(ref strPaging, "npage", currPage + 1);
        TemplateHandler.Assign(ref strPaging, "maxpage", numPages);
        TemplateHandler.Assign(ref strPaging, "aId", aID);
        TemplateHandler.Assign(ref strPaging, "txtID", txtID);
        TemplateHandler.Assign(ref strPaging, "width", width);

        if(!hasCheckAll)
            TemplateHandler.RemoveBlock(ref strPaging, "selectall");
        if(!hasPageSize)
            TemplateHandler.RemoveBlock(ref strPaging, "pagesize");

        TemplateHandler.Assign(ref strPaging, "pagesize", pageSize);
        TemplateHandler.Assign(ref strPaging, "page", currPage);
        TemplateHandler.Assign(ref strPaging, "all", numPages);
        TemplateHandler.RemoveBlock(ref strPaging, "headbutton");
        TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
        return strPaging;
    }

    /// <summary>
    /// Generate paging header and footer include button at top of table VinhTrinh@20090612
    /// </summary>
    /// <param name="str"></param>
    /// <param name="nChars"></param>
    /// <returns></returns>
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
        if(!hasExcel)
            TemplateHandler.RemoveBlock(ref strPaging, "excelbutton");
        return strPaging;
    }
    
    /// <summary>
    /// Split long string into many smaller lines DungTong@20090319
    /// </summary>
    /// <param name="str"></param>
    /// <param name="nChars"></param>
    /// <returns></returns>
    public static string BreakLongString(string str, int nChars)
    {

        if (str.Length <= nChars)
        {
            return str;
        }
        StringBuilder text = new StringBuilder();
        int totalLines = str.Length / nChars;
        if (str.Length % nChars != 0)
            totalLines++;
        int i = 0;
        for (i = 0; i < totalLines - 1; i++)
        {
            text.Append(str.Substring(i * nChars, nChars));
            text.Append("\n\r");
        }
        text.Append(str.Substring(i * nChars, str.Length - i * nChars));

        return text.ToString();
    }

    /// <summary>
    /// Common function to generate dropdown list in media gallery page by Peter 20090328
    /// </summary>
    /// <param name="dv">DataView of returned DataTable</param>
    /// <param name="type">type in ("Category","Language","Currency","Type","Size"))</param>
    /// <param name="activeValue">current selected value in the dropdownlist. string.Empty mean All</param>
    /// <returns>string for dropdownlist</returns>
    public static string GetDataOptionBanner(DataView dv, string id, string type, string activeValue, bool isGenAll)
    {
        dv.RowFilter = "type='" + type + "'";
        StringBuilder result = new StringBuilder();
        string opt = TemplateHandler.LoadTemplate("Option1.htm");
        TemplateHandler.Assign(ref opt, "id", id);
        string temp = TemplateHandler.CopyBlock(opt,"optionList");
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

    //@VinhTrinh
    public static string GenSubMenu(string[] arrUrl, string[] arrText, int width, int currMenu)
    {
        string strTpl = TemplateHandler.LoadTemplate("SubMenu.htm");
        TemplateHandler.Assign(ref strTpl, "width", width);
        string strMenuItem = TemplateHandler.CopyBlock(strTpl, "menu");
        StringBuilder strBuild = new StringBuilder();
        for (int i = 0; i < arrText.Length; i++)
        {
            string strTmp = strMenuItem;
            TemplateHandler.Assign(ref strTmp, "url", arrUrl[i]);
            TemplateHandler.Assign(ref strTmp, "text", arrText[i]);
            TemplateHandler.Assign(ref strTmp, "curr", (i == currMenu ? "id=\"current\"" : string.Empty));
            strBuild.AppendLine(strTmp);
        }
        TemplateHandler.ReplaceBlock(ref strTpl, "menu", strBuild.ToString());
        return strTpl;
    }

    /// <summary>
    /// Generate a sub page
    /// </summary>
    /// <param name="currPage">Menu index to active</param>
    /// <param name="paramIndexs">List of pages that have parameters</param>
    /// <param name="paramValues">List of values that have values</param>
    /// <param name="activePages">if we do not pass this param it means all of pages are active.Otherwise we will remove list of menu items not in active menu</param>
    /// <returns></returns>
    public static string GenSubPage(int currPage,string nMembers,string playerDetails, int[] paramIndexs, string[] paramValues, params int[] activeMenus)
    {
        string strTpl = TemplateHandler.LoadTemplate("SubPage.htm");        
        TemplateHandler.Assign(ref strTpl, "StatementHistory",Resources.Lang.StatementHistory);
        TemplateHandler.Assign(ref strTpl, "Edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref strTpl, "Member", Resources.Lang.Member);
        TemplateHandler.Assign(ref strTpl, "Note", Resources.Lang.Note);
        TemplateHandler.Assign(ref strTpl, "PlayersDetail", Resources.Lang.PlayersDetail);
        TemplateHandler.Assign(ref strTpl, "Adjustment", Resources.Lang.Adjustment);
        TemplateHandler.Assign(ref strTpl, "TrackingStatistics", Resources.Lang.TrackingStatistics);        
        int i;
        List<int> activeMenuList=new List<int>(activeMenus);
        /* Assign Parameters for page */
        if (paramIndexs != null && paramValues != null)
        {
            for (i = 0; i < paramIndexs.Length; i++)
            {
                TemplateHandler.Assign(ref strTpl, "param" + paramIndexs[i].ToString(), paramValues[i]);
            }   
        }
        /*Activate the current page */
        for (i = 1; i <= 7; i++)
        {
            if (activeMenus.Length==0 || activeMenuList.Contains(i))
            {
                TemplateHandler.Assign(ref strTpl, "curr" + i.ToString(), (i == currPage ? "id=\"current\"" : string.Empty));
                TemplateHandler.Assign(ref strTpl, "param" + i.ToString(), string.Empty);
            }
            else
            {
                TemplateHandler.RemoveBlock(ref strTpl, "MenuItem" + i.ToString());
            }
        }
        TemplateHandler.Assign(ref strTpl, "nMember", nMembers);
        TemplateHandler.Assign(ref strTpl, "nPlayersDetail", playerDetails);
        return strTpl;
    }

    public static string GenSubPage(int currPage, params int[] activeMenus)
    {
        return Common.GenSubPage(currPage, string.Empty, string.Empty, null, null, activeMenus);
    }

    public static string GenSubPage(int currPage, string nMembers, string playerDetails, params int[] activeMenus)
    {
        return Common.GenSubPage(currPage, nMembers, playerDetails, null, null,activeMenus);
    }

    public static string GenSubPage(int currPage,  int[] paramIndexs, string[] paramValues, params int[] activeMenus)
    {
        return Common.GenSubPage(currPage, string.Empty, string.Empty, paramIndexs, paramValues, activeMenus);
    }

    public static string GenListSportType(int intCurrent)
    {
        StringBuilder strBuild = new StringBuilder();
        strBuild.Append("<select id=\"slSportType\" name=\"slSportType\" onchange=\"LoadData(1);\">");
        if (intCurrent == 0)
        {
            strBuild.Append("<option value=\"0\" selected>" + Resources.Lang.All + "</option>");
        }
        else
            strBuild.Append("<option value=\"0\">" + Resources.Lang.All + "</option>");
        //Get List Sport type
        DbResult rs = DataHandler.Affiliate.SportTypeSel();
        if (rs.ResultSet.Tables.Count > 0)
        {
            DataTable dtb = rs.ResultSet.Tables[0];
            foreach (DataRow dr in dtb.Rows)
            {
                int intId = CastAide.AsInt32(dr["SportId"], 0);
                if (intId == intCurrent)
                    strBuild.Append("<option value=\"" + intId.ToString() + "\" selected>" + dr["SportName"].ToString() + "</option>");
                else
                    strBuild.Append("<option value=\"" + intId.ToString() + "\">" + dr["SportName"].ToString() + "</option>");
            }
        }
        strBuild.Append("</select>");
        return strBuild.ToString();
    }

    public static string CutLongText(string strContent, int numChar)
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

    public static void SendEmail(string mailTo, string subject, string message, string mailBcc = "", string mailCC = "")
    {
        var mailItem = new Fanex.MailDeliver.Model.MailRecord
        {
            MailTo = mailTo,
            Subject = subject,
            Title = subject,
            ////IsBodyHtml ready
            Body = message,
            MailBcc = mailBcc,
            MailCc = mailCC,
        };

        using (var client = new MailDeliver())
        {
            client.SendMail(mailItem);
        }
    }

    public static void SendEmailApproved(int affID, string strPreferLang, string strFname, string strUsername, string strEmail, string strPassword, string strEmailSuffix)
    {
        string[] strTags = { "fname", "username", "pass", "custid", "mailcontact", "url", "rooturl", "lang" };
        string[] strValue = { strFname, strUsername, strPassword, affID.ToString(), AppSettings.MailContact, GetRootUrl(), GetRootUrl(), strPreferLang };

        string FileName = "";
        string FileName2 = "";
        string FileName3 = "";
        if (string.IsNullOrEmpty(strPreferLang) || strPreferLang == "en-US")
        {
            FileName = "Email/AffApprove/Approved" + strEmailSuffix + ".htm";
            FileName2 = "Email/AffApprove/ApprovalEmail" + strEmailSuffix + ".htm";
            FileName3 = "Email/AffApprove/WithdrawalEmail" + strEmailSuffix + ".htm";             
        }
        else
        {
            FileName = "Email/AffApprove/Approved_" + strPreferLang + strEmailSuffix + ".htm";
            FileName2 = "Email/AffApprove/ApprovalEmail_" + strPreferLang + strEmailSuffix + ".htm";
            FileName3 = "Email/AffApprove/WithdrawalEmail_" + strPreferLang + strEmailSuffix + ".htm";
        }
        

        string strApproved = TemplateHandler.LoadHtml(FileName, strTags, strValue).Replace("\r\n", "");
        string strSubject = TemplateHandler.ReplaceBlock(ref strApproved, "subject", "").Replace("\r\n", "").Trim();
        Common.SendEmail(strEmail, strSubject, strApproved, AppSettings.BCCMail, AppSettings.CCMail);
        //DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strApproved, AppSettings.MailPwd);

        strApproved = TemplateHandler.LoadHtml(FileName2, strTags, strValue).Replace("\r\n", "");
        strSubject = TemplateHandler.ReplaceBlock(ref strApproved, "subject", "").Replace("\r\n", "").Trim();
        Common.SendEmail(strEmail, strSubject, strApproved, AppSettings.BCCMail, AppSettings.CCMail);
        //DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strApproved, AppSettings.MailPwd);

        strApproved = TemplateHandler.LoadHtml(FileName3, strTags, strValue).Replace("\r\n", "");
        strSubject = TemplateHandler.ReplaceBlock(ref strApproved, "subject", "").Replace("\r\n", "").Trim();
        Common.SendEmail(strEmail, strSubject, strApproved, AppSettings.BCCMail, AppSettings.CCMail);
        //DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strApproved, AppSettings.MailPwd);
    }

    public static void SendEmailDisapproved(string strPreferLang, string strFname, string strEmail, string strEmailSuffix)
    {
        string[] strTags = { "fname", "mailcontact", "rooturl", "lang" };
        string[] strValue = { strFname, AppSettings.MailContact, GetRootUrl(), strPreferLang };

        string FileName = "";
        if (string.IsNullOrEmpty(strPreferLang) || strPreferLang == "en-US")
        {
            FileName = "Email/AffDisapprove/Disapproved" + strEmailSuffix + ".htm";
        }
        else
        {
            FileName = "Email/AffDisapprove/Disapproved_" + strPreferLang + strEmailSuffix + ".htm";
        }

        string strDisapproved = TemplateHandler.LoadHtml(FileName, strTags, strValue).Replace("\r\n", "");
        string strSubject = TemplateHandler.ReplaceBlock(ref strDisapproved, "subject", "").Replace("\r\n", "").Trim();
        Common.SendEmail(strEmail, strSubject, strDisapproved, AppSettings.BCCMail, AppSettings.CCMail);
        //DNAide.Utils.SendMailService.SendMail2(AppSettings.MailServer, strEmail, AppSettings.FromMail, AppSettings.CCMail, AppSettings.BCCMail, strSubject, strDisapproved, AppSettings.MailPwd);
    }

    public static string GetIPCheckerURL(string ip)
    {
        StringBuilder strIPCheckerURL=new StringBuilder(AppSettings.IPCheckerURL);
        strIPCheckerURL.Replace("{ip-val}",ip);
        return strIPCheckerURL.ToString();
    }

    public static string GetWebsiteCheckerURL(string url)
    {
        StringBuilder strWebsiteCheckerURL = new StringBuilder(AppSettings.WebsiteCheckerURL);
        strWebsiteCheckerURL.Replace("{siteurl-val}", url);
        return strWebsiteCheckerURL.ToString();
    }
    /*public static string GetAffUsername(int AffId)
    {
        Hashtable hsAffInfo = (Hashtable)CacheUtil.GetHSAffInfo();
        return hsAffInfo[AffId.ToString()].ToString();
    }
    public static int GetAffId(string Username)
    {
        Hashtable hsAffInfo = (Hashtable)CacheUtil.GetHSAffInfo();
        foreach (DictionaryEntry de in hsAffInfo)
        {
            if (string.Compare(de.Value.ToString(), Username, true) == 0)
                return CastAide.AsInt32(de.Key);
        }
        return 0;
    }*/
    
    ///Johnny@20100317: Get suffix for sending mail template
    public static string EmailSuffix(bool MailOption)
    {
        return (MailOption ? "" : "_html");
    }

    public static bool GetDownTime(out string startTime, out string endTime)
    {
        startTime = endTime = null;

        if (!AppSettings.HasCheckUMTime)
            return AppSettings.HasCheckUMTime;

        #region check UM in web.config
        DateTime Now = DateTime.Now;
        if (Now >= AppSettings.UMStartTime && Now <= AppSettings.UMEndTime)
        {
            startTime = AppSettings.UMStartTime.ToString("hh:mm tt");
            endTime = AppSettings.UMEndTime.ToString("hh:mm tt"); ;
            return true;
        }
        #endregion
        ////
        // bool AgnetSiteIsUM = false;
        //   bool AffiliateSiteIsUM = false;
        //string TimeFrom = "", TimeTo = "";//To get UM start and end time.

        UMBusiness UMCtrl = new UMBusiness();
        string UMCtrlURL = WebConfigurationManager.AppSettings["LBUrl"];//Get UM Remoting Connection String
        bool isUM = false;

        if (HttpContext.Current.Cache["IsUM"] == null)
        {
            try
            {
                isUM = UMCtrl.isUnderMaintainence(Sites.Affiliate, UMCtrlURL, ref startTime, ref endTime);//Get Agnet Site UM Status.
                //Cache Information which is gotten from Service
                HttpContext.Current.Cache.Insert("IsUM", isUM, null, DateTime.Now.AddSeconds(TimeCacheUM), TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("StartTime", startTime, null, DateTime.Now.AddSeconds(TimeCacheUM), TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("EndTime", endTime, null, DateTime.Now.AddSeconds(TimeCacheUM), TimeSpan.Zero);
            }
            catch { }
        }
        else
        {
            isUM = CastAide.AsBoolean(HttpContext.Current.Cache["IsUM"]);
            startTime = CastAide.AsString(HttpContext.Current.Cache["StartTime"]);
            endTime = CastAide.AsString(HttpContext.Current.Cache["EndTime"]);
        }

        return isUM;
    }

    public static bool IsDownTime(out string startTime, out string endTime)
    {
        startTime = endTime = null;
        DateTime Now = DateTime.Now;
        if (Now >= AppSettings.UMStartTime && Now <= AppSettings.UMEndTime)
        {
            startTime = AppSettings.UMStartTime.ToString("hh:mm tt");
            endTime = AppSettings.UMEndTime.ToString("hh:mm tt"); ;
            return true;
        }
        //return true;
        if (string.IsNullOrEmpty(AppSettings.LBUrl))
        {
            return false;
        }

        bool isUM = false;
        if (HttpContext.Current.Cache["IsUM"] == null)
        {
            LoadBalancer LB = null;

            try
            {
                LB = (LoadBalancer)Activator.GetObject(typeof(LoadBalancer), AppSettings.LBUrl);
                isUM = LB.isUnderMaintainence(IBCSites.Agency);
                if (isUM)
                {
                    string[] UMTime = LB.GetUMTimeSetting();
                    startTime = UMTime[0];
                    endTime = UMTime[1];
                }
                //Cache Information which is gotten from Service
                DateTime TimeCache = DateTime.Now.AddSeconds(TimeCacheUM);
                HttpContext.Current.Cache.Insert("IsUM", isUM, null, TimeCache, TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("StartTime", startTime, null, TimeCache, TimeSpan.Zero);
                HttpContext.Current.Cache.Insert("EndTime", endTime, null, TimeCache, TimeSpan.Zero);

            }
            catch (Exception ex)
            {
                //DNAide.Web.DNAideLog.Write("LoadBalancerLibrary Error : " + ex.Message);
            }
        }
        else
        {
            isUM = CastAide.AsBoolean(HttpContext.Current.Cache["IsUM"]);
            startTime = CastAide.AsString(HttpContext.Current.Cache["StartTime"]);
            endTime = CastAide.AsString(HttpContext.Current.Cache["EndTime"]);
        }

        return isUM;
    }

	 public static string FormatStringHtml(string value)
    {
        return HttpUtility.HtmlDecode(value.Replace("\r\n", "<br/>"));
    }

    public static string FormatStringJavascript(string value)
    {
        return HttpUtility.HtmlDecode(value).Replace("<br/>", "\r\n");
    }
    //Upload Excel file to Server
    public static void UploadFileExcel(string path, HttpPostedFile fileUpload, ref string fileName, ref int errcode)
    {
        try
        {
            if (!string.IsNullOrEmpty(fileUpload.FileName))
            {
                if (!Directory.Exists(path))//Create folder
                    Directory.CreateDirectory(path);

                string Extension = Path.GetExtension(fileUpload.FileName);

                if (Extension.ToLower().Equals(".xls") || Extension.ToLower().Equals(".xlsx"))
                {
                    fileName = Path.Combine(path, fileName);
                    fileUpload.SaveAs(fileName);
                }
                else
                {
                    // Extension fail
                    errcode = 2;
                }
            }
            else
            {
                // file is null
                errcode = 1;
            }
        }
        catch
        {

            throw;
        }
    }

    //Open Excel file
    public static DataSet OpenExcelfile(string path, string extension)
    {
        // Create a DataSet which will hold the data extracted from the worksheet.
        DataSet ds = new DataSet();

        string connString = (extension == "xls" ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=Excel 8.0;Imex=1;HDR=Yes;"
                                                : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0");

        // Create the connection object
        OleDbConnection oledbConn = new OleDbConnection(connString);
        // Open connection
        oledbConn.Open();

        // Create OleDbCommand object and select data from worksheet Sheet1
        OleDbCommand cmd = new OleDbCommand("SELECT * FROM [playerstats$]", oledbConn);

        // Create new OleDbDataAdapter
        OleDbDataAdapter oleda = new OleDbDataAdapter();

        oleda.SelectCommand = cmd;

        // Fill the DataSet from the data extracted from the worksheet.
        oleda.Fill(ds, "Casino");
        // Close connection
        oledbConn.Close();
        return ds;
    }

    public static string ConvertExcelToXML(string path, ref XmlDocument doc)
    {
        XmlElement root = doc.CreateElement("root");
        doc.AppendChild(root);

        try
        {
            const int startRow = 1;

            if (path != null)
            {                

                // Get the file we are going to process
                var existingFile = new FileInfo(path);
                // Open and read the XlSX file.
                /*using (var package = new ExcelPackage(existingFile))
                {
                    // Get the work book in the file
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            // Get the first worksheet
                            ExcelWorksheet currentWorksheet = workBook.Worksheets.First();

                            // read some data
                            object colDate = currentWorksheet.Cells[startRow, 1].Value;
                            object colUsername = currentWorksheet.Cells[startRow, 2].Value;
                            object colCurrency = currentWorksheet.Cells[startRow, 3].Value;
                            object colDeposit = currentWorksheet.Cells[startRow, 4].Value;
                            object colBonus = currentWorksheet.Cells[startRow, 5].Value;
                            object colCasinoBets = currentWorksheet.Cells[startRow, 6].Value;
                            object colNetLoss = currentWorksheet.Cells[startRow, 7].Value;

                            if (((colDate != null) && (colDate.ToString() == "Date")) &&
                                ((colUsername != null) && (colUsername.ToString() == "Username")) &&
                                ((colCurrency != null) && (colCurrency.ToString() == "Currency")) &&
                                ((colDeposit != null) && (colDeposit.ToString() == "Deposits")) &&
                                ((colBonus != null) && (colBonus.ToString() == "Bonuses")) &&
                                ((colCasinoBets != null) && (colCasinoBets.ToString() == "Casino Bets")) &&
                                ((colNetLoss != null) && (colNetLoss.ToString() == "Net loss"))
                                )
                            {

                                for (int rowNumber = startRow + 1; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                                // read each row from the start of the data (start row + 1 header row) to the end of the spreadsheet.
                                {
                                    object Username = currentWorksheet.Cells[rowNumber, 2].Value;
                                    if (Username != null)
                                    {
                                        DateTime dtmDate;
                                        double deposit, bonus, turnover, winlost;
                                        if (!DateTime.TryParse(currentWorksheet.Cells[rowNumber, 1].Value.ToString().Trim(), out dtmDate)
                                            || !double.TryParse(currentWorksheet.Cells[rowNumber, 4].Value.ToString().Trim(), out deposit)
                                            || !double.TryParse(currentWorksheet.Cells[rowNumber, 5].Value.ToString().Trim(), out bonus)
                                            || !double.TryParse(currentWorksheet.Cells[rowNumber, 6].Value.ToString().Trim(), out turnover)
                                            || !double.TryParse(currentWorksheet.Cells[rowNumber, 7].Value.ToString().Trim(), out winlost)
                                            )
                                            throw new Exception("Data format is incorrect format at row " + rowNumber + " in excel file!");
                                        XmlElement transNode = doc.CreateElement("trans");

                                        transNode.SetAttribute("date", currentWorksheet.Cells[rowNumber, 1].Value.ToString().Trim());
                                        transNode.SetAttribute("username", currentWorksheet.Cells[rowNumber, 2].Value.ToString());
                                        transNode.SetAttribute("currency", currentWorksheet.Cells[rowNumber, 3].Value.ToString());
                                        transNode.SetAttribute("deposit", currentWorksheet.Cells[rowNumber, 4].Value.ToString());
                                        transNode.SetAttribute("bonus", currentWorksheet.Cells[rowNumber, 5].Value.ToString());
                                        transNode.SetAttribute("turnover", currentWorksheet.Cells[rowNumber, 6].Value.ToString());
                                        transNode.SetAttribute("winlost", currentWorksheet.Cells[rowNumber, 7].Value.ToString());
                                        root.AppendChild(transNode);
                                        //exampleDataList.Add(new ExampleData { KillingOccurance = col1Value.ToString(), NumberResults = Convert.ToInt32(col2Value) });
                                    }
                                }
                            }
                            else
                            {
                                return "Invalid Excel file data format. Excel file should have 7 columns ordered as: <b>Date, Username, Currency, Deposits, Bonuses, Casino Bets, Net Loss</b>";
                            }
                        }
                    }
                }*/
            }
            else
            {
                Console.WriteLine("Could not find excecuting path.");
            }
        }
        catch (IOException ioEx)
        {
            if (!String.IsNullOrEmpty(ioEx.Message))
            {
                if (ioEx.Message.Contains("because it is being used by another process."))
                {
                    return "Could not read Casino data. Please make sure it not open in Excel.";
                }
            }
            return "Could not read example data. " + ioEx.Message;
        }
        catch (Exception ex)
        {
            return "An error occured while reading example data. " + ex.Message;
        }
        return string.Empty;
    }

    public static string GetRootUrl()
    {
        return AppSettings.Session.AffType == AffType.European ? AppSettings.EuropeanAffiliateURL : AppSettings.AsianAffiliateURL;
    }

    public static string GetMarket(string market)
    {
        return string.IsNullOrEmpty(market) ? string.Empty : market;
    }

    public static string GetDeviceType(string deviceType)
    {
        return string.IsNullOrEmpty(deviceType) ? "0" : deviceType;
    }
}

public enum DTFormatType
{
    DT24h = 0
};
public class IBCException : Exception
{
    public IBCException() : base() { }
    public IBCException(string message) : base(message) { }
    public IBCException(string message, Exception ex) : base(message, ex) { }
    protected IBCException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}

