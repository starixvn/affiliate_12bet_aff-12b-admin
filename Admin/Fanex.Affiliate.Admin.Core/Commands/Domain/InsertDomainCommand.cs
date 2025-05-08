namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
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

        public string BlockTargetCountry { get; set; }
    }
}
