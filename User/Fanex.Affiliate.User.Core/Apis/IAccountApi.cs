namespace Fanex.Affiliate.User.Core.Apis
{
    using Fanex.Affiliate.User.Core.DTO.Apis;
    using Fanex.Affiliate.User.Core.MetalModels.Apis;
    using Refit;
    using System.Threading.Tasks;

    public interface IAccountApi
    {
        [Post("/api/Account/SetCustActStatus")]
        Task<AccountApiResult> ActiveCustActStatus([Body] AccountParam account);
    }
}
