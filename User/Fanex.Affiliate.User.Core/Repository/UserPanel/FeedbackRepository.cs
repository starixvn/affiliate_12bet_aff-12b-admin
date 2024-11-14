using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class FeedbackRepository : BaseRepository, IFeedbackRepository
    {
        private readonly IObjectDBBuilder _builder;

        public FeedbackRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<RateInfo> GetRateList()
        {
            const string uSpKey = "Aff_RateSel";

            using (var db = _builder.Build(uSpKey))
            {
                return db.Query<RateInfo>();
            }
        }

        public ResultInfo Insert(FeedbackParam param)
        {
            const string uSpKey = "Aff_User_FeedbackIns";

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

        public bool IsShowFeedback(int affId)
        {
            //const string SpKey = "Aff_User_FeedbackPopup_Check";
            //var parameterSet = new
            //{
            //    affId,
            //    IsShowPopup = false
            //};

            //using (var db = _builder.Build(SpKey))
            //{
            //    db.ExecuteNonQuery(parameterSet);

            //    return parameterSet.IsShowPopup;
            //}

            return false;
        }
    }
}
