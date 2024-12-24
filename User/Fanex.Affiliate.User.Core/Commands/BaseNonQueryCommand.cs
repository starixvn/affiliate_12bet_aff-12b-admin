namespace Fanex.Affiliate.User.Core.Commands
{
    using Data;
    using Data.Repository;

    public abstract class BaseNonQueryCommand : NonQueryCommand
    {
        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
