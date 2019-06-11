using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.EmailHistory;
using Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Services.EmailHistory
{
    public class EmailHistoryService : IEmailHistoryService
    {
        private readonly IEmailHistoryRepository _emailHistoryRepository;

        public EmailHistoryService(IEmailHistoryRepository emailHistoryRepository)
        {
            _emailHistoryRepository = emailHistoryRepository;
        }

        public EntityResult<EmailHistoryInfo> GetEmailHistoryList(EmailHistoryParam param)
        {
            switch (param.SearchBy)
            {
                case 0:
                    param.SenderName = param.SearchText;
                    break;

                case 1:
                    param.Email = param.SearchText;
                    break;

                case 2:
                    param.AffName = param.SearchText;
                    break;

                case -2:
                    param.Mid = -1;
                    break;

                default:
                    param.Mid = null;
                    break;
            }

            return _emailHistoryRepository.GetEmailHistoryList(param);
        }
    }
}
