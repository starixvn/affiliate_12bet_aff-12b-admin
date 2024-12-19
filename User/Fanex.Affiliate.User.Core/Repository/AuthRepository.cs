namespace Fanex.Affiliate.User.Core.Repository
{
    using Commands;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.Auth;
    using Extensions.Mappers;
    using MetalModels;

    public class AuthRepository : IAuthRepository
    {
        private readonly IDynamicRepository _repository;

        public AuthRepository(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public int GetOnUserId(OnUserIdParam onUserIdParam)
        {
            var command = new CheckInCommand();
            command.Map(onUserIdParam);

            _repository.Execute(command);

            return command.On_UserId;
        }

        public AuthorizedUserInfo GetUserInfo(string userName, bool isInternal)
        {
            var criteria = new GetUserInfoCriteria(userName, isInternal);
            return _repository.Get<AuthorizedUserInfo>(criteria);
        }

        public LoginResultInfo UpdateLoginResult(LoginResultParam loginResultParam)
        {
            var command = new UpdateLoginResultCommand();
            command.Map(loginResultParam);
            _repository.Execute(command);

            return new LoginResultInfo
            {
                IsOk = command.ErrCode == 0,
                Message = command.ErrMsg
            };
        }

        public string GetSessionId(int onUserId)
        {
            return _repository.Get<string>(new GetSessionIdCriteria(onUserId));
        }

        public bool Recheckin(int onUserId, bool isInternal)
        {
            var command = new RecheckinCommand(onUserId, isInternal);
            _repository.Execute(command);

            return command.IsKickedOut;
        }
    }
}