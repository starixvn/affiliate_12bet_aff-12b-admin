using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface IViewLogRepository
    {
        IEnumerable<ViewLog> GetViewLogList(ViewLogParam param);
    }
}
