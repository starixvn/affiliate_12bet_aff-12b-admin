using DNAide.Utils;
using Fanex.Affiliate.Admin.Core.Commands.Auth;
using Fanex.Affiliate.Admin.Core.Criterias.Auth;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.Auth
{
    public class AuthService : BaseDynamicService, IAuthService
    {
        public AuthService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IList<UserPermission> GetPermissionList(int userId)
        {
            var result = FetchTData<UserPermission>(new UserPermissionCriteria { UserId = userId });

            return result.Where(p => !p.FunctionName.Contains("TransferPlayer")).ToList(); //Hide Transfer Player
        }

        public bool ReCheckin(int onUserId)
        {
            var command = new ReCheckinCommand { OnUserId = onUserId };
            ExecuteCommand(command);

            return command.IsKickedOut;
        }

        public ResultInfo Login(LoginParam param)
        {
            var command = new LoginCommand
            {
                UserId = param.UserId,
                Ip = param.Ip,
                UserName = param.UserName,
                Password = Encryption.CfsEnCode(param.Password),
                UserHostName = param.UserHostName,
            };

            var result = ExecuteReturnResultInfo(command);
            param.UserId = command.UserId;

            return result;
        }

        public int LoginOnUser(LoginParam param, string sessionId, string serverId)
        {
            var command = new LoginOnUserCommand
            {
                UserId = param.UserId,
                Ip = param.Ip,
                SessionId = sessionId,
                ServerId = serverId,
            };

            ExecuteCommand(command);

            return command.OnUserId;
        }

        public void Logout(int onUserId)
        {
            ExecuteCommand(new LogoutCommand { OnUserId = onUserId });
        }

        public ResultInfo CheckPermission(CheckPermissionParam param)
        {
            var command = new CheckUserPermissionCommand
            {
                UserId = param.CustId,
                FuncName = param.FuncName,
            };

            DynamicRepository.Execute(command);

            return new ResultInfo(command.ErrCode, command.ErrMsg);
        }
    }
}