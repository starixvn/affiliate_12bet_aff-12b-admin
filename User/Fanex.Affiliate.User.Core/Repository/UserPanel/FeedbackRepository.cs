using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class FeedbackRepository : BaseRepository, IFeedbackRepository
    {
        private readonly IObjectDBBuilder _builder;

        public FeedbackRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public ResultInfo Insert(FeedbackParam param)
        {
            const string uSpKey = "";

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
            const string SpKey = "Aff_FeedbackPopup_Check";
            var parameterSet = new
            {
                affId,
                IsShowPopup = false
            };

            using (var db = _builder.Build(SpKey))
            {
                db.ExecuteNonQuery(parameterSet);

                return parameterSet.IsShowPopup;
            }
        }

        public void CloseFeedback(int affId)
        {
            const string SpKey = "";

            using (var db = _builder.Build(SpKey))
            {
                db.ExecuteNonQuery(new { affId });
            }
        }
    }
}
