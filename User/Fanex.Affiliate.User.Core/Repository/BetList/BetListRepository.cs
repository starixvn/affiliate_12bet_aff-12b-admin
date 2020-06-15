using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.BetList;

namespace Fanex.Affiliate.User.Core.Repository.BetList
{
    public class BetListRepository : IBetListRepository
    {
        private readonly IObjectDBBuilder _builder;

        public BetListRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public bool IsValidMember(BetListValidationParam validationParam)
        {
            const string uSpKey = "Aff_BetList_CheckBelonging";
            var mode = validationParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, mode))
            {
                db.ExecuteNonQuery(validationParam);
            }

            return validationParam.BelongLevel2;
        }
    }
}
