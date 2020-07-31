using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class EmailHistoryRepository : IEmailHistoryRepository
    {
        private readonly IObjectDBBuilder _builder;

        public EmailHistoryRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public ResultInfo Insert(ContactUsParam param)
        {
            const string uSpKey = "Aff_EmailHistoryIns";

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(param);
            }

            return new ResultInfo
            {
                IsOk = param.ErrCode == 0,
                Message = param.ErrMsg
            };
        }
    }
}
