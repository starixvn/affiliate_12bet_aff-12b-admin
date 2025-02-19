using Fanex.Affiliate.User.Core.Commands.ContactUs;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class ContactService : BaseDynamicService, IContactService
    {
        private readonly IEmailService _emailService;

        public ContactService(IEmailService emailService,
            IDynamicRepository dynamicRepository) : base (dynamicRepository)
        {
            _emailService = emailService;
        }

        public ResultInfo SendRequest(ContactUsParam param, EmailBaseParam emailParam)
        {
            var result = ExecuteReturnResultInfo(param.MapTo<ContactUsParam, CreateContactUsCommand>());

            if (result.IsOk)
            {
                _emailService.SendContactUsEmail(emailParam, param);
            }

            return result;
        }
    }
}
