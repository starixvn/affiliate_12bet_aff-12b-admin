using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Commands
{
    public abstract class BaseNonQueryCommand : NonQueryCommand
    {
        public virtual int UserId { get; set; }

        public string Ip { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
