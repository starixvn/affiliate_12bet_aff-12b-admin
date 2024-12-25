namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using Data.Repository;
    using DTO;

    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly IObjectDBBuilder _builder;
        private readonly IDynamicRepository _repository;

        public AccountRepository(IObjectDBBuilder builder, IDynamicRepository repository)
        {
            _builder = builder;
            _repository = repository;
        }

        public UserInfo GetUserInfoById(int affId, bool isInternal)
        {
            const string uSpKey = "Aff_User_GeneralInfo_Sel";
            var mode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, mode))
            {
                return db.QueryEntity<UserInfo>(new { affId });
            }
        }
    }
}