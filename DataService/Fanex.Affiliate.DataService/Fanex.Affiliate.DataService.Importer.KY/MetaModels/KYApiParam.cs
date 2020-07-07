
using System;

namespace Fanex.Affiliate.DataService.Importer.KYGaming.MetaModels
{
    public class KYApiParam
    {
        public int Agent { get; set; }

        public DateTime TimeStamp { get; set; }

        public string TimeStampString => new DateTimeOffset(TimeStamp).ToUnixTimeMilliseconds().ToString();

        public string EncryptedParam { get; set; }

        public string Currency { get; set; }

        public int OperateSubType { get; set; }

        public DateTime StartTime { get; set; }

        public string StartTimeString => new DateTimeOffset(TimeStamp).ToUnixTimeMilliseconds().ToString();

        public DateTime EndTime { get; set; }

        public string EndTimeString => new DateTimeOffset(TimeStamp).ToUnixTimeMilliseconds().ToString();

        public string Deskey { get; set; }

        public string Md5Key { get; set; }

        public string Key { get; set; }
    }
}
