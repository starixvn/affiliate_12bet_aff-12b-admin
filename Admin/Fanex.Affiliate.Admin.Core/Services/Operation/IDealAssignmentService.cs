namespace Fanex.Affiliate.Admin.Core.Services.Operation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO.Operation;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Email;
    using MetaModels.Operation;

    public interface IDealAssignmentService
    {
        Task<ResultInfo> AssignDeal(AssignDealParam param, AffInfoParam affInfo, AffInformationEmail emailParam);

        IEnumerable<NormalDeal> GetDealList(GetDealListParam param);

        bool HasPermissionAssignOnAff(int userId, int affId);
    }
}
