
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;

namespace Fanex.Affiliate.DataService.Importer.KYGaming.MetaModels
{
    public class KYGamingApiParam
    {
        public int Agent { get; set; }

        public string TimeStamp  => new DateTimeOffset(DateTimeConverter.ToGMT(DateTime.Now.ToUniversalTime(), TimeZoneId.GmtPlus8)).ToUnixTimeMilliseconds().ToString();

        public string EncryptedParam { get; set; }

        public string Currency { get; set; }

        public int OperateSubType { get; set; }

        public DateTime StartTime { get; set; }

        public string StartTimeString => new DateTimeOffset(DateTimeConverter.ToGMT(StartTime.ToUniversalTime(), TimeZoneId.GmtPlus8)).ToUnixTimeMilliseconds().ToString();

        public DateTime EndTime { get; set; }

        public string EndTimeString => new DateTimeOffset(DateTimeConverter.ToGMT(EndTime.ToUniversalTime(), TimeZoneId.GmtPlus8)).ToUnixTimeMilliseconds().ToString();

        public string Deskey { get; set; }

        public string Md5Key { get; set; }

        public string Key { get; set; }
    }
}
