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

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDynamicRepository _dynamicRepository;

        public AuthRepository(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }
        public IList<UserPermission> GetPermissionList(int userId)
        {
            var result = _dynamicRepository.Fetch<UserPermission>(new UserPermissionCriteria { UserId = userId });

            return result.Where(p => !p.FunctionName.Contains("TransferPlayer")).ToList(); //Hide Transfer Player
        }

        public bool ReCheckin(int onUserId)
        {
            var command = new ReCheckinCommand { OnUserId = onUserId };
            _dynamicRepository.Execute(command);

            return command.IsKickedOut;
        }

        public ResultInfo Login(LoginParam param)
        {
            var command = new LoginCommand
            {
                UserName = param.UserName,
                Password = Encryption.CfsEnCode(param.Password),
                UserHostName = param.UserHostName,
                Ip = param.Ip,
            };

            _dynamicRepository.Execute(command);
            param.UserId = command.UserId;

            return new ResultInfo {
                Code = command.ErrCode,
                Message = command.ErrMsg,
            };
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

            _dynamicRepository.Execute(command);

            return command.OnUserId;
        }

        public void Logout(int onUserId)
        {
            _dynamicRepository.Execute(new LogoutCommand { OnUserId = onUserId });
        }

        public ResultInfo CheckPermission(CheckPermissionParam param)
        {
            var criteria = new CheckUserPermissionCriteria
            {
                UserId = param.CustId,
                FuncName = param.FuncName,
            };

            return _dynamicRepository.Get<ResultInfo>(criteria);
        }
    }
}