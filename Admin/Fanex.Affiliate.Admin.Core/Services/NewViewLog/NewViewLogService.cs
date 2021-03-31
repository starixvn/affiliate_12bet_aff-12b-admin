using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.MetaModels.NewsViewLog;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services.NewViewLog
{
    public class NewViewLogService: INewViewLogService
    {
        private readonly IAuthService _authService;
        private readonly INewViewLogRepository _newViewLogRepository;

        public NewViewLogService(
            IAuthService authService,
            INewViewLogRepository newViewLogRepository)
        {
            _authService = authService;
            _newViewLogRepository = newViewLogRepository;
        }

        public EntityResult<NewViewLogInfo> GetNewViewLogList(NewViewLogParam param)
        {
            return _newViewLogRepository.GetNewViewLogList(param);
        }

        public EntityResult<NewViewLogDetailInfo> GetNewViewLogDetailList(NewViewLogParam param)
        {
            return _newViewLogRepository.GetNewViewLogDetailList(param);
        }
    }
}
