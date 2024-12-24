namespace Fanex.Affiliate.Admin.Core.Commands.Domain
{
    public class UpdateDomainCommand : InsertDomainCommand
    {
        public UpdateDomainCommand(string spKey) : base(spKey) { }

        public int DomainId { get; set; }
    }
}
