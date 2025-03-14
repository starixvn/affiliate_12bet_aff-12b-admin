namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    using Fanex.Data;

    public class InsertDomainCommand : BaseNonQueryCommand
    {
        private readonly string SpKey;

        public InsertDomainCommand(string spKey)
        {
            SpKey = spKey;
        }

        public override string GetSettingKey() => SpKey;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(SpKey);

        public string DomainName { get; set; }

        public bool IsSSL { get; set; }

        [SpParam(SqlParamName = "BlockMarket")]
        public string BlockMarkets { get; set; }
    }
}
