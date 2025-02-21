namespace Fanex.Affiliate.User.Core.Apis
{
    using System.Threading.Tasks;
    using Fanex.Affiliate.User.Core.DTO.Apis;
    using Refit;

    public interface ILicenseeApi
    {
        [Get("/api/customer/GetLoginCustomerInfo")]
        Task<MemberSiteCustomerInfo> GetCustomerInfoAsync(string userName, int siteId);

        [Post("/api/customer/UnlockUser")]
        Task<ApiResultInfo> UnlockUser(int[] userIds);
    }
}
