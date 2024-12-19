namespace Fanex.Affiliate.User.Core.Commands
{
    using Data.Repository;

    public abstract class BaseNonQueryCommand : NonQueryCommand
    {
        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
