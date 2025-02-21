namespace Fanex.Affiliate.Admin.Core.Commands.Administrator
{
    public abstract class BaseAccountCommand : BaseNonQueryCommand
    {
        public int CustId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int GroupId { get; set; }
    }
}
