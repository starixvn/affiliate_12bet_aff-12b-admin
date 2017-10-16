namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System;
    using System.Configuration;

    public class AppSettings : IAppSettings
    {
        public int CheckingTime => Convert.ToInt32(GetValue("CheckSumCheckingTime"));

        public bool IsLocalTest => Convert.ToBoolean(GetValue("IsLocalTest") == "1");

        public int DayToAdd => Convert.ToInt32(GetValue("DayToAdd"));

        public string ToMail => GetValue("ToMail");

        public string CcMail => GetValue("CCMail");

        public string BccMail => GetValue("BCCMail");

        public string EmailTemplateFolder => GetValue("CheckSumEmailTemplate");

        public string GetValue(string key) => ConfigurationManager.AppSettings[key];
    }
}
