using Fanex.Affiliate.Admin.Core.Commands;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.ChangeAdminPassword;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.ChangeAdminPassword
{
    public class ChangeAdminPasswordService : BaseDynamicService, IChangeAdminPasswordService
    {
        public ChangeAdminPasswordService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }
        public ResultInfo ChangePassword(ChangeAdminPasswordParam param)
        {
            var command = new ChangeAdminPasswordCommand
            {
                CustId = param.UserId,
                OldPwd = param.OldPassword,
                NewPwd = param.NewPassword,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }
    }
}
