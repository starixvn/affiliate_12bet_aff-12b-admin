using Fanex.Affiliate.Admin.Core.Commands.Administrator;

namespace Fanex.Affiliate.Admin.Core.Commands
{
    public class ChangeAdminPasswordCommand : ChangeAccountPasswordCommand
    {
        public string OldPwd { get; set; }
    }
}
