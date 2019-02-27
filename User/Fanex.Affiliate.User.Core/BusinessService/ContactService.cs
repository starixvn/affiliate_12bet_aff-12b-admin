using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Repository;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class ContactService : IContactService
    {
        private readonly IEmailHistoryRepository _emailHistoryRepository;
        private readonly IEmailService _emailService;

        public ContactService(IEmailHistoryRepository emailHistoryRepository, IEmailService emailService)
        {
            _emailHistoryRepository = emailHistoryRepository;
            _emailService = emailService;
        }

        public ResultInfo SendRequest(ContactUsParam param, EmailBaseParam emailParam)
        {
            var result = _emailHistoryRepository.Insert(param);

            if (result.IsOk)
            {
                _emailService.SendContactUsEmail(emailParam, param);
            }

            return result;
        }
    }
}
