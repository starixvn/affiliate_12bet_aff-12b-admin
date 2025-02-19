using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IContactService
    {
        ResultInfo SendRequest(ContactUsParam param, EmailBaseParam emailParam);
    }
}
