using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AffCasinoImportDataWinApp
{
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

        #endregion Column name

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
        
        public static int AdminID
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["adminID"]); }
        }

        public static int ForSystem
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["forSystem"]); }
        }

        #endregion Get config information

        public static string DownloadCsvFileFromApi(DateTime date)
        {
            string downloadURL = DailyDataURL.Replace("{date}", date.ToString("yyyy-MM-dd"));
            var filePath = "Casino " + date.ToString("yyyyMMdd") + FileExtension;
            string path = @PathSaveFile + date.ToString("yyyy/MM/");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            filePath = path + filePath;
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = null;
                wc.DownloadFile(downloadURL, filePath);
            }

            return filePath;
        }

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

        private static int GetIndexOfColName(Hashtable hs, string colName)
        {
            if (hs[colName] != null)
                return Convert.ToInt32(hs[colName]);
            else
                return Convert.ToInt32(hs["\"" + colName + "\""]);
        }
    }
}