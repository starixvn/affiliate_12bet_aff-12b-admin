namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetIpInfoCriteria : CriteriaBase
    {
        public GetIpInfoCriteria(string ip)
        {
            Ip = ip;
        }

        public string Ip { get; set; }

        public override string GetSettingKey() => "Acc_GetIPInfo_Full";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(Ip);
    }
}