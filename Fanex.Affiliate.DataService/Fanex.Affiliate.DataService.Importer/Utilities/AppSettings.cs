namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;

    public class AppSettings : IAppSettings
    {
        public int CheckingTime1 => Convert.ToInt32(GetValue("ImportCheckingTime1"));

        public int CheckingTime2 => Convert.ToInt32(GetValue("ImportCheckingTime2"));

        public string DailyDataUrl => GetValue("DailyDataURL");

        public string FileExtension => GetValue("FileExtension");

        public int SystemType => Convert.ToInt32(GetValue("SystemType"));

        public bool IsLocalTest => GetValue("IsLocalTest") == "1";

        public string PathSaveFile => GetValue("PathSaveFile");

        public int AdminId => Convert.ToInt32(GetValue("AdminId"));

        public string ToMail => GetValue("ToMail");

        public string CcMail => GetValue("CCMail");

        public string BccMail => GetValue("BCCMail");

        public string EbetFormatDateTime => GetValue("EbetFormatDateTime");

        public IList<string> SupportedProductList => GetValue("SupportedProductList").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        public string EbetTrackerFolder => GetValue("EbetTrackerFolder");

        public int EbetIntervalTime => Convert.ToInt32(GetValue("EbetIntervalTime"));

        public DateTime EbetStartTime => DateTime.ParseExact(GetValue("EbetStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

        public int EbetPageSize => Convert.ToInt32(GetValue("EbetPageSize"));

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}
