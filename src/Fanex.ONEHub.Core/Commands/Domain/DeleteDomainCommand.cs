namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class DeleteDomainCommand : BaseNonQueryCommand
    {
        private readonly string SpKey;

        public DeleteDomainCommand(string spKey)
        {
            SpKey = spKey;
        }
        public override string GetSettingKey() => SpKey;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(SpKey);

        public int DomainId { get; set; }
    }
}
