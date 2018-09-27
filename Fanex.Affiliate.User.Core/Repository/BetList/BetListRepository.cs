using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.BetList;

namespace Fanex.Affiliate.User.Core.Repository.BetList
{
    public class BetListRepository : IBetListRepository
    {
        public bool IsValidMember(BetListValidationParam validationParam)
        {
            const string uSpKey = "Aff_BetList_CheckBelonging";
            var mode = validationParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, mode))
            {
                objectDb.DB.ExecuteNonQuery(validationParam);
            }

            return validationParam.BelongLevel2;
        }
    }
}
