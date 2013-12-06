using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using DNAide.Data;
using DNAide.Web;
using DNAide.Core;
using DNAide.Utils;
using System.Collections;
using System.Security.Cryptography;
/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    private static string cfsKey = "&％#@?,:*!~^.";
    #region Get config information
    public static string TimeInterval
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["timeInterval"]; }
    }
    public static string DueTime
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["dueTime"]; }
    }
    public static int CheckingTime
    {
        get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["checkingTime"]); }
    }
    public static string IsSendMail
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["isSendMail"]; }
    }
    public static string FromMail
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["fromMail"]; }
    }
    public static string ToMail
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["toMail"]; }
    }
    public static string MailServer
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["mailServer"]; }
    }
    public static string CCMail
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["CCMail"]; }
    }
    public static string BCCMail
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["BCCMail"]; }
    }
    public static string PortMail
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["port"]; }
    }
    public static string PasswordMail
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["password"]; }
    }
    public static string SubjectMailError
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["subjectMailError"]; }
    }
    public static string SendMailService
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["ServiceName"]; }
    }
    public static string EnableSSL
    {
        get { return System.Configuration.ConfigurationSettings.AppSettings["enableSSL"]; }
    }
    public static bool ForTest
    {
        get { return (System.Configuration.ConfigurationSettings.AppSettings["forTest"] == "1"); }
    }

    public static int ForDate
    {
        get { return Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["forDate"]); }
    }
    #endregion

    public static string LoadFile(string fileName)
    {
        string fileContent = null;
        //Get file from folder BIN
        string fileN = AppDomain.CurrentDomain.BaseDirectory + fileName;
        if (File.Exists(fileN))
        {
            try
            {
                StreamReader fileReader = File.OpenText(fileN);
                fileContent = fileReader.ReadToEnd();
                fileReader.Close();
            }
            catch
            {
                fileContent = null;
            }
        }
        else
        {
            throw new Exception("Cannot find file" + fileN);
        }
        return fileContent;

    }

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

    public class DBType
    {
        public const string Affiliate = "Affiliate";
        public const string IncomeAccess = "IncomeAccess";
    }
}