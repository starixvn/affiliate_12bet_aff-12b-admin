using System;
using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public interface IAppSettings
    {
        int CheckingTime1 { get; }

        int CheckingTime2 { get; }

        string DailyDataUrl { get; }

        string FileExtension { get; }

        int SystemType { get; }

        bool IsLocalTest { get; }

        string PathSaveFile { get; }

        int AdminId { get; }

        string ToMail { get; }

        string CcMail { get; }

        string BccMail { get; }

        string EbetFormatDateTime { get; }

        IList<string> SupportedProductList { get; }

        string EbetTrackerFolder { get; }

        int EbetIntervalTime { get; }

        DateTime EbetStartTime { get; }

        int EbetPageSize { get; }

        string GetValue(string key);
    }
}