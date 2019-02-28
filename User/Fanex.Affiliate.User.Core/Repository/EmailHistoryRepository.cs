using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class EmailHistoryRepository : IEmailHistoryRepository
    {
        public ResultInfo Insert(ContactUsParam param)
        {
            const string uSpKey = "Aff_EmailHistoryIns";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                objectDb.DB.ExecuteNonQuery(param);
            }

            return new ResultInfo
            {
                IsOk = param.ErrCode == 0,
                Message = param.ErrMsg
            };
        }
    }
}
