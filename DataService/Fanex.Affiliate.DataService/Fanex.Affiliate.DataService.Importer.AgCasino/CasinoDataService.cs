namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Xml;
    using Fanex.Affiliate.DataService.Importer.Core.DTO;
    using Fanex.Affiliate.DataService.Importer.Core.Enum;
    using Fanex.Affiliate.DataService.Importer.Core.Utilities;
    using Repository;
    using AgCasino.Repository;
    using AgCasino.DTO;

    public class CasinoDataService : ICasinoDataService
    {
        private readonly ICasinoDataRepository _casinoDataImporterRepository;
        private readonly IAppSettings _appSettings;

        public CasinoDataService() : this(null, null)
        {
        }

        public CasinoDataService(ICasinoDataRepository casinoDataImporterRepository, IAppSettings appSetting)
        {
            _casinoDataImporterRepository = casinoDataImporterRepository ?? new CasinoDataRepository();
            _appSettings = appSetting ?? new AppSettings();
        }

        public ResultInfo ImportData(DateTime date)
        {
            var fileName = DownloadFile(date);
            var xmlDocument = ConvertCSVToXml(fileName);

            return ProcessImporting(date, xmlDocument.OuterXml);
        }

        public ResultInfo ImportData(DateTime date, string filePath)
        {
            var xmlDocument = ConvertCSVToXml(filePath);

            return ProcessImporting(date, xmlDocument.OuterXml);
        }

        private ResultInfo ProcessImporting(DateTime date, string xmlData)
        {
            var metaModel = new CasinoDataMetaModel
            {
                DateImport = date,
                XmlData = xmlData,
                AdminId = Convert.ToInt32(_appSettings.GetValue("AdminId"))
            };

            var systemType = (SystemType)Convert.ToInt32(_appSettings.GetValue("SystemType"));

            return SyncDataToDB(systemType, metaModel);
        }

        private string DownloadFile(DateTime date)
        {
            ServicePointManager
    .ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;
            var path = $"{_appSettings.GetValue("PathSaveFile")}{date.ToString("yyyy/MM/")}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = $"{path}Casino {date.ToString("yyyyMMdd")}{_appSettings.GetValue("FileExtension")}";
            var downloadURL = _appSettings.GetValue("DailyDataURL").Replace("{date}", date.ToString("yyyy-MM-dd"));
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = null;
                wc.DownloadFile(downloadURL, fileName);
            }

            return fileName;
        }

        private static XmlDocument ConvertCSVToXml(string fileName)
        {
            var doc = new XmlDocument();
            var root = doc.CreateElement("root");
            doc.AppendChild(root);

            using (var stream = new StreamReader(fileName))
            {
                var hsColumnName = new Hashtable();
                var lineContent = stream.ReadLine().Split(',');
                for (int i = 0; i < lineContent.Length; i++)
                {
                    if (!hsColumnName.ContainsKey(lineContent[i]))
                    {
                        hsColumnName.Add(lineContent[i], i);//Map order column to Column name
                    }
                }

                var indexDate = GetIndexOfColName(hsColumnName, ColumnName.Date);
                var indexUsername = GetIndexOfColName(hsColumnName, ColumnName.Username);
                var indexTurnover = GetIndexOfColName(hsColumnName, ColumnName.Turnover);
                var indexDeposit = GetIndexOfColName(hsColumnName, ColumnName.Deposit);
                var indexBonus = GetIndexOfColName(hsColumnName, ColumnName.Bonus);
                var indexIAWinlost = GetIndexOfColName(hsColumnName, ColumnName.IAWinlost);
                var indexWinlostAff = GetIndexOfColName(hsColumnName, ColumnName.Winlost);
                var indexProgressiveShare = GetIndexOfColName(hsColumnName, ColumnName.ProgressiveShare);
                var indexProgressiveWin = GetIndexOfColName(hsColumnName, ColumnName.ProgressiveWin);

                while (!stream.EndOfStream)
                {
                    lineContent = stream.ReadLine().Split(',');
                    if (!string.IsNullOrEmpty(lineContent[GetIndexOfColName(hsColumnName, "Username")]))
                    {
                        var transNode = doc.CreateElement("trans");
                        transNode.SetAttribute("date", lineContent[indexDate]);
                        transNode.SetAttribute("username", lineContent[indexUsername]);
                        transNode.SetAttribute("turnover", lineContent[indexTurnover]);
                        transNode.SetAttribute("deposit", lineContent[indexDeposit]);
                        transNode.SetAttribute("bonus", lineContent[indexBonus]);
                        transNode.SetAttribute("winlostaff", lineContent[indexWinlostAff]);
                        transNode.SetAttribute("winlost", lineContent[indexIAWinlost]);
                        transNode.SetAttribute("progressiveshare", lineContent[indexProgressiveShare]);
                        transNode.SetAttribute("progressivewin", lineContent[indexProgressiveWin]);
                        root.AppendChild(transNode);
                    }
                }
                return doc;
            }
        }

        private ResultInfo SyncDataToDB(SystemType systemType, CasinoDataMetaModel metaModel)
        {
            switch (systemType)
            {
                case SystemType.All:
                    var result = _casinoDataImporterRepository.ImportDataFor12Bet(metaModel);

                    if (result.IsSuccessful)
                    {
                        return _casinoDataImporterRepository.ImportDataForIncomeAccess(metaModel);
                    }

                    return result;

                case SystemType.Affiliate12Bet:
                    return _casinoDataImporterRepository.ImportDataFor12Bet(metaModel);

                case SystemType.IncomeAccess:
                    return _casinoDataImporterRepository.ImportDataForIncomeAccess(metaModel);

                default:
                    return new ResultInfo { Code = 999, Message = "System Type is not defined" };
            }
        }

        private static int GetIndexOfColName(Hashtable hs, string colName)
        {
            return Convert.ToInt32(hs[colName] ?? hs["\"" + colName + "\""]);
        }
    }
}