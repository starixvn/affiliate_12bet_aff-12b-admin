using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface ICommissionRepository
    {
        IEnumerable<CommissionSetting> GetCommissionSettingList(int affId, bool isInternal);
    }
}
