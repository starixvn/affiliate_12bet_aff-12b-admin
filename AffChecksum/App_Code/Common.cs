using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Fanex.Logging;

public class Common
{
    private static string cfsKey = "&％#@?,:*!~^.";

    #region Get config information

    public static string TimeInterval
    {
        get { return ConfigurationManager.AppSettings["timeInterval"]; }
    }

    public static string DueTime
    {
        get { return ConfigurationManager.AppSettings["dueTime"]; }
    }

    public static int CheckingTime
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["checkingTime"]); }
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

    public static string MailServer
    {
        get { return ConfigurationManager.AppSettings["mailServer"]; }
    }

    public static string CCMail
    {
        get { return ConfigurationManager.AppSettings["CCMail"]; }
    }

    public static string BCCMail
    {
        get { return ConfigurationManager.AppSettings["BCCMail"]; }
    }

    public static string PortMail
    {
        get { return ConfigurationManager.AppSettings["port"]; }
    }

    public static string PasswordMail
    {
        get { return ConfigurationManager.AppSettings["password"]; }
    }

    public static string SubjectMailError
    {
        get { return ConfigurationManager.AppSettings["subjectMailError"]; }
    }

    public static string SendMailService
    {
        get { return ConfigurationManager.AppSettings["ServiceName"]; }
    }

    public static string EnableSSL
    {
        get { return ConfigurationManager.AppSettings["enableSSL"]; }
    }

    public static bool ForTest
    {
        get { return (ConfigurationManager.AppSettings["forTest"] == "1"); }
    }

    public static int ForDate
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["forDate"]); }
    }

    #endregion Get config information

    public static void WriteErrorLog(string message, Exception exception)
    {
        var content = new StringBuilder().AppendLine(message).AppendLine(exception.Message);

        content.AppendLine("SOURCE: " + exception.Source)
               .AppendLine("TARGETSITE: " + exception.TargetSite)
               .AppendLine("STACKTRACE: " + exception.StackTrace);

        var sqlException = exception as SqlException;
        if (sqlException != null)
        {
            content.AppendLine("Procedure: " + sqlException.Procedure)
                .AppendLine("Line Number: " + sqlException.LineNumber)
                .AppendLine("Server: " + sqlException.Server);
        }

        LogService.Error(content.ToString(), exception);
    }

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