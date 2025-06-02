namespace Fanex.Affiliate.User.Core.Apis
{
    using Fanex.Affiliate.User.Core.DTO.Apis;
    using Refit;
    using System.Threading.Tasks;

    public interface ILicenseeApi
    {
        [Get("/api/customer/GetLoginCustomerInfo")]
        Task<MemberSiteCustomerInfo> GetCustomerInfoAsync(string userName, int siteId);

        [Post("/api/customer/UnlockUser")]
        Task<ApiResultInfo> UnlockUser(int[] userIds);

        [Get("/")]
        Task<string> AccessApi();
    }
}
