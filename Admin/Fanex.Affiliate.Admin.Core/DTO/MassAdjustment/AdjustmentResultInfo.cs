using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.DTO.MassAdjustment
{
    public class AdjustmentResultInfo
    {
        public ResultInfo Result { get; set; } = new ResultInfo();

        public List<ErroredAdjustmentInfo> ErroredAdjustmentInfoList { get; set; }

        public bool IsValidFile => !ErroredAdjustmentInfoList.Any();
    }
}
