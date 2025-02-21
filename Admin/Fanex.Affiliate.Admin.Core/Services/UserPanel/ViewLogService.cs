using Fanex.Affiliate.Admin.Core.Criterias.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class ViewLogService : BaseDynamicService, IViewLogService
    {
        public ViewLogService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<LogType> GetLogTypeList()
        {
            const string SpKey = "Aff_Admin_LogType_Sel";

            return FetchTData<LogType>(SpKey);
        }

        public EntityResult<ViewLog> GetViewLogList(ViewLogParam param)
        {
            var criteria = new ViewLogCriteria
            {
                AffId = param.AffId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
                LogType = param.LogType,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
            };

            return FetchEntityResult<ViewLog>(criteria);
        }
    }
}
