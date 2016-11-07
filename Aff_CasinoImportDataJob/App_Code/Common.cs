using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections;
using System.Security.Cryptography;
using System.Data.OleDb;
using System.Xml;
//using DNAide.Core;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    private static string cfsKey = "&％#@?,:*!~^.";

    #region Column name
    public class ColumnName
    {
        public static string Date
        {
            get { return ConfigurationManager.AppSettings["Date"]; }
        }

        public static string Username
        {
            get { return ConfigurationManager.AppSettings["Username"]; }
        }

        public static string Turnover
        {
            get { return ConfigurationManager.AppSettings["Turnover"]; }
        }

        public static string Winlost
        {
            get { return ConfigurationManager.AppSettings["Winlost"]; }
        }

        public static string Deposit
        {
            get { return ConfigurationManager.AppSettings["Deposit"]; }
        }

        public static string Bonus
        {
            get { return ConfigurationManager.AppSettings["Bonus"]; }
        }
        //IA column name
        public static string IADate
        {
            get { return ConfigurationManager.AppSettings["IADate"]; }
        }
        public static string IAWinlost
        {
            get { return ConfigurationManager.AppSettings["IAWinlost"]; }
        }

        public static string IABonus
        {
            get { return ConfigurationManager.AppSettings["IABonus"]; }
        }

        public static string IADeposit
        {
            get { return ConfigurationManager.AppSettings["IADeposit"]; }
        }
    }
    #endregion

    #region Get config information
    public static string TimeInterval
    {
        get { return ConfigurationManager.AppSettings["timeInterval"]; }
    }
    public static string DueTime
    {
        get { return ConfigurationManager.AppSettings["dueTime"]; }
    }
    public static int CheckingTime1
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["checkingTime1"]); }
    }
    public static int CheckingTime2
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["checkingTime2"]); }
    }
    public static string IsSendMail
    {
        get { return ConfigurationManager.AppSettings["isSendMail"]; }
    }
    public static string FromMail
    {
        get { return ConfigurationManager.AppSettings["fromMail"]; }
    }
    public static string ToMail
    {
        get { return ConfigurationManager.AppSettings["toMail"]; }
    }
    public static string CCMail
    {
        get { return ConfigurationManager.AppSettings["CCMail"]; }
    }
    public static string BCCMail
    {
        get { return ConfigurationManager.AppSettings["BCCMail"]; }
    }
    public static string SubjectMailError
    {
        get { return ConfigurationManager.AppSettings["subjectMailError"]; }
    }
    public static bool ForTest
    {
        get { return (ConfigurationManager.AppSettings["forTest"] == "1"); }
    }
    public static string DailyDataURL
    {
        get { return ConfigurationManager.AppSettings["DailyDataURL"]; }
    }
    public static string IADailyDataURL
    {
        get { return ConfigurationManager.AppSettings["IADailyDataURL"]; }
    }
    public static string PathSaveFile
    {
        get { return ConfigurationManager.AppSettings["pathSaveFile"]; }
    }
    public static string FileExtension
    {
        get { return ConfigurationManager.AppSettings["fileExtension"]; }
    }
    public static string SheetName
    {
        get { return ConfigurationManager.AppSettings["sheetName"]; }
    }
    public static int AdminID
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["adminID"]); }
    }
    public static int ForSystem
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["forSystem"]); }
    }
    public enum DateImport
    {
        Yesterday,
        Today
    }
    #endregion

    //Upload Excel file to Server
    public static void UploadFileExcel(DateImport val, ref string fileName)
    {
        DateTime dtm = DateTime.Now;
        if (val == DateImport.Yesterday)
            dtm = DateTime.Now.AddDays(-1);

        string downloadURL = DailyDataURL.Replace("{date}", dtm.ToString("yyyy-MM-dd"));
        fileName = "Casino " + dtm.ToString("yyyyMMdd") + FileExtension;
        string path = @PathSaveFile + dtm.ToString("yyyy/MM/");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        fileName = path + fileName;
        using (WebClient wc = new WebClient())
        {
            wc.Proxy = null;
            wc.DownloadFile(downloadURL, fileName);
        }
    }

    //Read CSV File
    public static XmlDocument ConvertCSVToXML(string fileName)
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root = doc.CreateElement("root");
        doc.AppendChild(root);

        StreamReader sr = new StreamReader(fileName);
        // for set encoding
        // StreamReader sr = new StreamReader(@"file.csv", Encoding.GetEncoding(1250));
        //Get name of column
        Hashtable hsColumnName = new Hashtable();
        string[] lineContent = sr.ReadLine().Split(',');
        for (int i = 0; i < lineContent.Length; i++)
        {
            if (!hsColumnName.ContainsKey(lineContent[i]))
                hsColumnName.Add(lineContent[i], i);//Map order column to Column name
        }
        
        int indexDate = GetIndexOfColName(hsColumnName, ColumnName.Date);
        int indexUsername = GetIndexOfColName(hsColumnName, ColumnName.Username);
        int indexTurnover = GetIndexOfColName(hsColumnName, ColumnName.Turnover);
        int indexWinlost = GetIndexOfColName(hsColumnName, ColumnName.Winlost);
        int indexDeposit = GetIndexOfColName(hsColumnName, ColumnName.Deposit);
        int indexBonus = GetIndexOfColName(hsColumnName, ColumnName.Bonus);

        //int indexIADate = GetIndexOfColName(hsColumnName, ColumnName.IADate);
        int indexIAWinlost = GetIndexOfColName(hsColumnName, ColumnName.IAWinlost);
        //int indexIADeposit = GetIndexOfColName(hsColumnName, ColumnName.IADeposit);
        //int indexIABonus = GetIndexOfColName(hsColumnName, ColumnName.IABonus);

        while (!sr.EndOfStream)
        {
            lineContent = sr.ReadLine().Split(',');
            if (!string.IsNullOrEmpty(lineContent[GetIndexOfColName(hsColumnName, "Username")]))
            {
                XmlElement transNode = doc.CreateElement("trans");
                transNode.SetAttribute("date", lineContent[indexDate]);
                transNode.SetAttribute("username", lineContent[indexUsername]);
                transNode.SetAttribute("turnover", lineContent[indexTurnover]);
                transNode.SetAttribute("deposit", lineContent[indexDeposit]);
                transNode.SetAttribute("bonus", lineContent[indexBonus]);
                transNode.SetAttribute("winlostaff", lineContent[GetIndexOfColName(hsColumnName, ColumnName.Winlost)]);
                transNode.SetAttribute("winlost", lineContent[indexIAWinlost]);
                root.AppendChild(transNode);
            }
        }
        sr.Close();
        return doc;
    }

    static int GetIndexOfColName(Hashtable hs, string colName)
    {
        if (hs[colName] != null)
            return Convert.ToInt32(hs[colName]);
        else
            return Convert.ToInt32(hs["\"" + colName + "\""]);
    }

    #region read excel file
    public void ReadFileExcel(string fileName, DateTime date)
    {
        fileName = fileName.Replace("\\", "/");

        string ext = fileName.Substring(fileName.LastIndexOf('.') + 1, fileName.Length - fileName.LastIndexOf('.') - 1);
        XmlDocument doc = new XmlDocument();
        XmlElement root = doc.CreateElement("root");
        doc.AppendChild(root);

        StringBuilder sbXMLData = new StringBuilder();//Build XML string to insert list transaction to DB
        try
        {
            DataSet ds = OpenExcelfile(fileName);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!string.IsNullOrEmpty(dr["username"].ToString()))
                    {
                        XmlElement transNode = doc.CreateElement("trans");
                        transNode.SetAttribute("username", dr["username"].ToString());
                        transNode.SetAttribute("turnover", dr["casino bets"].ToString());
                        transNode.SetAttribute("winlost", dr["net loss"].ToString());
                        transNode.SetAttribute("deposit", dr["deposits"].ToString());
                        transNode.SetAttribute("bonus", dr["bonuses"].ToString());
                        root.AppendChild(transNode);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return;
        }


        //Update database here
        //DbResult rs = DAL.DataAccess.CasinoDataUpdate(doc.OuterXml, true, date);

        /*
        //Send email if there is any errors
        if (CastAide.AsInt32(rs.Outputs["errcode"]) == 0)//Show list success trans, error trans here
        {
            if (rs.ResultSet.Tables.Count > 0)
            {
                string strTemp = TemplateHandler.LoadTemplate("Casino/UploadExcel.htm");
                TemplateHandler.Assign(ref strTemp, "username", Resources.Lang.Username);
                TemplateHandler.Assign(ref strTemp, "currency", Resources.Lang.Currency);
                TemplateHandler.Assign(ref strTemp, "turnover", Resources.Lang.Turnover);
                TemplateHandler.Assign(ref strTemp, "winlost", Resources.Lang.Winlost);
                TemplateHandler.Assign(ref strTemp, "deposit", Resources.Lang.Deposit);
                TemplateHandler.Assign(ref strTemp, "bonus", Resources.Lang.Bonus);

                context.Response.Write("<script type=\"text/javascript\">parent.UpdateSucc('");
                context.Response.Write(buildTableHtml(rs.ResultSet.Tables[0], strTemp, true));
                if (rs.ResultSet.Tables[1].Rows.Count > 0)
                    context.Response.Write(buildTableHtml(rs.ResultSet.Tables[1], strTemp, false));
                context.Response.Write("');</script>");
            }
        }
        else//Show error message here
        {
            context.Response.Write("<script type='text/javascript'>parent.UpdateErr(\"" + rs.Outputs["errmsg"] + "\");</script>");
        }*/

    }

    //Open Excel file
    public static DataSet OpenExcelfile(string path)
    {
        // Create a DataSet which will hold the data extracted from the worksheet.
        DataSet ds = new DataSet();
        string connString = (FileExtension == "xls" ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=Excel 8.0"
                                                : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0");

        // Create the connection object
        OleDbConnection oledbConn = new OleDbConnection(connString);
        // Open connection
        oledbConn.Open();
        // Create OleDbCommand object and select data from worksheet Sheetname in app.config
        OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + SheetName + "$]", oledbConn);

        // Create new OleDbDataAdapter
        OleDbDataAdapter oleda = new OleDbDataAdapter();

        oleda.SelectCommand = cmd;

        // Fill the DataSet from the data extracted from the worksheet.
        oleda.Fill(ds, "Casino");
        // Close connection
        oledbConn.Close();
        return ds;
    }

    #endregion

    #region EnCrypt and DeCrypt

    public static string CfsDeCrypt(string strText)
    {
        try
        {
            byte[] rgbIV = new byte[0];
            byte[] rgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
            byte[] buffer = new byte[strText.Length];
            rgbIV = Encoding.UTF8.GetBytes(cfsKey);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            buffer = Convert.FromBase64String(strText);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Encoding.UTF8.GetString(stream.ToArray());
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
    }

    public static string CfsEnCrypt(string strText)
    {
        try
        {
            byte[] rgbIV = new byte[0];
            byte[] rgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
            rgbIV = Encoding.UTF8.GetBytes(cfsKey);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(strText);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());
        }
        catch (Exception exception)
        {
            return exception.Message;
        }
    }

    #endregion

    public class DBType
    {
        public const string Affiliate = "Affiliate";
    }
}